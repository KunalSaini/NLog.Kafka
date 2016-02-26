Kafka target for NLog 
=====================
[![Build status](https://ci.appveyor.com/api/projects/status/utpusoaq5r1mutb3/branch/master?svg=true)](https://ci.appveyor.com/project/KunalSaini/nlog-kafka/branch/master)

A NLog target that post to Kafka queue 

Dependencies
------------

Uses [kafka-net](https://github.com/Jroland/kafka-net) as underlying connector to Kafka

Usage
-----

Add [NLog.Kafka](https://www.nuget.org/packages/NLog.Targets.Kafka) to your solution and update your nlog.config:

Add the following `target` to the `targets` section

```
    <target xsi:type="Kafka"
       name="KafkaTarget"
       topic="storm_troper"
       layout="Custom text ${longdate} level=${level}">
        <broker address="http://kafkaBroker1:9092"/>
        <broker address="http://kafkaBroker2:9092"/>
        <broker address="http://kafkaBroker3:9092"/>
        <broker address="http://kafkaBroker4:9092"/>
    </target>
```