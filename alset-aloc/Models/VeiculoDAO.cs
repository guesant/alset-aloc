using alset_aloc.Database;
using alset_aloc.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alset_aloc.Models
{
    class VeiculoDAO : IDAO<Veiculo>
    {
        private Conexao conn;

        public VeiculoDAO()
        {
            conn = new Conexao();
        }

        public void Delete(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    DELETE FROM veiculo
                    WHERE (id_vei = @idVei)
                ";

                query.Parameters.AddWithValue("@idVei", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veiculo não foi encontrado. Verifique e tente novamente.");
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

        public Veiculo GetById(int id)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_vei, modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    FROM veiculo
                    WHERE (id_vei = @idVei)
                    ;
                ";

                query.Parameters.AddWithValue("@idVei", id);

                MySqlDataReader dtReader = query.ExecuteReader();

                Veiculo veiculo = new Veiculo();

                while (dtReader.Read())
                {

                    veiculo.Id = Convert.ToInt64(dtReader["id_vei"].ToString());

                    veiculo.Modelo = dtReader["modelo_vei"].ToString()!;
                    veiculo.Marca = dtReader["marca_vei"].ToString()!;
                    veiculo.Ano = Convert.ToInt32(dtReader["ano_vei"].ToString()!);
                    veiculo.Placa = dtReader["placa_vei"].ToString()!;
                    veiculo.NumeroChassi = dtReader["numero_chassi_vei"].ToString()!;
                    veiculo.Cor = dtReader["cor_vei"].ToString()!;
                    veiculo.DataCompra = Convert.ToDateTime(dtReader["data_compra_vei"].ToString()!);
                    veiculo.Descricao = dtReader["descricao_vei"].ToString()!;

                    return veiculo;
                }

                throw new Exception("Não foi possível encontrar o veículo com o id fornecido. Verifique e tente novamente.");
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

        public void Insert(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    INSERT INTO 
                        veiculo (modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    VALUES
                        (@modelo, @marca, @ano, @placa, @numeroChassi, @cor, @dataCompra, @descricao)
                ";

                query.Parameters.AddWithValue("@modelo", t.Modelo);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@ano", t.Ano);
                query.Parameters.AddWithValue("@placa", t.Placa);
                query.Parameters.AddWithValue("@numeroChassi", t.NumeroChassi);
                query.Parameters.AddWithValue("@cor", t.Cor);
                query.Parameters.AddWithValue("@dataCompra", t.DataCompra);
                query.Parameters.AddWithValue("@descricao", t.Descricao);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veículo não foi cadastrado. Verifique e tente novamente.");
                }

                long veiculoId = query.LastInsertedId;

                t.Id = veiculoId;
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

        public List<Veiculo> List()
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    SELECT (id_vei, modelo_vei, marca_vei, ano_vei, placa_vei, numero_chassi_vei, cor_vei, data_compra_vei, descricao_vei)
                    FROM veiculo
                    ;
                "
                ;

                MySqlDataReader dtReader = query.ExecuteReader();

                List<Veiculo> listaDeRetorno = new List<Veiculo>();//Crie uma lista de Cliente

                while (dtReader.Read())
                {
                    Veiculo veiculo = new Veiculo();

                    veiculo.Id = Convert.ToInt64(dtReader["id_end"].ToString());

                    veiculo.Modelo = dtReader["modelo_vei"].ToString()!;
                    veiculo.Marca = dtReader["marca_vei"].ToString()!;
                    veiculo.Ano = Convert.ToInt32(dtReader["ano_vei"].ToString()!);
                    veiculo.Placa = dtReader["placa_vei"].ToString()!;
                    veiculo.NumeroChassi = dtReader["numero_chassi_vei"].ToString()!;
                    veiculo.Cor = dtReader["cor_vei"].ToString()!;
                    veiculo.DataCompra = Convert.ToDateTime(dtReader["data_compra_vei"].ToString()!);
                    veiculo.Descricao = dtReader["descricao_vei"].ToString()!;

                    listaDeRetorno.Add(veiculo);
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

        public void Update(Veiculo t)
        {
            try
            {
                var query = conn.Query();

                query.CommandText = @"
                    UPDATE veiculo
                    SET
                        modelo_vei = @modelo,
                        marca_vei = @marca,
                        ano_vei = @ano,
                        placa_vei = @placa,
                        numero_chassi_vei = @numeroChassi,
                        cor_vei = @cor,
                        data_compra_vei = @dataCompra,
                        descricao_vei = @descricao
                    WHERE (id_vei = @idVei);
                ";

                query.Parameters.AddWithValue("@modelo", t.Modelo);
                query.Parameters.AddWithValue("@marca", t.Marca);
                query.Parameters.AddWithValue("@ano", t.Ano);
                query.Parameters.AddWithValue("@placa", t.Placa);
                query.Parameters.AddWithValue("@numeroChassi", t.NumeroChassi);
                query.Parameters.AddWithValue("@cor", t.Cor);
                query.Parameters.AddWithValue("@dataCompra", t.DataCompra);
                query.Parameters.AddWithValue("@descricao", t.Descricao);

                query.Parameters.AddWithValue("@idVei", t.Id);

                var result = query.ExecuteNonQuery();

                if (result == 0)
                {
                    throw new Exception("O veículo não foi alterado. Verifique e tente novamente.");
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
