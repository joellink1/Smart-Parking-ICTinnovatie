using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICTinnovatie
{
    public partial class reserveerpagina : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //deze code ververst de data in het dropdownmenu en voegt er een neutrale optie "selecteer stad" aan toe. dit gebeurt enkel als de pagina vanaf een andere pagina benadert wordt
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Selecteer Stad", "0"));
            }
            else
            {
                //wanneer er op de knop wordt gedrukt zonder dat een stad geselecteerd is dan wordt het volgende label in de placeholder geplaatst.
                Label label1 = new Label();
                label1.Text = "Kies een stad om door te gaan";
                label1.ID = "label1";
                label1.ForeColor = System.Drawing.Color.Red;
                label1.Font.Bold = true;
                PlaceHolder1.Controls.Add(label1);
            }
            
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //de button controleert of een waarde behalve de neutrale waarde is geselecteerd in het dropdownmenu, en zoja, dan plaats hij de waarde in de querystring voor de volgende pagina
            if (Convert.ToInt32(DropDownList1.SelectedValue) != 0)
            {
                string data = DropDownList1.SelectedValue.ToString();
                Response.Redirect(string.Format("parkinggarages.aspx?id={0}", data));
            }
            else
            {
                
            }
            
        }
    }
}