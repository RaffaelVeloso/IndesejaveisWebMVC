using System;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.Web.Mvc;

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
        public HttpPostedFileBase im_vitrine { get; set; }

        [Display(Name = "Tipos de Noticia: ")]
        //Se nao tiver um Required, ele usa uma mensagem automatica dizendo a obrigatoriedade do campo.
        [Required(ErrorMessage = "Selecione o tipo da noticia.")]
        public TipoNoticia ds_tipo_noticia { get; set; }

        public DateTime dat_noticia { get; set; }

        public Int32 cod_imagem { get; set; }
    }
    //Pra preencher o dropdownlist
    public enum TipoNoticia
    {
        Novidade, Apresentação, Festival
    }

}