using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Travitor.Net.Http.Siren.Models;

namespace Travitor.Net.Http.Siren.Serialization {
    public class SirenContractResolver : CamelCasePropertyNamesContractResolver {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization) {
            JsonProperty property = base.CreateProperty(member, memberSerialization);

            if (property.DeclaringType == typeof(Document) && property.PropertyType == typeof(Actions)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Actions.Any();
                };
            }

            if (property.DeclaringType == typeof(Document) && property.PropertyType == typeof(Links)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Links.Any();
                };
            }

            if (property.DeclaringType == typeof(Document) && property.PropertyType == typeof(Entities)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Entities.Any();
                };
            }

            if (property.DeclaringType == typeof(Document) && property.PropertyType == typeof(Class)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Class.Any();
                };
            }

            if (property.DeclaringType == typeof(Document) && property.PropertyType == typeof(Rel)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Rel.Any();
                };
            }

            if (property.DeclaringType == typeof(Document) && property.PropertyName.Equals("Properties", StringComparison.OrdinalIgnoreCase)) {
                property.ShouldSerialize = instance => {
                    return (instance as Document).Properties.GetType().GetProperties().Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyType == typeof(Actions)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Actions.Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyType == typeof(Links)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Links.Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyType == typeof(Entities)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Entities.Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyType == typeof(Class)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Class.Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyType == typeof(Rel)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Rel.Any();
                };
            }

            if (property.DeclaringType == typeof(Entity) && property.PropertyName.Equals("Properties", StringComparison.OrdinalIgnoreCase)) {
                property.ShouldSerialize = instance => {
                    return (instance as Entity).Properties.GetType().GetProperties().Any();
                };
            }

            return property;
        }
    }
}
