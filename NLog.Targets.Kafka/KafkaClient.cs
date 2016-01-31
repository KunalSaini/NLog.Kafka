using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NLog.Targets
{
    public class KafkaClient : IDisposable
    {
        Producer _client;
        string _topic;
        public KafkaClient(string topic, List<Uri> kafkaOptions)
        {
            _topic = topic;
            var options = new KafkaOptions(kafkaOptions.ToArray());
            var router = new BrokerRouter(options);
            _client = new Producer(router);
        }

        public void Post(string message)
        {
            try
            {
                var queueMessage = new Message(message, Guid.NewGuid().ToString());
                var results =
                    _client.SendMessageAsync(_topic, new[] { queueMessage }, 1);
            }
            catch (KafkaApplicationException ex)
            {
                var strErrorMessage = ex.Message;
            }
            catch(Exception ex)
            {
                var m = ex.Message;
            }
        }

        public void Dispose()
        {
            if (_client != null)
                _client.Dispose();
        }
    }
}
