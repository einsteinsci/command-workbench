using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CommandGeneratorWPF
{
	public class QuotelessStringConverter : JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			if (objectType == typeof(string))
			{
				return true;
			}

			return false;
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
			}

			string s = value as string;

			s = s.Replace(" ", "_"); // replace spaces with underscores
			s = s.Replace("\n", "\\"); // newlines with backslashes
			s = s.Replace("\t", "\\t"); // tabs with \t
			s = s.Replace("\"", "'"); //double quotes with apostrophes

			writer.WriteRawValue(s);
		}
	}
}
