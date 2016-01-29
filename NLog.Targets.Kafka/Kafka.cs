using NLog.Common;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLog.Targets
{
    [Target("Kafka")]
    public class Kafka : TargetWithLayout
    {
        protected KafkaClient client
        {
            get
            {
                //TODO: Get rid of this ugliness
                if (_client == null)
                {
                    lock (lockObj)
                    {
                        if (_client == null)
                        {
                            var addresses = from x in this.brokers
                                            select new Uri(x.address);
                            _client = new KafkaClient(this.topic, addresses.ToList());
                        }
                    }
                }
                return _client;
            }
        }

        private KafkaClient _client;
        Object lockObj = new Object();

        protected override void Write(LogEventInfo logEvent)
        {
            var message = this.Layout.Render(logEvent);
            SendMessageToQueue(message);
            base.Write(logEvent);
        }

        private void SendMessageToQueue(string message)
        {
            client.Post(message);
        }

        protected override void CloseTarget()
        {
            if (_client != null)
                _client.Dispose();
            base.CloseTarget();
        }


        [RequiredParameter]
        public string topic { get; set; }

        [RequiredParameter]
        [ArrayParameter(typeof(KafkaBroker), "broker")]
        public IList<KafkaBroker> brokers { get; private set; }

    }
}
