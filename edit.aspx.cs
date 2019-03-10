using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;


public partial class _Default2 : System.Web.UI.Page
{
    string gameID;
    string theId;
    string imagesLibPath = "photos/";

    protected void page_init(object sender, EventArgs e)
    {
        gameID = (string)Session["theItemIdSessions"];

        XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
        XmlDataSource1.XPath = "/catalog/game[@id='" + gameID + "']/mission";

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
        gameID = (string)Session["theItemIdSessions"];

        XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
        XmlNodeList missionNodes = myGameXml.SelectNodes("catalog/game[@id = '" + gameID + "']/mission");
        XmlNodeList questionNodes = myGameXml.SelectNodes("catalog/game[@id = '" + gameID + "']/mission/question");

        saveBack.Enabled = false;
        saveContinue.Enabled = false;

        uploadBtn.Attributes["style"] += "display:block;";

        XmlNodeList missionCounter = myGameXml.SelectNodes("catalog/game[@id = '" + gameID + "']/mission");
        qCounter.Text = missionCounter.Count.ToString();
        Session["missionCounter"] = missionCounter.Count.ToString();

        XmlNode myQCounter = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']/qCounter");
        myQCounter.InnerXml = missionCounter.Count.ToString();


        XmlNode questionHead = myGame.SelectSingleNode("title");
        TextBox1.Text = questionHead.InnerXml;

        for (int i = 0; i < 4; i++)
        {
            TextBox distractorsTxt = new TextBox();
            distractorsTxt.ID = "distractorsTxt" + i.ToString();
            distractorsTxt.Attributes.Add("maxlength", "50");
            Panel3.Controls.Add(distractorsTxt);
        }

        Panel3.DataBind();


        if (Page.IsPostBack != true)
        {
            Session["action"] = "createQuestion";
            DropDownList1.SelectedValue = Session["selectedTime"].ToString();
            qTxt.Attributes.Add("maxlength", "150");

            for (int i = 0; i < 2; i++)
            {
                ((TextBox)FindControl("distractorsTxt" + i.ToString())).Enabled = true;
            }

            if(myGame.SelectSingleNode("qCounter").InnerXml == "30")
            {
                Panel3.Attributes["style"] += "display:none";
                Panel5.Attributes["style"] += "display:none";
                saveContinue.Attributes["style"] += "display:none";
                saveBack.Attributes["style"] += "display:none";
                back.Attributes["style"] += "display:none";
                Panel4.Attributes["style"] += "display:block";
                newQ.Enabled = false;

                if (myGame.Attributes["published"].InnerXml == "true")
                {
                    limitPublishBtn.Text = "בטל פרסום משחק";
                }
                else
                {
                    limitPublishBtn.Text = "פרסם משחק";
                }
            }
            else
            {
                Panel3.Attributes["style"] += "display:normal";
                Panel5.Attributes["style"] += "display:normal";
                Panel4.Attributes["style"] += "display:none";
                newQ.Enabled = true;
            }
        }

        else if (Page.IsPostBack != false)
        {
            
        }
    }


    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;

        XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
        XmlNodeList missionNodes = myGameXml.SelectNodes("catalog/game[@id = '" + gameID + "']/mission");

        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdOnEdit"] = i.Attributes["theItemId"];

        // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור
        switch (e.CommandName)
        {

            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה
            case "deleteRow":

                questionNameLbl.Text = myGameXml.SelectSingleNode("//game[@id='" + gameID + "']/mission[@id='" + theId + "']/question").InnerXml + "<span id='qMark' runat='server'>?</span>";
                Session["theItemIdOnEdit"] = i.Attributes["theItemId"];
                pnlModal_ModalPopupExtender.Show();
                if (questionNameLbl.Text.Length > 50)
                {
                    questionNameLbl.Text = string.Concat(questionNameLbl.Text.Substring(0, 23), "...");
                }

                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה
            case "edit":

                XmlNode thisMission = myGameXml.SelectSingleNode("//game[@id ='" + gameID + "']/mission[@id='" + theId + "']");
                XmlNode thisMissionDists = myGameXml.SelectSingleNode("//game[@id ='" + gameID + "']/mission[@id='" + theId + "']/distractors");
                XmlNodeList dist = thisMissionDists.SelectNodes("distractor");

                int distCounter = dist.Count;
                qTxt.Text = myGameXml.SelectSingleNode("//game[@id='" + gameID + "']/mission[@id='" + theId + "']/question").InnerXml;


                if((myGame.SelectSingleNode("qCounter").InnerXml == "30"))
                {
                    Panel3.Attributes["style"] += "display:normal";
                    Panel5.Attributes["style"] += "display:normal";
                    Panel4.Attributes["style"] += "display:none";
                    saveContinue.Attributes["style"] += "display:normal";
                    saveBack.Attributes["style"] += "display:normal";
                    back.Attributes["style"] += "display:normal";
                    newQ.Enabled = false;

                 
                }

                else
                {
                    newQ.Enabled = true;
                }

                for (int x = 0; x < 4; x++)
                {
                    ((TextBox)FindControl("distractorsTxt" + x.ToString())).Text = "";
                }

                for (int x = 0; x < distCounter; x++)
                {
                    ((TextBox)FindControl("distractorsTxt" + x.ToString())).Text = dist.Item(x).InnerXml;

                }

                if (distCounter > 1)
                { 
                        saveBack.Enabled = true;
                        saveContinue.Enabled = true;
                }

                if (thisMission.SelectSingleNode("question").Attributes["pic"].Value == "yes")
                {
                    thumb.Attributes["style"] = "display:block; border-radius:14px";
                    thumb.Height = 80;
                    thumb.Width = 90;
                    x.Attributes["style"] = "display:block; margin-right:60px;";
                    x.ImageUrl = "images/bin.png";
                    uploadBtn.Attributes["style"] += "display:none";
                    thumb.ImageUrl = thisMission.SelectSingleNode("question").Attributes["picUrlSmall"].InnerXml;

                }
                 else if(thisMission.SelectSingleNode("question").Attributes["pic"].Value == "no")
                {
                    uploadBtn.Attributes["style"] += "display:block;";
                    x.Attributes["style"] ="display:none";
                    thumb.Attributes["style"] = "display:none";
                }

                Session["thisMission"] = thisMission;
                Session["action"] = "editQuestion";

                break;
        }
    }

    protected void newQ_Click(object sender, EventArgs e)
    {
        qTxt.Text = "";

        for(int i=0; i < 4; i++)
        {
            ((TextBox)FindControl("distractorsTxt" + i.ToString())).Text = "";
        }

        thumb.Attributes["style"] = "display:none;";
        x.Attributes["style"] = "display:none;";
        Session["action"] = "createQuestion";
    }
  

   protected void confirmDelete(object sender, EventArgs e) 
    {
        //הסרת ענף של משחק קיים באמצעות זיהוי האיי דיי שניתן לו על ידי לחיצה עליו מתוך הטבלה
        //שמירה ועדכון לתוך העץ ולגריד ויו
        System.Xml.XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
        System.Xml.XmlNode node = myGameXml.SelectSingleNode("catalog/game[@id = '" + Session["theItemIdSessions"] + "']/mission[@id='" + Session["theItemIdOnEdit"] + "']");
        XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
        XmlNodeList missions = myGameXml.SelectNodes("catalog/game[@id = '" + gameID + "']/mission");

        int qCounter = Convert.ToInt16(myGame.SelectSingleNode("qCounter").InnerXml);
        qCounter--;
        string newQcounter = qCounter.ToString();
        myGame.SelectSingleNode("qCounter").InnerXml = newQcounter;

        if (Convert.ToInt16(myGame.SelectSingleNode("qCounter").InnerXml) < 5)
        {
            myGame.Attributes["published"].InnerXml = "false";
        }

        node.ParentNode.RemoveChild(node);
        XmlDataSource1.Save();
        GridView1.DataBind();
        Response.Redirect("edit.aspx");
    }

    protected void saveQ(object sender, EventArgs e)
    {
        XmlDocument myGameXml = new XmlDocument();
        myGameXml.Load(MapPath("trees/XMLFile.xml"));

        XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
        string file = imageUpload.PostedFile.ContentType;
            
            if (Session["action"].ToString() == "createQuestion")
        {
            XmlNode myMission = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']/mission");

            int myMissionId = Convert.ToInt16(myGame.Attributes["missionCounter"].InnerXml);
            myMissionId++;
            string myNewQuestionId = myMissionId.ToString();
            myGame.Attributes["missionCounter"].InnerXml = myNewQuestionId;

            XmlElement newMission = myGameXml.CreateElement("mission");
            newMission.SetAttribute("id", myNewQuestionId);
            myGame.AppendChild(newMission);

            XmlElement newQuestion = myGameXml.CreateElement("question");
            newQuestion.InnerXml = Server.UrlDecode(qTxt.Text);
            newMission.AppendChild(newQuestion);
            newQuestion.SetAttribute("pic", "no");
            newQuestion.SetAttribute("picUrl", "");
            newQuestion.SetAttribute("picUrlSmall", "");

            


            myGame.Attributes["time"].InnerXml = DropDownList1.SelectedValue;

            XmlElement newDistractorsTag = myGameXml.CreateElement("distractors");
            newMission.AppendChild(newDistractorsTag);

            for (int i = 0; i < 4; i++)
            {
                if (((TextBox)FindControl("distractorsTxt" + i.ToString())).Text != "")
                {
                    XmlElement newDistractor = myGameXml.CreateElement("distractor");
                    newDistractor.InnerXml = Server.UrlDecode(((TextBox)FindControl("distractorsTxt" + i.ToString())).Text);
                    newDistractorsTag.AppendChild(newDistractor);

                    if (i == 0)
                    {
                        newDistractor.SetAttribute("answer", "true");
                    }
                    else
                    {
                        newDistractor.SetAttribute("answer", "false");
                    }
                }
            }

            int qCounter = Convert.ToInt16(myGame.SelectSingleNode("qCounter").InnerXml);
            qCounter++;
            string newQcounter = qCounter.ToString();
            myGame.SelectSingleNode("qCounter").InnerXml = newQcounter;
            Session["gameQCounter"] = newQcounter;

            if (file.Contains("image"))
            {
                string fileType = imageUpload.PostedFile.ContentType;

                //שמירת הנתיב המלא של הקובץ
                string fileName = imageUpload.PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                // חיבור השם החדש עם הסיומת של הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
                string imageNewName = "pic" + myTime + endOfFileName;
                Session["imageNewNameX"] = imageNewName;

                //שמירה של הקובץ לספרייה בשם החדש שלו
                imageUpload.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);

                System.Drawing.Image bitmap;

                using (var bmpTemp = new System.Drawing.Bitmap(Server.MapPath(imagesLibPath) + imageNewName))
                {
                    bitmap = new System.Drawing.Bitmap(bmpTemp);
                }
                bitmap = FixedSize(bitmap, 150, 150);
                bitmap.Save(Server.MapPath(imagesLibPath) + "small_" + imageNewName);

                XmlNode myCurrentMission = myGame.SelectSingleNode("mission[@id = '" + myNewQuestionId + "']");
                myCurrentMission.SelectSingleNode("question").Attributes["pic"].InnerXml = "yes";

                newQuestion.Attributes["picUrlSmall"].InnerXml = imagesLibPath + "small_" + imageNewName;
                newQuestion.Attributes["picUrl"].InnerXml = imagesLibPath + imageNewName;
            }

            // myGame.FirstChild.InnerXml = TextBox1.Text;
            myGameXml.SelectSingleNode("//game[@id='" + gameID + "']/title").InnerXml = TextBox1.Text;

            XmlDataSource1.Save();
            myGameXml.Save(MapPath("trees/XMLFile.xml"));
            GridView1.DataBind();

            if (((Button)sender).ID == "saveContinue")
            {
                Response.Redirect("edit.aspx");
            }

            else
            {
                Response.Redirect("Default.aspx");
            }
        }

        else if (Session["action"].ToString() == "editQuestion")
        {      
            XmlNode thisMission = myGameXml.SelectSingleNode("//game[@id ='" + gameID + "']/mission[@id = '" + Session["theItemIdOnEdit"] + "']");
            XmlNode thisQuestion = myGameXml.SelectSingleNode("//game[@id ='" + gameID + "']/mission[@id = '" + Session["theItemIdOnEdit"] + "']/question");
            XmlNode thisMissionDists = myGameXml.SelectSingleNode("//game[@id ='" + gameID + "']/mission[@id='" + Session["theItemIdOnEdit"] + "']/distractors");
            XmlNodeList dist = thisMissionDists.SelectNodes("distractor");
            
            thisMissionDists.ParentNode.RemoveChild(thisMissionDists);
            int distCounter = dist.Count;
           
            XmlElement newDistractorsTag = myGameXml.CreateElement("distractors");
            thisMission.AppendChild(newDistractorsTag);


            myGame.Attributes["time"].InnerXml = DropDownList1.SelectedValue;

            for (int i=0; i < 4; i++)
            {
                if(((TextBox)FindControl("distractorsTxt" + i.ToString())).Text != "")
                {
                    XmlElement newDistractor = myGameXml.CreateElement("distractor");
                    newDistractor.InnerXml = Server.UrlDecode(((TextBox)FindControl("distractorsTxt" + i.ToString())).Text);
                    newDistractorsTag.AppendChild(newDistractor);

                    if (i == 0)
                    {
                        newDistractor.SetAttribute("answer", "true");
                    }
                    else
                    {
                        newDistractor.SetAttribute("answer", "false");
                    }
                }
                
            }
            

            myGameXml.SelectSingleNode("//game[@id='" + gameID + "']/mission[@id='" + Session["theItemIdOnEdit"] + "']/question").InnerXml = qTxt.Text;

            myGameXml.SelectSingleNode("//game[@id='" + gameID + "']/title").InnerXml = TextBox1.Text;
 

            if (file.Contains("image"))
            {
                string fileType = imageUpload.PostedFile.ContentType;

                //שמירת הנתיב המלא של הקובץ
                string fileName = imageUpload.PostedFile.FileName;
                // הסיומת של הקובץ
                string endOfFileName = fileName.Substring(fileName.LastIndexOf("."));
                // חיבור השם החדש עם הסיומת של הקובץ
                string myTime = DateTime.Now.ToString("dd_MM_yy-HH_mm_ss");
                string imageNewName = "pic" + myTime + endOfFileName;
                Session["imageNewNameX"] = imageNewName;


                //שמירה של הקובץ לספרייה בשם החדש שלו
                imageUpload.PostedFile.SaveAs(Server.MapPath(imagesLibPath) + imageNewName);



                System.Drawing.Image bitmap;

                using (var bmpTemp2 = new System.Drawing.Bitmap(Server.MapPath(imagesLibPath) + imageNewName))
                {
                    bitmap = new System.Drawing.Bitmap(bmpTemp2);
                }
                bitmap = FixedSize(bitmap, 120, 120);


                bitmap.Save(Server.MapPath(imagesLibPath) + "small_" + imageNewName);
                XmlNode myCurrentMission = myGame.SelectSingleNode("mission[@id = '" + thisMission + "']");
                thisQuestion.Attributes["pic"].InnerText = "yes";

                thisQuestion.Attributes["picUrlSmall"].InnerXml = imagesLibPath + "small_" + imageNewName;
                thisQuestion.Attributes["picUrl"].InnerXml = imagesLibPath + imageNewName;
            }

            else
            {
                thisQuestion.Attributes["pic"].InnerText = "no";
                thisQuestion.Attributes["picUrl"].InnerXml = "";
                thisQuestion.Attributes["picUrlSmall"].InnerXml = "";
            }

           
            XmlDataSource1.Save();
            myGameXml.Save(MapPath("trees/XMLFile.xml"));
            GridView1.DataBind();

            if (((Button)sender).ID == "saveContinue")
            {
                thumb.Attributes["imageUrl"] = "";
                Response.Redirect("edit.aspx");
            }

            else
            {
                Response.Redirect("Default.aspx");
            }
        }
        Session["action"] = "";
    }

    static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
    {
        int sourceWidth = Convert.ToInt32(imgPhoto.Width);
        int sourceHeight = Convert.ToInt32(imgPhoto.Height);
        int sourceX = 0;
        int sourceY = 0;
        int destX = 0;
        int destY = 0;

        float nPercent = 0;
        float nPercentW = 0;
        float nPercentH = 0;

        nPercentW = ((float)Width / (float)sourceWidth);
        nPercentH = ((float)Height / (float)sourceHeight);
        if (nPercentH < nPercentW)
        {
            nPercent = nPercentH;
            destX = System.Convert.ToInt16((Width -
                          (sourceWidth * nPercent)) / 2);
        }
        else
        {
            nPercent = nPercentW;
            destY = System.Convert.ToInt16((Height -
                          (sourceHeight * nPercent)) / 2);
        }

        int destWidth = (int)(sourceWidth * nPercent);
        int destHeight = (int)(sourceHeight * nPercent);

        System.Drawing.Bitmap bmPhoto = new System.Drawing.Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

        System.Drawing.Graphics grPhoto = System.Drawing.Graphics.FromImage(bmPhoto);
        grPhoto.Clear(System.Drawing.Color.White);
        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        grPhoto.DrawImage(imgPhoto,
            new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
            new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
            System.Drawing.GraphicsUnit.Pixel);

        grPhoto.Dispose();
        return bmPhoto;
    }

    protected void publishLimit(object sender, EventArgs e)
    {
        if (limitPublishBtn.Text == "פרסם משחק")
        {
            System.Xml.XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
            XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
            myGame.Attributes["published"].InnerXml = "true";
        }

        else
        {
            System.Xml.XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
            XmlNode myGame = myGameXml.SelectSingleNode("catalog/game[@id = '" + gameID + "']");
            myGame.Attributes["published"].InnerXml = "false";
        }
        XmlDataSource1.Save();

        Response.Redirect("Default.aspx");
    }

    protected void returnFunc(object sender, EventArgs e)
    {
        Response.Redirect("Default.aspx");
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["selectedTime"] = DropDownList1.SelectedValue;
    }
}