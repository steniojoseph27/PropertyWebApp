namespace PropertyWebApp.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class PropertyViewModel
    {
        [JsonProperty("properties")]
        public PropertyElement[] Properties { get; set; }
    }

    public partial class PropertyElement
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("address")]
        public Address Address { get; set; }

        [JsonProperty("financial")]
        public Financial Financial { get; set; }

        [JsonProperty("physical")]
        public Physical Physical { get; set; }
    }

    public partial class Address
    {
        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("state")]
        public State State { get; set; }

        [JsonProperty("zip")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Zip { get; set; }
    }

    public partial class Financial
    {
        [JsonProperty("listPrice")]
        public long ListPrice { get; set; }

        [JsonProperty("monthlyRent")]
        public long MonthlyRent { get; set; }
    }

    public partial class Physical
    {
        [JsonProperty("yearBuilt")]
        public long YearBuilt { get; set; }
    }

    public enum Country { Usa };

    public enum State { Ca, Fl };

    public enum PriceVisibility { ShowPrice };

    public enum PropertyType { Unspecified };

    public enum ContentType { ApplicationOctetStream, ImageJpeg };

    public enum ResourceType { Property3DTour, PropertyFloorPlan, PropertyPhoto };

    public enum Status { CertificationRejected, OffMarket, Sold };

    public enum Visibility { None, Public };

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                CountryConverter.Singleton,
                StateConverter.Singleton,
                PriceVisibilityConverter.Singleton,
                PropertyTypeConverter.Singleton,
                ContentTypeConverter.Singleton,
                ResourceTypeConverter.Singleton,
                StatusConverter.Singleton,
                VisibilityConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class CountryConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Country) || t == typeof(Country?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "USA")
            {
                return Country.Usa;
            }
            throw new Exception("Cannot unmarshal type Country");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Country)untypedValue;
            if (value == Country.Usa)
            {
                serializer.Serialize(writer, "USA");
                return;
            }
            throw new Exception("Cannot marshal type Country");
        }

        public static readonly CountryConverter Singleton = new CountryConverter();
    }

    internal class StateConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(State) || t == typeof(State?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CA":
                    return State.Ca;
                case "FL":
                    return State.Fl;
            }
            throw new Exception("Cannot unmarshal type State");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (State)untypedValue;
            switch (value)
            {
                case State.Ca:
                    serializer.Serialize(writer, "CA");
                    return;
                case State.Fl:
                    serializer.Serialize(writer, "FL");
                    return;
            }
            throw new Exception("Cannot marshal type State");
        }

        public static readonly StateConverter Singleton = new StateConverter();
    }

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }

    internal class PriceVisibilityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PriceVisibility) || t == typeof(PriceVisibility?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "ShowPrice")
            {
                return PriceVisibility.ShowPrice;
            }
            throw new Exception("Cannot unmarshal type PriceVisibility");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PriceVisibility)untypedValue;
            if (value == PriceVisibility.ShowPrice)
            {
                serializer.Serialize(writer, "ShowPrice");
                return;
            }
            throw new Exception("Cannot marshal type PriceVisibility");
        }

        public static readonly PriceVisibilityConverter Singleton = new PriceVisibilityConverter();
    }

    internal class PropertyTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(PropertyType) || t == typeof(PropertyType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "Unspecified")
            {
                return PropertyType.Unspecified;
            }
            throw new Exception("Cannot unmarshal type PropertyType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (PropertyType)untypedValue;
            if (value == PropertyType.Unspecified)
            {
                serializer.Serialize(writer, "Unspecified");
                return;
            }
            throw new Exception("Cannot marshal type PropertyType");
        }

        public static readonly PropertyTypeConverter Singleton = new PropertyTypeConverter();
    }

    internal class ContentTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ContentType) || t == typeof(ContentType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "application/octet-stream":
                    return ContentType.ApplicationOctetStream;
                case "image/jpeg":
                    return ContentType.ImageJpeg;
            }
            throw new Exception("Cannot unmarshal type ContentType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ContentType)untypedValue;
            switch (value)
            {
                case ContentType.ApplicationOctetStream:
                    serializer.Serialize(writer, "application/octet-stream");
                    return;
                case ContentType.ImageJpeg:
                    serializer.Serialize(writer, "image/jpeg");
                    return;
            }
            throw new Exception("Cannot marshal type ContentType");
        }

        public static readonly ContentTypeConverter Singleton = new ContentTypeConverter();
    }

    internal class ResourceTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(ResourceType) || t == typeof(ResourceType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Property3DTour":
                    return ResourceType.Property3DTour;
                case "PropertyFloorPlan":
                    return ResourceType.PropertyFloorPlan;
                case "PropertyPhoto":
                    return ResourceType.PropertyPhoto;
            }
            throw new Exception("Cannot unmarshal type ResourceType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (ResourceType)untypedValue;
            switch (value)
            {
                case ResourceType.Property3DTour:
                    serializer.Serialize(writer, "Property3DTour");
                    return;
                case ResourceType.PropertyFloorPlan:
                    serializer.Serialize(writer, "PropertyFloorPlan");
                    return;
                case ResourceType.PropertyPhoto:
                    serializer.Serialize(writer, "PropertyPhoto");
                    return;
            }
            throw new Exception("Cannot marshal type ResourceType");
        }

        public static readonly ResourceTypeConverter Singleton = new ResourceTypeConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "CertificationRejected":
                    return Status.CertificationRejected;
                case "OffMarket":
                    return Status.OffMarket;
                case "Sold":
                    return Status.Sold;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.CertificationRejected:
                    serializer.Serialize(writer, "CertificationRejected");
                    return;
                case Status.OffMarket:
                    serializer.Serialize(writer, "OffMarket");
                    return;
                case Status.Sold:
                    serializer.Serialize(writer, "Sold");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }

    internal class VisibilityConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Visibility) || t == typeof(Visibility?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "None":
                    return Visibility.None;
                case "Public":
                    return Visibility.Public;
            }
            throw new Exception("Cannot unmarshal type Visibility");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Visibility)untypedValue;
            switch (value)
            {
                case Visibility.None:
                    serializer.Serialize(writer, "None");
                    return;
                case Visibility.Public:
                    serializer.Serialize(writer, "Public");
                    return;
            }
            throw new Exception("Cannot marshal type Visibility");
        }

        public static readonly VisibilityConverter Singleton = new VisibilityConverter();
    }
}
