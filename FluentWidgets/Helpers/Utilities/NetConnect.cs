using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;

namespace FluentWidgets.Helpers.Utilities
{
    public class NetConnect
    {
        public static async Task<bool> IsOnline()
        {
            try
            {
                var url = CultureInfo.InstalledUICulture switch
                {
                    {Name: var n} when n.StartsWith("fa") => // Iran
                        "http://www.aparat.com",
                    {Name: var n} when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204"
                };

                var request = (HttpWebRequest) WebRequest.Create(url);
                request.KeepAlive = false;
                request.Timeout = TimeSpan.FromSeconds(10).Milliseconds;
                using var response = await request.GetResponseAsync() as HttpWebResponse;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}