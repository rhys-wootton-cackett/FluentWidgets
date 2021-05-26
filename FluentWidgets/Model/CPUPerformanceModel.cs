using System.Diagnostics;
using System.Linq;
using System.Management;

namespace FluentWidgets.Model
{
    public class CPUPerformanceModel
    {
        public CPUPerformanceModel()
        {
            PercentageProcessorTimeCounter = GetPerformanceCounter("Processor", "_Total", "% Processor Time");
            PercentagePrivilegedTimeCounter = GetPerformanceCounter("Processor", "_Total", "% Privileged Time");
            PercentagePerformanceCounter =
                GetPerformanceCounter("Processor Information", "_Total", "% Processor Performance");
            SystemUpTimeCounter = GetPerformanceCounter("System", "", "System Up Time");
        }

        public PerformanceCounter PercentageProcessorTimeCounter { get; }
        public PerformanceCounter PercentagePrivilegedTimeCounter { get; }
        public PerformanceCounter PercentagePerformanceCounter { get; }
        public PerformanceCounter SystemUpTimeCounter { get; }

        /// <summary>
        ///     Returns an object containing the value of the property that is returned from the Management
        ///     Object Searcher.
        /// </summary>
        /// <param name="scope">A <see cref="T:System.Management.ManagementScope" /> specifying the scope of the query.</param>
        /// <param name="query">An <see cref="T:System.Management.ObjectQuery" /> specifying the query to be invoked.</param>
        /// <param name="property">A property to look for in the returned <see cref="T:System.Management.ObjectQuery" /></param>
        /// <returns></returns>
        public object RunWmiQuery(string scope, string query, string property)
        {
            var searcher = new ManagementObjectSearcher(scope, query);

            foreach (var o in searcher.Get())
            {
                var queryObj = (ManagementObject) o;
                return queryObj[property];
            }

            // Could not find, so return null
            return default;
        }

        private static PerformanceCounter GetPerformanceCounter(string categoryName, string counters,
            string counterName)
        {
            var processorCategory = PerformanceCounterCategory.GetCategories()
                .FirstOrDefault(cat => cat.CategoryName.Equals(categoryName));
            var countersInCategory = processorCategory?.GetCounters(counters);
            var counter = countersInCategory?.First(cnt => cnt.CounterName.Equals(counterName));
            return counter;
        }
    }
}