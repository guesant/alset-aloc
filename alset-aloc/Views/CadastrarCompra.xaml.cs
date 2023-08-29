using alset_aloc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace alset_aloc.Views
    {
    /// <summary>
    /// Lógica interna para CadastrarVeiculo.xaml
    /// </summary>
    public partial class CadastrarCompra : Window
        {
        private long? _id;

        public CadastrarCompra()
            {
            InitializeComponent();
            }
        public CadastrarCompra(long? id = null)
            {

            if (id <= 0)
                {
                id = null;
                }

            _id = id;

            InitializeComponent();

            if (id != null)
                {
                FillForm();
                }
            }

        private void btCadastrar_Click(object sender , RoutedEventArgs e)
            {
            var fornecedor = new Fornecedor();
            var produto = new Produto();

            var compraAtual = _id != null ? (new CompraDAO()).GetById((int) _id) : null;

            if (compraAtual != null)
                {

                int? fornecedorId = (int?) compraAtual.FornecedorId;
                int? produtoId = (int?) compraAtual.ProdutoId;


                if (fornecedorId != null)
                    {
                    var fornecedorDAO = new FornecedorDAO();
                    fornecedor = fornecedorDAO.GetById((int) fornecedorId);
                    }

                if (produtoId != null)
                    {
                    var produtoDAO = new ProdutoDAO();
                    produto = produtoDAO.GetById((int) produtoId);
                    }
                }
            var compra = new Compra();

            if (compraAtual != null)
                {
                compra = (new CompraDAO()).GetById((int) compraAtual.Id);
                }

            if (txtProduto.Text.Length > 0)
                {
                compra.ProdutoId = Convert.ToInt64(txtProduto.Text);
                }

            if (txtFornecedor.Text.Length > 0)
                {
                compra.FornecedorId = Convert.ToInt64(txtFornecedor.Text);
                }


            compra.Quantidade = Convert.ToInt32(txtQuantidade.Text);
            compra.DataCompra = txtDataCompra.DisplayDate;
            compra.NumeroNota = txtNota.Text;


            if (produto.Id > 0)
            {
                var produtoDAO = new ProdutoDAO();
                produtoDAO.Update(produto);
            }
            else
            {
                var produtoDAO = new ProdutoDAO();
                produtoDAO.Insert(produto);
            }

            if (fornecedor.Id > 0)
            {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Update(fornecedor);
            }
            else
            {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Insert(fornecedor);
            }

            // veiculo.CargoId = txtveiculoNome.Text;

            if (compra.Id == 0)
                {
                var compraDAO = new CompraDAO();
                compraDAO.Insert(compra);
                MessageBox.Show("Compra cadastrado com sucesso!" , "ALOC - Alset");
                }
            else
                {
                var compraDAO = new CompraDAO();
                compraDAO.Update(compra);
                MessageBox.Show($"Compra {txtProduto.Text} atualizado com sucesso!" , "ALOC - Alset");
                }

            this.Close();
            }

        private void FillForm()
            {
            try
                {
                if (_id != 0 && _id != null)
                    {
                    var compraDAO = new CompraDAO();
                    var _compra = compraDAO.GetById((int) _id);
                    var produtoDAO = new ProdutoDAO();
                    var fornecedorDAO = new FornecedorDAO();

                    txtQuantidade.Text = Convert.ToString(_compra.Quantidade);
                    txtDataCompra.SelectedDate = _compra.DataCompra;
                    txtNota.Text = _compra.NumeroNota;

                    if (_compra.Id != null)
                        {
                        var _produto = produtoDAO.GetById((int) _compra.Id);

                        txtProduto.Text = Convert.ToString(_produto.Id);

                        }
                    if (_compra.Id != null)
                        {
                        var _fornecedor = fornecedorDAO.GetById((int) _compra.Id);

                        txtFornecedor.Text = Convert.ToString(_fornecedor.Id);

                        }
                    }


                }
            catch (Exception ex)
                {
                MessageBox.Show($"Erro! {ex}");
                }
            }

        }
    }
