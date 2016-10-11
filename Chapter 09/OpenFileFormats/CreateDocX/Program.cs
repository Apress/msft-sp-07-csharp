using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.IO.Packaging;

namespace MakePackage
{
    class Program
    {
        private const string wordSpace = @"http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        private const string partType = @"application/vnd.openxmlformats-officedocument.wordprocessingml.document.main+xml";
        private const string partUri = @"http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument";
        private const string partId = "rId1";

        static void Main(string[] args)
        {

            try
            {
                using (Package package = Package.Open(
                  args[0], FileMode.CreateNew, FileAccess.ReadWrite))
                {

                    //Build the document.xml file with text in it
                    XmlDocument documentXml = new XmlDocument();

                    XmlElement documentElement = documentXml.CreateElement(
                      "w:document", wordSpace);
                    documentXml.AppendChild(documentElement);

                    XmlElement bodyElement = documentXml.CreateElement(
                      "w:body", wordSpace);
                    documentElement.AppendChild(bodyElement);

                    XmlElement pElement = documentXml.CreateElement("w:p", wordSpace);
                    bodyElement.AppendChild(pElement);

                    XmlElement rElement = documentXml.CreateElement("w:r", wordSpace);
                    pElement.AppendChild(rElement);

                    XmlElement tElement = documentXml.CreateElement("w:t", wordSpace);
                    rElement.AppendChild(tElement);

                    XmlNode tNode = documentXml.CreateNode(
                      XmlNodeType.Text, "w:t", wordSpace);
                    tNode.Value = args[1];
                    tElement.AppendChild(tNode);

                    //Create the part item for document.xml
                    Uri Uri = new Uri("/word/document.xml", UriKind.Relative);
                    PackagePart partDocumentXML = package.CreatePart(Uri, partType);
                    StreamWriter stream = new
                    StreamWriter(partDocumentXML.GetStream(
                      FileMode.Create, FileAccess.Write));
                    documentXml.Save(stream);
                    stream.Close();
                    package.Flush();

                    //Create relationship for document.xml
                    package.CreateRelationship(Uri, TargetMode.Internal, partUri, partId);

                    package.Flush();
                    package.Close();
                }
            }
            catch (Exception x)
            {
                Console.WriteLine(x.Message);
            }
        }
    }
}
