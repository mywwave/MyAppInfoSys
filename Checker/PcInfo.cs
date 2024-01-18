using System.Net;

namespace Checker
{
    /// <summary>
    /// Класс для получения информации о компьютере
    /// </summary>
    public class PcInfo
    {
        #region Методы      
        public void PrintPCInfo()
        {
            string pcInfo = GetPCInfo();
            Console.WriteLine(pcInfo);
        }
        #endregion

        #region Получение строк
        private string GetPCInfo()
        {
            string username = Environment.UserName;
            string localIp = GetLocalIpAddress();

            return $"Имя пользователя: {username}\nЛокальный IP: {localIp}";
        }

        private string GetLocalIpAddress()
        {
            string localIp = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ipAddress in host.AddressList)
            {
                if (ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIp = ipAddress.ToString();
                    break;
                }
            }
            return localIp;
        }
        #endregion

    }
}