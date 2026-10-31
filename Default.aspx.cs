using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PlacementDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementDB"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSummary();
                LoadStudents();
            }
        }

        private void LoadSummary()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Students", con);

                lblTotal.Text = cmd.ExecuteScalar().ToString();

                cmd.CommandText =
                    "SELECT COUNT(*) FROM Students WHERE IsPlaced = 1";

                lblPlaced.Text = cmd.ExecuteScalar().ToString();

                cmd.CommandText =
                    "SELECT COUNT(*) FROM Students WHERE IsPlaced = 0";

                lblNotPlaced.Text = cmd.ExecuteScalar().ToString();

                int total = Convert.ToInt32(lblTotal.Text);
                int placed = Convert.ToInt32(lblPlaced.Text);

                double percentage = total > 0
                    ? (placed * 100.0) / total
                    : 0;

                lblPercentage.Text =
                    percentage.ToString("F2") + "%";

                cmd.CommandText =
                    "SELECT ISNULL(MAX(PackageLPA), 0) FROM Placements";

                lblHighestPackage.Text =
                    Convert.ToDecimal(cmd.ExecuteScalar())
                    .ToString("F2") + " LPA";
            }
        }

        private void LoadStudents()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT
                        S.EnrollmentNo,
                        S.StudentName,
                        S.Department,
                        P.CompanyName,
                        P.PackageLPA
                    FROM Students S
                    LEFT JOIN Placements P
                    ON S.StudentId = P.StudentId";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvPlacements.DataSource = dt;
                gvPlacements.DataBind();
            }
        }
    }
}