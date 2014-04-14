using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandGeneratorWPF.NBT
{
	public interface IJsonable
	{
		/// <summary>
		/// Allows custom JSON formatting and excluding items from tree
		/// </summary>
		/// <returns>JSON data in a string</returns>
		string ToJSON();
	}
}
