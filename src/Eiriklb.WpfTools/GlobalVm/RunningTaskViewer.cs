using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Eiriklb.WpfTools.GlobalVm
{
    public class RunningTaskViewer : Control
    {

        private Button? _buttonRunningTasks;
        private ProgressBar? _progressBar;

        static RunningTaskViewer()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RunningTaskViewer), new FrameworkPropertyMetadata(typeof(RunningTaskViewer)));
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _buttonRunningTasks = GetTemplateChild("PART_BtnRunningTasks") as Button;
            _progressBar = GetTemplateChild("PART_ProgressBar") as ProgressBar;

            if (_buttonRunningTasks != null)
                _buttonRunningTasks.Click +=_buttonRunningTasks_Click;
            SettNoTaskIsActive(this);
        }

        private void _buttonRunningTasks_Click(object sender, RoutedEventArgs e)
        {
            if (GetTemplateChild("MyPopup") is Popup popup)
            {
                popup.IsOpen = !popup.IsOpen;  // Toggle popup visibility
            }
        }

        public ObservableCollection<RunningTask> ObsCollRunningTasks
        {
            get => (ObservableCollection<RunningTask>)GetValue(ObsCollRunningTasksProperty);
            set => SetValue(ObsCollRunningTasksProperty, value);
        }

        // Using a DependencyProperty as the backing store for ObsCollRunningTasks.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObsCollRunningTasksProperty =
            DependencyProperty.Register(nameof(ObsCollRunningTasks), typeof(ObservableCollection<RunningTask>), typeof(RunningTaskViewer), new PropertyMetadata(null, OnObsCollRunningTasksChanged));



        private static void OnObsCollRunningTasksChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is RunningTaskViewer viewer && e.NewValue is ObservableCollection<RunningTask> newColl)
            {
                newColl.CollectionChanged+=(sender, e1) =>
                {


                    if (newColl.Count > 0)
                    {
                        SettSomeTaskIsActive(viewer, newColl);
                    }
                    else
                    {
                        SettNoTaskIsActive(viewer);
                    }



                };
            }
        }

        private static void SettNoTaskIsActive(RunningTaskViewer viewer)
        {
            if (viewer._buttonRunningTasks != null)
            {
                viewer._buttonRunningTasks.Visibility = Visibility.Collapsed;
            }
            if (viewer._progressBar != null)
                viewer._progressBar.Visibility = Visibility.Collapsed;
        }

        private static void SettSomeTaskIsActive(RunningTaskViewer viewer, ObservableCollection<RunningTask> newColl)
        {
            if (newColl.Count == 1)
            {
                if (viewer._buttonRunningTasks != null)
                {
                    viewer._buttonRunningTasks.Visibility = Visibility.Visible;
                    viewer._buttonRunningTasks.Content = newColl[0].StatusText;
                }
                if (viewer._progressBar != null)
                {
                    viewer._progressBar.Visibility = Visibility.Visible;
                }

            }
            else
            {
                if (viewer._buttonRunningTasks != null)
                {
                    viewer._buttonRunningTasks.Visibility = Visibility.Visible;
                    viewer._buttonRunningTasks.Content = newColl.Count + " running tasks...";
                }
                if (viewer._progressBar != null)
                {
                    viewer._progressBar.Visibility = Visibility.Visible;
                }

            }
        }

        public void CancelTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is RunningTask runningTask)
            {
                // Your logic to handle the cancel action here...
                ObsCollRunningTasks.Remove(runningTask);

                // Optionally close the Popup:
                if (GetTemplateChild("MyPopup") is Popup popup)
                {
                    popup.IsOpen = false;
                }
            }
        }
    }
}
