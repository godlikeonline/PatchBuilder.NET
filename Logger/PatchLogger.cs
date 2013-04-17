using System.Diagnostics;

namespace Logger {
    public class PatchLogger {
        public EventLog EventLog {get; set;}
        private const string LOG = "Application";

        public PatchLogger(string serviceName) {
            EventLog = new EventLog {Source = serviceName, Log = LOG};
        }
    }
}