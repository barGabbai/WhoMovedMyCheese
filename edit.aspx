<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="_Default2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="style/StyleSheet2.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script type="text/javascript">

        function browseImage() {
            document.getElementById('imageUpload').click();
        }

        function readURL(FileUpload) {

            if (FileUpload.files && FileUpload.files[0]) {

                var reader = new FileReader();

                reader.onload = function (e) {

                    var image = new Image();
                    image.src = e.target.result;
                    var mywidth = 0;
                    var myheight = 0;

                    image.onload = function () {
                        console.log(this.width);
                        if (this.width * (80 / 100) > this.height) {
                            mywidth = 100;
                            var smaller = 100 / this.width;
                            var newH = smaller * this.height;
                            myheight = newH;
                        }
                        else if (this.width * (80 / 100) < this.height) {
                            myheight = 80;
                            var newsize = 80 / this.height;
                            var newW = newsize * this.width;
                            mywidth = newW;
                        }
                        else {
                            mywidth = 100;
                            myheight = 80;
                        }

                        $('#thumb').css("display", "block");
                        $('#thumb').css("margin", "auto");
                        $('#thumb').css("position", "fixed");
                        $('#thumb').css("border-radius", "14px")
                        $('#thumb').attr('src', this.src);
                        $('#thumb').width(mywidth);
                        $('#thumb').height(myheight);


                    }

                }
                reader.readAsDataURL(FileUpload.files[0]);

                $('#x').css("display", "block");
                $('#x').css("margin-right", "52px");
                $('#x').css("margin-top", "70px");
                $('#x').css("position", "relative");
                var xBtn = new Image();
                xBtn.src = "/images/bin.png";
                $('#x').attr('src', xBtn.src);
                $('#uploadBtn').css("display", "none");


            }

        }

        function restoreUpload() {


            $('input[type="file"]').val(null);
            $('#uploadBtn').css("display", "block");
            $('#x').css("display", "none");
            $('#thumb').css("display", "none");
            $('#thumb').attr('src', "");


        }

 $(document).ready(function () {//בטעינה

            //$("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
            //$("#saveBack").attr("disabled", true);



            $("#distractorsTxt0").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

            $("#distractorsTxt1").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

            $("#distractorsTxt2").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

            $("#distractorsTxt3").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

            $("#qTxt").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

            $("#TextBox1").keyup(function () {//בעת לחיצה על כפתור במסיח הראשון תבדוק אם...
                if (($("#qTxt").val().length > 0) && ($("#distractorsTxt0").val().length > 0) && ($("#TextBox1").val().length > 2)) {//תיבת הזנת השאלה אינה ריקה, מסיח ראשון אינו ריק ושם המשחק אינו ריק
                    if (($("#distractorsTxt1").val().length > 0) || ($("#distractorsTxt2").val().length > 0) || ($("#distractorsTxt3").val().length > 0)) {
                        //אם התנאי נכון, אז תבדוק אם מסיח 2, 3 או 4 לא ריקים. אם התנאי נכון, מפעיל את כפתורי השמירה
                        $("#saveContinue").attr("disabled", false);
                        $("#saveBack").attr("disabled", false);
                    }
                    else {//אחרת, כפתורי שמירה לא פעילים
                        $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                        $("#saveBack").attr("disabled", true);
                    }
                }
                else {
                    $("#saveContinue").attr("disabled", true);//כפתורי השמירה לא פעילים
                    $("#saveBack").attr("disabled", true);
                }
            })

        });
    </script>
</head>
<body>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div id="timeNew">
            <h1>עריכת משחק
            </h1>
            <asp:Label ID="nameQ" runat="server">שם המשחק:</asp:Label>
            <asp:TextBox ID="TextBox1" runat="server" MaxLength="25"></asp:TextBox><asp:Label runat="server" ID="limitations">
                    בין 3 ל-25 תווים</asp:Label>
            <asp:Label ID="timePerQLbl" runat="server">זמן לשאלה:</asp:Label>
            <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Selected="True" Value="21">20 שניות</asp:ListItem>
                <asp:ListItem Value="31">30 שניות</asp:ListItem>
                <asp:ListItem Value="41">40 שניות</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="newQ" runat="server" Text="צור שאלה חדשה" OnClick="newQ_Click" />
            



        </div>

        <div id="wrap">

            <asp:Panel ID="Panel2" runat="server" Style="margin-top: 0px">
                <div id="qTbl">
                    <div id="qCounterLimitations">
                        <p id="q">שאלות</p>
                        <p id="limitationsQ">יש להזין בין 5 ל-30 שאלות</p>
                        <asp:Label ID="qNum" runat="server">מספר שאלות:</asp:Label>
                        <asp:Label ID="qCounter" runat="server"></asp:Label>
                    </div>
                    <div id="gridWrap">
                        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="~/trees/XMLFile.xml" XPath="catalog/game/mission"></asp:XmlDataSource>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="XmlDataSource1" Width="320px" OnRowCommand="GridView1_RowCommand1" BorderStyle="None" GridLines="None" ShowHeader="False">
                            <Columns>
                                <asp:TemplateField HeaderText="מחיקה">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="deleteImageButton" runat="server" CommandName="deleteRow" ImageUrl="~/images/delete.png" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="עריכה" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="editImageButton" theItemId='<%#XPathBinder.Eval(Container.DataItem,"@id")%>' runat="server" ImageUrl="~/images/edit.png" CommandName="edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="שאלות">
                                    <ItemTemplate>
                                        <asp:Label ID="questionLbl" runat="server" Width="270px" Text='<%#Server.UrlDecode(XPathBinder.Eval(Container.DataItem, "question").ToString())%>'> 				</asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle Wrap="False" />
                            <FooterStyle BackColor="#fccb98" Font-Bold="True" ForeColor="Black" />
                            <HeaderStyle BackColor="#fccb98" Font-Bold="True" ForeColor="Black" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#E6E6E6" ForeColor="black" BorderStyle="None" CssClass=".rowStyleGrid" Wrap="False" />
                            <SelectedRowStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Black" Wrap="False" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>

                    </div>
                </div>
            </asp:Panel>
        </div>

        <div id="wrap2">
            <asp:Panel ID="Panel4" runat="server" Style="display: none;">
                <div id="limitLblDiv">
                    <asp:Label ID="limitLbl" runat="server">הגעת למספר השאלות המירבי</asp:Label>
                </div>
                <div id="panel4btns">
                    <asp:Button ID="limitPublishBtn" runat="server" Text="פרסם משחק" OnClick="publishLimit" />
                    <asp:Button ID="limitBackBtn" runat="server" Text="חזור לעמוד הראשי" OnClick="returnFunc" />
                </div>
            </asp:Panel>


            <asp:Panel ID="Panel5" runat="server">
                <%--<asp:Button ID="newQ" runat="server" Text="צור שאלה חדשה" OnClick="newQ_Click" />--%>
                <div id="questionDiv">

                    <asp:Label ID="typeQLbl" runat="server">הקלד שאלה <span id="limitations3">עד 150 תווים</span></asp:Label>
                    <span id="picSpan">הוסף תמונה</span>
                </div>

                <div style="display: inline;">
                    <asp:TextBox ID="qTxt" runat="server" Height="85px" TextMode="MultiLine" Width="268px"></asp:TextBox>
                </div>
                <div id="picUploadDiv" style="display: inline-block; float: left; text-align: center; margin-left: 104px; margin-top: 30px;">
                    <input type="button" id="uploadBtn" runat="server" onclick="browseImage()" value="+" />
                    <asp:Image ID="thumb" runat="server" />
                    <asp:Image ID="x" runat="server" onClick="restoreUpload()" />

                </div>



                <input type="file" onchange="readURL(this);" name="fileUpload" id="imageUpload" runat="server" style="display: none;" accept=".png,.jpeg,.jpg,.gif" />

            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" Width="100%">








                <div id="distDiv">
                    <%-- <asp:Label ID="rightDistLbl" runat="server" Text="תשובה נכונה<br/>(אחת בלבד)"></asp:Label>--%>
                    <asp:Label ID="typeD" runat="server">הזנת תשובות      <span id="limitations4">לפחות 2 תשובות, עד 50 תווים לתשובה</span></asp:Label>
                    <br />
                    <asp:Label ID="rightDistLbl" runat="server">תשובה נכונה:</asp:Label>
                </div>
                <%--  <div id="radios">

  <asp:RadioButtonList ID="distRadios" runat="server"></asp:RadioButtonList>


                </div>--%>
            </asp:Panel>
            <div id="bottomBtns">
                <asp:Button ID="saveContinue" runat="server" OnClick="saveQ" Enabled="false" Text="שמור והמשך" />
                <asp:Button ID="saveBack" runat="server" OnClick="saveQ" Enabled="false" Text="שמור וחזור" />
                <asp:Button ID="back" runat="server" OnClick="returnFunc" Text="חזור ללא שמירה" />
            </div>
        </div>

        <asp:HiddenField ID="hfHidden" runat="server" />

        <cc1:ModalPopupExtender ID="pnlModal_ModalPopupExtender" runat="server" BehaviorID="pnlModal_ModalPopupExtender" DynamicServicePath="" TargetControlID="hfHidden" PopupControlID="pnlModal" CancelControlID="btnClose" BackgroundCssClass="modalBg">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlModal" runat="server" CssClass="modalPopup">
            האם ברצונך למחוק את השאלה:
        <p>
            <asp:Label runat="server" ID="questionNameLbl"></asp:Label>
        </p>
            <div>
                <asp:Button ID="btnOk" runat="server" Text="אישור" OnClick="confirmDelete" /><asp:Button ID="btnClose" runat="server" Text="סגור" />
            </div>
            <br />

        </asp:Panel>

    </form>
</body>
</html>
