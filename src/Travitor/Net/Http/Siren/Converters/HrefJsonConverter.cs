using Newtonsoft.Json;
using System;
using Travitor.Net.Http.Siren.Models;

namespace Travitor.Net.Http.Siren.Converters {
    public class HrefJsonConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return object.Equals(objectType, typeof(Href));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            switch (reader.TokenType) {
                case JsonToken.String:
                    return new Href((string)reader.Value);
                case JsonToken.Null:
                    return null;
                default:
                    throw new InvalidOperationException("Unable to deserialize Href from token type {0}".FormatWith(reader.TokenType));
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (null == value && serializer.NullValueHandling == NullValueHandling.Include) {
                writer.WriteNull();
                return;
            }

            var href = value as Href;
            if (href != null) {
                writer.WriteValue(href.OriginalString);
                return;
            }

            throw new InvalidOperationException("Unable to serialize {0} with {1}".FormatWith(value.GetType(), typeof(HrefJsonConverter).Name));
        }
    }
}
