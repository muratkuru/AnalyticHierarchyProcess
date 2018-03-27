using System;
using System.Configuration;

namespace AnalyticHierarchyProcess
{
    public class RandomIndex
    {
        public double GetValue(int index, string authorName)
        {
            var values = ConfigurationManager.AppSettings[authorName] 
                ?? throw new Exception($"There is no value for {authorName}");
            var value = values.Split(' ')[index].Trim();

            if (double.TryParse(value, out double result))
                return result;

            throw new Exception($"App.config: '{value}' is not a valid decimal.");
        }
    }
}
