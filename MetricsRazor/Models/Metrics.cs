
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsRazor.Models
{
        public class Metrics
        {
            public DateTime timestamp { get; set; }
            public Environment environment { get; set; }
            public Context[] contexts { get; set; }
        }

        public class Environment
        {
            public string machineName { get; set; }
            public string processName { get; set; }
            public string os { get; set; }
            public string osVersion { get; set; }
            public string cpuCount { get; set; }
            public string hostName { get; set; }
            public DateTime localTime { get; set; }
            public string entryAssemblyName { get; set; }
            public string entryAssemblyVersion { get; set; }
        }

        public class Context
        {
            public string context { get; set; }
            public Apdexscore[] apdexScores { get; set; }
            public Counter[] counters { get; set; }
            public Gauge[] gauges { get; set; }
            public object[] histograms { get; set; }
            public Meter[] meters { get; set; }
            public Timer[] timers { get; set; }
        }

        public class Apdexscore
        {
            public string name { get; set; }
            public int frustrating { get; set; }
            public int sampleSize { get; set; }
            public int satisfied { get; set; }
            public float score { get; set; }
            public int tolerating { get; set; }
            public Tags tags { get; set; }
        }

        public class Tags
        {
            public string app { get; set; }
            public string server { get; set; }
            public string env { get; set; }
        }

        public class Counter
        {
            public string name { get; set; }
            public string unit { get; set; }
            public int count { get; set; }
            public object[] items { get; set; }
            public Tags1 tags { get; set; }
        }

        public class Tags1
        {
            public string app { get; set; }
            public string server { get; set; }
            public string env { get; set; }
            public string http_status_code { get; set; }
        }

        public class Gauge
        {
            public string name { get; set; }
            public string unit { get; set; }
            public object value { get; set; }
            public Tags2 tags { get; set; }
        }

        public class Tags2
        {
            public string app { get; set; }
            public string server { get; set; }
            public string env { get; set; }
            public string route { get; set; }
        }

        public class Meter
        {
            public string name { get; set; }
            public string unit { get; set; }
            public int count { get; set; }
            public float fifteenMinuteRate { get; set; }
            public float fiveMinuteRate { get; set; }
            public object[] items { get; set; }
            public float meanRate { get; set; }
            public float oneMinuteRate { get; set; }
            public string rateUnit { get; set; }
            public Tags3 tags { get; set; }
        }

        public class Tags3
        {
            public string app { get; set; }
            public string server { get; set; }
            public string env { get; set; }
            public string route { get; set; }
            public string http_status_code { get; set; }
        }

        public class Timer
        {
            public string name { get; set; }
            public string unit { get; set; }
            public int activeSessions { get; set; }
            public int count { get; set; }
            public string durationUnit { get; set; }
            public Histogram histogram { get; set; }
            public Rate rate { get; set; }
            public string rateUnit { get; set; }
            public Tags4 tags { get; set; }
        }

        public class Histogram
        {
            public float lastValue { get; set; }
            public float max { get; set; }
            public float mean { get; set; }
            public float median { get; set; }
            public float min { get; set; }
            public float percentile75 { get; set; }
            public float percentile95 { get; set; }
            public float percentile98 { get; set; }
            public float percentile99 { get; set; }
            public float percentile999 { get; set; }
            public int sampleSize { get; set; }
            public float stdDev { get; set; }
            public float sum { get; set; }
        }

        public class Rate
        {
            public float fifteenMinuteRate { get; set; }
            public float fiveMinuteRate { get; set; }
            public float meanRate { get; set; }
            public float oneMinuteRate { get; set; }
        }

        public class Tags4
        {
            public string app { get; set; }
            public string server { get; set; }
            public string env { get; set; }
            public string route { get; set; }
        }
    
}
