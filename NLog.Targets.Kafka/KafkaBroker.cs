using NLog.Config;
using System;

namespace NLog.Targets
{
    [NLogConfigurationItem]
    [ThreadAgnostic]
    public class KafkaBroker
    {
        public Uri address { get; set; }
    }
}
