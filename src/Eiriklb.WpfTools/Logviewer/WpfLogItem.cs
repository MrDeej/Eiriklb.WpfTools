using Microsoft.Extensions.Logging;

namespace Eiriklb.WpfTools.Logviewer;

public record WpfLogItem(string? Message, Exception? Ex, LogLevel LogLevel)
{
    public DateTime LogTime { get; } = DateTime.Now;

    public string ForegroundColor => Ex != null ? "Red" : "White";
}