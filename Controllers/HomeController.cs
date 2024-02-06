using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Xml.Linq;
using TestMVC.Models;
using static System.Collections.Specialized.BitVector32;

namespace TestMVC.Controllers
{
    public class HomeController : Controller
    {
        //private TestDataDbContext db = new TestDataDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MS1_10()
        {
            // List<TestMVC.Models.TestClass> phones = db.TestClasses.ToList();
            //List<Phone> cityname = new List<Phone>();
            //string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            //SqlConnection con = new SqlConnection(constring);
            //SqlCommand cmd = new SqlCommand("SELECT ID, SECTION, USERNAME, POSITION, TEL, NOTE FROM Phone", con);

            //con.Open();
            //SqlDataReader rdr = cmd.ExecuteReader();
            //while (rdr.Read())
            //{
            //    Phone ci = new Phone();
            //    ci.Id       = Convert.ToInt32(rdr["ID"]);
            //    ci.Section  = rdr[1].ToString();
            //    ci.Name     = rdr[2].ToString();
            //    ci.Position = rdr[3].ToString();
            //    ci.Tel      = rdr[4].ToString();
            //    ci.Note     = rdr[5].ToString();
            //    cityname.Add(ci);
            //}
            //Console.WriteLine(cityname);
            //con.Close();
            //List<Phone> phonelistsection = GetListSection();
            //List<Phone> phonelistdetail = GetListDetail();
            //Console.WriteLine(phonelistsection);
            //Console.WriteLine(phonelistdetail);

            //foreach (Phone p in phonelistsection) {
            //    p.Lista = phonelistdetail.Where(x => x.Section == p.Section).ToList();
            //}

            //Console.WriteLine(phonelistsection);
            // var vm = new TestClass();
            //vm.Cities = GetCities();

           // return View(cityname);
            return View();
        }
        [HttpPost]
        public ActionResult PostSec(string section)
        {
            List<Phone> phonelistdetail = GetListDetails(section);
            Console.WriteLine(phonelistdetail);
            return Json(phonelistdetail, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PostDelete(string Id)
        {
            //   List<Phone> phonelistdetail = GetListDetails(int);
            // Console.WriteLine(phonelistdetail);
           // List<Phone> data = new List<Phone>();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("DELETE FROM Phone where ID = "+Convert.ToInt32(Id)+"", con);

            con.Open();

            cmd.ExecuteNonQuery();
            con.Close();



            return Json(Id, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PostUpdate(string Id)
        {
            //   List<Phone> phonelistdetail = GetListDetails(int);
            // Console.WriteLine(phonelistdetail);
            // List<Phone> data = new List<Phone>();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
           
            // SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
          //  SqlDataReader dr;
            con.Open();
            //  SqlCommand cmd = new SqlCommand("update Phone SET STATUS = @STATUS  where ID = " + Convert.ToInt32(Id) + "", con);
            cmd.Connection = con;
            cmd.CommandText = "update Phone SET STATUS = @STATUS  where ID = @ID";
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt32(Id));
            cmd.Parameters.AddWithValue("@STATUS",false);
            //dr = cmd.ExecuteReader();

            //if (dr.Read()) {
            //    Console.WriteLine();
            //}
            //    Console.WriteLine();
            //}

              cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();



            return Json(Id, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PostInsert(Phone data)
        {
            try
            {
                Console.WriteLine(data);
                Console.WriteLine();
                if (string.IsNullOrEmpty(data.Section))
                 //   throw new HttpResponseException(HttpStatusCode.NotFound, "");

                 return Json(new { status = "error", message = "กรุณาระบุ Section" });
                if (string.IsNullOrEmpty(data.Name))
                    return Json(new { status = "error", message = "กรุณาระบุ User Name" });
                if (string.IsNullOrEmpty(data.Position))
                    return Json(new { status = "error", message = "กรุณาระบุ Position" });
                if (string.IsNullOrEmpty(data.Tel))
                    return Json(new { status = "error", message = "กรุณาระบุ Tel" });
                if(data.Theater == 0)
                    return Json(new { status = "error", message = "กรุณาระบุ Theater" });
                //   List<Phone> phonelistdetail = GetListDetails(int);
                // Console.WriteLine(phonelistdetail);
                // List<Phone> data = new List<Phone>();
                string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
                SqlConnection con = new SqlConnection(constring);

                // SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand();
                //  SqlDataReader dr;
                con.Open();
                //  SqlCommand cmd = new SqlCommand("update Phone SET STATUS = @STATUS  where ID = " + Convert.ToInt32(Id) + "", con);
                cmd.Connection = con;
                if (data.Id != 0) {
                    //edit
                    if(data.Name == data.Namee && data.Section == data.Sectione && data.Position == data.Positione && data.Tel == data.Tele && data.Note == data.Notee && data.Theater == data.Theatere)
                        return Json(new { status = "error", message = "ไม่พบการแก้ไขข้อมูล" });
                    cmd.CommandText = "update Phone SET SECTION = @SECTION, USERNAME = @USERNAME, POSITION = @POSITION, TEL = @TEL, NOTE = @NOTE, THEATER = @THEATER  where ID = @ID";
                    //where
                    cmd.Parameters.AddWithValue("@ID", data.Id);
                    //condition
                    cmd.Parameters.AddWithValue("@SECTION", data.Section);
                    cmd.Parameters.AddWithValue("@USERNAME", data.Name);
                    cmd.Parameters.AddWithValue("@POSITION", data.Position);
                    cmd.Parameters.AddWithValue("@TEL", data.Tel);
                    if (string.IsNullOrEmpty(data.Note))
                    {
                        cmd.Parameters.AddWithValue("@NOTE", "***New Update");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NOTE", data.Note);
                    }
                    cmd.Parameters.AddWithValue("@THEATER",data.Theater);
                } else {
                    //save

                    cmd.CommandText = "INSERT INTO  Phone (SECTION, USERNAME, POSITION, TEL, NOTE, STATUS, THEATER) VALUES(@SECTION, @USERNAME, @POSITION, @TEL, @NOTE, @STATUS, @THEATER)";

                    cmd.Parameters.AddWithValue("@SECTION", data.Section);
                    cmd.Parameters.AddWithValue("@USERNAME", data.Name);
                    cmd.Parameters.AddWithValue("@POSITION", data.Position);
                    cmd.Parameters.AddWithValue("@TEL", data.Tel);
                    cmd.Parameters.AddWithValue("@NOTE", data.Note);
                    cmd.Parameters.AddWithValue("@STATUS", true);
                    cmd.Parameters.AddWithValue("@THEATER", data.Theater);
                }

             
              
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();


               // return Json(data, JsonRequestBehavior.AllowGet);
                //save customer
                return Json(new { status = "success", message = "successfully" });
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                Response.StatusDescription = ex.Message;
                //to do: log error
                //  throw new HttpResponseException(HttpStatusCode.NotFound);
                // return Request.CreateErrorResponse(HttpStatusCode.NotFound,ex.Message);
                //   return NotFound("Customer does not have any account");
                return Json(new { status = "error", message = ex.Message});
            }
            
        }
        private List<Phone> GetListDetails(string section)
        {
            List<Phone> data = new List<Phone>();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT ID, SECTION, USERNAME, POSITION, TEL, NOTE, THEATER FROM Phone where STATUS = 'true' AND SECTION ='" + section + "'", con);
     
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Phone ci = new Phone();
                ci.Id = Convert.ToInt32(rdr["ID"]);
                ci.Section = rdr[1].ToString();
                ci.Name = rdr[2].ToString();
                ci.Position = rdr[3].ToString();
                ci.Tel = rdr[4].ToString();
                ci.Note = rdr[5].ToString();
                ci.Theater = Convert.ToInt32(rdr[6]);
                data.Add(ci);
            }
            con.Close();
            Console.WriteLine(data);
            return data;
        }

        public ActionResult GetMethod()
        {
            List<Phone> phonelistsection = GetListSection();
            List<Phone> phonelistdetail = GetListDetail();
            List<Phone> phonelistdetail1 = GetListDetail();
            Console.WriteLine(phonelistsection);
            Console.WriteLine(phonelistdetail);

            foreach (Phone p in phonelistsection)
            {
                p.Lista = phonelistdetail.Where(x => x.Section == p.Section).ToList();
                p.Lista1 = phonelistdetail1.Where(x => x.Section == p.Section).ToList();
            }

            var ff = from a in phonelistsection
                     select new {
                         a,
                         Lista = (from v in phonelistdetail
                                    where v.Section == a.Section
                                    select v).ToList(),
              };

            Console.WriteLine(phonelistsection);
            return Json(phonelistsection, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMethod1()
        {
            List<Phone> phonelistsection = GetListSection();
            List<Phone> phonelistdetail = GetListDetail();
            List<Phone> phonelistdetail1 = GetListDetail();
            Console.WriteLine(phonelistsection);
            Console.WriteLine(phonelistdetail);

            foreach (Phone p in phonelistsection)
            {
                p.Lista = phonelistdetail.Where(x => x.Section == p.Section).ToList();
                p.Lista1 = phonelistdetail1.Where(x => x.Section == p.Section).ToList();
            }

            var ff = from a in phonelistsection
                     select new
                     {
                         a,
                         Lista = (from v in phonelistdetail
                                  where v.Section == a.Section
                                  select v).ToList(),
                     };

            Console.WriteLine(phonelistsection);
            return Json(ff, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMethods()
        {
            List<Phone> phonelistdetail = GetListDetail();
    

            Console.WriteLine(phonelistdetail);
            return Json(phonelistdetail, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetTest()
        {
            Phone ii = GetSection();
            List<Phone> phonelistdetail = GetListDetail();

            ii.Lista = phonelistdetail.Where (x => x.Section == ii.Section).ToList();
            


            Console.WriteLine(ii);
            //Console.WriteLine(phonelistdetail);
            return Json(ii, JsonRequestBehavior.AllowGet);
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
                ci.Row = Convert.ToInt32(rdr["ID"]);
                ci.Id = Convert.ToInt32(rdr[0]);
                ci.Section = rdr[1].ToString();
                ci.Name = rdr[2].ToString();
                ci.Position = rdr[3].ToString();
                ci.Tel = rdr[4].ToString();
                ci.Note = rdr[5].ToString();
                ci.Theater = Convert.ToInt32(rdr[6]);
                data.Add(ci);
            }
            con.Close();
            return data;
        }
        private List<Phone> GetListSection()
        {
            List<Phone> data = new List<Phone>();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            SqlConnection con = new SqlConnection(constring);
            SqlCommand cmd = new SqlCommand("SELECT SECTION FROM Phone GROUP BY SECTION", con);

            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Phone ci = new Phone();
                ci.Section = rdr[0].ToString();
                data.Add(ci);
            }
            con.Close();
            return data;
        }

        private Phone GetSection()
        {
            Phone ci = new Phone();
            string constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constring))
            {
                SqlCommand cmd = new SqlCommand("SELECT ID, SECTION, USERNAME, POSITION, TEL, NOTE, THEATER FROM Phone where STATUS = 'true' and ID = 17 ", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ci.Row = Convert.ToInt32(rdr["ID"]);
                    ci.Id = Convert.ToInt32(rdr[0]);
                    ci.Section = rdr[1].ToString();
                    ci.Name = rdr[2].ToString();
                    ci.Position = rdr[3].ToString();
                    ci.Tel = rdr[4].ToString();
                    ci.Note = rdr[5].ToString();
                    ci.Theater = Convert.ToInt32(rdr[6]);
                }
                con.Close();
            }
            return ci;
        }
        private List<SelectListItem> GetCities()
        {
            var options = new List<SelectListItem>();
            var constring = ConfigurationManager.ConnectionStrings["TestDbContextConString"].ConnectionString;
            using (var c = new SqlConnection(constring))
            {
                using (var cmd = new SqlCommand("SELECT ID, USERNAME FROM Phone where STATUS = 'true'", c))
                {
                    c.Open();
                    var rdr = cmd.ExecuteReader();
                    options = new List<SelectListItem>();
                    while (rdr.Read())
                    {
                        var o = new SelectListItem
                        {
                            Value = rdr.GetInt32(rdr.GetOrdinal("ID")).ToString(),
                            Text = rdr.GetString(rdr.GetOrdinal("Description"))
                        };
                        options.Add(o);
                    }
                }
            }
            return options;
        }

    }
}
