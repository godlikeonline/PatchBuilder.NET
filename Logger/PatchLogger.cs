using System.Diagnostics;

namespace Logger {
    public class PatchLogger {
        public EventLog _eventLog {get; set;}
        private const string LOG = "Application";

        public PatchLogger(string serviceName) {
            _eventLog = new EventLog();
            _eventLog.Source = serviceName;
            _eventLog.Log = LOG;
        }
    }
}
