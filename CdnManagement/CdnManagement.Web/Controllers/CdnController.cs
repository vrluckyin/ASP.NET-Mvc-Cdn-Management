using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CdnManagement.Web.Models;
namespace CdnManagement.Web.Controllers
{
    public class CdnController : Controller
    {
        //
        // GET: /Cdn/

        public ActionResult View1()
        {
            Employee m = new Employee();
            m.FirstName = "Vips";
            m.LastName = "Patel";
            return View(m);
        }
        public ActionResult View2()
        {
            Employee m = new Employee();
            m.FirstName = "Vips";
            m.LastName = "Patel";
            return View(m);
        }
        public ActionResult View3()
        {
            Employee m = new Employee();
            m.FirstName = "Vips";
            m.LastName = "Patel";
            return View(m);
        }

    }
}
