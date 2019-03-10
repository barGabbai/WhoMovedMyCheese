<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet.css" rel="stylesheet" />
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>

        $(document).ready(function () {
            $("#Button1").attr("disabled", true);

            $("#gameName").keyup(function () {
                if ($("#gameName").val().length > 2) {
                    $("#Button1").attr("disabled", false);
                } else {
                    $("#Button1").attr("disabled", true);

                }
            }
            )
        })

    </script>
    
</head>
<body>

    <form id="form1" runat="server">

    <div style="direction: rtl">

        <asp:Panel ID="Panel1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" />
    <h1>המשחקים שלי</h1>
        <h2>שם המשחק&nbsp;&nbsp;&nbsp; <span id="limitations">   בין 3 ל-25 תווים</span></h2>
        <asp:TextBox ID="gameName" runat="server" Width="233px" EnableTheming="False" MaxLength="25"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="צור משחק" OnClick="Button1_Click" Style="border-radius:14px; border:none; font-size:11pt; font-weight:bold; height:25px;" />
        <br />
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml" XPath="/catalog/game" OnTransforming="XmlDataSource1_Transforming"></asp:XmlDataSource>
        <br />
            <div id="gridDiv">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GridView1_RowCommand1" CssClass="mainGrid" BorderStyle="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="שם המשחק">
                    <ItemTemplate>
	                <asp:Label ID="nameLbl" runat="server" CssClass="iconsGrid" Width="270px" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "title").ToString())%>'> 	</asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="קוד משחק">
                    <ItemTemplate>
	              <asp:Label ID="codeLbl" Width="80px" CssClass="iconsGrid" Text='<%#XPathBinder.Eval(Container.DataItem,"@code")%>' runat="server" />
                    </ItemTemplate>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="מספר שאלות">
                    <ItemTemplate>
	                <asp:Label ID="numOfQLbl" runat="server" CssClass="iconsGrid" Width="50px" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "qCounter").ToString())%>'> 	</asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="עריכה">
	            <ItemTemplate>
		            <asp:ImageButton ID="editImageButton"  theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/edit.png" CommandName="edit" />
	            </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="מחיקה">
	<ItemTemplate>
		<asp:ImageButton ID="deleteImageButton"  theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/bin.png" CommandName="deleteRow" />
	</ItemTemplate>
</asp:TemplateField>


                <asp:TemplateField HeaderText="פרסם">
                    <ItemTemplate>
    <%--<asp:ImageButton ID="ImageButton1" imageUrl='<%#XPathBinder.Eval(Container.DataItem,"@publishedUrl")%>' runat="server" CommandName="publish" /> --%>
		   <asp:CheckBox ID="publishCbx" runat="server"  Enabled="false" OnCheckedChanged="gridChange"  AutoPostBack="true" />
	</ItemTemplate>
                </asp:TemplateField>


               </Columns>
            <FooterStyle BackColor="#fccb98" Font-Bold="True" ForeColor="Black" />
            <HeaderStyle BackColor="#fccb98" Font-Bold="True" ForeColor="Black" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#E6E6E6" ForeColor="black" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" CssClass=".rowStyleGrid" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Black" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView>

            </div>
        <br />
        <br />
           </asp:Panel>

    </div> 
        <asp:HiddenField ID="hfHidden" runat="server" />

          <cc1:ModalPopupExtender ID="pnlModal_ModalPopupExtender" runat="server" BehaviorID="pnlModal_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfHidden" PopupControlID="pnlModal" CancelControlID="btnClose" BackgroundCssClass="modalBg">
        </cc1:ModalPopupExtender>
                   
    <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup" >
        האם ברצונך למחוק את המשחק:
        <p>
        <asp:Label runat="server" ID="gameNameLbl"></asp:Label>
        </p>
        <div>
     <asp:Button ID="btnOk" runat="server" Text="אישור" onClick="confirmDelete"/><asp:Button ID="btnClose" runat="server" Text="סגור" />
   </div>
        <br />
        
    </asp:Panel>    

 
    </form>
</body>
</html>
