using alset_aloc.Interfaces;
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
        private long? _id;

        public CadastrarFuncionario()
        {
            InitializeComponent();
        }
        
        public CadastrarFuncionario(long? id = null)
        {

            if(id <= 0)
            {
                id = null;
            }

            _id = id;

            InitializeComponent();

            if(id != null)
            {
                FillForm();
            }
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var endereco = new Endereco();
            var funcionarioAtual = _id != null ? (new FuncionarioDAO()).GetById((int)_id) : null;

            if (funcionarioAtual != null)
            {

                int? enderecoId = (int?)funcionarioAtual.EnderecoID;


                if(enderecoId != null)
                {
                    var enderecoDao = new EnderecoDAO();
                    endereco = enderecoDao.GetById((int)enderecoId);
                }
            }

            if (cbEnderecoPais.SelectedValue == null)
            {
                cbEnderecoPais.SelectedIndex = 1;
            }

            ComboBoxItem typeItem = (ComboBoxItem)cbEnderecoPais.SelectedValue;
            endereco.Pais = typeItem.Content.ToString();



            endereco.CodigoPostal = txtEnderecoCodigoPostal.Text;
            endereco.UF = txtEnderecoUF.Text;
            endereco.Cidade = txtEnderecoCidade.Text;

            endereco.Rua = txtEnderecoRua.Text;
            endereco.Numero = Convert.ToInt32(txtEnderecoNumero.Text);
            endereco.Bairro = txtEnderecoBairro.Text;
            endereco.Complemento = txtEnderecoComplemento.Text;


            var funcionario = new Funcionario();

            if(funcionarioAtual != null)
            {
                funcionario = (new FuncionarioDAO()).GetById((int)funcionarioAtual.Id);
            }

            funcionario.Nome = txtFuncionarioNome.Text;
            funcionario.DataNascimento = txtFuncionarioDataNascimento.DisplayDate;
            funcionario.Cpf = txtFuncionarioCpf.Text;
            funcionario.Rg = txtFuncionarioRg.Text;
            funcionario.CNH = txtFuncionarioCNH.Text;
            funcionario.Email = txtFuncionarioEmail.Text;
            funcionario.Telefone = txtFuncionarioTelefone.Text;
            funcionario.Genero = txtFuncionarioGenero.Text;
            funcionario.Cargo = txtFuncionarioCargo.Text;

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

            funcionario.EnderecoID = endereco.Id;


            if (funcionario.Id == 0)
            {
                var funcionarioDao = new FuncionarioDAO();
                funcionarioDao.Insert(funcionario);
                MessageBox.Show("Funcionário cadastrado com sucesso!", "ALOC - Alset");
            }
            else 
            { 
                var funcionarioDao = new FuncionarioDAO();
                funcionarioDao.Update(funcionario);
                MessageBox.Show($"Funcionário {txtFuncionarioNome.Text} atualizado com sucesso!", "ALOC - Alset");
            }

            this.Close();
        }
    
        private void FillForm()
        {
            try
            {
                if (_id != 0 && _id != null)
                {
                    var funcionarioDAO = new FuncionarioDAO();
                    var _funcionario = funcionarioDAO.GetById((int)_id);
                    var enderecoDAO = new EnderecoDAO();

                    txtFuncionarioNome.Text = _funcionario.Nome;
                    txtFuncionarioDataNascimento.SelectedDate = _funcionario.DataNascimento;
                    txtFuncionarioCpf.Text = _funcionario.Cpf;
                    txtFuncionarioRg.Text = _funcionario.Rg;
                    txtFuncionarioCNH.Text = _funcionario.CNH;
                    txtFuncionarioEmail.Text = _funcionario.Email;
                    txtFuncionarioTelefone.Text = _funcionario.Telefone;
                    txtFuncionarioGenero.Text = _funcionario.Genero;
                    txtFuncionarioCargo.Text = _funcionario.Cargo;

                    if (_funcionario.EnderecoID != null)
                    {
                        var _endereco = enderecoDAO.GetById((int)_funcionario.EnderecoID);

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
    
    }
}
