using KafkaNet;
using KafkaNet.Model;
using System;
using System.Linq;

namespace NLog.Targets
{
    public static class KafkaConnectionHelper
    {
        static Producer _producer = null;

        public static Producer GetProducer(this Kafka kafkaObj)
        {
            if (_producer == null)
            {
                var addresses = from x in kafkaObj.brokers
                                select new Uri(x.address);
                var router = new BrokerRouter(new KafkaOptions(addresses.ToArray()));
                _producer = new Producer(router);
            }
            return _producer;
        }

        public static void CloseProducer()
        {
            if (_producer != null)
                _producer.Dispose();
            _producer = null;
        }
    }
}
