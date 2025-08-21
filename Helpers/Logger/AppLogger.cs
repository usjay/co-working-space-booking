using PayMedia.DotNet.Utils.Logger.Configs;
using PayMedia.DotNet.Utils.Logger;
using System.Text.Json;

namespace coreworking_space_booking_backend.Helpers.Logger
{
    public class AppLogger : IAppLogger
    {
        private readonly ILog<LogConfigurations> _log;

        public AppLogger(ILog<LogConfigurations> log)
        {
            _log = log;
        }

        public void LogMethodStart(string source, string method, params object[] args)
        {
            string message = args.Any() 
                ? $"[{method}] Starting request processing. Parameters: {FormatParameters(args)}" 
                : $"[{method}] Starting request processing.";
            _log.Info(source, message);
        }

        public void LogMethodStop(string source, string method, params object[] args)
        {
            string message = args.Any()
                ? $"[{method}] Successfully completed request processing. Results: {FormatParameters(args)}"
                : $"[{method}] Successfully completed request processing.";
            _log.Info(source, message);
        }

        public void LogMethodInfo(string source, string method, string info, params object[] args)
        {
            string message = args.Any()
                ? $"[{method}] In Progress: {info}. Details: {FormatParameters(args)}"
                : $"[{method}] In Progress: {info}";
            _log.Info(source, message);
        }

        public void LogEmpty(string source, string method, string name, params object[] args)
        {
            string message = args.Any()
                    ? $"[{method}] No data found for {name}. Parameters: {FormatParameters(args)}"
                    : $"[{method}] No data found for {name}.";
            _log.Info(source, message);
        }

        public void LogError(string source, string method, Exception exception, params object[] args)
        {
            string exceptionDetails = $"{exception.Message}{(exception.InnerException != null ? $" Inner Exception: {exception.InnerException.Message}" : "")}";

            string message = args.Any()
                ? $"[{method}] Proccess finished but not successfully completed due to an Exception: {exceptionDetails}. Parameters: {FormatParameters(args)}"
                : $"[{method}] Proccess finished but not successfully completed due to an Exception: {exceptionDetails}";

            _log.Error(source, message);
        }

        public void LogWarning(string source, string method, string warning, params object[] args)
        {
            string message = args.Any()
                ? $"[{method}] Warning: {warning}. Parameters: {FormatParameters(args)}"
                : $"[{method}] Warning: {warning}";

            _log.Warning(source, message);
        }

        public void LogCount(string source, string method,string name, int count, params object[] args)
        {
            string countMessage = $"[{method}] Data found for {name}: {count} record{(count != 1 ? "s" : "")}";

            string message = args.Any()
                ? $"{countMessage}. Parameters: {FormatParameters(args)}"
                : $"{countMessage}.";
            _log.Info(source, message);
        }
        public void LogMethodStopUnsuccessful(string source, string method, string reason, params object[] args)
        {

            string message = args.Any()
                ? $"[{method}] Request processing completed unsuccessfully. Reason: {reason}. Context: {FormatParameters(args)}"
                : $"[{method}] Request processing completed unsuccessfully. Reason: {reason}";
            _log.Info(source, message);

        }

        // INFO :: helper methods

        private static string GetObjectKeyValues(object obj) 
        {
            if (obj == null) { return string.Empty; }

            string key = obj.GetType().Name;
            if (key.StartsWith("<>f__AnonymousType"))
            {
                return JsonSerializer.Serialize(obj);
            }
            string value = JsonSerializer.Serialize(obj);
            return $"{key}: {value}";
        }

        private static string FormatParameters(object[] obj)
        {
            return string.Join(", ", obj.Select(GetObjectKeyValues));
        }

        
    }
}
