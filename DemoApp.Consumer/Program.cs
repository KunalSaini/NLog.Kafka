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
            var k = new KafkaClient();
            foreach (var item in k.Recieve())
            {
                Console.WriteLine(item);
            } 
        }
    }
}
