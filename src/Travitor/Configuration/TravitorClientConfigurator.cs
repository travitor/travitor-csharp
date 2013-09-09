using System;
using System.Diagnostics.Contracts;

namespace Travitor.Configuration {
    internal class TravitorClientConfigurator : ITravitorClientConfigurator {
        private TravitorClientSettings _settings = new TravitorClientSettings();

        public TravitorClientConfigurator()
            : this(TravitorClientSettings.Default) {
        }

        private TravitorClientConfigurator(ITravitorClientSettings settings) {
            _settings.Address = settings.Address;
            _settings.Password = settings.Password;
            _settings.Provider = settings.Provider;
            _settings.Realm = settings.Realm;
            _settings.Tenant = settings.Tenant;
            _settings.Username = settings.Username;
        }

        public ITravitorClientConfigurator Address(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Address = value;
            return this;
        }

        public ITravitorClientConfigurator Tenant(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Tenant = value;
            return this;
        }

        public ITravitorClientConfigurator Realm(Uri value) {
            Contract.Requires<ArgumentNullException>(value != null);
            _settings.Realm = value;
            return this;
        }

        public ITravitorClientConfigurator Provider(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Provider = value;
            return this;
        }

        public ITravitorClientConfigurator Username(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Username = value;
            return this;
        }

        public ITravitorClientConfigurator Password(string value) {
            Contract.Requires<ArgumentException>(value.IsNotNullOrEmpty());
            _settings.Password = value;
            return this;
        }

        public ITravitorClientSettings Settings {
            get { return _settings; }
        }
    }
}
