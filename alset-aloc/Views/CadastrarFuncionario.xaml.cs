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
    /// Lógica interna para CadastrarFuncionario.xaml
    /// </summary>
    public partial class CadastrarFuncionario : Window
    {
        public CadastrarFuncionario()
        {
            InitializeComponent();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var enderecoDao = new EnderecoDAO();

            var endereco = new Endereco();

            ComboBoxItem typeItem = (ComboBoxItem)cbEnderecoPais.SelectedItem;
            endereco.Pais = typeItem.Content.ToString();

            endereco.CodigoPostal = txtEnderecoCodigoPostal.Text;
            endereco.UF = txtEnderecoUF.Text;
            endereco.Cidade = txtEnderecoCidade.Text;

            endereco.Rua = txtEnderecoRua.Text;
            endereco.Numero = Convert.ToInt32(txtEnderecoNumero.Text);
            endereco.Bairro = txtEnderecoBairro.Text;
            endereco.Complemento = txtEnderecoComplemento.Text;

            enderecoDao.Insert(endereco);


            var funcionario = new Funcionario();

            funcionario.EnderecoID = endereco.Id;

            funcionario.Nome = txtFuncionarioNome.Text;
            
            funcionario.DataNascimento = txtFuncionarioDataNascimento.DisplayDate;
            funcionario.Cpf = txtFuncionarioCpf.Text;
            funcionario.Rg = txtFuncionarioRg.Text;
            funcionario.CNH = txtFuncionarioCNH.Text;
            funcionario.Email = txtFuncionarioEmail.Text;
            funcionario.Telefone = txtFuncionarioTelefone.Text;
            funcionario.Genero = txtFuncionarioGenero.Text;
            
            // funcionario.CargoId = txtFuncionarioNome.Text;

            var funcionarioDao = new FuncionarioDAO();

            funcionarioDao.Insert(funcionario);

            MessageBox.Show("Funcionário cadastrado com sucesso!", "ALOC - Alset");

            this.Close();
        }
    }
}
