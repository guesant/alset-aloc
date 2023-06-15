using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace alset_aloc.Models
{
    class UsuarioDAO : IDAO<Usuario>
    {
        private Conexao conn;

        public UsuarioDAO()
        {
            conn = new Conexao();
        }

        static Usuario ParseReader(MySqlDataReader dtReader)
        {
            Usuario usuario = new Usuario();

            usuario.Id = dtReader.GetInt64("id_usua");

            usuario.Username = dtReader.GetString("usuario_usua");
            usuario.Senha = dtReader.GetString("senha_usua");

            var rawFuncionarioId = dtReader.GetOrdinal("id_func_fk");
            usuario.FuncionarioId = dtReader.IsDBNull(rawFuncionarioId) ? null : dtReader.GetInt64(rawFuncionarioId);

            return usuario;
        }

        static void BindQuery(Usuario t, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@usuario", t.Username);
            query.Parameters.AddWithValue("@senha", t.Senha);
            query.Parameters.AddWithValue("@funcionarioId", t.FuncionarioId);            
        }

        static void BindQueryId(long id, MySqlCommand query)
        {
            query.Parameters.AddWithValue("@idUsua", id);
        }

        public void Delete(Usuario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM usuario
                    WHERE (id_usu = @idUsua)
                ";

                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O usuario não foi encontrado. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public Usuario GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_usua, usuario_usua, senha_usua, id_func_fk)
                    FROM usuario
                    WHERE (id_usua = @idUsua)
                    ;
                ";

                BindQueryId(id, query);

                MySqlDataReader dtReader = query.ExecuteReader();


                while (dtReader.Read())
                {
                    Usuario usuario = ParseReader(dtReader);
                    return usuario;
                }

                throw new Exception("Não foi possível encontrar o usuário com o id fornecido. Verifique e tente novamente.");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Insert(Usuario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        usuario (usuario_usua, senha_usua, id_func_fk)
                    VALUES
                        (@usuario, @senha, @funcionarioId)
                ";

                BindQuery(t, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O usuario não foi cadastrado. Verifique e tente novamente.");
                }

                t.Id = query.LastInsertedId;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public List<Usuario> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_usua, usuario_usua, senha_usua, id_func_fk)
                    FROM usuario
                    ;
                ";


                MySqlDataReader dtReader = query.ExecuteReader();

                List<Usuario> listaDeRetorno = new List<Usuario>();

                while (dtReader.Read())
                {
                    Usuario usuario = ParseReader(dtReader);
                    listaDeRetorno.Add(usuario);
                }

                return listaDeRetorno;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        public void Update(Usuario t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE usuario
                    SET
                        usuario_usua = @usuario,
                        senha_usua = @senha,
                        id_func_fk = @funcionarioId
                    WHERE (id_usua = @idUsua);
                ";

                BindQuery(t, query);
                BindQueryId(t.Id, query);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O usuario não foi alterado. Verifique e tente novamente.");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
