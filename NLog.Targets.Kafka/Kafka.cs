using KafkaNet;
using KafkaNet.Protocol;
using NLog.Common;
using NLog.Config;
using System;
using System.Collections.Generic;

namespace NLog.Targets
{
    [Target("Kafka")]
    public class Kafka : TargetWithLayout
    {
        public Kafka()
        {
            brokers = new List<KafkaBroker>();
        }
        protected override void Write(LogEventInfo logEvent)
        {
            var message = this.Layout.Render(logEvent);
            SendMessageToQueue(message);
            base.Write(logEvent);
        }

        private void SendMessageToQueue(string message)
        {
            try
            {
                var queueMessage = new Message(message);
                this.GetProducer().SendMessageAsync(topic, new[] { queueMessage }, 1);
            }
            catch (Exception ex)
            {
                InternalLogger.Error("Unable to send message to kafka queue", ex);
            }
        }

        protected override void CloseTarget()
        {
            KafkaConnectionHelper.CloseProducer();
            base.CloseTarget();
        }


        [RequiredParameter]
        public string topic { get; set; }

        [RequiredParameter]
        [ArrayParameter(typeof(KafkaBroker), "broker")]
        public IList<KafkaBroker> brokers { get; set; }

    }
}
