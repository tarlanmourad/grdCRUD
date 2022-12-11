using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace grdCRUD
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection conn = new SqlConnection("Data Source=TARLAN\\LOCALHOST;Initial Catalog=Northwind;Integrated Security=True");
        SqlCommand cmd;
        String query;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) BindGrid();
        }

        protected void grdView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grdView.EditIndex = -1;
            BindGrid();
        }

        protected void grdView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label id = grdView.Rows[e.RowIndex].FindControl("lblID") as Label;
            conn.Open();
            query = "DELETE FROM Products WHERE ProductID=@ID";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id.Text));

            if(cmd.ExecuteNonQuery() == 1)
            {
                string script = "alert(\"Deleted row\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                string script = "alert(\"Not deleted\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

            conn.Close();
            grdView.EditIndex = -1;
            BindGrid();
        }

        protected void grdView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdView.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void grdView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label id = grdView.Rows[e.RowIndex].FindControl("lblID") as Label;

            string name = ((TextBox)grdView.Rows[e.RowIndex].Cells[1].Controls[0]).Text.ToString();
            string quantity = ((TextBox)grdView.Rows[e.RowIndex].Cells[2].Controls[0]).Text.ToString();
            string price = ((TextBox)grdView.Rows[e.RowIndex].Cells[3].Controls[0]).Text.ToString();
            string stock = ((TextBox)grdView.Rows[e.RowIndex].Cells[4].Controls[0]).Text.ToString();

            conn.Open();
            query = "UPDATE Products SET ProductName=@name, QuantityPerUnit=@quantity, UnitPrice=@price, UnitsInStock=@stock WHERE ProductID=@ID";
            cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(id.Text));
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@stock", stock);

            if (cmd.ExecuteNonQuery() == 1)
            {
                string script = "alert(\"Updated row\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                string script = "alert(\"Not updated\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }

            conn.Close();
            grdView.EditIndex = -1;
            BindGrid();
        }

        private void BindGrid()
        {
            conn.Open();
            query = "SELECT ProductID, ProductName, QuantityPerUnit, UnitPrice, UnitsInStock FROM Products";
            cmd = new SqlCommand(query, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            grdView.DataSource = ds;
            grdView.DataBind();
            conn.Close();
        }


    }
}