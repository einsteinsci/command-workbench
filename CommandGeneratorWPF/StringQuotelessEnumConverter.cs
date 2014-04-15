using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommandGeneratorWPF
{
	public class StringQuotelessEnumConverter : JsonConverter
	{

		public override bool CanConvert(Type objectType)
		{
			return objectType.IsEnum;
		}

		public override object ReadJson(JsonReader reader, Type objectType, 
			object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}

			Enum bob = (Enum)value;

			string enumName = bob.ToString();

			writer.WriteRawValue(enumName);
		}
	}
}
