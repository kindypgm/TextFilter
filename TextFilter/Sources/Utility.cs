using System;
using System.Collections.Generic;
using System.Text;

namespace TextFiltering
{
	internal static class DictionaryExtensions
	{
		public static bool TryGetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, out TValue value, TValue defaultValue)
		{
			if( true == dict.TryGetValue(key, out value) )
				return true;
			else
			{
				value = defaultValue;
				return false;
			}
		}
	}

	internal class StringList : List<String>
	{
		public static implicit operator String[](StringList stringList)
		{
			return stringList.ToArray();
		}
	}
}
