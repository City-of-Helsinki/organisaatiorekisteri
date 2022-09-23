using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
//using System.Web.Http;
//using System.Web.Mvc;

namespace OrganizationRegister.Api.Tunnistamo
{
    [RoutePrefix("v1/Tunnistamo")]
    public class TunnistamoController : ApiController
    {
        public void W(object s)
        {
            System.Diagnostics.Trace.WriteLine("[" + this.GetType() + "][" + this.GetHashCode() + "] " + s);
        }

        // GET: Tunnistamo
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public IHttpActionResult Index()
        {
            return Ok(DateTime.Now);
        }

        [HttpPost]
        [Route("LoginUrlTest")]
        public IHttpActionResult LoginUrlTest()
        {
            W("LoginUrlTest");
            return Ok("https://www.google.fi");
        }

        [HttpPost]
        [Route("LoginUrl")]
        public IHttpActionResult LoginUrl()
        {
            W("LoginUrl");
            return Ok("https://www.google.fi");
        }

        [HttpPost]
        [Route("TunnistamoLogin")]
        public IHttpActionResult TunnistamoLogin()
        {
            W("TunnistamoLogin");
            return Ok("success");
        }
    }
}