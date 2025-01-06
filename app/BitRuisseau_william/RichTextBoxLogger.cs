using Microsoft.Extensions.Logging;



public class RichTextBoxLogger(string name, RichTextBox richTextBox) : ILogger
{
    public IDisposable BeginScope<TState>(TState state) => null;

    public bool IsEnabled(LogLevel logLevel)
    {
        // Customize the log levels that this logger responds to
        return logLevel >= LogLevel.Information;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        if (!IsEnabled(logLevel))
            return;

        var logMessage = formatter(state, exception);
        if (!string.IsNullOrEmpty(logMessage))
        {
            var formattedMessage = $"[{logLevel}] {name}: {logMessage}\n";
            AppendLogMessage(formattedMessage);
        }
    }

    private void AppendLogMessage(string message)
    {
        if (richTextBox.InvokeRequired)
        {
            richTextBox.Invoke(new Action(() => richTextBox.AppendText(message)));
        }
        else
        {
            richTextBox.AppendText(message);
        }
    }
}