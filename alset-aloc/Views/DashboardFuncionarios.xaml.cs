using alset_aloc.Helpers;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace alset_aloc.Views
{
    /// <summary>
    /// Interação lógica para DashboardFuncionarios.xam
    /// </summary>
    public partial class DashboardFuncionarios : UserControl
    {
        public List<long> idsSelecionados = new List<long>();

        public DashboardFuncionarios()
        {
            InitializeComponent();

            DefinirColunas();

            CarregarBusca();
        }

        private void DefinirColunas()
        {
            dgFuncionarios.Columns.Clear();

            // ...

            DataGridCheckBoxColumn colunaSelect = new DataGridCheckBoxColumn();
            Binding colunaSelectBinding = new Binding("IsSelected");
            colunaSelect.Binding = colunaSelectBinding;
            colunaSelect.Header = "#";
            colunaSelect.IsReadOnly = false;
            dgFuncionarios.Columns.Add(colunaSelect);

            // ...

            DataGridTextColumn colunaId = new DataGridTextColumn();
            Binding colunaIdBinding = new Binding("Item.Id");
            colunaId.Binding = colunaIdBinding;
            colunaId.Header = "ID";
            colunaId.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaId);

            // ...

            DataGridTextColumn colunaNome = new DataGridTextColumn();
            Binding colunaNomeBinding = new Binding("Item.Nome");
            colunaNome.Binding = colunaNomeBinding;
            colunaNome.Header = "Nome";
            colunaNome.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaNome);

            // ...

            DataGridTextColumn colunaDataNascimento = new DataGridTextColumn();
            Binding colunaDataNascimentoBinding = new Binding("Item.DataNascimento");
            colunaDataNascimentoBinding.StringFormat = "dd/MM/yyyy";
            colunaDataNascimento.Binding = colunaDataNascimentoBinding;
            colunaDataNascimento.Header = "Data de Nascimento";
            colunaDataNascimento.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaDataNascimento);

            // ...

            DataGridTextColumn colunaCpf = new DataGridTextColumn();
            Binding colunaCpfBinding = new Binding("Item.Cpf");
            colunaCpf.Binding = colunaCpfBinding;
            colunaCpf.Header = "CPF";
            colunaCpf.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaCpf);

            // ...

            DataGridTextColumn colunaRg = new DataGridTextColumn();
            Binding colunaRgBinding = new Binding("Item.Rg");
            colunaRg.Binding = colunaRgBinding;
            colunaRg.Header = "RG";
            colunaRg.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaRg);

            // ...

            DataGridTextColumn colunaEmail = new DataGridTextColumn();
            Binding colunaEmailBinding = new Binding("Item.Email");
            colunaEmail.Binding = colunaEmailBinding;
            colunaEmail.Header = "E-mail";
            colunaEmail.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaEmail);

            // ...

            DataGridTextColumn colunaTelefone = new DataGridTextColumn();
            Binding colunaTelefoneBinding = new Binding("Item.Telefone");
            colunaTelefone.Binding = colunaTelefoneBinding;
            colunaTelefone.Header = "Telefone";
            colunaTelefone.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaTelefone);

            // ...

            DataGridTextColumn colunaGenero = new DataGridTextColumn();
            Binding colunaGeneroBinding = new Binding("Item.Genero");
            colunaGenero.Binding = colunaGeneroBinding;
            colunaGenero.Header = "Genero";
            colunaGenero.IsReadOnly = true;
            dgFuncionarios.Columns.Add(colunaGenero);
        }


        private void CarregarBusca()
        {
            var funcionarioDAO = new FuncionarioDAO();
            var funcionarios = funcionarioDAO.List();

            var data = funcionarios.Select(funcionario => new TableEntry<Funcionario>(funcionario, this.idsSelecionados)).ToList();

            dgFuncionarios.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarFuncionario();
            form.ShowDialog();
            this.CarregarBusca();
        }
    }
}
