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
using System.Security.Cryptography;

namespace alset_aloc.Views
{
    /// <summary>
    /// Lógica interna para CadastrarCliente.xaml
    /// </summary>
    public partial class CadastrarCliente : Window
    {

        private long? _id;

        public CadastrarCliente()
        {
            InitializeComponent();
            
            BuscarDados();
        }

        public CadastrarCliente(long? id)
        {
            if (id <= 0)
            {
                id = null;
            }

            _id = id;

            InitializeComponent();
            
            BuscarDados();

            if (id != null)
            {
                FillForm();
            }
        }

        private void BuscarDados()
        {
            cbEnderecoPais.ItemsSource = new PaisDAO().List();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var enderecoDAO = new EnderecoDAO();
            var endereco = new Endereco();
            var clienteAtual = _id != null ? (new ClienteDAO()).GetById((int)_id) : null;

            if (clienteAtual != null)
            {
                int? enderecoId = (int?)clienteAtual.EnderecoId;

                if (enderecoId != null)
                {
                    endereco = enderecoDAO.GetById((int)enderecoId);
                }
            }

            if (cbEnderecoPais.SelectedValue == null)
            {
                cbEnderecoPais.SelectedIndex = 1;
            }


            endereco.Pais = (string)cbEnderecoPais.SelectedItem;

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

            this.Close();
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillForm()
        {
            try
            {
                if (_id != 0 && _id != null)
                {
                    var clienteDAO = new ClienteDAO();
                    var _cliente = clienteDAO.GetById((int)_id);
                    var enderecoDAO = new EnderecoDAO();

                    txtClienteNome.Text = _cliente.Nome;
                    txtClienteCPF.Text = _cliente.CPF;
                    txtClienteRg.Text = _cliente.RG;
                    txtClienteCNH.Text = _cliente.CNH;
                    txtClienteEmail.Text = _cliente.Email;
                    txtClienteTelefone.Text = _cliente.Telefone;
                    txtClienteGenero.Text = _cliente.Genero;
                    txtClienteDataNascimento.Text = _cliente.DataNascimento.ToString();

                    if (_cliente.EnderecoId != null)
                    {
                        var _endereco = enderecoDAO.GetById((int)_cliente.EnderecoId);

                        txtEnderecoBairro.Text = _endereco.Bairro;
                        txtEnderecoCidade.Text = _endereco.Cidade;
                        txtEnderecoCodigoPostal.Text = _endereco.CodigoPostal;
                        txtEnderecoComplemento.Text = _endereco.Complemento;
                        txtEnderecoNumero.Text = _endereco.Numero.ToString();
                        txtEnderecoRua.Text = _endereco.Rua;
                        txtEnderecoUF.Text = _endereco.UF;
                        
                        cbEnderecoPais.SelectedItem = _endereco.Pais;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
