using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.IO.Packaging;

namespace WordCleaner
{
    public class Worker
    {
        //Namespace and URI constants
        private const string wordSpace = @"http://schemas.openxmlformats.org/wordprocessingml/2006/main";
        private const string docUri = @"/word/document.xml";

        public void Sanitize(string packagePath)
        {
            try
            {
                //Open the package
                using (Package package = Package.Open(packagePath, FileMode.Open, FileAccess.ReadWrite))
                {

                    //Get the document part
                    Uri uriDocument = new Uri(docUri, UriKind.Relative);
                    PackagePart documentPart = package.GetPart(uriDocument);

                    //Load the document part into a stream
                    Stream partStream = documentPart.GetStream(FileMode.Open, FileAccess.ReadWrite);

                    //Add the namespace manager to reference the Word namespace
                    NameTable nameTable = new NameTable();
                    XmlNamespaceManager manager = new XmlNamespaceManager(nameTable);
                    manager.AddNamespace("w", wordSpace);

                    //Create a temporary XML document from the stream
                    //so we can manipulate the XML elements
                    XmlDocument tempDoc = new XmlDocument(nameTable);
                    tempDoc.Load(partStream);

                    //Remove deleted text from temporary XML document
                    XmlNodeList delNodes = tempDoc.SelectNodes("//w:del", manager);
                    foreach (XmlNode delNode in delNodes)
                    {
                        delNode.ParentNode.RemoveChild(delNode);
                    }

                    //Promote the inserted text in temporary XMl document
                    //so it appears normally in the Word document
                    XmlNodeList insNodes = tempDoc.SelectNodes("//w:ins", manager);
                    foreach (XmlNode insNode in insNodes)
                    {
                        foreach (XmlNode childNode in insNode.ChildNodes)
                        {
                            insNode.ParentNode.InsertBefore(childNode, insNode);
                        }

                        insNode.ParentNode.RemoveChild(insNode);
                    }

                    //Remove comments text from temporary XML document
                    //Must remove several different elements to accomplish this
                    XmlNodeList commentStartNodes = tempDoc.SelectNodes(
                                                    "//w:commentRangeStart", manager);
                    foreach (XmlNode commentStartNode in commentStartNodes)
                    {
                        commentStartNode.ParentNode.RemoveChild(commentStartNode);
                    }

                    XmlNodeList commentEndNodes = tempDoc.SelectNodes(
                                                  "//w:commentRangeEnd", manager);
                    foreach (XmlNode commentEndNode in commentEndNodes)
                    {
                        commentEndNode.ParentNode.RemoveChild(commentEndNode);
                    }

                    XmlNodeList commentRefNodes = tempDoc.SelectNodes(
                                                  "//w:commentReference", manager);
                    foreach (XmlNode commentRefNode in commentRefNodes)
                    {
                        commentRefNode.ParentNode.RemoveChild(commentRefNode);
                    }

                    //Save the temporary XMl document changes back to the document part
                    documentPart.GetStream().SetLength(0);
                    tempDoc.Save(documentPart.GetStream());

                    //delete the relationship with the comments part
                    Uri uriComments = new Uri("/word/comments.xml", UriKind.Relative);
                    PackagePart commentsPart = package.GetPart(uriComments);

                    PackageRelationshipCollection relationships = documentPart.GetRelationships();

                    foreach (PackageRelationship relationship in relationships)
                    {
                        if (relationship.TargetUri.ToString() == "comments.xml")
                        {
                            documentPart.DeleteRelationship(relationship.Id);
                            break;
                        }
                    }

                    //Delete comments part from package
                    package.DeletePart(uriComments);

                    //Save the package changes
                    package.Flush();
                    package.Close();

                }
            }
            catch (Exception x)
            {
                LogMessage(x.Message, EventLogEntryType.Error);
            }
        }

        static void LogMessage(string message, EventLogEntryType entry)
        {
            if (!EventLog.SourceExists("Word Cleaner"))
                EventLog.CreateEventSource("Word Cleaner", "Application");
            EventLog.WriteEntry("Word Cleaner", message, entry);
        }
    }
}
