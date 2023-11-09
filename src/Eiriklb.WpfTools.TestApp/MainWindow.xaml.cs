using Eiriklb.WpfTools.Logviewer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Eiriklb.WpfTools.TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogger _logger;
        public MainWindow(ILogger logger)
        {
            this._logger = logger;
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            _logger.LogInformation("Initialized {window}", nameof(MainWindow));
            base.OnInitialized(e);
        }

        private async void ButtonStartTask_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Vm.TasksStarted++;

                var taskText = Vm.TaskText + $" ({Vm.TasksStarted})";


                using var task = Vm.AddTask(taskText, Vm.CanUserCancel);
                var token = task.GetToken();

                _logger.LogInformation("Startet {taskname}", taskText);
                await Task.Delay(Vm.Duration, token);
                _logger.LogInformation("Finished {taskname}", taskText);


            }
            catch (OperationCanceledException)
            {
                //this is ok
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.GetType().Name, ex.Message,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void ButtonShowLogs_Click(object sender, RoutedEventArgs e)
        {
            var logViewer = App.ServiceProvider.GetRequiredService<LogViewer>();
            logViewer.Show();
        }
    }
}