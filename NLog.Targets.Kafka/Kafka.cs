using NLog.Common;
using NLog.Config;

namespace NLog.Targets
{
    [Target("Kafka")]
    public class Kafka : TargetWithLayout
    {
        public Kafka()
        {
            this.Host = "localhost";
        }

        protected override void Write(LogEventInfo logEvent)
        {
            var message = this.Layout.Render(logEvent);
            SendMessageToQueue(message);
            base.Write(logEvent);
        }

        private void SendMessageToQueue(string message)
        {
            throw new System.NotImplementedException();
        }

        [RequiredParameter]
        public string Host { get; set; }
    }
}
