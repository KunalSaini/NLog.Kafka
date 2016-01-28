using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Connecting to kafka queue");
            var k = new KafkaClient("storm_troper", new List<Uri> {
                new Uri("http://kafkaBroker5:9092"),
                new Uri("http://kafkaBroker6:9092"),
                new Uri("http://kafkaBroker7:9092"),
                new Uri("http://kafkaBroker8:9092")
            });

            foreach (var item in k.Recieve())
            {
                Console.WriteLine(item);
            } 
        }
    }
}
