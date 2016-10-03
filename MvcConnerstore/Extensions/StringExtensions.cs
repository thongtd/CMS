using System.Text;

namespace MvcConnerstore.Extensions
{
    public static class StringExtension
    {
        public static string JsEncode(this string value, bool appendQuotes = true)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();

            if (appendQuotes)
            {
                sb.Append("\"");
            }

            foreach (char c in value)
            {
                switch (c)
                {
                    case '\"': sb.Append("\\\""); break;
                    case '\\': sb.Append("\\\\"); break;
                    case '\b': sb.Append("\\b"); break;
                    case '\f': sb.Append("\\f"); break;
                    case '\n': sb.Append("\\n"); break;
                    case '\r': sb.Append("\\r"); break;
                    case '\t': sb.Append("\\t"); break;
                    default:
                        var i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else { sb.Append(c); }
                        break;
                }
            }

            if (appendQuotes)
            {
                sb.Append("\"");
            }

            return sb.ToString();
        }
    }
}
