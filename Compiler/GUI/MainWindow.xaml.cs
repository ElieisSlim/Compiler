using System.Windows;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace Compiler.MainWindow
{
    public partial class MainWindow : Window
    {
        //Start Gui Research OxyPlot library
        //Didn't have time to get this OxyPlot Shell working
        private LineSeries graphSeries;

        public MainWindow(PlotView plotView)
        {
            plotView.Model = new PlotModel { Title = "Graph", Subtitle = "X vs Y" };
            graphSeries = new LineSeries();
            plotView.Model.Series.Add(graphSeries);
        }

        private void CommandTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // Check for "Enter" key press
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                string command = commandTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(command))
                {
                    // Parse the input command to get X and Y values (assuming format: "X Y")
                    string[] values = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (values.Length == 2 && double.TryParse(values[0], out double xValue) && double.TryParse(values[1], out double yValue))
                    {
                        // Add the data point to the graph
                        graphSeries.Points.Add(new DataPoint(xValue, yValue));
                        plotView.InvalidatePlot();
                    }
                }

                // Clear the command prompt text box after processing the input
                commandTextBox.Text = string.Empty;
            }
        }
    }
}