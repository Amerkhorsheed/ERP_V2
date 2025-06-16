using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Erp_V1.BLL;
using Erp_V1.Common;
using Erp_V1.DAL.DTO;
using Erp_V1;

namespace Erp_V1
{
    /// <summary>
    /// Provides the primary user interface for system authentication.
    /// Features an enhanced visual design and comprehensive logging of login attempts.
    /// Upon successful authentication, it navigates the user to the main application form.
    /// </summary>
    public partial class FrmLogin : Form
    {
        #region Fields

        private readonly EmployeeBLL _employeeBll;
        private readonly RolePermissionBLL _permissionBll;

        private Timer _fadeInTimer;
        private bool _isUsernameActive = false;
        private bool _isPasswordActive = false;
        private bool _isLoginButtonHovered = false;
        private bool _isCloseButtonHovered = false;

        // Constants for placeholder text
        private const string UsernamePlaceholder = "Username";
        private const string PasswordPlaceholder = "Password";

        #endregion

        #region Constructor & Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="FrmLogin"/> class.
        /// Sets up form components, enhanced UI elements, timers, placeholders, and event handlers.
        /// </summary>
        public FrmLogin()
        {
            InitializeComponent(); // Method from FrmLogin.Designer.cs

            _employeeBll = new EmployeeBLL();
            _permissionBll = new RolePermissionBLL();

            InitializeEnhancedUI();
            InitializeTimers();
            SetupPlaceholders();
            HookCustomEvents();
        }

        /// <summary>
        /// Configures advanced UI properties such as double buffering, custom form border,
        /// panel gradients, and typography using the centralized UIStyles.
        /// </summary>
        private void InitializeEnhancedUI()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                          ControlStyles.UserPaint |
                          ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();

            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = UIStyles.FormBackground;
            this.Font = UIStyles.DefaultFont;

            // Left Panel Styling
            pnlLeft.BackColor = Color.Transparent;
            pnlLeft.Paint += PnlLeft_Paint;

            // Right Panel Styling
            pnlRight.BackColor = UIStyles.RightPanelBackgroundStart;
            pnlRight.Paint += PnlRight_Paint;

            // Typography
            lblAppName.Font = UIStyles.AppNameFont;
            lblAppName.ForeColor = UIStyles.TextLight;
            lblSlogan.Font = UIStyles.SloganFont;
            lblSlogan.ForeColor = UIStyles.TextLight;

            // Use 'lblLogin' as per your Designer.cs
            lblLogin.Font = UIStyles.LoginHeaderFont;
            lblLogin.ForeColor = UIStyles.TextPrimary;

            // Input Controls Styling
            // Use 'pnlUsername' and 'pnlPassword' as the container panels from your Designer.cs
            StyleInputControl(pnlUsername, txtUsername, picUsername);
            StyleInputControl(pnlPassword, txtPassword, picPassword);

            // Button Styling
            StyleButton(btnLogin, true);
            StyleButton(btnClose, false);

            txtUsername.Font = UIStyles.InputTextFont;
            txtPassword.Font = UIStyles.InputTextFont;
        }

        /// <summary>
        /// Initializes and configures the fade-in timer for the form.
        /// </summary>
        private void InitializeTimers()
        {
            _fadeInTimer = new Timer { Interval = 20 };
            _fadeInTimer.Tick += FadeInTimer_Tick;
        }

        /// <summary>
        /// Sets the initial placeholder text and color for username and password fields.
        /// </summary>
        private void SetupPlaceholders()
        {
            txtUsername.Text = UsernamePlaceholder;
            txtUsername.ForeColor = UIStyles.TextSecondary;

            txtPassword.Text = PasswordPlaceholder;
            txtPassword.ForeColor = UIStyles.TextSecondary;
            txtPassword.UseSystemPasswordChar = false;
        }

        /// <summary>
        /// Hooks custom event handlers for interactive UI elements.
        /// </summary>
        private void HookCustomEvents()
        {
            // Field focus/blur - pass the correct panel names
            txtUsername.Enter += (s, e) => OnFieldFocus(txtUsername, pnlUsername, UsernamePlaceholder, true);
            txtUsername.Leave += (s, e) => OnFieldBlur(txtUsername, pnlUsername, UsernamePlaceholder, true);

            txtPassword.Enter += (s, e) => OnFieldFocus(txtPassword, pnlPassword, PasswordPlaceholder, false);
            txtPassword.Leave += (s, e) => OnFieldBlur(txtPassword, pnlPassword, PasswordPlaceholder, false);

            txtUsername.KeyDown += TxtFields_KeyDown;
            txtPassword.KeyDown += TxtFields_KeyDown;

            btnLogin.MouseEnter += (s, e) => OnButtonHover(btnLogin, true, true);
            btnLogin.MouseLeave += (s, e) => OnButtonHover(btnLogin, true, false);
            btnClose.MouseEnter += (s, e) => OnButtonHover(btnClose, false, true);
            btnClose.MouseLeave += (s, e) => OnButtonHover(btnClose, false, false);

            // Click events are wired in Designer.cs (e.g., this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);)
            // Ensure method names here match: btnLogin_Click, btnClose_Click

            this.Load += FrmLogin_Load;
        }

        #endregion

        #region UI Drawing & Styling Methods

        private void PnlLeft_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);

            using (var brush = UIStyles.CreateVerticalGradientBrush(rect, UIStyles.PrimaryColorDark, UIStyles.PrimaryColorLight))
            {
                e.Graphics.FillRectangle(brush, rect);
            }

            using (var patternBrush = new SolidBrush(UIStyles.DotPatternColor))
            {
                int step = 60;
                for (int x = 0; x < panel.Width; x += step)
                {
                    for (int y = 0; y < panel.Height; y += step)
                    {
                        e.Graphics.FillEllipse(patternBrush, x, y, 2, 2);
                    }
                }
            }
        }

        private void PnlRight_Paint(object sender, PaintEventArgs e)
        {
            var panel = sender as Panel;
            if (panel == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, panel.Width, panel.Height);
            using (var brush = UIStyles.CreateVerticalGradientBrush(rect, UIStyles.RightPanelBackgroundStart, UIStyles.RightPanelBackgroundEnd))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void StyleInputControl(Panel container, TextBox textBox, PictureBox icon)
        {
            // The container (pnlUsername or pnlPassword) itself has its BackColor set in the Designer.
            // We are adding a Paint event to it for borders, so we don't need to set container.BackColor here
            // if the designer already does. However, for consistency with the dynamic styling approach:
            container.BackColor = UIStyles.LightGrayBackground; // Or keep as per designer and remove this line.
                                                                // For this example, let's assume we override it.
            container.Padding = new Padding(5); // Adjust padding if needed

            if (icon != null)
            {
                icon.SizeMode = PictureBoxSizeMode.Zoom; // Changed from StretchImage for better icon aspect
                icon.BackColor = Color.Transparent;
            }

            textBox.BorderStyle = BorderStyle.None;
            textBox.BackColor = container.BackColor; // Match container's dynamically set background

            // This Paint event handler will draw the border and shadow for the container panel
            container.Paint -= InputContainer_Paint; // Remove existing to avoid duplicates if re-called
            container.Paint += InputContainer_Paint;
        }

        // Shared Paint event for input containers (pnlUsername, pnlPassword)
        private void InputContainer_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            if (p == null) return;

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Fill background (if StyleInputControl sets it, or use p.BackColor if designer sets it)
            using (var bg = new SolidBrush(p.BackColor)) // Use the panel's current BackColor
            {
                e.Graphics.FillRectangle(bg, 0, 0, p.Width, p.Height);
            }

            bool isActive = false;
            if (p == pnlUsername) isActive = _isUsernameActive;
            else if (p == pnlPassword) isActive = _isPasswordActive;

            Color borderColor = isActive ? UIStyles.AccentTeal : UIStyles.TextSecondary;

            using (var pen = new Pen(borderColor, UIStyles.InputBorderThickness))
            {
                e.Graphics.DrawRectangle(pen, 0, 0, p.Width - 1, p.Height - 1);
            }

            using (var shadowBrush = new SolidBrush(UIStyles.SubtleShadow))
            {
                e.Graphics.FillRectangle(shadowBrush, UIStyles.InputBorderThickness, UIStyles.InputBorderThickness,
                                         p.Width - (2 * UIStyles.InputBorderThickness), 2);
            }
        }


        private void StyleButton(Button btn, bool isPrimary)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.Font = UIStyles.ButtonFont;
            btn.Height = isPrimary ? 50 : 45; // Slightly different heights as per your designer
            btn.TextAlign = ContentAlignment.MiddleCenter;

            // Remove existing handler to prevent multiple subscriptions if this method is called again
            btn.Paint -= Button_Paint;
            btn.Paint += Button_Paint;


            // Set initial colors (Paint event handles hover)
            if (isPrimary)
            {
                btn.Tag = "Primary"; // Store type for Paint event
                btn.BackColor = UIStyles.AccentTeal;
                btn.ForeColor = UIStyles.TextLight;
            }
            else
            {
                btn.Tag = "Secondary"; // Store type for Paint event
                btn.BackColor = Color.Transparent;
                btn.ForeColor = UIStyles.WarningRed;
                // Designer sets border for btnClose, Paint event will enhance it
            }
        }

        // Shared Paint event for buttons
        private void Button_Paint(object sender, PaintEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            bool isPrimary = (button.Tag as string) == "Primary";

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle rect = new Rectangle(0, 0, button.Width, button.Height);

            Color baseColor, hoverColor, textColor, borderColor;
            bool isHovered;

            if (isPrimary)
            {
                baseColor = UIStyles.AccentTeal;
                hoverColor = UIStyles.AccentTealHover;
                textColor = UIStyles.TextLight;
                borderColor = Color.Transparent;
                isHovered = _isLoginButtonHovered;
            }
            else // Secondary (Close button)
            {
                baseColor = button.BackColor; // Use BackColor from designer (Transparent)
                hoverColor = UIStyles.WarningRedHoverBackground;
                textColor = UIStyles.WarningRed;
                borderColor = UIStyles.WarningRed; // From designer
                isHovered = _isCloseButtonHovered;
            }

            Color currentBgColor = isHovered ? hoverColor : baseColor;

            using (var bgBrush = new SolidBrush(currentBgColor))
            {
                e.Graphics.FillRectangle(bgBrush, rect);
            }

            if (!isPrimary) // For secondary button, draw border as per designer but allow hover to modify
            {
                using (var borderPen = new Pen(borderColor, UIStyles.ButtonBorderThickness))
                {
                    e.Graphics.DrawRectangle(borderPen,
                        UIStyles.ButtonBorderThickness / 2,
                        UIStyles.ButtonBorderThickness / 2,
                        rect.Width - UIStyles.ButtonBorderThickness,
                        rect.Height - UIStyles.ButtonBorderThickness);
                }
            }

            TextRenderer.DrawText(e.Graphics, button.Text, button.Font, rect, textColor,
                                  TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            if (isHovered)
            {
                using (var highlightBrush = new SolidBrush(UIStyles.SubtleHighlight))
                {
                    e.Graphics.FillRectangle(highlightBrush, 0, 0, rect.Width, rect.Height / 3);
                }
            }
        }


        #endregion

        #region Event Handlers

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            this.Opacity = 0;
            _fadeInTimer.Start();

            if (Environment.OSVersion.Version.Major >= 10)
            {
                try
                {
                    int val = 2;
                    DwmSetWindowAttribute(this.Handle, DWMWA_SYSTEMBACKDROP_TYPE, ref val, sizeof(int));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to apply DWM shadow: " + ex.Message);
                }
            }
        }

        private void FadeInTimer_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1.0)
            {
                this.Opacity += 0.05;
            }
            else
            {
                this.Opacity = 1.0;
                _fadeInTimer.Stop();
                _fadeInTimer.Dispose();
                _fadeInTimer = null;
            }
        }

        private void TxtFields_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        // Name matches FrmLogin.Designer.cs: this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string usernameInput = txtUsername.Text.Trim();
            string passwordInput = txtPassword.Text;

            string username = (usernameInput == UsernamePlaceholder) ? string.Empty : usernameInput;
            string password = (passwordInput == PasswordPlaceholder) ? string.Empty : passwordInput;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowStyledMessage("Username and password are required.", "Validation Error", MessageBoxIcon.Warning);
                return;
            }

            btnLogin.Enabled = false;
            btnLogin.Text = "AUTHENTICATING...";

            try
            {
                EmployeeDTO employeeData = _employeeBll.Select();
                var authenticatedUser = employeeData.Employees
                    .FirstOrDefault(emp => emp.UserNo.ToString() == username);

                bool isAuthenticationSuccessful = false;
                if (authenticatedUser != null)
                {
                    isAuthenticationSuccessful = _employeeBll.Authenticate(authenticatedUser.EmployeeID, password);
                }

                string clientMacAddress = MachineInfo.GetPrimaryMacAddress();
                string logName = authenticatedUser != null ? $"{authenticatedUser.Name} {authenticatedUser.Surname}" : username;
                int logUserNo = authenticatedUser?.UserNo ?? 0;

                if (isAuthenticationSuccessful)
                {
                    var permissions = _permissionBll.GetPermissionsByRole(authenticatedUser.RoleID);
                    UserSession.StartSession(authenticatedUser.EmployeeID, authenticatedUser.Name + " " + authenticatedUser.Surname, authenticatedUser.RoleID, permissions);

                    string successLog = $"{DateTime.UtcNow:o} | UserNo: {logUserNo} | Name: {logName} | MAC: {clientMacAddress} | Status: Login Successful";
                    Logger.LogSecurityEvent(successLog);

                    FadeOutAndShowMainForm(); // Changed to open Form1
                }
                else
                {
                    string failureLog = $"{DateTime.UtcNow:o} | UserNo: {logUserNo} | Name: {logName} | MAC: {clientMacAddress} | Status: Login Failed (Invalid Credentials)";
                    Logger.LogSecurityEvent(failureLog);
                    ShowStyledMessage("Invalid username or password. Please try again.", "Authentication Failed", MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                string errorMac = MachineInfo.GetPrimaryMacAddress();
                string errorLog = $"{DateTime.UtcNow:o} | UserNo: N/A | Name: {username} | MAC: {errorMac} | Status: System Error during login | Exception: {ex}";
                Logger.LogSecurityEvent(errorLog);
                ShowStyledMessage($"An unexpected error occurred during login. Please contact support.\nDetails: {ex.Message}", "System Error", MessageBoxIcon.Error);
            }
            finally
            {
                btnLogin.Text = "LOGIN";
                btnLogin.Enabled = true;
            }
        }

        // Name matches FrmLogin.Designer.cs: this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
        private void btnClose_Click(object sender, EventArgs e)
        {
            FadeOutAndExit();
        }

        // Name matches FrmLogin.Designer.cs: this.pnlPassword.Paint += new System.Windows.Forms.PaintEventHandler(this.pnl_Paint_Underline);
        // This method is required by the designer. Actual drawing for pnlUsername/pnlPassword is done by InputContainer_Paint
        // which is dynamically added in StyleInputControl. So, this can be left empty.
        private void pnl_Paint_Underline(object sender, PaintEventArgs e)
        {
            // No operation needed here if StyleInputControl's dynamic Paint event handles drawing.
            // This method exists to satisfy the designer wiring.
            // Alternatively, the Paint event handler added in StyleInputControl could be named
            // pnl_Paint_Underline, and the designer wiring would call it directly,
            // but that would mean StyleInputControl couldn't be as generic.
            // For now, let InputContainer_Paint handle the drawing.
        }

        #endregion

        #region Helper Methods

        private void OnFieldFocus(TextBox textBox, Panel container, string placeholder, bool isUsernameField)
        {
            if (isUsernameField) _isUsernameActive = true;
            else _isPasswordActive = true;

            if (textBox.Text == placeholder)
            {
                textBox.Text = string.Empty;
                textBox.ForeColor = UIStyles.TextPrimary;
                if (!isUsernameField)
                {
                    textBox.UseSystemPasswordChar = true;
                }
            }
            container.Invalidate();
        }

        private void OnFieldBlur(TextBox textBox, Panel container, string placeholder, bool isUsernameField)
        {
            if (isUsernameField) _isUsernameActive = false;
            else _isPasswordActive = false;

            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = placeholder;
                textBox.ForeColor = UIStyles.TextSecondary;
                if (!isUsernameField)
                {
                    textBox.UseSystemPasswordChar = false;
                }
            }
            container.Invalidate();
        }

        private void OnButtonHover(Button button, bool isPrimaryButton, bool isHovered)
        {
            if (isPrimaryButton) _isLoginButtonHovered = isHovered;
            else _isCloseButtonHovered = isHovered;

            button.Invalidate();
        }

        /// <summary>
        /// Initiates a fade-out animation and then opens the main application form (Form1).
        /// </summary>
        private void FadeOutAndShowMainForm()
        {
            var fadeOutTimer = new Timer { Interval = 20 };
            fadeOutTimer.Tick += (s, ev) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    this.Opacity = 0;
                    fadeOutTimer.Stop();
                    fadeOutTimer.Dispose();

                    this.Hide();

                    // Assuming Form1 is your main application window
                    Form1 mainForm = new Form1(); // Ensure Form1 is defined in your project
                    mainForm.Show();

                    // Optionally close the login form if Form1 manages app lifecycle
                    // this.Close();
                }
            };
            fadeOutTimer.Start();
        }


        private void FadeOutAndExit()
        {
            var fadeOutTimer = new Timer { Interval = 20 };
            fadeOutTimer.Tick += (s, ev) =>
            {
                if (this.Opacity > 0)
                {
                    this.Opacity -= 0.1;
                }
                else
                {
                    fadeOutTimer.Stop();
                    fadeOutTimer.Dispose();
                    Application.Exit();
                }
            };
            fadeOutTimer.Start();
        }

        private void ShowStyledMessage(string message, string title, MessageBoxIcon icon)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, icon);
        }

        #endregion

        #region Windows API for Shadow (DWM)

        private const int DWMWA_SYSTEMBACKDROP_TYPE = 38;
        // private const int DWMSBT_MAINWINDOW = 2; // Example, not used directly here

        [System.Runtime.InteropServices.DllImport("dwmapi.dll")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int leftWidth;
            public int rightWidth;
            public int topHeight;
            public int bottomHeight;
        }

        #endregion
    }
}