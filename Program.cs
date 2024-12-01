using Proxy_pattern;
using System.Runtime.InteropServices.JavaScript;

internal class Program
{
    private static void Main(string[] args)
    {
        ISubject proxyAdmin = new Proxy("admin");

        Console.WriteLine(proxyAdmin.Request("Request1"));
        Console.WriteLine(proxyAdmin.Request("Request2"));

        ISubject proxyGuest = new Proxy("guest");

        Console.WriteLine(proxyGuest.Request("Request1"));
        Console.WriteLine(proxyGuest.Request("Request2"));
    }
}
