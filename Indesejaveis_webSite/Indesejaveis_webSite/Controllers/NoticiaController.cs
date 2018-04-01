using Indesejaveis_webSite.Models;
using System;
using System.Drawing;
using System.IO;
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

        [HttpPost]
        public ActionResult CadastroNoticia(NoticiaModel noticia)
        {

            string path = Server.MapPath("~/ImagensVitrine/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

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
    }
}