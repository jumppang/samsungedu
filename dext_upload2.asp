<%@ Language=VBScript %>
<%
    Dim UploadForm
    Set UploadForm = Server.CreateObject("DEXT.FileUpload")

    UploadForm.DefaultPath="D:\TEMP"

    For Each item In UploadForm("files") 
	    item.Save 
    Next 

    '?꾩쓽 援щЦ?� ?꾨옒?� 媛숇떎.
    'For i = 1 To UploadForm("files").Count
    '	FilePath = UploadForm("files")(i).Save(,False)	
    'Next
%>

<!DOCTYPE html>
<html>
<head>
    <title></title>
    <meta content="text/html; charset=ks_c_5601-1987" http-equiv="content-Type" />
</head>
<body>
	<div class="leftTopIndentOne">
        <p><span class="bold">First File</span></p>
        <p>Original Path :          <%= UploadForm("files")(1).FilePath             %></p>	
		<p>File Size :              <%= UploadForm("files")(1).FileLen              %> bytes</p>
		<p>Mime Type :              <%= UploadForm("files")(1).MimeType             %></p>
		<p>Last Saved File Name :   <%= UploadForm("files")(1).LastSavedFileName    %></p>
		<p>Last Saved File Path :   <%= UploadForm("files")(1).LastSavedFilePath    %></p><br />
		
 
    </div>		
</body>
</html>

<%
    Set UploadForm = Nothing
%>
