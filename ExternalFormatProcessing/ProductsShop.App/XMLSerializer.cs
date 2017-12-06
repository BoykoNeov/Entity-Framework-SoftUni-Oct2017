namespace ProductsShop.App
{
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;

    internal static class XMLSerializer
    {
        internal static string SerializeXML(XDocument xObject, bool identation)
        {
            string result = string.Empty;

            using (var stringWriter = new StringWriter())
            {
                XmlWriter xmlTextWriter;

                if (identation)
                {
                    XmlWriterSettings settings = new XmlWriterSettings
                    {
                        Indent = true,
                        IndentChars = "  ",
                        NewLineChars = "\r\n",
                        NewLineHandling = NewLineHandling.Replace
                    };

                    xmlTextWriter = XmlWriter.Create(stringWriter, settings);
                }
                else
                {
                    xmlTextWriter = XmlWriter.Create(stringWriter);
                }

                using (xmlTextWriter)
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