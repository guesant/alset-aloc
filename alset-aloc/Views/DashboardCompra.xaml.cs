using System;
using alset_aloc.Helpers;
using alset_aloc.Interfaces;
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
using System.Data;

namespace alset_aloc.Views
{
    /// <summary>
    /// Lógica interna para DashboardCompra.xaml
    /// </summary>
    public partial class DashboardCompra : UserControl
    {
        public List<long> selectedIds = new List<long>();

        public DashboardCompra()
        {
            InitializeComponent();

            DefineColumns();

            LoadSearch();
        }

        private void DefineColumns()
        {
            dgCompras.Columns.Clear();

            DataGridCheckBoxColumn selectedColumn = new DataGridCheckBoxColumn();
            Binding columnSelectBinding = new Binding("IsSelected");
            selectedColumn.Binding = columnSelectBinding;
            selectedColumn.Header = "#";
            selectedColumn.IsReadOnly = false;
            dgCompras.Columns.Add(selectedColumn);

            DataGridTextColumn idColumn = new DataGridTextColumn();
            Binding columnIdBinding = new Binding("Item.Id");
            idColumn.Binding = columnIdBinding;
            idColumn.Header = "Código";
            idColumn.IsReadOnly = true;
            dgCompras.Columns.Add(idColumn);

            DataGridTextColumn dateColumn = new DataGridTextColumn();
            Binding columnDateBinding = new Binding("Item.DataCompra");
            columnDateBinding.StringFormat = "dd/MM/yyyy";
            dateColumn.Binding = columnDateBinding;
            dateColumn.Header = "Data Compra";
            dateColumn.IsReadOnly = true;
            dgCompras.Columns.Add(dateColumn);

            DataGridTextColumn numeroColumn = new DataGridTextColumn();
            Binding columnNumberBinding = new Binding("Item.NumeroNota");
            numeroColumn.Binding = columnNumberBinding;
            numeroColumn.Header = "Numero Nota";
            numeroColumn.IsReadOnly = true;
            dgCompras.Columns.Add(numeroColumn);

            DataGridTextColumn produtoIdColumn = new DataGridTextColumn();
            Binding columnProdutoIdBinding = new Binding("Item.ProdutoId");
            produtoIdColumn.Binding = columnProdutoIdBinding;
            produtoIdColumn.Header = "Produto Id";
            produtoIdColumn.IsReadOnly = true;
            dgCompras.Columns.Add(produtoIdColumn);

            DataGridTextColumn fornecedorIdColumn = new DataGridTextColumn();
            Binding columnFornecedorIdBinding = new Binding("Item.FornecedorId");
            fornecedorIdColumn.Binding = columnFornecedorIdBinding;
            fornecedorIdColumn.Header = "Produto Id";
            fornecedorIdColumn.IsReadOnly = true;
            dgCompras.Columns.Add(fornecedorIdColumn);


            }

        private void LoadSearch()
        {
            var compraDAO = new CompraDAO();
            var compras = compraDAO.List();

            var dataRequired = compras.Select(compra => new TableEntry<Compra>(compra , this.selectedIds)).ToList();
             
            dgCompras.ItemsSource = dataRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarCompra();
            form.ShowDialog();
            this.LoadSearch();
        }
        private void dgCompras_MouseDoubleClick(object sender , MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid) sender ,
                                    e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null)
                return;

            var tableEntry = row.DataContext as TableEntry<Compra>;

            var comp = tableEntry.Item;

            var window = new CadastrarCompra(comp.Id);
            window.ShowDialog();

            LoadSearch();

        }

    }
}
