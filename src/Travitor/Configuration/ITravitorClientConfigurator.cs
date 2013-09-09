using System;

namespace Travitor.Configuration {
    public interface ITravitorClientConfigurator {
        ITravitorClientConfigurator Address(Uri value);
        ITravitorClientConfigurator Tenant(Uri value);
        ITravitorClientConfigurator Realm(Uri value);
        ITravitorClientConfigurator Provider(string value);
        ITravitorClientConfigurator Username(string value);
        ITravitorClientConfigurator Password(string value);
    }
}
