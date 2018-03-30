using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.IO;

namespace Indesejaveis_webSite.Models
{
    public class NoticiaModel
    {
        public Int32 cod_noticia { get; set; }

        [Display(Name = "Titulo da Noticia: ")]
        [Required(ErrorMessage = "De um titulo a noticia.")]
        [MaxLength(50, ErrorMessage = "Numero maximo de caracters foi passado. (Max: 50)")]
        public String nom_titulo_noticia { get; set; }

        [Display(Name = "Descricao da Noticia: ")]
        [Required(ErrorMessage = "Digite o conteudo da noticia.")]
        [MaxLength(1000, ErrorMessage = "Numero maximo de caracters foi passado. (Max: 50)")]
        public String ds_noticia { get; set; }

        [Display(Name = "Imagem da Noticia: ")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif|.jepg)$", ErrorMessage = "Somente Imagens sao permitidas.")]
        public HttpPostedFileBase im_vitrine { get; set; }

        [Display(Name = "Tipos de Noticia: ")]
        //Se nao tiver um Required, ele usa uma mensagem automatica dizendo a obrigatoriedade do campo.
        [Required(ErrorMessage = "Selecione o tipo da noticia.")]
        public TipoNoticia ds_tipo_noticia { get; set; }

        public DateTime dat_noticia { get; set; }

        public Int32 cod_imagem { get; set; }
    }
    //Pra preencher o dropdown
    public enum TipoNoticia
    {
        Novidade, Apresentação, Festival
    }
}