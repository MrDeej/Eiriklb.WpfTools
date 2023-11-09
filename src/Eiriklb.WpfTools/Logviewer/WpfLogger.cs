using System.Windows;
using Microsoft.Extensions.Logging;

namespace Eiriklb.WpfTools.Logviewer
{
    public class WpfLogger : Microsoft.Extensions.Logging.ILogger
    {
        private readonly LogViewerVm _vm;

        // A class that represents a logging scope
        private class WpfLoggerScope : IDisposable
        {
            public object State { get; }
            public WpfLoggerScope(object state)
            {
                this.State = state;
            }

            public void Dispose()
            {
                // You can perform any cleanup here if needed
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            // You can use the current scope's state to enrich your log message
            var scope = CurrentScope.Value;
            var message = formatter(state, exception);
            if (scope != null)
            {
                message += $" (Scope: {scope.State})";
            }

            Application.Current.Dispatcher.Invoke(
                () => 
                InMemoryData.Instance.ObsCollLogg.Add(
                    new WpfLogItem(message, exception, logLevel)),
                System.Windows.Threading.DispatcherPriority.Background);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= _vm.SelectedLogLevel;
        }

        // A field that holds the current scope using AsyncLocal
        private static readonly AsyncLocal<WpfLoggerScope?> CurrentScope = new();

        public WpfLogger(LogViewerVm vm)
        {
            this._vm = vm;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            // Create a new scope and set it as the current one
            var scope = new WpfLoggerScope(state);
            var parent = CurrentScope.Value;
            CurrentScope.Value = scope;

            // Return an IDisposable that restores the parent scope when disposed
            return new DisposableAction(() =>
            {
                CurrentScope.Value = parent;
            });
        }

    }
    // A helper class that executes an action when disposed
    public class DisposableAction : IDisposable
    {
        private readonly Action _action;
        public DisposableAction(Action action)
        {
            _action = action;
        }

        public void Dispose()
        {
            _action();
        }
    }
}
