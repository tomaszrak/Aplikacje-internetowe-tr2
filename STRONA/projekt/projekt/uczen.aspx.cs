﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace projekt
{
    public partial class uczen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["zalogowany"] != null)
            {
                if ((int)Session["zalogowany"] == 1)
                {
                    Server.Transfer("dyrektor.aspx");
                }

                if ((int)Session["zalogowany"] == 2)
                {
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString))
                    {
                        string polecenie = "SELECT UCZEN.ID_Uczen,UCZEN.Imie,UCZEN.Nazwisko,UCZEN.Email,KLASA.Nazwa FROM UCZEN,KLASA WHERE Login=@login AND Haslo=@haslo AND UCZEN.ID_Klasa=KLASA.ID_Klasa";
                        SqlCommand cmd = new SqlCommand(polecenie, con);
                        cmd.Parameters.AddWithValue("@login", Session["login"].ToString());
                        cmd.Parameters.AddWithValue("@haslo", Session["haslo"].ToString());
                        con.Open();
                        using (SqlDataReader rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                Session["id"] = rdr["ID_Uczen"].ToString();
                                Session["imie"] = rdr["Imie"].ToString();
                                Session["nazwisko"] = rdr["Nazwisko"].ToString();
                                Session["email"] = rdr["Email"].ToString();
                                Session["klasa"] = rdr["Nazwa"].ToString();
                            }
                        }
                        con.Close();
                    }
                    Label3.Text = Session["login"].ToString();
                    Label1.Text = Session["imie"].ToString() + " " + Session["nazwisko"].ToString();
                    Label2.Text = Session["email"].ToString();
                    Label4.Text = Session["klasa"].ToString();
                }

                if ((int)Session["zalogowany"] == 3)
                {
                    Server.Transfer("nauczyciel.aspx");
                }
            }
            else
            {
                Server.Transfer("Glowna.aspx");
            }
        }
    }
}