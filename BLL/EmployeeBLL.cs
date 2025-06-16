using BCrypt.Net;
using Erp_V1.DAL;        // Assuming EMPLOYEE entity is defined here
using Erp_V1.DAL.DAO;    // DAO classes (EmployeeDAO, DepartmentDAO, etc.) are here
using Erp_V1.DAL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // For ValidationException
using System.Linq;

namespace Erp_V1.BLL
{
    /// <summary>
    /// Manages the business logic for Employees.
    /// This class handles validation, password hashing, and interaction with the DAO layer.
    /// </summary>
    public class EmployeeBLL
    {
        #region Fields & Dependencies

        private readonly EmployeeDAO _empDao = new EmployeeDAO();
        private readonly DepartmentDAO _deptDao = new DepartmentDAO();
        private readonly PositionDAO _posDao = new PositionDAO();
        private readonly RoleDAO _roleDao = new RoleDAO();

        #endregion

        #region Constants

        private const int MinPasswordLength = 12;
        private const int BCryptWorkFactor = 12;

        #endregion

        #region Public Business Methods

        /// <summary>
        /// Creates a new employee after validating details and hashing the password.
        /// </summary>
        /// <param name="dto">DTO containing the new employee's details.</param>
        /// <returns>True if the employee was successfully created and role assigned; false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        /// <exception cref="ValidationException">Thrown if employee details are invalid.</exception>
        /// <exception cref="ArgumentException">Thrown if the password is invalid.</exception>
        /// <exception cref="Exception">Wraps any underlying DAO exception.</exception>
        public bool Insert(EmployeeDetailDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Validate core employee details
            ValidateEmployeeDetails(dto, isUpdate: false);

            // Hash and validate the provided password
            string passwordHash = HashPassword(dto.Password);

            // Map DTO → Entity
            var employee = MapToEntity(dto, passwordHash);
            employee.ID = 0; // Ensure ID is zero so that EF/DAO does an INSERT

            try
            {
                // Perform the insert
                bool success = _empDao.Insert(employee);

                // If insert succeeded and a valid role was provided, assign the role
                if (success && dto.RoleID > 0 && employee.ID > 0)
                {
                    bool roleAssigned = _empDao.AssignRole(employee.ID, dto.RoleID);
                    return roleAssigned;
                }

                return success;
            }
            catch (Exception ex)
            {
                // Wrap and rethrow so the calling layer can inspect the inner exception
                throw new Exception("An error occurred while inserting the employee. See InnerException for details.", ex);
            }
        }

        /// <summary>
        /// Updates an existing employee's details. Re-hashes the password if a new one was provided.
        /// </summary>
        /// <param name="dto">DTO containing the updated employee details.</param>
        /// <returns>True if the employee was successfully updated and role assigned; false otherwise.</returns>
        /// <exception cref="ArgumentNullException">Thrown if dto is null.</exception>
        /// <exception cref="ValidationException">Thrown if employee details are invalid for an update.</exception>
        /// <exception cref="ArgumentException">Thrown if a new password is invalid.</exception>
        /// <exception cref="InvalidOperationException">Thrown if an existing password hash cannot be found when needed.</exception>
        /// <exception cref="Exception">Wraps any underlying DAO exception.</exception>
        public bool Update(EmployeeDetailDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            // Validate core data including presence of EmployeeID
            ValidateEmployeeDetails(dto, isUpdate: true);

            // Determine which password hash to use
            string passwordHash;
            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                // New password provided → validate & hash
                passwordHash = HashPassword(dto.Password);
            }
            else
            {
                // No new password → fetch existing hash from DAO
                passwordHash = GetExistingPassword(dto.EmployeeID);
                if (string.IsNullOrEmpty(passwordHash))
                {
                    throw new InvalidOperationException(
                        $"Could not retrieve existing password hash for EmployeeID {dto.EmployeeID}. Cannot update without a password."
                    );
                }
            }

            // Map DTO → Entity (with updated fields)
            var employee = MapToEntity(dto, passwordHash);

            try
            {
                // Perform the update
                bool success = _empDao.Update(employee);

                // If update succeeded and a valid RoleID was provided, update the role
                if (success && dto.RoleID > 0)
                {
                    bool roleAssigned = _empDao.AssignRole(employee.ID, dto.RoleID);
                    return roleAssigned;
                }

                return success;
            }
            catch (Exception ex)
            {
                // Wrap and rethrow
                throw new Exception("An error occurred while updating the employee. See InnerException for details.", ex);
            }
        }

        /// <summary>
        /// Verifies an employee's password attempt against their stored hash.
        /// </summary>
        /// <param name="employeeId">The ID of the employee trying to authenticate.</param>
        /// <param name="inputPassword">The password attempt.</param>
        /// <returns>True if the password is correct, false otherwise.</returns>
        public bool Authenticate(int employeeId, string inputPassword)
        {
            if (employeeId <= 0 || string.IsNullOrEmpty(inputPassword))
                return false;

            string storedHash = GetExistingPassword(employeeId);
            if (string.IsNullOrEmpty(storedHash))
                return false;

            return VerifyPasswordInternal(inputPassword, storedHash);
        }

        /// <summary>
        /// Marks an employee as deleted (logical delete).
        /// </summary>
        /// <param name="dto">DTO containing the EmployeeID to delete.</param>
        /// <returns>True if successful, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if dto is null or EmployeeID is invalid.</exception>
        public bool Delete(EmployeeDetailDTO dto)
        {
            if (dto == null || dto.EmployeeID <= 0)
                throw new ArgumentException("Valid EmployeeID must be provided.", nameof(dto));

            try
            {
                return _empDao.Delete(new EMPLOYEE { ID = dto.EmployeeID });
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while deleting EmployeeID {dto.EmployeeID}. See InnerException for details.", ex);
            }
        }

        /// <summary>
        /// Restores a logically deleted employee.
        /// </summary>
        /// <param name="dto">DTO containing the EmployeeID to restore.</param>
        /// <returns>True if successful, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if dto is null or EmployeeID is invalid.</exception>
        public bool GetBack(EmployeeDetailDTO dto)
        {
            if (dto == null || dto.EmployeeID <= 0)
                throw new ArgumentException("Valid EmployeeID must be provided.", nameof(dto));

            try
            {
                return _empDao.GetBack(dto.EmployeeID);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while restoring EmployeeID {dto.EmployeeID}. See InnerException for details.", ex);
            }
        }

        /// <summary>
        /// Retrieves data required to populate employee-related views or DTOs.
        /// Includes lists of employees, departments, positions, and roles.
        /// </summary>
        /// <returns>An EmployeeDTO containing the requested data.</returns>
        public EmployeeDTO Select()
        {
            // Note: For large datasets, consider pagination or filtering in a real-world scenario.
            return new EmployeeDTO
            {
                Departments = _deptDao.Select(),
                Positions = _posDao.Select(),
                Employees = _empDao.Select(),
                Roles = _roleDao.Select()
            };
        }

        #endregion

        #region Private Password Handling

        /// <summary>
        /// Validates a password against complexity rules.
        /// </summary>
        /// <param name="password">The password to validate.</param>
        /// <returns>A list of error messages. Returns an empty list if the password is valid.</returns>
        private List<string> GetPasswordComplexityErrors(string password)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add("Password cannot be empty.");
                return errors;
            }

            if (password.Length < MinPasswordLength)
                errors.Add($"Password must be at least {MinPasswordLength} characters long.");
            if (!password.Any(char.IsUpper))
                errors.Add("Password must contain at least one uppercase letter (A-Z).");
            if (!password.Any(char.IsLower))
                errors.Add("Password must contain at least one lowercase letter (a-z).");
            if (!password.Any(char.IsDigit))
                errors.Add("Password must contain at least one number (0-9).");
            if (password.All(char.IsLetterOrDigit))
                errors.Add("Password must contain at least one special character (e.g., !, @, #, $).");

            return errors;
        }

        /// <summary>
        /// Hashes a password using BCrypt after validating its complexity.
        /// </summary>
        /// <param name="password">The plain-text password to hash.</param>
        /// <returns>The BCrypt hash of the password.</returns>
        /// <exception cref="ArgumentException">Thrown if the password does not meet complexity rules.</exception>
        private string HashPassword(string password)
        {
            var errors = GetPasswordComplexityErrors(password);
            if (errors.Any())
            {
                throw new ArgumentException(
                    $"Password does not meet complexity requirements: {string.Join(" ", errors)}",
                    nameof(password)
                );
            }

            return BCrypt.Net.BCrypt.EnhancedHashPassword(password, BCryptWorkFactor);
        }

        /// <summary>
        /// Verifies if a plain-text password matches a stored hash using BCrypt.
        /// </summary>
        /// <param name="inputPassword">The password provided by the user.</param>
        /// <param name="storedHash">The hash stored in the database.</param>
        /// <returns>True if the password matches the hash, false otherwise.</returns>
        private bool VerifyPasswordInternal(string inputPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedHash))
                return false;

            try
            {
                return BCrypt.Net.BCrypt.EnhancedVerify(inputPassword, storedHash);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // In production, log the invalid hash format.
                return false;
            }
        }

        /// <summary>
        /// Retrieves the stored password hash for a specific employee.
        /// </summary>
        /// <param name="employeeId">The ID of the employee.</param>
        /// <returns>The stored password hash, or null/empty if not found.</returns>
        private string GetExistingPassword(int employeeId)
        {
            return _empDao.GetPasswordHash(employeeId);
        }

        #endregion

        #region Private Validation & Mapping

        /// <summary>
        /// Validates the core details of an EmployeeDetailDTO, throwing an exception on failure.
        /// </summary>
        /// <param name="dto">The DTO to validate.</param>
        /// <param name="isUpdate">True if validating for an update (requires EmployeeID).</param>
        /// <exception cref="ValidationException">Thrown if validation fails, containing combined error messages.</exception>
        private void ValidateEmployeeDetails(EmployeeDetailDTO dto, bool isUpdate = false)
        {
            var errors = new List<ValidationResult>();

            if (isUpdate && dto.EmployeeID <= 0)
                errors.Add(new ValidationResult("EmployeeID must be provided for an update.", new[] { nameof(dto.EmployeeID) }));

            if (dto.UserNo <= 0)
                errors.Add(new ValidationResult("User number must be positive.", new[] { nameof(dto.UserNo) }));

            if (string.IsNullOrWhiteSpace(dto.Name))
                errors.Add(new ValidationResult("Name cannot be empty.", new[] { nameof(dto.Name) }));

            if (string.IsNullOrWhiteSpace(dto.Surname))
                errors.Add(new ValidationResult("Surname cannot be empty.", new[] { nameof(dto.Surname) }));

            if (dto.DepartmentID <= 0)
                errors.Add(new ValidationResult("A valid Department must be selected.", new[] { nameof(dto.DepartmentID) }));

            if (dto.PositionID <= 0)
                errors.Add(new ValidationResult("A valid Position must be selected.", new[] { nameof(dto.PositionID) }));

            // Optional: You could check here if DepartmentID/PositionID actually exist
            // by calling _deptDao.Exists(dto.DepartmentID) or _posDao.Exists(dto.PositionID).
            // But that requires “Exists” methods in your DAO layer.

            if (errors.Any())
            {
                throw new ValidationException(
                    "Employee data validation failed: " + string.Join("; ", errors.Select(e => e.ErrorMessage))
                );
            }
        }

        /// <summary>
        /// Maps an EmployeeDetailDTO to an EMPLOYEE entity instance.
        /// </summary>
        /// <param name="dto">The source Data Transfer Object.</param>
        /// <param name="passwordHash">The password hash (new or existing) to assign.</param>
        /// <returns>A new EMPLOYEE entity.</returns>
        private EMPLOYEE MapToEntity(EmployeeDetailDTO dto, string passwordHash)
        {
            return new EMPLOYEE
            {
                ID = dto.EmployeeID,
                UserNo = dto.UserNo,
                Name = dto.Name,
                Surname = dto.Surname,
                Password = passwordHash,
                BirthDay = dto.BirthDay,
                Address = dto.Address,
                ImagePath = dto.ImagePath,
                Salary = dto.Salary,
                DepartmentID = dto.DepartmentID,
                PositionID = dto.PositionID
            };
        }

        #endregion
    }
}
