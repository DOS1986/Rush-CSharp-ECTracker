using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace DB.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum DatabaseType
    {
        [EnumMember(Value = "SQLite")]
        SqLite,
        [EnumMember(Value = "NoSQL")]
        NoSql,
        [EnumMember(Value = "JsonFiles")]
        JsonFiles
    }
}
