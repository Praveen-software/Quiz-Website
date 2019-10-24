using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();

    SqlDataAdapter sda = new SqlDataAdapter();
    DataSet ds = new DataSet();
    string cnString = ConfigurationManager.ConnectionStrings["MyMakeConnectionString"].ConnectionString;
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void signin_Click(object sender, EventArgs e)
    {
        UserDetails user = null;
        con = new SqlConnection(cnString);


        cmd = new SqlCommand("Select* from tblQuizReg Where Email ='" + your_name.Text + "' and Paasword= '" + your_pass.Text + "'", con);
        cmd.CommandType = CommandType.Text;
        sda = new SqlDataAdapter(cmd);
        ds = new DataSet();
        sda.Fill(ds, "tblQuizReg");
        if (ds.Tables["tblQuizReg"].Rows.Count > 0)
        {
            DataRow dr = ds.Tables["tblQuizReg"].Rows[0];
            user = new UserDetails()
            {
                Id = Convert.ToInt32(dr[0]),
                Name = dr[1].ToString(),
                Email = dr[3].ToString(),
                Paasword = dr[2].ToString(),
                MobileNo = dr[4].ToString(),
                CollegeName = dr[5].ToString(),
            };



            Session["Name"] = user.Name;
            Session["Email"] = user.Email;
            Session["MobileNo"] = user.MobileNo;
            Session["CollegeName"] = user.CollegeName;

            {
                Response.Redirect("QuizGame.aspx");

            }


        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Your Id And Password Is Incoreect!!!')", true);
        }
    }

}