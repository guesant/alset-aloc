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
    /// Lógica interna para CadastrarFornecedor.xaml
    /// </summary>
    public partial class CadastrarFornecedor : Window
        {
        private long? _id;

        public CadastrarFornecedor()
            {
            InitializeComponent();
            }

        public CadastrarFornecedor(long? id = null)
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

            //#####################################################################
            var endereco = new Endereco();
            var fornecedorAtual = _id != null ? (new FornecedorDAO()).GetById((int) _id) : null;

            if (fornecedorAtual != null)
                {

                int? enderecoId = (int?) fornecedorAtual.EnderecoId;


                if (enderecoId != null)
                    {
                    var enderecoDao = new EnderecoDAO();
                    endereco = enderecoDao.GetById((int) enderecoId);
                    }
                }

            if (cbEnderecoPais.SelectedValue == null)
                {
                cbEnderecoPais.SelectedIndex = 1;
                }

            ComboBoxItem typeItem = (ComboBoxItem) cbEnderecoPais.SelectedValue;
            endereco.Pais = typeItem.Content.ToString();



            endereco.CodigoPostal = txtEnderecoCodigoPostal.Text;
            endereco.UF = txtEnderecoUF.Text;
            endereco.Cidade = txtEnderecoCidade.Text;

            endereco.Rua = txtEnderecoRua.Text;
            endereco.Numero = Convert.ToInt32(txtEnderecoNumero.Text);
            endereco.Bairro = txtEnderecoBairro.Text;
            endereco.Complemento = txtEnderecoComplemento.Text;


            var fornecedor = new Fornecedor();

            if (fornecedorAtual != null)
                {
                fornecedor = (new FornecedorDAO()).GetById((int) fornecedorAtual.Id);
                }

            fornecedor.CNPJ = txtFornecedorCnpj.Text;
            fornecedor.RazaoSocial = txtFornecedorRazaoSocial.Text;
            fornecedor.NomeFantasia = txtFornecedorNomeFantasia.Text;
            fornecedor.Email = txtFornecedorEmail.Text;
            fornecedor.Telefone = txtFornecedorTelefone.Text;


            if (endereco.Id > 0)
                {
                var enderecoDao = new EnderecoDAO();
                enderecoDao.Update(endereco);
                }
            else
                {
                var enderecoDao = new EnderecoDAO();
                enderecoDao.Insert(endereco);
                }

            fornecedor.EnderecoId = endereco.Id;


            if (fornecedor.Id == 0)
                {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Insert(fornecedor);
                MessageBox.Show("Fornecedor cadastrado com sucesso!" , "ALOC - Alset");
                }
            else
                {
                var fornecedorDAO = new FornecedorDAO();
                fornecedorDAO.Update(fornecedor);
                MessageBox.Show($"Fornecedor {txtFornecedorNomeFantasia.Text} atualizado com sucesso!" , "ALOC - Alset");
                }

            this.Close();
            }


        private void FillForm()
            {
            try
                {
                if (_id != 0 && _id != null)
                    {
                    var fornecedorDAO = new FornecedorDAO();
                    var _fornecedor = fornecedorDAO.GetById((int) _id);
                    var enderecoDAO = new EnderecoDAO();

                    txtFornecedorCnpj.Text = _fornecedor.CNPJ;
                    txtFornecedorRazaoSocial.Text = _fornecedor.RazaoSocial;
                    txtFornecedorNomeFantasia.Text = _fornecedor.NomeFantasia;
                    txtFornecedorEmail.Text = _fornecedor.Email;
                    txtFornecedorTelefone.Text = _fornecedor.Telefone;

                    if (_fornecedor.EnderecoId != null)
                        {
                        var _endereco = enderecoDAO.GetById((int) _fornecedor.EnderecoId);


                        txtEnderecoBairro.Text = _endereco.Bairro;
                        txtEnderecoCidade.Text = _endereco.Cidade;
                        txtEnderecoCodigoPostal.Text = _endereco.CodigoPostal;
                        txtEnderecoComplemento.Text = _endereco.Complemento;
                        txtEnderecoNumero.Text = _endereco.Numero.ToString();
                        txtEnderecoRua.Text = _endereco.Rua;
                        txtEnderecoUF.Text = _endereco.UF;
                        cbEnderecoPais.SelectedValue = _endereco.Pais;
                        }
                    }
                }
            catch (Exception ex)
                {
                MessageBox.Show($"Erro! {ex}");
                }
            }

        private void txtFornecedorRazaoSocial_TextChanged(object sender , TextChangedEventArgs e)
            {

            }
        }
    }

