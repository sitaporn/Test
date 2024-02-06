using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVC.Models
{
    public class Phone
    {
        public int Row { get; set; }
        public int Id { get; set; }
        public string Section { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Tel { get; set; }
        public string Note { get; set; }
        public string Sectione { get; set; }
        public int Theater { get; set; }
        public string Namee { get; set; }
        public string Positione { get; set; }
        public string Tele { get; set; }
        public string Notee { get; set; }
        public int Theatere { get; set; }
        public bool Status { get; set; }
        public List<SelectListItem> Cities { set; get; }
        public List<Phone> Lista { set; get; }
        public List<Phone> Lista1 { set; get; }
    }
}