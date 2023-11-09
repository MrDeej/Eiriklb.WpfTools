using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;

namespace Eiriklb.WpfTools.Logviewer
{
    public class InMemoryData : INotifyPropertyChanged
    {

        public static InMemoryData Instance { get; } = new InMemoryData();


        private InMemoryData()
        {


            if (!DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject())) return;

        }


        public ObservableCollection<WpfLogItem> ObsCollLogg { get; } = new();



        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    public record WpfLogItem(string? Message, Exception? Ex, LogLevel LogLevel)
    {
        public DateTime LogTime { get; } = DateTime.Now;

        public string ForegroundColor => Ex != null ? "Red" : "White";
    }
}
