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
    /// Interação lógica para DashboardRecebimentos.xam
    /// </summary>
    public partial class DashboardRecebimentos : UserControl
    {
        public List<long> selectedIds = new List<long>();

        public DashboardRecebimentos()
        {
            InitializeComponent();
            DefineColumns();
            LoadSearch();
        }

        private void DefineColumns()
        {
            DataGridCheckBoxColumn selectedColumn = new DataGridCheckBoxColumn();
            Binding columnSelectBinding = new Binding("IsSelected");
            selectedColumn.Binding = columnSelectBinding;
            selectedColumn.Header = "#";
            selectedColumn.IsReadOnly = false;
            dgVeiculo.Columns.Add(selectedColumn);

            DataGridTextColumn idColumn = new DataGridTextColumn();
            Binding columnIdBinding = new Binding("Item.Id");
            idColumn.Binding = columnIdBinding;
            idColumn.Header = "Código";
            idColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(idColumn);

            DataGridTextColumn descricaoColumn = new DataGridTextColumn();
            Binding descricaoColumnBinding = new Binding("Item.Descricao");
            descricaoColumn.Binding = descricaoColumnBinding;
            descricaoColumn.Header = "Descrição";
            descricaoColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(descricaoColumn);

            DataGridTextColumn pagadorColumn = new DataGridTextColumn();
            Binding pagadorColumnBinding = new Binding("Item.Pagador");
            pagadorColumn.Binding = pagadorColumnBinding;
            pagadorColumn.Header = "Pagador";
            pagadorColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(pagadorColumn);


            DataGridTextColumn valorColumn = new DataGridTextColumn();
            Binding valorColumnBinding = new Binding("Item.Valor");
            valorColumn.Binding = valorColumnBinding;
            valorColumn.Header = "Valor";
            valorColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(valorColumn);


            DataGridTextColumn dataCredenciamentoColumn = new DataGridTextColumn();
            Binding dataCredenciamentoColumnBinding = new Binding("Item.DataCredenciamento")
            {
                StringFormat = "dd/MM/yyyy" // Add the StringFormat here
            };
            dataCredenciamentoColumn.Binding = dataCredenciamentoColumnBinding;
            dataCredenciamentoColumn.Header = "Data de Credenciamento";
            dataCredenciamentoColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(dataCredenciamentoColumn);

            DataGridTextColumn dataVencimentoColumn = new DataGridTextColumn();
            Binding dataVencimentoColumnBinding = new Binding("Item.DataVencimento")
            {
                StringFormat = "dd/MM/yyyy" // Add the StringFormat here
            };
            dataVencimentoColumn.Binding = dataVencimentoColumnBinding;
            dataVencimentoColumn.Header = "Data de Vencimento";
            dataVencimentoColumn.IsReadOnly = true;
            dgVeiculo.Columns.Add(dataVencimentoColumn);

        }

        private void LoadSearch()
        {
            var recebimentoDAO = new RecebimentoDAO();
            var recebimentos = recebimentoDAO.List();

            var dataRequired = recebimentos.Select(recebimento => new TableEntry<Recebimento>(recebimento, this.selectedIds)).ToList();

            dgVeiculo.ItemsSource = dataRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastroReecbimentos();
            form.ShowDialog();
            this.LoadSearch();
        }

        private void dgVeiculo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                        e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null)
                return;

            var tableEntry = row.DataContext as TableEntry<Recebimento>;

            var func = tableEntry.Item;

            var window = new CadastroReecbimentos(func.Id);
            window.ShowDialog();

            LoadSearch();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Deseja excluir os registros?", "Confurmar exclusão.", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                foreach (TableEntry<Recebimento> tableEntry in dgVeiculo.Items)
                {

                    if (tableEntry.IsSelected)
                    {
                        Recebimento recebimento = tableEntry.Item;

                        var recebimentoDAO = new RecebimentoDAO();

                        recebimentoDAO.Delete(recebimento);


                    }
                    LoadSearch();
                }
            }
        }
    }
}
