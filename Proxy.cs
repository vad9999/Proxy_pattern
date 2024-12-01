using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy_pattern
{
    public class Proxy : ISubject
    {
        private RealSubject _realSubject;
        private Dictionary<string, string> _cache;
        private DateTime _lastCacheTime;
        private readonly TimeSpan _cacheExpiration;
        private readonly string _userRole;

        public Proxy(string userRole)
        {
            _realSubject = new RealSubject();
            _cache = new Dictionary<string, string>();
            _lastCacheTime = DateTime.MinValue;
            _cacheExpiration = TimeSpan.FromMinutes(1);
            _userRole = userRole;
        }

        private bool HasPermission(string request)
        {
            if (_userRole == "admin") return true;

            if (_userRole == "guest" && request == "Request1") return true;

            return false;
        }

        public string Request(string request)
        {
            if (!HasPermission(request))
            {
                return "Доступ запрещен: у вас нет разрешения на выполнение этого запроса.";
            }

            if (_cache.ContainsKey(request) && DateTime.Now - _lastCacheTime < _cacheExpiration)
            {
                Console.WriteLine("Proxy: возврат кэшированного ответа.");
                return _cache[request];
            }

            Console.WriteLine("Proxy: Делегирование запроса в RealSubject.");
            string response = _realSubject.Request(request);

            _cache[request] = response;
            _lastCacheTime = DateTime.Now;

            return response;
        }
    }
}
