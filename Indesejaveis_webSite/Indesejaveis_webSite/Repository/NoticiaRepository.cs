using Indesejaveis_webSite.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;

namespace Indesejaveis_webSite.Repository
{
    public class NoticiaRepository
    {

        private SqlConnection _con;

        private void Connection()
        {
            string baseString = ConfigurationManager.ConnectionStrings["stringConexao"].ToString();
            _con = new SqlConnection(baseString);
        }

        public bool InserirNoticia(NoticiaModel noticiaObj)
        {
            try
            {

                Connection();

                using (SqlCommand command = new SqlCommand("in_tb_noticia", _con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nom_titulo_noticia", noticiaObj.nom_titulo_noticia);
                    command.Parameters.AddWithValue("@ds_noticia", noticiaObj.ds_noticia);
                    command.Parameters.AddWithValue("@ds_tipo_noticia", noticiaObj.ds_tipo_noticia.ToString());
                    command.Parameters.AddWithValue("@im_vitrine", noticiaObj.nom_imagem);

                    _con.Open();

                    command.ExecuteNonQuery();

                }

                    _con.Close();

                    return true;

            }
            catch (Exception e)
            {
                _con.Close();
                throw new Exception("Erro ao registrar Noticia. Error: " + e);
            }
        }

        public bool AtualizarNoticia(NoticiaModel noticiaObj)
        {

            try
            {

                Connection();

                using (SqlCommand command = new SqlCommand("up_tb_noticia", _con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cod_noticia", noticiaObj.cod_noticia);
                    command.Parameters.AddWithValue("@nom_titulo_noticia", noticiaObj.nom_titulo_noticia);
                    command.Parameters.AddWithValue("@ds_noticia", noticiaObj.ds_noticia);
                    command.Parameters.AddWithValue("@ds_tipo_noticia", noticiaObj.tipo_noticia_selected);
                    command.Parameters.AddWithValue("@im_vitrine", noticiaObj.nom_imagem);

                    _con.Open();

                    command.ExecuteNonQuery();

                }

                _con.Close();

                return true;

            }
            catch (Exception e)
            {
                _con.Close();
                throw new Exception("Erro ao atualizar Noticia. Error: " + e);
            }

        }

        public NoticiaModel BuscarNoticia (string nomTituloNoticia = null, string dsNoticia = null, string dsTipoNoticia = null)
        {
            try
            {
                NoticiaModel noticiaObj = new NoticiaModel();

                using (SqlCommand command = new SqlCommand("se_tb_noticia", _con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@nom_titulo_noticia", nomTituloNoticia);
                    command.Parameters.AddWithValue("@ds_noticia", dsNoticia);
                    command.Parameters.AddWithValue("@ds_tipo_noticia", dsTipoNoticia);

                    _con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    noticiaObj.cod_noticia = Convert.ToInt32(reader["cod_noticia"]);
                    noticiaObj.nom_titulo_noticia = Convert.ToString(reader["nom_titulo_noticia"]);
                    noticiaObj.tipo_noticia_selected = Convert.ToString(reader["ds_tipo_noticia"]);
                    noticiaObj.ds_noticia = Convert.ToString(reader["ds_noticia"]);
                    noticiaObj.dat_noticia = Convert.ToDateTime(reader["dat_noticia"]);
                    noticiaObj.cod_imagem = Convert.ToInt32(reader["cod_imagem"]);
                    //im_vitrine = nom_imagem
                    noticiaObj.nom_imagem = Convert.ToString(reader["nom_imagem"]);

                    _con.Close();

                    return noticiaObj;

                }
            }
            catch(Exception e)
            {
                _con.Close();
                throw new Exception("Erro ao buscar a Noticia. Error: " + e);
            }
        }

        public List<NoticiaModel> ListarNoticias()
        {
            
            List<NoticiaModel> listaNoticias = new List<NoticiaModel>();

            try
            {

                Connection();

                using (SqlCommand command = new SqlCommand("se_all_tb_noticia", _con))
                {

                    command.CommandType = CommandType.StoredProcedure;

                    _con.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        NoticiaModel noticia = new NoticiaModel()
                        {

                            cod_noticia = Convert.ToInt32(reader["cod_noticia"]),
                            nom_titulo_noticia = Convert.ToString(reader["nom_titulo_noticia"]),
                            tipo_noticia_selected = Convert.ToString(reader["ds_tipo_noticia"]),
                            ds_noticia = Convert.ToString(reader["ds_noticia"]),
                            dat_noticia = Convert.ToDateTime(reader["dat_noticia"]),
                            cod_imagem = Convert.ToInt32(reader["cod_imagem"]),
                            //im_vitrine = nom_imagem
                            nom_imagem = Convert.ToString(reader["nom_imagem"])

                        };

                        listaNoticias.Add(noticia);
                    }

                    _con.Close();

                    return listaNoticias;
                }
            }
            catch (Exception e)
            {
                _con.Close();
                throw new Exception("Erro ao buscar Noticias. Error: " + e);
            }

        }

        public bool DeletarNoticia(int codNoticia)
        {

            try
            {

                Connection();

                using (SqlCommand command = new SqlCommand("de_tb_noticia", _con))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@cod_noticia", codNoticia);

                    _con.Open();

                    command.ExecuteNonQuery();

                }

                _con.Close();

                return true;

            }
            catch (Exception e)
            {
                _con.Close();
                throw new Exception("Erro ao deletar Noticia. Error: " + e);
            }

        }

    }
}