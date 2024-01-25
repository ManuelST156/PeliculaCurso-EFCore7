using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PeliculaEntity.Entidades.Configuraciones
{
    //Entidad interfaz que sirve para traer la fecha desde el backend osea desde la api la fecha en formato especifico
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParseExact(reader.GetString(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
                {
                    return result;
                }
            }

            return DateTime.MinValue; // O el valor que desees en caso de error
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd"));
        }
    }
    






}
