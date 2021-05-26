using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using FluentWidgets.WidgetModel;
using ScottPlot;
using ScottPlot.Control;
using Color = System.Drawing.Color;

namespace FluentWidgets.Widgets
{
    /// <summary>
    ///     Interaction logic for TestWidget.xaml
    /// </summary>
    public partial class CPUPerformance
    {
        private readonly Color CPU_COLOR = ColorTranslator.FromHtml("#117DBB");
        private readonly CPUPerformanceWidgetModel _cpuWidgetModel = new CPUPerformanceWidgetModel();
        private readonly DispatcherTimer _renderTimer;

        public CPUPerformance()
        {
            InitializeComponent();
            DataContext = _cpuWidgetModel;
            NameScope.SetNameScope(ContextMenu, NameScope.GetNameScope(this));

            pltUtilisation.RightClicked -= pltUtilisation.DefaultRightClickEvent;
            pltUtilisation.BorderBrush = (SolidColorBrush) new BrushConverter().ConvertFromString("#" + CPU_COLOR.Name);

            pltUtilisation.Configuration.LeftClickDragPan = false;
            pltUtilisation.Configuration.RightClickDragZoom = false;
            pltUtilisation.Configuration.ScrollWheelZoom = false;
            pltUtilisation.Configuration.Quality = QualityMode.High;
            pltUtilisation.Configuration.LockHorizontalAxis = true;
            pltUtilisation.Configuration.LockHorizontalAxis = true;
            pltUtilisation.Configuration.DoubleClickBenchmark = false;

            pltUtilisation.Plot.Frameless();
            pltUtilisation.Plot.AxisAuto(0, 0);
            pltUtilisation.Plot.SetAxisLimits(yMin: 0, yMax: 100, xMin: 0, xMax: 60);
            pltUtilisation.Plot.XAxis.ManualTickSpacing(4);
            pltUtilisation.Plot.YAxis.ManualTickSpacing(10);
            pltUtilisation.Plot.Grid(color: Color.FromArgb(64, CPU_COLOR));

            // Plot the overall processor time
            var overallPlot = pltUtilisation.Plot.AddSignal(_cpuWidgetModel.Utilisation);
            overallPlot.Color = CPU_COLOR;
            overallPlot.MarkerSize = 0;
            overallPlot.FillType = FillType.FillBelow;
            overallPlot.FillColor1 = Color.FromArgb(64, CPU_COLOR);

            // Plot the overall kernel time
            var kernelPlot = pltUtilisation.Plot.AddSignal(_cpuWidgetModel.KernalUtilisation);
            kernelPlot.Color = CPU_COLOR;
            kernelPlot.MarkerSize = 0;
            kernelPlot.FillType = FillType.FillBelow;
            kernelPlot.FillColor1 = Color.FromArgb(64, CPU_COLOR);

            _renderTimer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(1)};
            _renderTimer.Tick += (sender, e) =>
            {
                var brush = ((SolidColorBrush) bdrMain.Background).Color;
                var color = Color.FromArgb(brush.A, brush.R, brush.G, brush.B);
                pltUtilisation.Plot.Style(color, color);
                pltUtilisation.Render();
            };
            _renderTimer.Start();
        }
    }
}