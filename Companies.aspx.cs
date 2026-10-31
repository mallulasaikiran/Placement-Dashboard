using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace PlacementDashboard
{
    public partial class Companies : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementDB"]
            .ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCompanies();
                LoadCompanySummary();
            }
        }

        private void LoadCompanies()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"
                    SELECT
                        P.CompanyName,
                        S.StudentName,
                        S.EnrollmentNo,
                        P.JobRole,
                        P.PackageLPA
                    FROM Placements P
                    INNER JOIN Students S
                    ON P.StudentId = S.StudentId";

                SqlDataAdapter da =
                    new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();

                da.Fill(dt);

                gvCompanies.DataSource = dt;
                gvCompanies.DataBind();
            }
        }

        private void LoadCompanySummary()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                string query = @"
                    SELECT
                        COUNT(DISTINCT CompanyName),
                        MAX(PackageLPA),
                        AVG(PackageLPA)
                    FROM Placements";

                SqlCommand cmd = new SqlCommand(query, con);

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        lblCompanies.Text = dr[0].ToString();

                        lblHighestPackage.Text =
                            dr.IsDBNull(1) ? "0.00" :
                            Convert.ToDecimal(dr[1]).ToString("F2");

                        lblAveragePackage.Text =
                            dr.IsDBNull(2) ? "0.00" :
                            Convert.ToDecimal(dr[2]).ToString("F2");
                    }
                }
            }
        }
    }
}