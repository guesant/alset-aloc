using System;
using alset_aloc.Models;
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
    /// Lógica interna para CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {
        public CadastrarCliente()
        {
            InitializeComponent();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var enderecoDAO = new EnderecoDAO();
            var endereco = new Endereco();

            ComboBoxItem countryItem = (ComboBoxItem)cbEnderecoPais.SelectedItem;
            endereco.Pais = countryItem.Content.ToString();

            endereco.CodigoPostal = txtEnderecoCodigoPostal.Text;
            endereco.UF = txtEnderecoUF.Text;
            endereco.Cidade = txtEnderecoCidade.Text;

            endereco.Rua = txtEnderecoRua.Text;
            endereco.Numero = Convert.ToInt32(txtEnderecoNumero.Text);
            endereco.Bairro = txtEnderecoBairro.Text;
            endereco.Complemento = txtEnderecoComplemento.Text;

            enderecoDAO.Insert(endereco);

            var cliente = new Cliente();
            var clienteDAO = new ClienteDAO();

            cliente.EnderecoId = endereco.Id;

            cliente.Nome = txtClienteNome.Text;
            cliente.CPF = txtClienteCPF.Text;
            cliente.RG = txtClienteRg.Text;
            cliente.CNH = txtClienteCNH.Text;
            cliente.Email = txtClienteEmail.Text;
            cliente.Genero = txtClienteGenero.Text;
            cliente.DataNascimento = txtClienteDataNascimento.DisplayDate;
            cliente.Telefone = txtClienteTelefone.Text;
            
            clienteDAO.Insert(cliente);

            MessageBox.Show("Cliente cadastrado com sucesso!", "ALOC - Alset");
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
