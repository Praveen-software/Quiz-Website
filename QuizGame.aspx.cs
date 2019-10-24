using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class QuizGame : System.Web.UI.Page
{
    string cnString = ConfigurationManager.ConnectionStrings["MyMakeConnectionString"].ConnectionString;
    SqlConnection sqlconn;
    private DataTable DataTableQuestions
    {
        get { return (DataTable)ViewState["Question"]; }
        set { ViewState["Question"] = value; }
    }

    private int QuestionIndex
    {
        get { return (int)ViewState["QuestionIndex"]; }
        set { ViewState["QuestionIndex"] = value; }
    }

    private int Score
    {
        get { return (int)ViewState["Score"]; }
        set { ViewState["Score"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            UserDetails pUser = (UserDetails)Session["user"];

            string Name = Session["Name"].ToString();
            string Email = Session["Email"].ToString();
            string MobileNo = Session["MobileNo"].ToString();
            string College = Session["CollegeName"].ToString();

            DataTableQuestions = PopulateQuestions();
            QuestionIndex = 0;
            Score = 0;
            GetCurrentQuestion(QuestionIndex, DataTableQuestions);


        }
    }

    protected void Next(object sender, EventArgs e)
    {

        QuestionIndex++;
        GetCurrentQuestion(QuestionIndex, DataTableQuestions, int.Parse(rbtnOptions.SelectedItem.Value));


    }

    //protected void Start(object sender, EventArgs e)
    //{
    //    Response.Redirect(Request.Url.AbsoluteUri);
    //}

    private DataTable PopulateQuestions()
    {
        DataTable dt = new DataTable();
        string constr = ConfigurationManager.ConnectionStrings["MyMakeConnectionString"].ConnectionString;
        using (SqlConnection _cn = new SqlConnection(constr))
        {
            using (SqlCommand _cmd = new SqlCommand("SELECT TOP 10 * FROM Question_Answer ORDER By NEWID()", _cn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(_cmd))
                {
                    _cn.Open();
                    da.Fill(dt);
                    _cn.Close();
                }
            }
        }
        return dt;
    }

    private void GetCurrentQuestion(int index, DataTable dtQuestions, int? selectedOption = null)
    {

        if (selectedOption != null)
        {
            string option = string.Empty;
            DataRow row = dtQuestions.Rows[index - 1];
            if (selectedOption == 0)
            {
                option = row["OptionA"].ToString();
            }
            else if (selectedOption == 1)
            {
                option = row["OptionB"].ToString();
            }
            else if (selectedOption == 2)
            {
                option = row["OptionC"].ToString();
            }
            else if (selectedOption == 3)
            {
                option = row["OptionD"].ToString();
            }
            if (option == row["Answer"].ToString())
            {
              Score= Score+2;
            }
        }
        if (index < dtQuestions.Rows.Count)
        {
            DataRow row = dtQuestions.Rows[index];
            lblQuestion.Text = row["Question"].ToString();
            List<ListItem> options = new List<ListItem>();
            ListItem option1 = new ListItem(row["OptionA"].ToString(), "0");
            ListItem option2 = new ListItem(row["OptionB"].ToString(), "1");
            ListItem option3 = new ListItem(row["OptionC"].ToString(), "2");
            ListItem option4 = new ListItem(row["OptionD"].ToString(), "3");
            options.AddRange(new ListItem[4] { option1, option2, option3, option4 });
            List<ListItem> randomOptions = RandomizeList(options);
            rbtnOptions.Items.Clear();
            rbtnOptions.Items.AddRange(randomOptions.ToArray());
            rbtnOptions.DataBind();

        }
        else
        {
            if (index == 10)
            {
                

                sqlconn = new SqlConnection(cnString);
                sqlconn.Open();
                string query = "insert into tbl_QuizScore values('" + Session["Name"] + "','" + Session["Email"] + "','" + Session["MobileNo"] + "','" + Score + "','" + Session["CollegeName"] + "','" + DateTime.Now + "','0')";
                SqlCommand sqlcommand = new SqlCommand(query, sqlconn);
                int k = sqlcommand.ExecuteNonQuery();
                if (k != 0)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "msg", "alert('Sucess!!')", true);

                    sqlconn.Close();

                }

            }
            
            dvResult.Visible = true;
            lblResult.Text = string.Format("You Scored {0}/{1}", Score, "20");
        }
    }

    public List<ListItem> RandomizeList(List<ListItem> originalList)
    {
        List<ListItem> randomList = new List<ListItem>();
        Random random = new Random();
        ListItem value = default(ListItem);
        while (originalList.Count() > 0)
        {
            var nextIndex = random.Next(0, originalList.Count());
            value = originalList[nextIndex];
            randomList.Add(value);
            originalList.RemoveAt(nextIndex);
        }
        return randomList;
    }
}