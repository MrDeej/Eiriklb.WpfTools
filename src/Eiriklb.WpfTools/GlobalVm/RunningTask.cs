using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Eiriklb.WpfTools.GlobalVm
{
    public class RunningTask : IDisposable, INotifyPropertyChanged
    {
        private readonly ObservableCollection<RunningTask>? _obsCollHostedIn;
        private bool _disposed;
        private CancellationTokenSource _cancellationTokenSource;


        public CancellationToken ResetToken()
        {
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            return _cancellationTokenSource.Token;
        }

        public CancellationToken GetToken()
        {
            return _cancellationTokenSource.Token;
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }


        public RunningTask(string statusText, bool canUserCancel, ObservableCollection<RunningTask> obsCollHostedIn)
        {
            _statusText = statusText;
            _obsCollHostedIn = obsCollHostedIn ?? throw new ArgumentNullException(nameof(obsCollHostedIn));
            CancelCommand = new RelayCommand(a => { Cancel(); });
            _cancellationTokenSource = new CancellationTokenSource();
            _canUserCancel = canUserCancel;
        }

        private string _statusText;

        public string StatusText
        {
            get => _statusText;
            set
            {
                if (value == _statusText)
                    return;

                _statusText = value;
                NotifyPropertyChanged();
            }
        }


        private bool _canUserCancel;

        public bool CanUserCancel
        {
            get => _canUserCancel;
            set
            {
                if (value == _canUserCancel)
                    return;

                _canUserCancel = value;
                NotifyPropertyChanged();
            }
        }

        public ICommand CancelCommand { get; }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                // free managed resources
                _obsCollHostedIn?.Remove(this);
                _cancellationTokenSource?.Dispose();
            }

            // free unmanaged resources

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~RunningTask()
        {
            Dispose(false);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
