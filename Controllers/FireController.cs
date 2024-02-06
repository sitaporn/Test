using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestMVC.Models;

namespace TestMVC.Controllers
{
    public class FireController : Controller
    {
        // GET: โรพำ
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FType()
        {
            return View();
        }
        public ActionResult FSize()
        {
            return View();
        }
        public ActionResult FPlace()
        {
            return View();
        }

        public ActionResult GetTest()
        {
            
            List<Fire> ii = GetListDetail();
            Fire ft = null;
            Fire fs = null;
            Fire fp = null;
            int a, b, c;
            foreach (Fire i in ii) {
              

                a = i.Ftype;
                b = i.Fsize;
                c = i.Fplace;
                 ft = GetType(a);
                 fs = GetSize(b);
                 fp = GetPlace(c);

                i.Ftypes = ft.Names;
                i.Fsizes = fs.Name;
                i.Fplaces = fp.Name;
            }

            return Json(ii, JsonRequestBehavior.AllowGet);
        }
        private List<Fire> GetListDetail()
         {
            List<Fire> data = new List<Fire>();
            string constring = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT ID,F_TYPE,F_SIZE,F_PLACE,START_DATE,END_DATE FROM ListDetail", con);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Fire ci = new Fire();
                ci.Id = Convert.ToInt32(rdr[0]);
                ci.Ftype = Convert.ToInt32(rdr[1]);
                ci.Fsize = Convert.ToInt32(rdr[2]);
                ci.Fplace = Convert.ToInt32(rdr[3]);
                //ci.StartDs = Convert.ToDateTime(rdr[4]).ToString("dd/MM/yyyy");
                //ci.EndDs = Convert.ToDateTime(rdr[5]).ToString("dd/MM/yyyy");
            }
            con.Close();
            return data;
        }
        private Fire GetType(int a)
        {
            Fire ci = new Fire();
            string constring = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT ID, NAME,NAME_EN FROM FType where ID="+a+"", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ci.Id = Convert.ToInt32(rdr[0]);
                    ci.Name = rdr[1].ToString();
                    ci.Names = rdr[2].ToString();
                }
                con.Close();
            }
            return ci;
        }
        private Fire GetSize(int b)
        {
            Fire ci = new Fire();
            string constring = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT ID, NAME FROM FSize where ID="+b+"", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ci.Id = Convert.ToInt32(rdr[0]);
                    ci.Name = rdr[1].ToString();
                }
                con.Close();
            }
            return ci;
        }
        private Fire GetPlace(int c)
        {
            Fire ci = new Fire();
            string constring = ConfigurationManager.ConnectionStrings["TestDb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT ID, NAME FROM FPlace where ID="+c+"", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ci.Id = Convert.ToInt32(rdr[0]);
                    ci.Name = rdr[1].ToString();
                }
                con.Close();
            }
            return ci;
        }
    }
}