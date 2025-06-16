using System;

namespace Erp_V1.DAL
{
    /// <summary>
    /// Thrown when a business rule prevents an operation (e.g. trying to delete an in‐use entity).
    /// </summary>
    public class BusinessRuleException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public BusinessRuleException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleException"/> class with serialized data.
        /// This constructor is needed for serialization when an exception propagates from a remoting server to the client. 
        /// </summary>
        /// <param name="info">The <see cref="System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data.</param>
        /// <param name="context">The <see cref="System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected BusinessRuleException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
