using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLog.Targets
{
    public class KafkaClient : IDisposable
    {
        Producer _client;
        public KafkaClient(List<Uri> kafkaOptions)
        {
            var options = new KafkaOptions(kafkaOptions.ToArray());
            var router = new BrokerRouter(options);
            _client = new Producer(router);
        }

        public KafkaClient()
            : this(new List<Uri> { new Uri("http://172.16.37.92:9092/") })
        { }

        public void Post(string message)
        {
            try
            {
                var queueMessage = new Message(message, Guid.NewGuid().ToString());
                _client.SendMessageAsync("storm_trooper", new[] { queueMessage }).Wait();
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
