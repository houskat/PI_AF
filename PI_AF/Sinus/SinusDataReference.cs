using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OSIsoft.AF.Asset;
using System.Runtime.InteropServices;
using PI_AF.Sinus;

namespace PI_AF.Sinus
{
    [Description("SinusTest; IndSoft Sinus test")]
    [Guid("B7896848-BDA7-4D51-939A-682A92BC1A0E")]
    public class RandomDataReference : AFDataReference
    {

        public override Type EditorType
        {
            get
            {
                return typeof(RandomConfigEditor); 
            }
        }

        public override AFDataReferenceMethod SupportedMethods
        {
            get
            {
                return AFDataReferenceMethod.CreateConfig |  AFDataReferenceMethod.GetValue | AFDataReferenceMethod.GetValues | AFDataReferenceMethod.MultipleAttributes | AFDataReferenceMethod.ZeroAndSpan;
            }
        }

        public override AFDataReferenceContext SupportedContexts
        {
            get { return AFDataReferenceContext.All; }
        }

        
        public override string ConfigString
        {
            get { return _Config.GetConfigString(); }
            set
            {
                var cfg = RandomConfig.FromConfigString(value);
                if (cfg == null)
                {
                    MessageBox.Show(string.Format("Could not parse: {0}", value));
                    return;
                }
                this._Config = cfg;
                SaveConfigChanges(); // must be, or you loose the config string upon PI AF explorer exit
            }
        }

        private RandomConfig _Config = new RandomConfig();
        private long counter = 0;



        public override AFValues GetValues(object context, OSIsoft.AF.Time.AFTimeRange timeRange, int numberOfValues, AFAttributeList inputAttributes, AFValues[] inputValues)
        {

            AFValues res = new AFValues();
            if (
                numberOfValues < 0 //ekvidistant
                ||
                numberOfValues > 0 //nice plot
                )
            {
                numberOfValues = Math.Abs(numberOfValues);
            }

            if (numberOfValues == 0)//measured data
            {
                numberOfValues = 100;//it is computed, so there is no measured DateTime point
            }

            var ticksStep = (timeRange.EndTime.UtcTime - timeRange.StartTime.UtcTime).Ticks/numberOfValues;

            DateTime time = timeRange.StartTime.UtcTime;

            while (time < timeRange.EndTime.UtcTime)
            {

                AFValue valuePoint = new AFValue();
                valuePoint.Value = _getValue(time);
                valuePoint.Timestamp = new OSIsoft.AF.Time.AFTime(time);
                res.Add(valuePoint);

                time = time.AddTicks(ticksStep);
            }


            return res;
        }


        private double _getValue(DateTime time)
        {

            double totalSecs = (time - DateTime.MinValue).TotalSeconds;

            double res = Math.Sin(2 * Math.PI * _Config.Frequency * totalSecs);

            if (_Config.UseCounter)
                res = res + ++counter*100;

            return res;
        }

        public override AFValue GetValue(object context, object timeContext, AFAttributeList inputAttributes, AFValues inputValues)
        {
            //contexts
            OSIsoft.AF.Analysis.AFCase afc;//event frame - derives from AFObject - for storing data result of analysis
            OSIsoft.AF.Analysis.AFAnalysis afa;//derives from AFObject
            OSIsoft.AF.Modeling.AFModel afm;//aka element derives from AFObject


            
            double? value = null;
            DateTime? time = null;

            if (timeContext == null)
            {
                time = DateTime.UtcNow;
                value = _getValue(time.Value);
            }
            if (timeContext is OSIsoft.AF.Time.AFTime)
            {
                var monoTime = (OSIsoft.AF.Time.AFTime) timeContext;
                time = monoTime.UtcTime;
                value = _getValue(time.Value);
            }
            if (timeContext is OSIsoft.AF.Time.AFTimeRange)
            {
                var timeWindow = (OSIsoft.AF.Time.AFTimeRange) timeContext;
                
                //well, mean value is zero...
                value = 0;
            }


            AFValue res = new AFValue();
            if (time.HasValue)
            {
                res.Value = value;
                res.Timestamp = new OSIsoft.AF.Time.AFTime(time.Value);
            }
            else
                res.IsGood = false;

            return res;

        }


        //returns input needs for calculations based on PI AF attributes
        public override AFAttributeList GetInputs(object context)
        {
            return base.GetInputs(context);
        }

        public override AFValue RecordedValue(OSIsoft.AF.Time.AFTime time, OSIsoft.AF.Data.AFRetrievalMode mode, AFAttributeList inputAttributes, AFValues inputValues)
        {
            return base.RecordedValue(time, mode, inputAttributes, inputValues);
        }

        public override AFValues RecordedValues(OSIsoft.AF.Time.AFTimeRange timeRange, OSIsoft.AF.Data.AFBoundaryType boundaryType, string filterExpression, bool includeFilteredValues, AFAttributeList inputAttributes, AFValues[] inputValues, List<OSIsoft.AF.Time.AFTime> inputTimes, int maxCount = 0)
        {
            return base.RecordedValues(timeRange, boundaryType, filterExpression, includeFilteredValues, inputAttributes, inputValues, inputTimes, maxCount);
        }

        public override AFValues RecordedValuesByCount(OSIsoft.AF.Time.AFTime startTime, int count, bool forward, OSIsoft.AF.Data.AFBoundaryType boundaryType, string filterExpression, bool includeFilteredValues, AFAttributeList inputAttributes, AFValues[] inputValues, List<OSIsoft.AF.Time.AFTime> inputTimes)
        {
            return base.RecordedValuesByCount(startTime, count, forward, boundaryType, filterExpression, includeFilteredValues, inputAttributes, inputValues, inputTimes);
        }

        public override void SetValue(object context, AFValue newValue)
        {
            base.SetValue(context, newValue);
        }

        public override void UpdateValue(AFValue value, OSIsoft.AF.Data.AFUpdateOption updateOption)
        {
            base.UpdateValue(value, updateOption);
        }

        public override OSIsoft.AF.AFErrors<AFValue> UpdateValues(AFValues values, OSIsoft.AF.Data.AFUpdateOption updateOption)
        {
            return base.UpdateValues(values, updateOption);
        }

        public override IDictionary<OSIsoft.AF.Data.AFSummaryTypes, AFValue> Summary(OSIsoft.AF.Time.AFTimeRange timeRange, OSIsoft.AF.Data.AFSummaryTypes summaryType, OSIsoft.AF.Data.AFCalculationBasis calcBasis, OSIsoft.AF.Data.AFTimestampCalculation timeType)
        {
            return base.Summary(timeRange, summaryType, calcBasis, timeType);
        }

        public override IDictionary<OSIsoft.AF.Data.AFSummaryTypes, AFValues> Summaries(OSIsoft.AF.Time.AFTimeRange timeRange, OSIsoft.AF.Time.AFTimeSpan summaryDuration, OSIsoft.AF.Data.AFSummaryTypes summaryType, OSIsoft.AF.Data.AFCalculationBasis calcBasis, OSIsoft.AF.Data.AFTimestampCalculation timeType)
        {
            return base.Summaries(timeRange, summaryDuration, summaryType, calcBasis, timeType);
        }

        public override IDictionary<OSIsoft.AF.Data.AFSummaryTypes, AFValues> FilteredSummaries(OSIsoft.AF.Time.AFTimeRange timeRange, OSIsoft.AF.Time.AFTimeSpan summaryDuration, string filterExpression, OSIsoft.AF.Data.AFSummaryTypes summaryType, OSIsoft.AF.Data.AFCalculationBasis calcBasis, OSIsoft.AF.Data.AFSampleType sampleType, OSIsoft.AF.Time.AFTimeSpan sampleInterval, OSIsoft.AF.Data.AFTimestampCalculation timeType)
        {
            return base.FilteredSummaries(timeRange, summaryDuration, filterExpression, summaryType, calcBasis, sampleType, sampleInterval, timeType);
        }


    }
}
