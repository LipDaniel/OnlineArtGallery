using Newtonsoft.Json;
using OnlineArtGallery.Models.Entities;
using OnlineArtGallery.Models.ModelView;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace OnlineArtGallery.Controllers
{
    public class BEDashboardController : Controller
    {
        private GalleryArtEntities db = new GalleryArtEntities();
        // GET: BEDashboard
        public Object GetRevenueEachYear()
        {
            using (SqlConnection con = new SqlConnection("workstation id=GalleryArt.mssql.somee.com;packet size=4096;user id=nhannguyen_SQLLogin_1;pwd=4d52kand8k;data source=GalleryArt.mssql.somee.com;persist security info=False;initial catalog=GalleryArt"))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "revenue_2023";
                    cmd.Connection.Open();
                    object o = cmd.ExecuteScalar();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    cmd.Connection.Close();
                    var res = JsonConvert.SerializeObject(dataset.Tables[0]);
                    return res;
                }
            }
        }

        public Object GetRevenueEachYear2023()
        {
            using (SqlConnection con = new SqlConnection("workstation id=GalleryArt.mssql.somee.com;packet size=4096;user id=nhannguyen_SQLLogin_1;pwd=4d52kand8k;data source=GalleryArt.mssql.somee.com;persist security info=False;initial catalog=GalleryArt"))
            {
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "revenue_2022";
                    cmd.Connection.Open();
                    object o = cmd.ExecuteScalar();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    da.Fill(dataset);
                    cmd.Connection.Close();
                    var res = JsonConvert.SerializeObject(dataset.Tables[0]);
                    return res;
                }
            }
        }
    }
}