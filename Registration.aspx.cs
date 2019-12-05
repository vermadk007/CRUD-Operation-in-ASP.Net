using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CRUD
{
    public partial class Registration : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Fill_Gender();
                Fill_Qualification();
                Fill_Hobbies();
                Fill_Grid();
            }
        }
        public void Fill_Grid()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_Registration_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                grd.DataSource = ds;
                grd.DataBind();
               
            }
            con.Close();
        }

        public void Fill_Gender()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_gender_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                rblgender.DataValueField = "GID";
                rblgender.DataTextField = "GName";
                rblgender.DataSource = ds;
                rblgender.DataBind();
                rblgender.Items[0].Selected = true;
            }
            con.Close();
        }
        public void Fill_Qualification()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_Qualification_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlqualification.DataValueField = "QID";
                ddlqualification.DataTextField = "QName";
                ddlqualification.DataSource = ds;
                ddlqualification.DataBind();
                ddlqualification.Items.Insert(0,new ListItem("--Select--","0"));
            }
            con.Close();
        }
        public void Fill_Hobbies()
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("usp_Hobbies_select", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                cblhobbies.DataValueField = "HID";
                cblhobbies.DataTextField = "HName";
                cblhobbies.DataSource = ds;
                cblhobbies.DataBind();

            }
            con.Close();
        }

        protected void Btnsave_Click(object sender, EventArgs e)
        {
            string HOB = "";
            for (int i = 0; i < cblhobbies.Items.Count; i++)
            {
                if (cblhobbies.Items[i].Selected == true)
                {
                    HOB += cblhobbies.Items[i].Text + ",";
                }
            }
            HOB = HOB.TrimEnd(',');
            string FN = "";
            FN = Path.GetFileName(fufile.PostedFile.FileName);

            if (Btnsave.Text == "Save")
            {
                
                fufile.SaveAs(Server.MapPath("Files" + "\\" + FN));
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_Registration_Insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Gender", rblgender.SelectedValue);
                cmd.Parameters.AddWithValue("@Qualification", ddlqualification.SelectedValue);
                cmd.Parameters.AddWithValue("@Hobbies", HOB);
                cmd.Parameters.AddWithValue("@Files", FN);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_Registration_Update", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RID",ViewState["RID"]);
                cmd.Parameters.AddWithValue("@Name", txtname.Text);
                cmd.Parameters.AddWithValue("@Gender", rblgender.SelectedValue);
                cmd.Parameters.AddWithValue("@Qualification", ddlqualification.SelectedValue);
                cmd.Parameters.AddWithValue("@Hobbies", HOB);
                if (FN != "")
                {
                    cmd.Parameters.AddWithValue("@Files", FN);
                    File.Delete(Server.MapPath("Files" + "\\" + ViewState["Files"]));
                    fufile.SaveAs(Server.MapPath("Files" + "\\" + FN));
                    
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Files", ViewState["Files"]);
                }
                cmd.ExecuteNonQuery();
                con.Close();
            }
            Fill_Grid();

        }

        protected void grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EDT")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_Registration_Edit", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RID", e.CommandArgument);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtname.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                    rblgender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                    ddlqualification.SelectedValue = ds.Tables[0].Rows[0]["Qualification"].ToString();
                    string[] arr = ds.Tables[0].Rows[0]["Hobbies"].ToString().Split(',');
                    cblhobbies.ClearSelection();
                    for (int i = 0; i < cblhobbies.Items.Count; i++)
                    {
                        for (int j = 0; j < arr.Length; j++)
                        {
                            if (cblhobbies.Items[i].Text == arr[j])
                            {
                                cblhobbies.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                    ViewState["Files"] = ds.Tables[0].Rows[0]["Files"].ToString();
                    ViewState["RID"] = e.CommandArgument.ToString();
                    Btnsave.Text = "Update";

                }
                con.Close();
            }
            else if (e.CommandName == "DLT")
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("usp_Registration_Delete",con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@RID",e.CommandArgument);
                cmd.ExecuteNonQuery();
                con.Close();
                Fill_Grid();
            }
        }
    }
}
