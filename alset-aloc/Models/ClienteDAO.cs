using alset_aloc.Database;
using alset_aloc.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace alset_aloc.Models
{
    class ClienteDAO : IDAO<Cliente>
    {
        private Conexao conn;

        public ClienteDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Cliente t)
        {
            throw new NotImplementedException();
        }

        public Cliente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Cliente t)
        {
            try
            {
                var query = conn.Query();
                
                query.CommandText = @"
                    INSERT INTO 
                        cliente (nome_cli, data_nascimento_cli, cpf_cli, rg_cli, cnh_cli, email_cli, telefone_cli, genero_cli, id_end_fk)
                    VALUES
                        (@nome, @dataNascimento, @cpf, @rg, @cnh, @email, @telefone, @genero, @idEnd)
                ";

                query.Parameters.AddWithValue("@nome", t.Nome);
                query.Parameters.AddWithValue("@dataNascimento", t.DataNascimento);
                query.Parameters.AddWithValue("@cpf", t.CPF);
                query.Parameters.AddWithValue("@rg", t.RG);
                query.Parameters.AddWithValue("@cnh", t.CNH);
                query.Parameters.AddWithValue("@email", t.Email);
                query.Parameters.AddWithValue("@telefone", t.Telefone);
                query.Parameters.AddWithValue("@genero", t.Genero);


                query.Parameters.AddWithValue("@genero", t.Genero);


                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O cliente não foi cadastrado. Verifique e tente novamente.");
                }
                

                // long compraId = query.LastInsertedId;
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

        public List<Cliente> List()
        {
            throw new NotImplementedException();
        }

        public void Update(Cliente t)
        {
            throw new NotImplementedException();
        }
    }
}
