<%@ Page Language="C#" %>
<%@ Assembly Name="Microsoft.SharePoint, Version=12.0.0.0, Culture=neutral,PublicKeyToken=71e9bce111e9429c"%>
<%@ Assembly Name="WordCleaner, Version=1.0.0.0, Culture=neutral,PublicKeyToken=8c9fc716f38d08b2"%>
<%@ Import Namespace="WordCleaner" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="Microsoft.SharePoint"%>
<%@ Import Namespace="Microsoft.SharePoint.WebControls"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Word Cleaner Worker Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
     <%

      try
      {
        //Get top-level site
        SPSite site = SPControl.GetContextSite(Context);

        //Build the destination path
        string source = "http://" + site.HostName + Request.QueryString["Item"];
        string cache = System.Environment.GetFolderPath(Environment.SpecialFolder.InternetCache);
        string downPath = cache + "\\" + source.Substring(source.LastIndexOf("/") + 1);
        string extension = source.Substring(source.LastIndexOf(".") + 1);

        //Make sure it is a DOCX file
        if(extension.ToUpper()!="DOCX")
          throw new Exception("Only DOCX files can be cleaned.");

        //Download file
        System.Net.WebClient client = new System.Net.WebClient();
        client.Credentials = System.Net.CredentialCache.DefaultCredentials;
        client.DownloadFile(source, downPath); 

        //Sanitize Document
        WordCleaner.Worker worker = new WordCleaner.Worker();
        worker.Sanitize(downPath);

        //Upload File
        FileStream stream = new FileStream(downPath, FileMode.Open, FileAccess.Read);
        BinaryReader reader = new BinaryReader(stream);
        byte[] bytes = reader.ReadBytes((int)stream.Length);
        reader.Close();
        stream.Close();
        client.UploadData(source, "PUT", bytes);


        //Redirect back to library
        Response.Redirect(source.Substring(0, source.LastIndexOf("/")) + "/Forms/AllItems.aspx");
    }
      catch (Exception x)
      {
        Response.Write(x.Message + "\n");
      }

            %>
        </div>
    </form>
</body>
</html>
