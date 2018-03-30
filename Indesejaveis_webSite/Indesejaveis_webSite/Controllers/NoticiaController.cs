using Indesejaveis_webSite.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Indesejaveis_webSite.Controllers
{
    public class NoticiaController : Controller
    {
        // GET: Noticia
        public ActionResult CadastroNoticia()
        {
            return View();
        }

        #region Mensagem

        [HttpPost]
        public ActionResult CadastroNoticia(NoticiaModel noticia)
        {
            string path = Server.MapPath("~/ImagensVitrine/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (noticia.im_vitrine != null)
            {
                string fileName = Path.GetFileName(noticia.im_vitrine.FileName);
                noticia.im_vitrine.SaveAs(path + fileName);
                ViewBag.Message += string.Format("<b>{0}</b> uploaded.<br />", fileName);
            }

            return View();
        }

        #endregion

    }
}