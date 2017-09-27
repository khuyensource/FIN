<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNewEmployee.ascx.cs"
    Inherits="MaricoPay.uc.ucNewEmployee" %>
<%@ Register Src="ucComboDepartment.ascx" TagName="department" TagPrefix="uc5" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<%@ Register Src="ucViewStatus.ascx" TagName="ucViewStatus" TagPrefix="uc6" %>
<style type="text/css">
    html, body
    {
        height: 100%;
        margin: 0;
        padding: 0;
    }
    .style2
    {
        width: 247px;
    }
    .divLeft
    {
        float: left;
        margin-left: 10px;
        margin-top: 10px;
        margin-bottom: 5px;
    }
    .divRight
    {
        float: right;
        margin-right: 10px;
        margin-top: 10px;
        margin-bottom: 5px;
    }
</style>
<script type="text/javascript">

    function GetUserName() {
        var objUserInfo = new ActiveXObject("WScript.network");
        document.write(objUserInfo.ComputerName + "<br>");
        document.write(objUserInfo.UserDomain + "<br>");
        document.write(objUserInfo.UserName + "<br>");
        var uname = objUserInfo.UserName;
        alert(uname);
    }
    function getUser() {
        return Components.classes["@mozilla.org/process/environment;1"].getService(Components.interfaces.nsIEnvironment).get('USERNAME');
    }
    function getLogin() {
        var szLogin = '<%Response.Write(Request.ServerVariables["LOGON_USER"]); %>'

        var length = szLogin.length;
        alert(szLogin);
        return szLogin;
    }
</script>
<script language="javascript" type="text/javascript">
    function DoPostBack(obj) {
        __doPostBack(obj.id, 'OtherInformation');
    }
   
</script>
<h2>
    Welcome to FIN SYSTEM!
</h2>
<%-- <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />--%>
<uc4:uscMsgBox ID="MsgBox1" runat="server" />
<fieldset>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="getLogin();"
        Visible="false" />
    <legend style="font-size: large; font-weight: bold; font-style: oblique">MEMBER CREATION
    </legend>
    <div>
        <table>
            <tr>
                <td valign="top">
                    <div class="divLeft">
                        <table>
                            <tr>
                                <td style="width: 20%;">
                                    Email:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmail" runat="server" Width="290px" Font-Names="Arial" AutoPostBack="true"
                                        Font-Size="Medium" OnTextChanged="txtEmail_TextChanged"></asp:TextBox>
                                    <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                    <asp:Button ID="btNew" runat="server" Text="New" OnClick="btNew_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Full name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtFullName" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                    <asp:CheckBox ID="chkManager" runat="server" Text="Is manager" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Display Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDisplayname" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    FirstName:
                                </td>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtFirstName" runat="server" Width="130px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                            </td>
                                            <td style="width: 20%;">
                                                LastName:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLastName" runat="server" Width="70px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Gender:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropGender" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium">
                                        <asp:ListItem Text="Nam/Male" Value="M" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="Nữ/Female" Value="F"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Company:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropCompany" AutoPostBack="true" DataTextField="Name" DataValueField="Company_PK"
                                        Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium" OnSelectedIndexChanged="dropCompany_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Department:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropDepartment" DataTextField="Description" AutoPostBack="true"
                                        DataValueField="CostCenter" Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium"
                                        OnSelectedIndexChanged="dropDepartment_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Position:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropPosition" DataTextField="Description" DataValueField="Position_PK"
                                        Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                    <a href="~/NewPosition.aspx" onclick="window.open(this.href, 'mywin','left=300,top=300,width=500,height=150,toolbar=1,resizable=0'); return false;">
                                        New..</a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Level:
                                </td>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="dropLevel" DataTextField="Name" DataValueField="ID" Width="150px"
                                                    runat="server" Font-Names="Arial" Font-Size="Medium">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 20%;">
                                                Grade:
                                            </td>
                                            <td align="left">
                                                <telerik:RadNumericTextBox ID="radNumGrade" runat="server" MaxValue="100" MinValue="0" Width="50px"  Font-Names="Arial" Font-Size="Medium">
                                                    <NegativeStyle Resize="None"></NegativeStyle>
                                                    <NumberFormat ZeroPattern="n" DecimalDigits="0"></NumberFormat>
                                                    <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                                    <ReadOnlyStyle Resize="None"></ReadOnlyStyle>
                                                    <FocusedStyle Resize="None"></FocusedStyle>
                                                    <DisabledStyle Resize="None"></DisabledStyle>
                                                    <InvalidStyle Resize="None"></InvalidStyle>
                                                    <HoveredStyle Resize="None"></HoveredStyle>
                                                    <EnabledStyle Resize="None"></EnabledStyle>
                                                </telerik:RadNumericTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    MaritalStatus:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropMari" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium">
                                        <asp:ListItem Text="Married" Value="Married" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="Single" Value="Single"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    DOB:
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="radDateDOB" runat="server" Width="190px" Font-Names="Arial"
                                        Font-Size="Small">
                                        <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                        </Calendar>
                                        <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                            LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        </DateInput>
                                      <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Address:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAddress" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    PersonalEMail:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalEmail" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    PersonalTaxNo:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtPersonalTaxNo" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Manager's (N+1):
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropN1" DataTextField="Fullname" DataValueField="Username"
                                        Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Need approve by RSM (N+3):
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chkN3" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    Vendor code (SAP):
                                    <asp:TextBox ID="txtVendorSAP" runat="server" Width="60px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Senior Manager:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropSenior" DataTextField="fullname" DataValueField="email"
                                        Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Director:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropDirector" DataTextField="fullname" DataValueField="email"
                                        Width="290px" runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    VP:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropVP" DataTextField="fullname" DataValueField="email" Width="290px"
                                        runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    COO:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropCOO" DataTextField="fullname" DataValueField="email" Width="290px"
                                        runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Area(For Sales Only):
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropArea" DataTextField="Area" DataValueField="ID" Width="290px"
                                        runat="server" Font-Names="Arial" Font-Size="Medium">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btSave" runat="server" Text="Save" OnClick="btSave_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td valign="top">
                    <div class="divRight">
                        <table>
                            <tr>
                                <td style="width: 20%;">
                                    StartingDate:
                                </td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="radDateStart" runat="server" Width="190px" Font-Names="Arial"
                                        Font-Size="Small">
                                        <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            EnableWeekends="True" FastNavigationNextText="&amp;lt;&amp;lt;">
                                        </Calendar>
                                        <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd-MMM-yy" DateFormat="dd-MMM-yy"
                                            LabelWidth="40%">
                                            <EmptyMessageStyle Resize="None"></EmptyMessageStyle>
                                        </DateInput>
                                      <%--  <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>--%>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    StatusEmployee:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropStatusEmployee" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium">
                                        <asp:ListItem Text="Chính thức/Permanent" Value="Permanent" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="Thời vụ/Temp" Value="Temp"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Licence:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropLicence" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium">
                                        <asp:ListItem Text="E1-Nhân viên văn phòng" Value="E1" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="EOK-Sales Sup and Temp" Value="EOK"> </asp:ListItem>
                                        <asp:ListItem Text="NMB-Thực tập và công nhân" Value="NoMailBook"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    BaseTown:
                                </td>
                                <td align="left">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="droBasetown" DataTextField="TenTP" DataValueField="MaTP" AutoPostBack="false"
                                                    Font-Names="Arial" Font-Size="Medium" Width="120px" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width: 20%;">
                                                Addition:
                                            </td>
                                            <td align="left">
                                                <telerik:RadComboBox RenderMode="Lightweight" DataTextField="TenTP" DataValueField="MaTP"
                                                    ID="radcomboAddition" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                                    Label="" Width="100px" Font-Names="Arial" Font-Size="Medium" DropDownWidth="150px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    ReplaceWho:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtReplaceWho" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    GroupMail:
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox RenderMode="Lightweight" DataTextField="DisplayName" DataValueField="Email"
                                        ID="radcomboGroupEmail" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                        Label="" Width="290px" Font-Names="Arial" Font-Size="Medium" DropDownWidth="290px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    GroupPermision:
                                </td>
                                <td align="left">
                                    <telerik:RadComboBox RenderMode="Lightweight" DataTextField="Name" DataValueField="Name"
                                        ID="radcomboPermision" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                                        Label="" Width="290px" Font-Names="Arial" Font-Size="Medium" DropDownWidth="290px">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    OU_ICPVIETNAM:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOuICP" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    ComputerType:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="dropComputertype" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium">
                                        <asp:ListItem Text="Laptop" Value="LAPTOP" Selected="True"> </asp:ListItem>
                                        <asp:ListItem Text="Desktop" Value="DESKTOP"> </asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <asp:CheckBox ID="chkTelephone" Text="Telephone" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkEmployeeCard" Text="EmployeeCard" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkOfficeEntryCard" Text="OfficeEntryCard" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkParking" Text="Parking" runat="server" Font-Names="Arial" Font-Size="Medium" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">
                                    <table>
                                        <tr>
                                            <td align="left">
                                                <asp:CheckBox ID="chkTelephonelist" Text="Telephonelist" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkCompanyGift" Text="CompanyGift" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkStationary" Text="Stationary" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                            <td align="left">
                                                <asp:CheckBox ID="chkNamecard" Text="Namecard" runat="server" Font-Names="Arial"
                                                    Font-Size="Medium" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    BankAccountNumber:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankAccNum" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    BankName:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankName" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    BankCity:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankCity" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    AccountHolerName:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtAccountHolder" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    BankCode:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtBankCode" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    SwiftCode:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtSwiftcode" runat="server" Width="290px" Font-Names="Arial" Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Emergency Name:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmergencyContract" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;">
                                    Emergency Number:
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtEmergencyContractNum" runat="server" Width="290px" Font-Names="Arial"
                                        Font-Size="Medium"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</fieldset>
