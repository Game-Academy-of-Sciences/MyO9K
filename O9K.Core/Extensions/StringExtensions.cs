using System;
using System.Linq;
using System.Text;

namespace O9K.Core.Extensions
{
	// Token: 0x020000A6 RID: 166
	public static class StringExtensions
	{
		// Token: 0x060004BE RID: 1214 RVA: 0x0001F748 File Offset: 0x0001D948
		internal static string RemoveLevel(this string name)
		{
			string result;
			try
			{
				int num = name.Length - 1;
				if (!char.IsDigit(name[num]))
				{
					result = name;
				}
				else
				{
					result = name.Substring(0, num).TrimEnd(new char[]
					{
						'_'
					});
				}
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		internal static string ToSentenceCase(this string name)
		{
			StringBuilder stringBuilder = new StringBuilder().Append(name[0]);
			foreach (char c in name.Skip(1))
			{
				if (char.IsUpper(c))
				{
					stringBuilder.Append(" ").Append(char.ToLower(c));
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
