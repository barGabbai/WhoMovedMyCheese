<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet3.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript">
  
        $(document).ready(function () {
            
            if(($("#TextBox1").val().length>0) && ($("#TextBox2").val().length>0)){
                $("#submitBtn").attr("disabled", false);
            }

            $("#TextBox2").keyup(function () {
                $("#Fb").hide();
                

                if ($("#TextBox2").val() != "" && $("#TextBox1").val() != "") {
                    // if ($("#distRadios_0").attr("checked") == true || $("#distRadios_1").attr("checked") == true || $("#distRadios_2").attr("checked") == true || $("#distRadios_3").attr("checked") == true) {
                    $("#submitBtn").attr("disabled", false);
                }
                else {
                    $("#submitBtn").attr("disabled", true);
                }

                $("#TextBox1").keyup(function () {
                    $("#Fb").hide();
                   

                    if ($("#TextBox2").val() != "" && $("#TextBox1").val() != "") {
                        // if ($("#distRadios_0").attr("checked") == true || $("#distRadios_1").attr("checked") == true || $("#distRadios_2").attr("checked") == true || $("#distRadios_3").attr("checked") == true) {
                        $("#submitBtn").attr("disabled", false);
                    
                    
                    }
                    else {
                        $("#submitBtn").attr("disabled", true);
                    }
            

                });

               
            })})</script>
</head>
<body>
    <form id="form1" runat="server">
   
        <asp:Panel ID="Panel1" runat="server">
 <div id="box">
     <div id="usernameDiv">
     <asp:Label ID="Label1" runat="server" Text="שם משתמש:"></asp:Label><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
     </div>
         <div id="passwordDiv">
     <asp:Label ID="Label2" runat="server" Text="סיסמא:"></asp:Label><asp:TextBox ID="TextBox2" runat="server" TextMode="Password"></asp:TextBox>
            
 </div> 
     <div>
 <asp:Label ID="Fb" runat="server" Visible="false" Style="font-size:10pt; color:#b70101; margin-right:84px;">שם משתמש/סיסמא לא נכונים</asp:Label>
            </div>
 </div>
           
            <div id="enter">
               
                <asp:Button ID="submitBtn" runat="server" Text="אישור" Enabled="false" onClick="enterPlatform"/>
           
                 </div>
        </asp:Panel>
    
    </form>
</body>
</html>
