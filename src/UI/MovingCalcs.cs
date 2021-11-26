using Newtonsoft.Json;

using System;
using System.Collections.Generic;


namespace AITool
{
    public class MovingCalcs
    {

        [JsonIgnore]
        public Queue<Decimal> samples = new Queue<Decimal>();
        public int windowSize = 250;
        public int lastDayOfYear = 0;
        public int lastMonth = 0;
        [JsonIgnore]
        public Decimal sampleAccumulator { get; set; } = 0;
        [JsonProperty("Average")]
        public Decimal Avg { get; set; } = 0;
        [JsonIgnore]
        public string AvgS { get { return this.Avg.ToString("#####0"); } }
        public Decimal Min { get; set; } = 0;
        [JsonIgnore]
        public string MinS { get { return this.Min.ToString("#####0"); } }
        public Decimal Max { get; set; } = 0;
        [JsonIgnore]
        public string MaxS { get { return this.Max.ToString("#####0"); } }
        [JsonIgnore]
        public int Count { get; set; } = 0;
        public int CountToday { get; set; } = 0;
        public int CountMonth { get; set; } = 0;
        public Decimal Current { get; set; } = 0;
        [JsonIgnore]
        public DateTime TimeInitialized { get; set; } = DateTime.Now;
        public string ItemName { get; set; } = "Items";
        public bool IsTime { get; set; } = false;
        public string ToStringFormat { get; set; } = "#####0";
        [JsonConstructor]
        public MovingCalcs() { this.UpdateDate(true); }
        public MovingCalcs(int windowSize, string itemName, bool IsTime, string ToStringFormat = "#####0")
        {
            this.ToStringFormat = ToStringFormat;
            this.windowSize = windowSize;
            this.ItemName = ItemName;
            this.IsTime = IsTime;
        }
        public double ItemsPerMinute()
        {

            this.UpdateDate(false);

            if (this.CountToday == 0)
                return 0;

            return this.CountToday / (DateTime.Now - TimeInitialized).TotalMinutes;
        }
        public double ItemsPerSecond()
        {
            this.UpdateDate(false);

            if (this.CountToday == 0)
                return 0;

            return this.CountToday / (DateTime.Now - TimeInitialized).TotalSeconds;
        }


        public void AddToCalc(double newSample)
        {
            this.AddToCalc(Convert.ToDecimal(newSample));
        }
        public void AddToCalc(int newSample)
        {
            this.AddToCalc(Convert.ToDecimal(newSample));
        }
        public void AddToCalc(long newSample)
        {
            this.AddToCalc(Convert.ToDecimal(newSample));
        }
        public void UpdateDate(bool init)
        {
            if (DateTime.Now.DayOfYear != this.lastDayOfYear)
            {
                if (init)
                    this.CountToday = 0;
                else
                    this.CountToday = 1;

                this.lastDayOfYear = DateTime.Now.DayOfYear;

                if (DateTime.Now.Month != this.lastMonth)
                {
                    if (init)
                        this.CountMonth = 0;
                    else
                        this.CountMonth = 1;

                    this.lastMonth = DateTime.Now.Month;
                }
            }
        }
        public void AddToCalc(Decimal newSample)
        {


            if (newSample > 0)
            {
                this.Current = newSample;

                this.Count++;
                this.CountToday++;
                this.CountMonth++;

                this.UpdateDate(false);

                this.sampleAccumulator += newSample;
                this.samples.Enqueue(newSample);

                if (this.samples.Count > this.windowSize)
                {
                    this.sampleAccumulator -= this.samples.Dequeue();
                }

                if (this.sampleAccumulator > 0)  //divide by 0?
                    this.Avg = this.sampleAccumulator / this.samples.Count;

                if (this.Min == 0)
                {
                    this.Min = newSample;
                }
                else
                {
                    this.Min = Math.Min(newSample, this.Min);
                }
                this.Max = Math.Max(newSample, this.Max);

            }

        }

        public override string ToString()
        {
            string ms = "";
            if (this.IsTime)
                ms = "ms";

            return $"{this.Count} {this.ItemName} | {this.CountToday} today | {this.CountMonth} Month | {this.ItemsPerMinute().ToString("#####0")}/MIN (Min={this.Min.ToString(this.ToStringFormat)}{ms},Max={this.Max.ToString(this.ToStringFormat)}{ms},Avg={this.Avg.ToString(this.ToStringFormat)}{ms},Last={this.Current.ToString(this.ToStringFormat)}{ms})";
        }
    }

}

