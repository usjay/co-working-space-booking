namespace coreworking_space_booking_backend.Helpers.Logger
{
    public interface IAppLogger
    {
        /// <summary>
        /// Logs the start of a method execution.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="requestParamenters">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogMethodStart("UserService", "CreateUser", "{"key1": value}", "{"key2": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] INFO: UserService       | [CreateUser] Starting request processing. Parameters: key1: value, key1: value
        /// </example>
        void LogMethodStart(string source, string method, params object[] requestParamenters);

        /// <summary>
        /// Logs the successful completion of a method execution.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="resultParameters">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogMethodStop("UserService", "CreateUser", "{"key1": value}", "{"key2": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] INFO: UserService       | [CreateUser] Successfully completed request processing. Results: key1: value, key1: value
        /// </example>
        void LogMethodStop(string source, string method, params object[] resultParameters);

        /// <summary>
        /// Logs additional information during method execution.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="message">The informational message to log.</param>
        /// <param Name="args">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogMethodInfo("UserService", "CreateUser", "Validating user input", "{"key": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] INFO: UserService       | [CreateUser] In Progress: Validating user input. Details: key1: value
        /// </example>
        void LogMethodInfo(string source, string method, string message, params object[] args);

        /// <summary>
        /// Logs when a query or list operation returns no results.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="name">The Name of the entity or data being queried.</param>
        /// <param Name="args">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogEmpty("UserService", "GetUsersByRole", "users", "{"key": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] INFO: UserService       | [CreateUser] No data found for users. Parameters:  key1: value
        /// </example>
        void LogEmpty(string source, string method, string name, params object[] args);

        /// <summary>
        /// Logs when an exception occurs during method execution.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="exception">The exception that occurred.</param>
        /// <param Name="args">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogError("UserService", "CreateUser", ex, "{"key": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] ERROR: UserService       | [CreateUser] Exception: {exception details}. Parameters: key1: value
        /// </example>
        void LogError(string source, string method, Exception exception, params object[] args);

        /// <summary>
        /// Logs warning messages during method execution.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="warning">The warning message to log.</param>
        /// <param Name="args">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogWarning("UserService", "UpdateUser", "User profile incomplete", "{"key": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] WARN: UserService       | [UpdateUser] Warning: User profile incomplete. Parameters: key1: value
        /// </example>
        void LogWarning(string source, string method, string warning, params object[] args);

        /// <summary>
        /// Logs the count of results returned by a query or list operation.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="name">The Name of the entity or data being counted.</param>
        /// <param Name="count">The number of items found.</param>
        /// <param Name="args">
        /// Optional parameters for the method. You may log these parameters in the log file. 
        /// Typically, pass them as a JSON object; however, for simpler types like strings or integers, 
        /// wrap them in an anonymous object (e.g., new { variable }) to ensure consistent logging.
        /// </param>
        /// <example>
        /// LogCount("UserService", "GetActiveUsers", "users", 5, "{"key": value}");
        /// // Outputs:[yyyy-MM-dd hh:mm:ss] INFO: UserService       |[GetActiveUsers] Data found for users: 5 records. Parameters: Parameters: key1: value
        /// </example>
        void LogCount(string source, string method, string name, int count, params object[] args);

        /// <summary>
        /// Logs when a method completes unsuccessfully with a specific reason.
        /// </summary>
        /// <param Name="source">The source/class Name where the logging occurs.</param>
        /// <param Name="method">The Name of the method being executed.</param>
        /// <param Name="reason">The reason for unsuccessful completion.</param>
        /// <param Name="args">
        /// Optional parameters to provide additional context.
        /// Pass them as an anonymous object (e.g., new { Id = 123, Status = "failed" }).
        /// </param>
        /// <example>
        /// LogMethodStopUnsuccessful("UserService", "CreateUser", "Invalid Email format", new { userId = 123, Email = "invalid@" });
        /// // Outputs: [yyyy-MM-dd hh:mm:ss] WARN: UserService | [CreateUser] Request processing completed unsuccessfully. Reason: Invalid Email format. Context: {"userId": 123, "Email": "invalid@"}
        /// </example>
        void LogMethodStopUnsuccessful(string source, string method, string reason, params object[] args);
        


    }
}
