using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace consegna_backend_u1_s3_l3
{
    public partial class _Default : Page
    {
        int countSalaNord = 0;
        int countSalaEst = 0;
        int countSalaSud = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"SELECT Tipo_Sala,Ridotto, COUNT(*) AS nPrenotazioni FROM Prenotazioni  GROUP BY Tipo_Sala,Ridotto ORDER BY Sala ASC";
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    //string tipoSala = reader.GetString(0);
                    //string bigliettiVenduti = reader.GetString(1).ToString();
                    //string bigliettiRidotti = reader.GetString(2).ToString();

                    //SalaNord.InnerText = tipoSala;
                    //bigliettiNord.InnerText = bigliettiVenduti;
                    //bigliettiRidottiNord.InnerText = bigliettiRidotti;
                }
            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["myConnectionStringDb"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO Prenotazioni (Nome , Cognome , Tipo_Sala, Ridotto) VALUES (@nome, @cognome, @DropDownList1, @ridotto)";

                cmd.Parameters.AddWithValue("@nome", nome.Text);
                cmd.Parameters.AddWithValue("@cognome", cognome.Text);
                cmd.Parameters.AddWithValue("@DropDownList1", DropDownList1.SelectedValue);
                if (ridotto.Checked)
                {
                    cmd.Parameters.AddWithValue("@ridotto", true);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ridotto", false);

                }
                demo.InnerText = "Prenotato con successo!";
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Response.Write("Errore");
                Response.Write(ex.Message);
            }
            finally
            {
                conn.Close();
                nome.Text = string.Empty;
                cognome.Text = string.Empty;
                ridotto.Checked = false;
            }

        }
    }
}