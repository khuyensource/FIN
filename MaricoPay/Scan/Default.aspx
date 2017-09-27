<%@ Page language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="false" Inherits="testScan.WebForm1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript" type="text/javascript">
function runApp() 
{ 
var shell = new ActiveXObject("WScript.shell");
shell.run("c:\\tztwain\\tztwain.exe", 1, true); 
}
</script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<table align="center">
				<tr colspan="4">
					<INPUT type="button" value="Scan" onclick="javascript:window.open('file:///C:/TZTwain/TZTwain.exe',1);">
					<input type="button" name="button1" value="Scan XP sp2 users" onClick="runApp()">
					<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="TZTwain.exe">Download</asp:HyperLink>
					<!--<input type="button" name="button2" value="Wia" onClick="runWia()">-->
				</tr>
				<tr>
					<td>
						<asp:Image id="Image1" runat="server" ImageUrl="upload/j2.jpg" Height="192px" Width="300px"
							EnableViewState="False"></asp:Image>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
