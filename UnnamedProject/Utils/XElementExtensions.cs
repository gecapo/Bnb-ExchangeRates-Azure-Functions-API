using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace UnnamedProject
{
    public static class XElementExtensions
    {
        public static TResult Deserialize<TResult>(string xml) where TResult : class
        {
            using var sr = new StringReader(xml);
            var xs = new XmlSerializer(typeof(TResult));
            return (TResult)xs.Deserialize(sr);
        }

        public static bool TryParse(string text, out XElement xElement)
        {
            XElement xml = null;
            try
            {
                xml = XElement.Parse(text);
                xElement = xml;
                return true;
            }
            catch (Exception)
            {
                //Parsing fails and should fail only when job is triggered for the current date and rates have not been uploaded.
                xElement = xml;
                return false;
            }
        }
    }
}

