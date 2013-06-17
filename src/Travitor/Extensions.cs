﻿using Microsoft.IdentityModel.Tokens.JWT;
using Microsoft.WindowsAzure.ActiveDirectory.Authentication;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Runtime.InteropServices;
using System.Security;

namespace Travitor {
    public static class Extensions {
        internal static bool IsNotNullOrEmpty(this string self) {
            return false == string.IsNullOrEmpty(self);
        }

        internal static string Unsecure(this SecureString value) {
            if (null == value) {
                throw new ArgumentNullException("value");
            }

            IntPtr ptr = IntPtr.Zero;
            try {

                ptr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(ptr);
            }
            finally {
                Marshal.ZeroFreeGlobalAllocUnicode(ptr);
            }
        }

        internal static SecureString Secure(this string value) {
            if (null == value) {
                throw new ArgumentNullException("value");
            }

            unsafe {
                fixed (char* chars = value) {
                    var secure = new SecureString(chars, value.Length);
                    secure.MakeReadOnly();
                    return secure;
                }
            }
        }

        internal static IList<IdentityProviderDescriptor> GetProviders(this AuthenticationContext self, Uri targetService) {
            Contract.Requires<ArgumentNullException>(targetService != null);
            return self.GetProviders(targetService.ToString());
        }

        internal static AssertionCredential AcquireToken(this AuthenticationContext self, Uri targetService, IdentityProviderDescriptor identityProvider, Credential credential) {
            return self.AcquireToken(targetService.ToString(), identityProvider, credential);
        }

        internal static JWTSecurityToken AsSecurityToken(this AssertionCredential self) {
            return new JWTSecurityToken(self.Assertion);
        }
    }
}