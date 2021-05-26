using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using FluentWidgets.Helpers;
using FluentWidgets.Model;
using ScottPlot;

namespace FluentWidgets.WidgetModel
{
    public class CPUPerformanceWidgetModel : INotifyPropertyChanged
    {
        private readonly CPUPerformanceModel _cpuModel = new CPUPerformanceModel();
        private Timer _updateTimer;

        public CPUPerformanceWidgetModel()
        {
            _updateTimer = new Timer(UpdateCpuValues, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));
            MaxSpeed = Convert.ToDouble(_cpuModel.RunWmiQuery("root\\CIMV2", "SELECT *, Name FROM Win32_Processor",
                "MaxClockSpeed"));
        }

        public string ModelTitle { get; } = "CPUPerformance";

        public string Name
        {
            get
            {
                var name = _cpuModel.RunWmiQuery("root\\CIMV2", "SELECT * FROM Win32_Processor", "Name")
                    .ToString().Replace("(TM)", "™").Replace("(tm)", "™").Replace("(R)", "®")
                    .Replace("(r)", "®").Replace("(C)", "©").Replace("(c)", "©")
                    .Replace("    ", " ").Replace("  ", " ");
                return name.Remove(name.IndexOf("CPU", StringComparison.Ordinal));
            }
        }

        public double MaxSpeed { get; set; }
        public double[] Utilisation { get; set; } = new double[61];
        public double[] KernalUtilisation { get; set; } = new double[61];
        public int CurrentUtilisation { get; set; }
        public double Speed { get; set; }
        public int Processes { get; set; }
        public int Threads { get; set; }
        public int Handles { get; set; }
        public TimeSpan UpTime { get; set; }
        public bool ShowKernelTimes { get; set; } = true;

        public ICommand ToggleKernelGraphCommand => new DelegateCommand(o => ToggleKernelGraph((Plot) o));
        public event PropertyChangedEventHandler PropertyChanged;

        private void UpdateCpuValues(object sender)
        {
            // Update CPU utilisation values
            Array.Copy(Utilisation, 1, Utilisation, 0, Utilisation.Length - 1);
            Utilisation[Utilisation.Length - 1] = _cpuModel.PercentageProcessorTimeCounter.NextValue();

            Array.Copy(KernalUtilisation, 1, KernalUtilisation, 0, KernalUtilisation.Length - 1);
            KernalUtilisation[KernalUtilisation.Length - 1] = _cpuModel.PercentagePrivilegedTimeCounter.NextValue();
            ;

            CurrentUtilisation = (int) Utilisation.LastOrDefault();

            // Update CPU speed
            var maxSpeed = MaxSpeed / 1000;
            Speed = maxSpeed * _cpuModel.PercentagePerformanceCounter.NextValue() / 100;

            // Update process, thread and handles count
            var procs = Process.GetProcesses();
            Processes = procs.Length;
            Threads = procs.Sum(p => p.Threads.Count);
            Handles = procs.Sum(p => p.HandleCount);

            // Get the system up time
            UpTime = TimeSpan.FromSeconds(_cpuModel.SystemUpTimeCounter.NextValue());
        }

        private void ToggleKernelGraph(Plot plot)
        {
            ShowKernelTimes = plot.GetPlottables()[1].IsVisible = !plot.GetPlottables()[1].IsVisible;
        }
    }
}