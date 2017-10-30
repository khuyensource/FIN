<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransferBudget.aspx.cs" Inherits="MaricoPay.TransferBudget" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/uc/ucMsgBox.ascx" TagName="uscMsgBox" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
  
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }

        .RadComboBox_Default
        {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadComboBox
        {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
        }

        .RadComboBox_Default
        {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadComboBox
        {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
        }

        .RadComboBox_Default
        {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadComboBox
        {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
        }

        .RadComboBox_Default
        {
            color: #333;
            font: normal 12px/16px "Segoe UI",Arial,Helvetica,sans-serif;
        }

        .RadComboBox
        {
            margin: 0;
            padding: 0;
            *zoom: 1;
            display: -moz-inline-stack;
            display: inline-block;
            *display: inline;
            text-align: left;
            vertical-align: middle;
            _vertical-align: top;
            white-space: nowrap;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInputCellLeft
        {
            background-position: 0 -88px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInputCellLeft
        {
            background-position: 0 -88px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInputCellLeft
        {
            background-position: 0 -88px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInputCellLeft
        {
            background-position: 0 -88px;
        }

        .RadComboBox_Default .rcbInputCellLeft
        {
            background-position: 0 0;
        }

        .RadComboBox_Default .rcbInputCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbInputCell
        {
            width: 100%;
            height: 20px;
            _height: 22px;
            line-height: 20px;
            _line-height: 22px;
            text-align: left;
            vertical-align: middle;
        }

        .RadComboBox .rcbInputCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbInputCellLeft
        {
            background-position: 0 0;
        }

        .RadComboBox_Default .rcbInputCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbInputCell
        {
            width: 100%;
            height: 20px;
            _height: 22px;
            line-height: 20px;
            _line-height: 22px;
            text-align: left;
            vertical-align: middle;
        }

        .RadComboBox .rcbInputCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbInputCellLeft
        {
            background-position: 0 0;
        }

        .RadComboBox_Default .rcbInputCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbInputCell
        {
            width: 100%;
            height: 20px;
            _height: 22px;
            line-height: 20px;
            _line-height: 22px;
            text-align: left;
            vertical-align: middle;
        }

        .RadComboBox .rcbInputCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbInputCellLeft
        {
            background-position: 0 0;
        }

        .RadComboBox_Default .rcbInputCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbInputCell
        {
            width: 100%;
            height: 20px;
            _height: 22px;
            line-height: 20px;
            _line-height: 22px;
            text-align: left;
            vertical-align: middle;
        }

        .RadComboBox .rcbInputCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput
        {
            color: #333;
        }

        .RadComboBox .rcbReadOnly .rcbInput
        {
            cursor: default;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput
        {
            color: #333;
        }

        .RadComboBox .rcbReadOnly .rcbInput
        {
            cursor: default;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput
        {
            color: #333;
        }

        .RadComboBox .rcbReadOnly .rcbInput
        {
            cursor: default;
        }

        .RadComboBox_Default .rcbReadOnly .rcbInput
        {
            color: #333;
        }

        .RadComboBox .rcbReadOnly .rcbInput
        {
            cursor: default;
        }

        .RadComboBox_Default .rcbInput
        {
            color: #333;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .RadComboBox .rcbInput
        {
            margin: 0;
            padding: 0;
            border: 0;
            background: 0;
            padding: 2px 0 1px;
            _padding: 2px 0 0;
            width: 100%;
            _height: 18px;
            outline: 0;
            -webkit-appearance: none;
        }

        .RadComboBox_Default .rcbInput
        {
            color: #333;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .RadComboBox .rcbInput
        {
            margin: 0;
            padding: 0;
            border: 0;
            background: 0;
            padding: 2px 0 1px;
            _padding: 2px 0 0;
            width: 100%;
            _height: 18px;
            outline: 0;
            -webkit-appearance: none;
        }

        .RadComboBox_Default .rcbInput
        {
            color: #333;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .RadComboBox .rcbInput
        {
            margin: 0;
            padding: 0;
            border: 0;
            background: 0;
            padding: 2px 0 1px;
            _padding: 2px 0 0;
            width: 100%;
            _height: 18px;
            outline: 0;
            -webkit-appearance: none;
        }

        .RadComboBox_Default .rcbInput
        {
            color: #333;
            font: normal 12px "Segoe UI",Arial,Helvetica,sans-serif;
            line-height: 16px;
        }

        .RadComboBox .rcbInput
        {
            margin: 0;
            padding: 0;
            border: 0;
            background: 0;
            padding: 2px 0 1px;
            _padding: 2px 0 0;
            width: 100%;
            _height: 18px;
            outline: 0;
            -webkit-appearance: none;
        }

        .RadComboBox input
        {
            height: auto;
            box-shadow: none;
            outline: 0;
        }

        .RadComboBox input
        {
            height: auto;
            box-shadow: none;
            outline: 0;
        }

        .RadComboBox input
        {
            height: auto;
            box-shadow: none;
            outline: 0;
        }

        .RadComboBox input
        {
            height: auto;
            box-shadow: none;
            outline: 0;
        }



        input
        {
            font-family: verdana, arial, helvetica, sans-serif;
            font-size: 10px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbArrowCellRight
        {
            background-position: -162px -176px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbArrowCellRight
        {
            background-position: -162px -176px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbArrowCellRight
        {
            background-position: -162px -176px;
        }

        .RadComboBox_Default .rcbReadOnly .rcbArrowCellRight
        {
            background-position: -162px -176px;
        }

        .RadComboBox_Default .rcbArrowCellRight
        {
            background-position: -18px -176px;
        }

        .RadComboBox_Default .rcbArrowCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbArrowCell
        {
            width: 18px;
        }

        .RadComboBox .rcbArrowCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbArrowCellRight
        {
            background-position: -18px -176px;
        }

        .RadComboBox_Default .rcbArrowCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbArrowCell
        {
            width: 18px;
        }

        .RadComboBox .rcbArrowCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbArrowCellRight
        {
            background-position: -18px -176px;
        }

        .RadComboBox_Default .rcbArrowCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbArrowCell
        {
            width: 18px;
        }

        .RadComboBox .rcbArrowCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadComboBox_Default .rcbArrowCellRight
        {
            background-position: -18px -176px;
        }

        .RadComboBox_Default .rcbArrowCell
        {
            background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png');
            _background-image: url('mvwres://Telerik.Web.UI, Version=2014.2.724.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSpriteIE6.png');
        }

        .RadComboBox .rcbArrowCell
        {
            width: 18px;
        }

        .RadComboBox .rcbArrowCell
        {
            margin: 0;
            padding: 0;
            background-color: transparent;
            background-repeat: no-repeat;
            *zoom: 1;
        }

        .RadInput_Default
        {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput
        {
            vertical-align: middle;
        }

        .RadInput_Default
        {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput
        {
            vertical-align: middle;
        }

        .RadInput_Default
        {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput
        {
            vertical-align: middle;
        }
    </style>

      <script language="JavaScript" type="text/javascript">

          var sec = 0;
       

          var sec = 0;
          function closewindow() {
              sec++;
              if (sec == 10) {
                  window.parent.close();
              }
              window.setTimeout("closewindow();", 1000);

          }


          function GetRadWindow() {
              var oWindow = null;
              if (window.radWindow) oWindow = window.radWindow;
              else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
              return oWindow;
          }

          function CloseDialog() {
              GetRadWindow().close();
              return false;
          }



    </script>
   <script language="javascript">
       function CloseWindow11() {
           window.open('', '_self', '');
           window.close();
       }
    </script>
</head>
<body>
  
    <form id="form1" runat="server">
       
            <asp:ScriptManager ID="ScriptManager1"  runat="server" ></asp:ScriptManager> 
          <uc4:uscMsgBox ID="MsgBox1" runat="server" />
    <div>
      <br />
        <table class="auto-style1" >
             <tr>
                <td>Qúy/Quater </td>
                <td>

            <telerik:RadComboBox ID="rdquater" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdquater_SelectedIndexChanged"   >
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Q1" Value="1" />
                    <telerik:RadComboBoxItem runat="server" Text="Q2" Value="2" />
                    <telerik:RadComboBoxItem runat="server" Text="Q3" Value="3" />
                    <telerik:RadComboBoxItem runat="server" Text="Q4" Value="4" />
                </Items>
                    </telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
              
            </tr>

            <tr>
                <td>Quốc gia/Country </td>
                <td>

            <telerik:RadComboBox ID="Rdcbocountry_From" runat="server" DataTextField="Country" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="Rdcbocountry_From_SelectedIndexChanged" ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
                <td>Quốc gia/Country select</td>
                <td>

            <telerik:RadComboBox ID="Rdcbocountry_To" runat="server" DataTextField="Country" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="Rdcbocountry_To_SelectedIndexChanged" ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Phòng ban/Department </td>
                <td>

            <telerik:RadComboBox ID="RdDepartment_from" runat="server" DataTextField="budgetOwner" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="RdDepartment_from_SelectedIndexChanged" ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
                <td>Phòng ban/Department </td>
                <td>

            <telerik:RadComboBox ID="RdDepartment_To" runat="server" DataTextField="budgetOwner" DataValueField="ID" AutoPostBack="True"  ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Ngành hàng/Bussiness Unit</td>
                <td> <telerik:RadComboBox ID="rdcboBU_From" runat="server" DataTextField="BU" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="rdcboBrand_From_SelectedIndexChanged" ></telerik:RadComboBox></td>
                <td>&nbsp;</td>
                <td>Ngành hàng/Bussiness Unit</td>
                <td><telerik:RadComboBox ID="rdcboBU_To" runat="server" DataTextField="BU" DataValueField="ID"  AutoPostBack="True" OnSelectedIndexChanged="rdcboBrand_To_SelectedIndexChanged" >  </telerik:RadComboBox> </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Nhãn hàng/Brand</td>
                <td><telerik:RadComboBox ID="RdBrand_From" runat="server" DataTextField="Brand" DataValueField="ID"  AutoPostBack="True" OnSelectedIndexChanged="RdBrand_From_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
                <td>Nhãn hàng/Brand</td>
                <td><telerik:RadComboBox ID="RdBrand_To" runat="server" DataTextField="Brand" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="RdBrand_To_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Chủng loại/Category</td>
                <td><telerik:RadComboBox ID="RdCat_From" runat="server" DataTextField="Category" DataValueField="ID"  AutoPostBack="True" OnSelectedIndexChanged="RdCat_From_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text=">>>>" OnClick="Button1_Click1" Font-Bold="True" Height="30px" />
                </td>
                <td>Chủng loại/Category</td>
                <td><telerik:RadComboBox ID="RdCat_To" runat="server" DataTextField="Category" DataValueField="ID"  AutoPostBack="True" OnSelectedIndexChanged="RdCat_To_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Hoạt động/Activity Group</td>
                <td><telerik:RadComboBox ID="RdActivityGroup_From" runat="server" DataTextField="ActivityGroup" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="RdActivityGroup_From_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Close" OnClick="Button2_Click1" Font-Bold="True" Height="31px" Width="54px" />
                </td>
                <td>Hoạt động/Activity Group</td>
                <td><telerik:RadComboBox ID="RdActivityGroup_To" runat="server" DataTextField="ActivityGroup" DataValueField="ID" AutoPostBack="True" OnSelectedIndexChanged="RdActivityGroup_To_SelectedIndexChanged"  ></telerik:RadComboBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Ngân sách còn lại/Budget available</td>
                <td>
                    <telerik:RadNumericTextBox ID="radnumtxtAivailable_From" Value="0" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" runat="server"  >
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;</td>
                <td>Ngân sách còn lại/Budget available</td>
                <td>
                    <telerik:RadNumericTextBox ID="radnumtxtAivailable_To" Value="0" ReadOnly="true" AutoPostBack="true" NumberFormat-DecimalSeparator="," Type="Number" runat="server"  >
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>Số tiền cần chuyển/Amount to be transferred</td>
                <td>
                    <telerik:RadNumericTextBox ID="radnumtxtASPFvalue_From" Value="0" NumberFormat-DecimalSeparator="," Type="Number" runat="server"   >
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;</td>
                <td>Số tiền sẽ nhận/Amount will get</td>
                <td>
                    <telerik:RadNumericTextBox ID="radnumtxtASPFvalue_To" Value="0"  NumberFormat-DecimalSeparator="," Type="Number" runat="server"  >
                        <NumberFormat DecimalDigits="0" />
                    </telerik:RadNumericTextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
           
        </table>
    </div>
    </form>
</body>
</html>
