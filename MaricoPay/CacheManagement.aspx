<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CacheManagement.aspx.cs" Inherits="MaricoPay.CacheManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">di
    <div>

        <table>
            <tr>
                <td>
                    <asp:Button ID="btDepartment" runat="server" Text="Clear Cache Department" OnClick="btDepartment_Click" />'

                </td>
                 <td>
                    <asp:Button ID="btTinhThanh" runat="server" Text="Clear Cache Tinh Thanh" OnClick="btTinhThanh_Click" />'

                </td>
                 <td>
                    <asp:Button ID="btQuanHuyen" runat="server" Text="Clear Cache Quan Huyen" OnClick="btQuanHuyen_Click" />'

                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btCharge" runat="server" Text="Clear Cache Charge" OnClick="btCharge_Click"  />'

                </td>
                 <td>
                  <asp:Button ID="btCategory" runat="server" Text="Clear Cache Category (Claim office)" OnClick="btCategory_Click"   />
                </td>
                 <td>
                  <asp:Button ID="btIOcontract" runat="server" Text="Clear Cache IO contract" OnClick="btIOcontract_Click"  />'
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Button ID="btEmailLG_FC" runat="server" Text="Clear Cache Email LG_FC" OnClick="btEmailLG_FC_Click"/>'

                </td>
                 <td>
                   <asp:Button ID="btOrgType" runat="server" Text="Clear Cache Contract Org & Type" OnClick="btOrgType_Click" />
                </td>
                 <td>
                  
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
