using Indesejaveis_webSite.Models;
using System;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Indesejaveis_webSite.Repository;


namespace Indesejaveis_webSite.Controllers
{
    /*
     -Tentar passar imagem com c'odigo html quando for colocar descricao da imagem.
     -Separar metodo de validar imagem para poder utilizar ele no update.
         
         
         
         */
    public class NoticiaController : Controller
    {
        // GET: Noticia
        public ActionResult CadastroNoticia()
        {
            return View();
        }

        private NoticiaRepository _repositorio;

        [HttpPost]
        public ActionResult CadastroNoticia(NoticiaModel noticia)
        {

            string path = Server.MapPath("~/ImagensVitrine/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var ImageNull = noticia.im_vitrine as HttpPostedFileBase;

            if (ImageNull == null)
            {
                switch (noticia.ds_tipo_noticia.ToString())
                {
                    case "Novidade":
                        noticia.nom_imagem = "Novidade.jpg";
                        break;
                    case "Apresentação":
                        noticia.nom_imagem = "Apresentacao.jpg";
                        break;
                    case "Festival":
                        noticia.nom_imagem = "Festival.jpg";
                        break;
                }

                //Chama o metodo que realiza o insert na base.
                InNoticia(noticia);

                ViewBag.MessageCadastroSucesso += string.Format("Noticia Registrada com Sucesso.");
                //Limpa os campos do formulario, precisa estar antes do View().
                ModelState.Clear();
            }
            else
            {

                //Resgatando o nome da imagem para jogar no viewbag.
                string oldFileName = Path.GetFileName(noticia.im_vitrine.FileName);

                //Gera um indenficador QUASE unico para renomear a imagem e salva-la na pagina.
                string newFileName = Guid.NewGuid().ToString("N");

                //chama o metodo de validacao de imagens que retorna true ou false.
                if (validarImagem(noticia.im_vitrine))
                {

                    //resgata o formato da imagem para renomear com a extensao.
                    string[] formatoImagem = noticia.im_vitrine.ContentType.Split('/');

                    //salva a imagem na pasta determinada no path.
                    noticia.im_vitrine.SaveAs(path + newFileName + "." + formatoImagem[1]);

                    noticia.nom_imagem = newFileName + "." + formatoImagem[1];

                    //Chama o metodo que realiza o insert na base.
                    InNoticia(noticia);

                    ViewBag.MessageSucesso += string.Format("<b>{0}</b> Sucesso ao subir a imagem.<br />", oldFileName);
                    ViewBag.MessageCadastroSucesso += string.Format("Noticia Registrada com Sucesso.");
                    //Limpa os campos do formulario, precisa estar antes do View().
                    ModelState.Clear();
                }
                else
                {
                    /*ViewBag usa o {0}, {1}, {2} e assim vai, e voce vai colocando os campos no final de acordo com a ordem que deseja. 
                    Ex: fileName, isso faz concatenar a mensagem com variaveis.*/
                    ViewBag.MessageErro += string.Format("Somente arquivos .jpeg .jpg .png .gif. com Resolucao Max 2mb 684x312.");
                }
            }
            return View();
        }

        public bool validarImagem(HttpPostedFileBase imagem)
        {
            try { 
                
                //resgata o formato do arquivo.
                string[] tipoImagem = imagem.ContentType.Split('/');

                //resgata o tamanho do arquivo.
                int tamImagem = imagem.ContentLength;

                //Convert a imagem em bitmap para resgatar altura e largura.
                Bitmap bitmapImage = new Bitmap(imagem.InputStream, false);

                int weight = bitmapImage.Height;
                int height = bitmapImage.Width;

                //Validacao que deve ser uma imagem, com as dimensoes de 684x312 e deve ser menos que 2mb.
                if (imagem != null && tipoImagem[0].Contains("image") && weight <= 312 && height <= 684 && tamImagem <= 2000000)
                {
                    if ((tipoImagem[1].Contains("jpeg") || tipoImagem[1].Contains("jpg") || tipoImagem[1].Contains("png") || tipoImagem[1].Contains("gif")))
                    {
                        return true;
                    }
                }
                return false;
                }
            catch (Exception)
            {
                return false;
            }
        }

        public ActionResult ListarNoticias()
        {
            _repositorio = new NoticiaRepository();

            _repositorio.ListarNoticias();

            ModelState.Clear();

            return View(_repositorio.ListarNoticias());
        }

        public ActionResult InNoticia()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InNoticia(NoticiaModel noticia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repositorio = new NoticiaRepository();

                    if (_repositorio.InserirNoticia(noticia))
                    {
                        ViewBag.Mesagem = "Noticia Cadastrada com Sucesso!";
                    }
                }

                return View();
            }
            catch (Exception e)
            {
                return View("ListarNoticias");
            }
           
        }

        public ActionResult UpNoticia(int codNoticia)
        {
            _repositorio = new NoticiaRepository();
            return View(_repositorio.ListarNoticias().Find(l => l.cod_noticia.Equals(codNoticia)));
        }

        [HttpPost]
        public ActionResult UpNoticia(int codNoticia, NoticiaModel noticia)
        {
            try
            {
                _repositorio = new NoticiaRepository();
                _repositorio.AtualizarNoticia(noticia);
                return RedirectToAction("ListarNoticias");
            }
            catch (Exception e)
            {
                return View("ListarNoticias");
            }
        }

        public ActionResult DeNoticia(int codNoticia)
        {
            try
            {
                _repositorio = new NoticiaRepository();
                if(_repositorio.DeletarNoticia(codNoticia))
                {
                    ViewBag.Mesagem = "Noticia Cadastrada com Sucesso!";
                }
                return RedirectToAction("ListarNoticias");
            }
            catch (Exception e)
            {
                return View("ListarNoticias");
            }
        }
    }
}