using System;

namespace Travitor.Configuration {
    internal class TravitorClientSettings : ITravitorClientSettings {
        private static Lazy<TravitorClientSettings> _default = new Lazy<TravitorClientSettings>(() => {
            return new TravitorClientSettings {
                Address = new Uri("https://api.travitor.com/api"),
                Tenant = new Uri("https://travitor.accesscontrol.windows.net"),
                Realm = new Uri("https://login.travitor.com"),
                Provider = "Hughes",
            };
        });

        public static ITravitorClientSettings Default { get { return _default.Value; } }

        public Uri Address { get; set; }
        public Uri Tenant { get; set; }
        public Uri Realm { get; set; }
        public string Provider { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
