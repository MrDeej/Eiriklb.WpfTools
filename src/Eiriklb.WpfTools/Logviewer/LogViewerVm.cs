using System.ComponentModel;
using System.Runtime.CompilerServices;
using Eiriklb.WpfTools.GlobalVm;
using Microsoft.Extensions.Logging;

namespace Eiriklb.WpfTools.Logviewer
{
    public class LogViewerVm : ParentViewModel, INotifyPropertyChanged
    {
        private bool _keepNewItemsInfocus;

        public bool KeepNewItemsInFocus
        {
            get => _keepNewItemsInfocus;
            set
            {
                if (value == _keepNewItemsInfocus)
                    return;

                _keepNewItemsInfocus = value;
                NotifyPropertyChanged();
            }
        }


        private LogLevel _selectedLogLevel = LogLevel.Information; /*(LogLevel)Properties.Settings.Default.Loglevel;*/

        public LogLevel SelectedLogLevel
        {
            get => _selectedLogLevel;
            set
            {
                if (value == _selectedLogLevel)
                    return;

                //Properties.Settings.Default.Loglevel = (int)value;
                //Properties.Settings.Default.Save();
                _selectedLogLevel = value;
                NotifyPropertyChanged();
            }
        }

        public LogLevel[] AllLogLevels { get; } = (LogLevel[])Enum.GetValues(typeof(LogLevel));

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
