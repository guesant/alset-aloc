using alset_aloc.Interfaces;
using alset_aloc.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace alset_aloc.Views
{
    /// <summary>
    /// Lógica interna para DashboardClientes.xaml
    /// </summary>
    public partial class DashboardClientes : UserControl
    {
        public List<long> selectedIds = new List<long>();

        public DashboardClientes()
        {
            InitializeComponent();

            DefineColumns();

            LoadSearch();

        }

        private void DefineColumns()
        {
            dgClientes.Columns.Clear();

            DataGridCheckBoxColumn selectedColumn = new DataGridCheckBoxColumn();
            Binding columnSelectBinding = new Binding("IsSelected");
            selectedColumn.Binding = columnSelectBinding;
            selectedColumn.Header = "#";
            selectedColumn.IsReadOnly = false;
            dgClientes.Columns.Add(selectedColumn);

            DataGridTextColumn idColumn = new DataGridTextColumn();
            Binding columnIdBinding = new Binding("Item.Id");
            idColumn.Binding = columnIdBinding;
            idColumn.Header = "Código";
            idColumn.IsReadOnly = true;
            dgClientes.Columns.Add(idColumn);

            DataGridTextColumn nameColumn = new DataGridTextColumn();
            Binding columnNameBinding = new Binding("Item.Nome");
            nameColumn.Binding = columnNameBinding;
            nameColumn.Header = "Nome";
            nameColumn.IsReadOnly = true;
            dgClientes.Columns.Add(nameColumn);

            DataGridTextColumn dateColumn = new DataGridTextColumn();
            Binding columnDateBinding = new Binding("Item.DataNascimento");
            columnDateBinding.StringFormat = "dd/MM/yyyy";
            dateColumn.Binding = columnDateBinding;
            dateColumn.Header = "Data de Nascimento";
            dateColumn.IsReadOnly = true;
            dgClientes.Columns.Add(dateColumn);

            DataGridTextColumn cpfColumn = new DataGridTextColumn();
            Binding columnCpfBinding = new Binding("Item.CPF");

            cpfColumn.Binding = columnCpfBinding;
            cpfColumn.Header = "CPF";
            cpfColumn.IsReadOnly = true;
            dgClientes.Columns.Add(cpfColumn);

            DataGridTextColumn rgColumn = new DataGridTextColumn();
            Binding columnRgBinding = new Binding("Item.RG");
            rgColumn.Binding = columnRgBinding;
            rgColumn.Header = "RG";
            rgColumn.IsReadOnly = true;
            dgClientes.Columns.Add(rgColumn);

            DataGridTextColumn cnhColumn = new DataGridTextColumn();
            Binding columnCnhBinding = new Binding("Item.CNH");
            cnhColumn.Binding = columnCnhBinding;
            cnhColumn.Header = "CNH";
            cnhColumn.IsReadOnly = true;
            dgClientes.Columns.Add(cnhColumn);

            DataGridTextColumn emailColumn = new DataGridTextColumn();
            Binding columnEmailBinding = new Binding("Item.Email");
            emailColumn.Binding = columnEmailBinding;
            emailColumn.Header = "E-Mail";
            emailColumn.IsReadOnly = true;
            dgClientes.Columns.Add(emailColumn);

            DataGridTextColumn telefoneColumn = new DataGridTextColumn();
            Binding columnTelefoneBinding = new Binding("Item.Telefone");
            telefoneColumn.Binding = columnTelefoneBinding;
            telefoneColumn.Header = "Telefone";
            telefoneColumn.IsReadOnly = true;
            dgClientes.Columns.Add(telefoneColumn);

            DataGridTextColumn generoColumn = new DataGridTextColumn();
            Binding columnGeneroBinding = new Binding("Item.Genero");
            generoColumn.Binding = columnGeneroBinding;
            generoColumn.Header = "Gênero";
            generoColumn.IsReadOnly = true;
            dgClientes.Columns.Add(generoColumn);

        }

        private void LoadSearch()
        {
            var clienteDAO = new ClienteDAO();
            var clientes = clienteDAO.List();

            var dataRequired = clientes.Select(cliente => new TableEntry<Cliente>(cliente, this.selectedIds)).ToList();

            dgClientes.ItemsSource = dataRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarCliente();
            form.ShowDialog();
            this.LoadSearch();
        }

        private void dgClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                    e.OriginalSource as DependencyObject) as DataGridRow;
       
            if (row == null) return; 

            var tableEntry = row.DataContext as TableEntry<Cliente>;

            var cliente = tableEntry.Item;

            var window = new CadastrarCliente(cliente.Id);
            window.ShowDialog();

            LoadSearch();


        }
    }

        private void Button_Click_1(object sender , RoutedEventArgs e)
            {
                var result = MessageBox.Show("Deseja excluir os registros?" , "Confirm" , MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    foreach (TableEntry<Cliente> tableEntry in dgClientes.Items)
                        {
                        if (tableEntry.IsSelected)
                            {
                            // A linha foi selecionada, você pode acessar o objeto Funcionario associado a esta linha.
                            Cliente cliente= tableEntry.Item;

                            var clienteDAO = new ClienteDAO();

                            clienteDAO.Delete(cliente);

                            // Faça o que precisar com o objeto funcionario.
                            }
                        }
                } //ao clicar neste botão ele verifica todos os campos que possuem checkbox marcada e retorna a linha em que em que o checkbox se encontra
                    LoadSearch();
                
        }
        }
}
