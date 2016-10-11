using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.Packaging;

namespace PackageItems
{
    class Program
    {
        private const string wordSpace =
          @"http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        static void Main(string[] args)
        {
            try
            {
                using (Package package = Package.Open
                      (args[0], FileMode.Open, FileAccess.ReadWrite))
                {
                    //Get the document part and load it into XML document
                    Uri uriDocument = new Uri("/word/document.xml", UriKind.Relative);
                    PackagePart documentPart = package.GetPart(uriDocument);

                    Stream partStream = package.GetPart(uriDocument).GetStream(
                        FileMode.Open, FileAccess.ReadWrite);

                    NameTable nameTable = new NameTable();
                    XmlNamespaceManager manager = new XmlNamespaceManager(nameTable);
                    manager.AddNamespace("w", wordSpace);

                    XmlDocument document = new XmlDocument(nameTable);
                    document.Load(partStream);

                    XmlNodeList textNodes = document.SelectNodes("//w:t", manager);
                    foreach (XmlNode textNode in textNodes)
                    {
                        Console.WriteLine(textNode.InnerText);
                    }
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
    }
}
