using NLog;
using NLog.Targets;
using System;
using System.Windows;

namespace DemoApp.Producer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public MainWindow()
        {
            InitializeComponent();
        }
        //KafkaClient kafkaClient = new KafkaClient();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //kafkaClient.Post("Hello World at - " + DateTime.Now.Ticks);
            logger.Trace("This is a sample trace message " + DateTime.Now.Ticks);
        }
    }
}
