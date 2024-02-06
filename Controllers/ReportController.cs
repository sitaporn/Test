using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Reporting.WebForms;
using TestMVC.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TestMVC.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMethod()
        {
            List<Phone> phonelistdetail = GetListDetail();
            LocalReport lr = new LocalReport();
            // string exeFolder = Application.StartupPath;

            //string reportpath = Path.Combine(exeFolder, "Report", "Report1.rdlc");
            string reportpath = Server.MapPath(@"/Report/Report1.rdlc");
            lr.ReportPath = reportpath;
            ReportDataSource re  = new ReportDataSource();
             re.Name = "Reports";
             re.Value = phonelistdetail;
             lr.DataSources.Add(re);
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = lr.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            string fileName = "LogInDetails-" + "test" + "_Test_" + ".pdf";
            string base64EncodedPDF = System.Convert.ToBase64String(bytes);
            return Json(base64EncodedPDF, JsonRequestBehavior.AllowGet);
        }
        private List<Phone> GetListDetail()
        {
            List<Phone> data = new List<Phone>();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT ID, SECTION, USERNAME, POSITION, TEL, NOTE, THEATER FROM Phone where STATUS = 'true'", con);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Phone ci = new Phone();
                ci.Row      = Convert.ToInt32(rdr["ID"]);
                ci.Id       = Convert.ToInt32(rdr[0]);
                ci.Section  = rdr[1].ToString();
                ci.Name     = rdr[2].ToString();
                ci.Position = rdr[3].ToString();
                ci.Tel      = rdr[4].ToString();
                ci.Note     = rdr[5].ToString();
                ci.Theater  = Convert.ToInt32(rdr[6]);
                data.Add(ci);
            }
            con.Close();
            return data;
        }
    }
}