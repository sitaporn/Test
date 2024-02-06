using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVC.Models
{
    public class Fire
    {
        public int Row { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Names { get; set; }
        public int Ftype { get; set; }
        public int Fsize { get; set; }
        public int Fplace { get; set; }
        public string Ftypes { get; set; }
        public string Fsizes { get; set; }
        public string Fplaces { get; set; }
        public DateTime? StartD { get; set; }
        public DateTime? EndD { get; set; }
        public string StartDs { get; set; }
        public string EndDs { get; set; }
        public DateTime? CheckD { get; set; }
        public bool D1 { get; set; }
        public bool D2 { get; set; }
        public bool D3 { get; set; }
        public bool D4 { get; set; }
        public bool D5 { get; set; }
        public string FDetail { get; set; }
        public bool FRemark { get; set; }
        public List<Phone> Lista { set; get; }
        public List<Phone> Lista1 { set; get; }
    }
}