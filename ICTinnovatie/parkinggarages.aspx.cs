using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ICTinnovatie
{
    public partial class parkinggarages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //hier worden de elementen op de pagina aangemaakt en gevuld vanuit de database wanneer deze dat niet vanzelf zijn. 
                //dit gebeurt alleen wanneer de pagina vanaf een andere pagina benaderd wordt.
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("Selecteer Parkeergarage", "0"));
                DataTable dt = new DataTable();
                SqlConnection connection = new SqlConnection("Data Source=LAPTOP-13F9062R;Initial Catalog=ICTinnovatie;Integrated Security=True");
                connection.Open();
                string data = Request.QueryString["id"];
                SqlCommand sqlcmd = new SqlCommand(string.Format("SELECT * FROM citytbl WHERE ID = {0}", data), connection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);


                sqlda.Fill(dt);
                Label1.Text = dt.Rows[0]["naam"].ToString();
            }
            else
            {
                //hier wordt de foutmelding in de placeholder geplaatst wanneer er geen parkeergarage is gekozen.
                Label label2 = new Label();
                label2.Text = "Kies een parkeergarage om door te gaan";
                label2.ID = "label2";
                label2.ForeColor = System.Drawing.Color.Red;
                label2.Font.Bold = true;
                PlaceHolder1.Controls.Add(label2);
            }
               


            
        }

        protected void button1_Click(object sender, EventArgs e)
        {
            //deze button brengt de gebruiker terug naar de eerste pagina
            Response.Redirect("reserveerpagina.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //deze button controleert of de gebruiker een waarde heeft gekozen in de dropdowntable en zet deze waarde vervolgens in de querystring om naar de volgende pagina te gaan.
            if (Convert.ToInt32(DropDownList1.SelectedValue) != 0)
            {
                string data = DropDownList1.SelectedValue.ToString();
                Response.Redirect(string.Format("klantinformatie.aspx?id={0}", data));
            }
        }
    }
}