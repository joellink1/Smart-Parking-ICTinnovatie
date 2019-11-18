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
    public partial class fail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //het hele proces waarin deze pagina geladen wordt is identiek aan de klantinformatie pagina, met uitzondering van een extra label. dit label wordt afgehandeld in de HTML voor de pagina
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("reserveerpagina.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
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
            SqlConnection connection = new SqlConnection("Data Source=LAPTOP-13F9062R;Initial Catalog=ICTinnovatie;Integrated Security=True");
            connection.Open();
            DataTable dt = new DataTable();
            SqlCommand sqlcmnd1 = new SqlCommand("SELECT * FROM customerinformationtbl", connection);
            SqlDataAdapter sqlda1 = new SqlDataAdapter(sqlcmnd1);

            sqlda1.Fill(dt);
            string parkinggarage_id = Request.QueryString["id"];
            string klantnaam = TextBox1.Text.ToString();
            string emailadres = TextBox2.Text.ToString();
            string kenteken = TextBox3.Text.ToString();
            DataTable customers = new DataTable();
            SqlCommand sqlcmnd8 = new SqlCommand(string.Format("SELECT * FROM customerinformationtbl WHERE naam='{0}' AND email='{1}' AND kenteken='{2}'", klantnaam, emailadres, kenteken), connection);
            SqlDataAdapter sqlda9 = new SqlDataAdapter(sqlcmnd8);
            sqlda9.Fill(customers);
            string customerinformation_id = "";
            foreach (DataRow row in customers.Rows)
            {
                customerinformation_id = row["ID"].ToString();
            }
            DataTable parkingspots = new DataTable();
            SqlCommand sqlcmnd3 = new SqlCommand(string.Format("SELECT ID, gereserveerd, status FROM parkingspottbl WHERE parkinggarage_id = {0}", parkinggarage_id), connection);
            SqlDataAdapter sqlda3 = new SqlDataAdapter(sqlcmnd3);
            sqlda3.Fill(parkingspots);
            int parkingspot_id = 0;
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
            if (parkingspot_id > 0)
            {
                SqlCommand sqlcmnd2 = new SqlCommand(string.Format("INSERT INTO customerinformationtbl (naam, email, kenteken) VALUES ('{0}','{1}','{2}')", klantnaam, emailadres, kenteken), connection);
                sqlcmnd2.ExecuteNonQuery();
                SqlCommand sqlcmnd4 = new SqlCommand(string.Format("UPDATE parkingspottbl SET gereserveerd ='1' WHERE ID = {0}", parkingspot_id), connection);
                sqlcmnd4.ExecuteNonQuery();
                string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                SqlCommand sqlcmnd5 = new SqlCommand(string.Format("INSERT INTO factstbl (klantgegevens_id, parkingspot_id, datum) VALUES ('{0}', '{1}', '{2}')", customerinformation_id, parkingspot_id, timestamp), connection);
                sqlcmnd5.ExecuteNonQuery();
            }
            else
            {
                string data = Request.QueryString["id"];
                Response.Redirect(string.Format("fail.aspx?id={0}", data));
            }
        }
    }
}