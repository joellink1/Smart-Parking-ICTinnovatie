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
    public partial class klantinformatie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Hier wordt de pagina en de data op de pagina aangemaakt wanneer deze wordt benaderd vanaf een andere pagina.
                DataTable dt = new DataTable();
                SqlConnection connection = new SqlConnection("Data Source=LAPTOP-13F9062R;Initial Catalog=ICTinnovatie;Integrated Security=True");
                connection.Open();
                string data = Request.QueryString["id"];
                SqlCommand sqlcmd = new SqlCommand(string.Format("SELECT * FROM parkinggaragetbls WHERE ID = {0}", data), connection);
                SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                sqlda.Fill(dt);
                Label2.Text = dt.Rows[0]["naam"].ToString();
                DataTable dt2 = new DataTable();
                string city_id = dt.Rows[0]["city_id"].ToString();
                SqlCommand sqlcmd2 = new SqlCommand(string.Format("SELECT * FROM citytbl WHERE ID = {0}", city_id), connection);
                SqlDataAdapter sqlda2 = new SqlDataAdapter(sqlcmd2);
                sqlda2.Fill(dt2);
                Label1.Text = dt2.Rows[0]["naam"].ToString();
            }
            else
            {
                //hier wordt een melding geplaatst in de placeholder op de pagina wanneer niet alle informatie correct ingevuld is.
                Label label2 = new Label();
                label2.Text = "Vul AUB alle velden in met correcte informatie";
                label2.ID = "label2";
                label2.ForeColor = System.Drawing.Color.Red;
                label2.Font.Bold = true;
                PlaceHolder1.Controls.Add(label2);
            }
             

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Deze button stuurt de gebruiker terug naar de eerste pagina
            Response.Redirect("reserveerpagina.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //Deze button stuurt de gebruiker terug naar de parkeergarage pagina, en geeft daarbij ook de geselecteerde stad ID mee
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection("Data Source=LAPTOP-13F9062R;Initial Catalog=ICTinnovatie;Integrated Security=True");
            connection.Open();
            string data = Request.QueryString["id"];
            SqlCommand sqlcmd = new SqlCommand(string.Format("SELECT * FROM parkinggaragetbls WHERE ID = {0}", data), connection);
            SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);


            sqlda.Fill(dt);
            string data1 = dt.Rows[0]["city_id"].ToString();
            Response.Redirect(string.Format("parkinggarages.aspx?id={0}", data1));
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            //dit is de button die de data wegschrijft in de database wanneer deze correct is.
            SqlConnection connection = new SqlConnection("Data Source=LAPTOP-13F9062R;Initial Catalog=ICTinnovatie;Integrated Security=True");
            connection.Open();
            //hier wordt de data uit de textboxen gehaald, en tegelijkertijd de ID van de parkeergarage uit de querystring
            string parkinggarage_id = Request.QueryString["id"];
            string klantnaam = TextBox1.Text.ToString();
            string emailadres = TextBox2.Text.ToString();
            string kenteken = TextBox3.Text.ToString();  
            //deze statement zorgt ervoor dat de bewerking alleen door gaat wanneer naam en kenteken niet leeg zijn en emailadres een domeinnaam bevat
            if (klantnaam != "" && emailadres.Contains("@") && emailadres.Contains(".") && kenteken != "")
            {
                DataTable parkingspots = new DataTable();
                SqlCommand sqlcmnd3 = new SqlCommand(string.Format("SELECT ID, gereserveerd, status FROM parkingspottbl WHERE parkinggarage_id = {0}", parkinggarage_id), connection);
                SqlDataAdapter sqlda3 = new SqlDataAdapter(sqlcmnd3);
                sqlda3.Fill(parkingspots);
                int parkingspot_id = 0;
                //hier wordt een parkeerplek gezocht in de geselecteerde parkeergarage
                foreach (DataRow row in parkingspots.Rows)
                {
                    string gereserveerdstatus = row["gereserveerd"].ToString();
                    string statusstatus = row["status"].ToString();
                    if (statusstatus == "False")
                    {
                        if (gereserveerdstatus == "False")
                        {
                            parkingspot_id = Convert.ToInt32(row["ID"].ToString());
                            break;
                        }
                    }
                }
                //wanneer een parkeergarage is gevonden dan wordt parkingspot_id iets anders dan 0 en kan de volgende statement doorgaan, anders gaat het webform door naar het einde
                if (parkingspot_id > 0)
                {
                    //hier wordt de informatie in de tabel met klantgegevens gezet, vervolgens wordt de parkeerplaats op bezet gezet, en als laatste wordt een link gemaakt met de factstable(inclusief een timestamp in een string)
                    SqlCommand sqlcmnd2 = new SqlCommand(string.Format("INSERT INTO customerinformationtbl (naam, email, kenteken) VALUES ('{0}','{1}','{2}')", klantnaam, emailadres, kenteken), connection);
                    sqlcmnd2.ExecuteNonQuery();
                    DataTable customers = new DataTable();
                    SqlCommand sqlcmnd8 = new SqlCommand(string.Format("SELECT * FROM customerinformationtbl WHERE naam='{0}' AND email='{1}' AND kenteken='{2}'", klantnaam, emailadres, kenteken), connection);
                    SqlDataAdapter sqlda9 = new SqlDataAdapter(sqlcmnd8);
                    sqlda9.Fill(customers);
                    string customerinformation_id = "";
                    foreach (DataRow row in customers.Rows)
                    {
                        customerinformation_id = row["ID"].ToString();
                    }
                    SqlCommand sqlcmnd4 = new SqlCommand(string.Format("UPDATE parkingspottbl SET gereserveerd ='1' WHERE ID = {0}", parkingspot_id), connection);
                    sqlcmnd4.ExecuteNonQuery();
                    string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                    SqlCommand sqlcmnd5 = new SqlCommand(string.Format("INSERT INTO factstbl (klantgegevens_id, parkingspot_id, datum) VALUES ('{0}', '{1}', '{2}')", customerinformation_id, parkingspot_id, timestamp), connection);
                    sqlcmnd5.ExecuteNonQuery();

                    DataTable dt4 = new DataTable();
                    string data = Request.QueryString["id"];
                    SqlCommand sqlcmd = new SqlCommand(string.Format("SELECT * FROM parkinggaragetbls WHERE ID = {0}", data), connection);
                    SqlDataAdapter sqlda = new SqlDataAdapter(sqlcmd);
                    sqlda.Fill(dt4);
                    string parkinggarage = dt4.Rows[0]["naam"].ToString();
                    DataTable dt2 = new DataTable();
                    string city_id = dt4.Rows[0]["city_id"].ToString();
                    SqlCommand sqlcmd2 = new SqlCommand(string.Format("SELECT * FROM citytbl WHERE ID = {0}", city_id), connection);
                    SqlDataAdapter sqlda2 = new SqlDataAdapter(sqlcmd2);
                    sqlda2.Fill(dt2);
                    string city = dt2.Rows[0]["naam"].ToString();
                    DataTable parkingspot = new DataTable();
                    SqlCommand sqlcmnd7 = new SqlCommand(string.Format("SELECT * FROM parkingspottbl WHERE ID = '{0}'", parkingspot_id), connection);
                    SqlDataAdapter sqlda5 = new SqlDataAdapter(sqlcmnd7);
                    sqlda5.Fill(parkingspot);
                    //hier wordt de laatste informatie voor de succespagina opgevraagd om alle data vervolgens mee te geven in de querystring
                    string parkingspotnr = parkingspot.Rows[0]["parkeerplek"].ToString();
                    string parkingspotzone = parkingspot.Rows[0]["zone"].ToString();

                    Response.Redirect(string.Format("succes.aspx?stad={0}&garage={1}&naam={2}&email={3}&kenteken={4}&plek={5}&zone={6}", city, parkinggarage, klantnaam, emailadres, kenteken, parkingspotnr, parkingspotzone));
                }
                else
                {
                    //wanneer er geen parkeerplek vrij was wordt er doorverwezen naar de fail pagina, met daarop een medling die de gebruiker prompt een andere parkeergarage te kiezen.
                    string data = Request.QueryString["id"];
                    Response.Redirect(string.Format("fail.aspx?id={0}", data));
                }
            }
            
        }
    }
}