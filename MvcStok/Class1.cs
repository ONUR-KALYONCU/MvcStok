using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcStok
{
    public class Class1
    {
        public IEnumerable<SelectListItem> Urunler { get; set; }

        public IEnumerable<SelectListItem> Kategoriler { get; set; }
        public IEnumerable<SelectListItem> AltKategoriler { get; set; }
        public IEnumerable<SelectListItem> Markalar { get; set; }
    }
}