using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Lifetime.ApplicationStarted.Register(() =>
{
    Console.WriteLine("Starting Docker-Compose File...");

    var proc = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = @"C:\Windows\System32\cmd.exe",
            Arguments = "/c docker-compose up -d",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        }
    };

    proc.Start();
    proc.Close();
    proc.Dispose();
});

AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
{
    var proc = new Process
    {
        StartInfo = new ProcessStartInfo
        {
            FileName = @"C:\Windows\System32\cmd.exe",
            Arguments = "/c docker-compose down",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        }
    };

    proc.Start();
    proc.Close();
    proc.Dispose();
};

app.Run();