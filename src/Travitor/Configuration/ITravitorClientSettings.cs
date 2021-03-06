﻿using System;

namespace Travitor.Configuration {
    public interface ITravitorClientSettings {
        Uri Address { get; }
        Uri Tenant { get; }
        Uri Realm { get; }
        string Provider { get; }
        string Username { get; }
        string Password { get; }
    }
}
