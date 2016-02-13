using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Linq;

namespace DemoApp.Consumer
{
    class Program
    {
        static KafkaNet.Consumer _consumer;
        static string _topic;
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Connecting to kafka queue");
                _topic = "storm_troper";
                var options = new KafkaOptions(
                    new Uri("http://kafkaBroker5:9092"),
                    new Uri("http://kafkaBroker6:9092"),
                    new Uri("http://kafkaBroker7:9092"),
                    new Uri("http://kafkaBroker8:9092")
                );
                var router = new BrokerRouter(options);
                var coption = new ConsumerOptions(_topic, router);
                _consumer = new KafkaNet.Consumer(coption);
                var offset = _consumer.GetTopicOffsetAsync(_topic, 1000000).Result;
                var t = from x in offset select new OffsetPosition(x.PartitionId, x.Offsets.Max());
                _consumer.SetOffsetPosition(t.ToArray());

                foreach (var message in _consumer.Consume())
                {
                    Console.WriteLine("Response: P{0},O{1} : {2}",
                    message.Meta.PartitionId, 
                    message.Meta.Offset,
                    System.Text.Encoding.Default.GetString(message.Value));
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
