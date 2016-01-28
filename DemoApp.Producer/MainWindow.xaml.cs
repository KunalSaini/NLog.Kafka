using NLog;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DemoApp.Producer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Logger logger;
        
        static MainWindow()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button)sender;
            switch (b.Name)
            {
                case "Trace":
                    logger.Trace("This is a sample trace message");
                    break;
                case "Debug":
                    logger.Debug("This is a sample debug message", "important");
                    break;
                case "Warn":
                    logger.Warn("This is a sample warn message");
                    break;
                case "Error":
                    logger.Error("This is a sample error message");
                    break;
                case "Fatal":
                    logger.Fatal("This is a sample fatal message");
                    break;
                default:
                    logger.Info("This is a sample default info message");
                    break;
            }
        }
    }
}
