using alset_aloc.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace alset_aloc.Views
{
    /// <summary>
    /// Interação lógica para DashboardFuncionarios.xam
    /// </summary>
    public partial class DashboardFuncionarios : UserControl
    {
        public DashboardFuncionarios()
        {
            InitializeComponent();
            
            DefinirColunas();

            CarregarBusca();
        }

        private void DefinirColunas()
        {
            DataGridTextColumn colunaId = new DataGridTextColumn();
            Binding colunaIdBinding = new Binding("Id");            
            colunaId.Binding = colunaIdBinding;
            colunaId.Header = "ID";            
            dgFuncionarios.Columns.Add(colunaId);
            
            // ...

            DataGridTextColumn colunaNome = new DataGridTextColumn();
            Binding colunaNomeBinding = new Binding("Nome");
            colunaNome.Binding = colunaNomeBinding;
            colunaNome.Header = "Nome";
            dgFuncionarios.Columns.Add(colunaNome);

            // ...

            DataGridTextColumn colunaDataNascimento = new DataGridTextColumn();
            Binding colunaDataNascimentoBinding = new Binding("DataNascimento");
            colunaDataNascimentoBinding.StringFormat = "dd/MM/yyyy";
            colunaDataNascimento.Binding = colunaDataNascimentoBinding;
            colunaDataNascimento.Header = "Data de Nascimento";
            dgFuncionarios.Columns.Add(colunaDataNascimento);

            // ...

            DataGridTextColumn colunaCpf = new DataGridTextColumn();
            Binding colunaCpfBinding = new Binding("Cpf");
            colunaCpf.Binding = colunaCpfBinding;
            colunaCpf.Header = "CPF";
            dgFuncionarios.Columns.Add(colunaCpf);

            // ...

            DataGridTextColumn colunaRg = new DataGridTextColumn();
            Binding colunaRgBinding = new Binding("Rg");
            colunaRg.Binding = colunaRgBinding;
            colunaRg.Header = "RG";
            dgFuncionarios.Columns.Add(colunaRg);

            // ...

            DataGridTextColumn colunaEmail = new DataGridTextColumn();
            Binding colunaEmailBinding = new Binding("Email");
            colunaEmail.Binding = colunaRgBinding;
            colunaEmail.Header = "E-mail";
            dgFuncionarios.Columns.Add(colunaEmail);

            // ...

            DataGridTextColumn colunaTelefone = new DataGridTextColumn();
            Binding colunaTelefoneBinding = new Binding("Telefone");
            colunaTelefone.Binding = colunaRgBinding;
            colunaTelefone.Header = "Telefone";
            dgFuncionarios.Columns.Add(colunaTelefone);

            // ...

            DataGridTextColumn colunaGenero = new DataGridTextColumn();
            Binding colunaGeneroBinding = new Binding("Genero");
            colunaGenero.Binding = colunaRgBinding;
            colunaGenero.Header = "Genero";
            dgFuncionarios.Columns.Add(colunaGenero);
        }


        private void CarregarBusca()
        {
            var funcionarioDAO = new FuncionarioDAO();

            var funcionarios = funcionarioDAO.List();

            dgFuncionarios.ItemsSource = funcionarios;
        }
    }
}
