using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    string gameID;
    protected void Page_Load(object sender, EventArgs e)
    {
        XmlDocument myDoc = new XmlDocument();
        myDoc.Load(MapPath("trees/XMLFile.xml"));
        XmlNodeList myGames = myDoc.SelectNodes("//game");

        int gamesQuantity = myGames.Count;
        string gamesQuantityNew = gamesQuantity.ToString();
        myDoc.SelectSingleNode("//gCounter").InnerXml = gamesQuantityNew;

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox pubChk = (CheckBox)GridView1.Rows[i].FindControl("publishCbx");

            if (myGames.Item(i).Attributes["published"].Value == "true")
            {

                pubChk.Enabled = true;
                pubChk.Checked = true;
            }
            else if (myGames.Item(i).Attributes["published"].Value == "false")
            {
                if (Convert.ToInt16(((Label)GridView1.Rows[i].FindControl("numOfQLbl")).Text) > 4)
                {
                    pubChk.Enabled = true;
                    pubChk.Checked = false;
                }

                else
                {
                    myGames.Item(i).Attributes["published"].Value = "false";
                    pubChk.Enabled = false;
                    pubChk.Checked = false;

                }
            }
        }
    }

    protected void gridChange(object sender, EventArgs e)
    {
        XmlDocument myGame = new XmlDocument();
        myGame.Load(MapPath("trees/XMLFile.xml"));
        XmlNodeList myGames = myGame.SelectNodes("//game");

        GridViewRow row = ((GridViewRow)((CheckBox)sender).NamingContainer);
        int index = row.RowIndex;
        CheckBox pubChk = (CheckBox)GridView1.Rows[index].FindControl("publishCbx");

        if (myGames.Item(index).Attributes["published"].Value == "true")
        {
            myGames.Item(index).Attributes["published"].Value = "false";
            pubChk.Checked = false;

        }
        else
        {
            myGames.Item(index).Attributes["published"].Value = "true";
            pubChk.Checked = true;
        }

        myGame.Save(MapPath("trees/XMLFile.xml"));
    }


    protected void confirmDelete(object sender, EventArgs e)
    {
        System.Xml.XmlDocument Document = XmlDataSource1.GetXmlDocument();
        System.Xml.XmlNode node = Document.SelectSingleNode("/catalog/game[@id='" + Session["theItemIdSessions"] + "']");
        node.ParentNode.RemoveChild(node);
        XmlDataSource1.Save();
        GridView1.DataBind();

        Response.Redirect("Default.aspx");
    }


protected void Button1_Click(object sender, EventArgs e)
    {
        // טעינה של העץ
        XmlDocument xmlDoc = XmlDataSource1.GetXmlDocument();
        XmlElement myNewGame = xmlDoc.CreateElement("game");

        // הקפצה של מונה האי די בתוך קובץ האקס אם אל באחד
        int myId = Convert.ToInt16(xmlDoc.SelectSingleNode("//gCounter").InnerXml);
        myId++;
        string myNewId = myId.ToString();
        xmlDoc.SelectSingleNode("//gCounter").InnerXml = myNewId;
        
        //יצירת ענף משחק
        XmlElement myNewGameNode = xmlDoc.CreateElement("game");
        myNewGameNode.SetAttribute("id", myNewId); //יצירת איידי
        int GameCode = Convert.ToInt16(100 + myId - 1);//הקפצה של קוד משחק
        string NewGameCode = GameCode.ToString();
        myNewGameNode.SetAttribute("code", GameCode.ToString());//יצירת קוד למשחק
        myNewGameNode.SetAttribute("published","false");//יצירת אטריביוט של פרסום
        myNewGameNode.SetAttribute("missionCounter", "0");
        myNewGameNode.SetAttribute("time", "20");

        //יצירת ענף שם
        XmlElement myNewNameNode = xmlDoc.CreateElement("title");
        myNewNameNode.InnerXml = Server.UrlDecode(gameName.Text);
        myNewGameNode.AppendChild(myNewNameNode);

        //יצירת ענף מונה שאלות
        XmlElement myQuestionCounterNode = xmlDoc.CreateElement("qCounter");
        myQuestionCounterNode.InnerXml = Server.UrlDecode("0");
        myNewGameNode.AppendChild(myQuestionCounterNode);

        //הוספה לעץ
        xmlDoc.SelectSingleNode("/catalog").PrependChild(myNewGameNode);
        XmlDataSource1.Save();
        GridView1.DataBind();
   
        gameName.Text = "";
        Response.Redirect("Default.aspx");
    }

 
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        XmlDocument myGameXml = XmlDataSource1.GetXmlDocument();
        // תחילה אנו מבררים מהו ה -אי די- של הפריט בעץ ה אקס אם אל
        ImageButton i = (ImageButton)e.CommandSource;
        // אנו מושכים את האי די של הפריט באמצעות מאפיין לא שמור במערכת שהוספנו באופן ידני לכפתור-תמונה
        string theId = i.Attributes["theItemId"];
        Session["theItemIdSession"] = i.Attributes["theItemId"];
      // עלינו לברר איזו פקודה צריכה להתבצע - הפקודה רשומה בכל כפתור
        switch (e.CommandName)
        {

            //אם נלחץ על כפתור מחיקה יקרא לפונקציה של מחיקה
            case "deleteRow":
                //לקיחת ID לפי הלחיצה של המשתמש 

                gameNameLbl.Text = myGameXml.SelectSingleNode("//game[@id='" + theId + "']/title").InnerXml + "<span id='qMark' runat='server'>?</span>";
                Session["theItemIdSessions"] = i.Attributes["theItemId"];
                pnlModal_ModalPopupExtender.Show();
    
                break;

            //אם נלחץ על כפתור עריכה (העפרון) נעבור לדף עריכה
            case "edit":
                Session["selectedTime"] = myGameXml.SelectSingleNode("//game[@id='" + theId + "']").Attributes["time"].InnerXml;
                Session["theItemIdSessions"] = i.Attributes["theItemId"];
                Edit((string)Session["theItemIdSessions"]);
              
                break;

        }
    }
    void Edit(string theItemId)
    {
        Response.Redirect("edit.aspx");
    }

    void publish()
    {
        
    }



    protected void XmlDataSource1_Transforming(object sender, EventArgs e)
    {

    }
}
