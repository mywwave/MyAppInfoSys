using System.Net;

public class ExternalIpAddress
{
    public void GetExternalIpAddress()
    {
        string exIp = new WebClient().DownloadString("https://api.ipify.org"); 
        Console.WriteLine("Внешний IP: " + exIp); 
    }
}