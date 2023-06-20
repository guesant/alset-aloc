using alset_aloc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public CadastrarFornecedor()
        {
            InitializeComponent();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var enderecoDao = new EnderecoDAO();

            var endereco = new Endereco();

            ComboBoxItem typeItem = (ComboBoxItem) cbEnderecoPais.SelectedItem;
            endereco.Pais = typeItem.Content.ToString();

            endereco.CodigoPostal = txtEnderecoCodigoPostal.Text;
            endereco.UF = txtEnderecoUF.Text;
            endereco.Cidade = txtEnderecoCidade.Text;

            endereco.Rua = txtEnderecoRua.Text;
            endereco.Numero = Convert.ToInt32(txtEnderecoNumero.Text);
            endereco.Bairro = txtEnderecoBairro.Text;
            endereco.Complemento = txtEnderecoComplemento.Text;

            enderecoDao.Insert(endereco);


            var fornecedor = new Fornecedor();

            fornecedor.EnderecoId = endereco.Id;

            fornecedor.CNPJ = txtFornecedorCnpj.Text;

            fornecedor.RazaoSocial = txtFornecedorRazaoSocial.Text;
            fornecedor.NomeFantasia = txtFornecedorNomeFantasia.Text;
            fornecedor.Email = txtFornecedorEmail.Text;
            fornecedor.Telefone = txtFornecedorTelefone.Text;



            var fornecedorDao = new FornecedorDAO();

            fornecedorDao.Insert(fornecedor);

            MessageBox.Show("Forncedor cadastrado com sucesso!" , "ALOC - Alset");

            this.Close();
            }
    }
}
