using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Sign_Up : System.Web.UI.Page
{
    string cnString = ConfigurationManager.ConnectionStrings["MyMakeConnectionString"].ConnectionString;
    SqlConnection sqlconn;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void signup_Click(object sender, EventArgs e)
    {
        sqlconn = new SqlConnection(cnString);
        sqlconn.Open();
        string query = "insert into tblQuizReg values('" + txtname.Text + "','" + txtpassword.Text + "','" + txtemail.Text + "','" + txtmobileno.Text + "','" + ddlcollege.SelectedItem + "')";
        SqlCommand sqlcommand = new SqlCommand(query, sqlconn);
        int k = sqlcommand.ExecuteNonQuery();
        if (k != 0)
        {
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('Your Registrtion is sucessfull!!');", true);
           
            sqlconn.Close();
            Response.Redirect("Login.aspx");

        }

    }

    [System.Web.Services.WebMethod]
    public static string CheckEmail(string useroremail)
    {
        string retval = "";
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyMakeConnectionString"].ConnectionString);
        con.Open();
        SqlCommand cmd = new SqlCommand("select Email from tblQuizReg where Email=@UserNameorEmail", con);
        cmd.Parameters.AddWithValue("@UserNameorEmail", useroremail);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.HasRows)
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }

        return retval;

    }

}
