namespace ProductsShop.App
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    internal static class XMLSerializer
    {
        internal static string SerializeXML(XDocument xObject)
        {
            string result;

            using (var stringWriter = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "  ",
                    NewLineChars = "\r\n",
                    NewLineHandling = NewLineHandling.Replace
                };

                using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
                {
                    xObject.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    result = stringWriter.GetStringBuilder().ToString();
                }
            }

            return result;
        }
    }
}