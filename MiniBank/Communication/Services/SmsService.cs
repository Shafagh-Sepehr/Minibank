using MiniBank.Communication.Abstractions;

namespace MiniBank.Communication.Services;

public class SmsService: IDisposable, ISmsService
{
    private readonly FileStream _fileStream = File.OpenWrite(@"C:\Users\shafaghs\Desktop\PlayGround\SmsService.txt");
    private          bool       _disposed;
    
    public void Send(string message, string phoneNumber)
    {
        File.WriteAllText(@"C:\Users\shafaghs\Desktop\PlayGround\SmsSerivce.txt",$"to {phoneNumber}: {message}");
    }
    
    
    public void Dispose()
    {
        if (!_disposed)
        {
            _fileStream.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
