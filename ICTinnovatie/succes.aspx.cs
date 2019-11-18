using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ICTinnovatie
{
    public partial class succes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //deze pagina haalt de eerder meegegeven informatie uit de querystring en geeft deze vervolgens weer met een label die aangeeft wat de informatie betekend
                string city = Request["stad"];
                string garage = Request["garage"];
                string naam = Request["naam"];
                string email = Request["email"];
                string kenteken = Request["kenteken"];
                string parkingspotnr = Request["plek"];
                string parkingspotzone = Request["zone"];
                Label2.Text = "stad: " + city;
                Label3.Text = "garage: " + garage;
                Label4.Text = "naam: " + naam;
                Label5.Text = "email-adres: " + email;
                Label6.Text = "kenteken: " + kenteken;
                Label7.Text = "parkeerplek nummer: " + parkingspotnr;
                Label8.Text = "parkeerplek zone: " + parkingspotzone;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //deze knop brengt de gebruiker terug naar de eerste pagina
            Response.Redirect("reserveerpagina.aspx");
        }
    }
}