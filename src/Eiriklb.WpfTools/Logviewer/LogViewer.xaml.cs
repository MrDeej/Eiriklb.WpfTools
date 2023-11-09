using System.Windows;
using System.Windows.Controls;

namespace Eiriklb.WpfTools.Logviewer
{
    /// <summary>
    /// Interaction logic for LogViewer.xaml
    /// </summary>
    public partial class LogViewer : Window
    {
        private volatile bool _isUserScroll = true;
        private Guid _delayGuid;


        public LogViewer()
        {
            InitializeComponent();
            ViewModel.InMemoryData.ObsCollLogg.CollectionChanged+=ObsCollLogg_CollectionChanged;
        }



        private void BtnClearLog_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.InMemoryData.ObsCollLogg.Clear();
        }

        private void ListBoxLog_Loaded(object sender, RoutedEventArgs e)
        {
            var listBox = (ListBox)sender;

            var scrollViewer = WpfExtensions.FindControl<ScrollViewer>(listBox);

            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += (o, eInner) =>
                {
                    if (eInner.VerticalChange == 0.0)
                        return;

                    if (_isUserScroll)
                    {
                        if (eInner.VerticalChange > 0.0)
                        {
                            var scrollerOffset = eInner.VerticalOffset + eInner.ViewportHeight;
                            if (Math.Abs(scrollerOffset - eInner.ExtentHeight) < 5.0)
                            {
                                // The user has tried to move the scroll to the bottom, activate autoscroll.
                                ViewModel.KeepNewItemsInFocus = true;
                            }
                        }
                        else
                        {
                            // The user has moved the scroll up, deactivate autoscroll.
                            ViewModel.KeepNewItemsInFocus = false;
                        }
                    }
                    _isUserScroll = true;


                };
            }

            var lastItem = ViewModel.InMemoryData.ObsCollLogg.LastOrDefault();

            if (lastItem != null) ListBoxLog.ScrollIntoView(lastItem);
        }

        private async void ObsCollLogg_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems?[0] is not WpfLogItem item) return;

            var thisGuid = Guid.NewGuid();
            _delayGuid = thisGuid;
            await Task.Delay(20);
            if (_delayGuid != thisGuid)
                return;

            if (ViewModel.KeepNewItemsInFocus)
            {
                _isUserScroll = false;
                ListBoxLog.ScrollIntoView(item);
            }
        }
    }
}
