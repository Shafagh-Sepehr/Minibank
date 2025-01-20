using MiniBank.AppSettings.Abstractions;
using MiniBank.Communication.Abstractions;

namespace MiniBank.Communication.Services;

public class SmsService(IAppSettings appSettings) : IDisposable, ISmsService
{
    private readonly FileStream _fileStream = File.OpenWrite(appSettings.SmsServiceFilePath);
    private          bool       _disposed;
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _fileStream.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
    
    public void Send(string message, string phoneNumber)
    {
        File.WriteAllText(appSettings.SmsServiceFilePath, $"to {phoneNumber}: {message}");
    }
}
