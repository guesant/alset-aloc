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

            DataGridCheckBoxColumn idColumn = new DataGridCheckBoxColumn();
            Binding columnIdBinding = new Binding("Item.Id");
            idColumn.Binding = columnIdBinding;
            idColumn.Header = "Código";
            idColumn.IsReadOnly = true;
            dgClientes.Columns.Add(idColumn);

            DataGridCheckBoxColumn nameColumn = new DataGridCheckBoxColumn();
            Binding columnNameBinding = new Binding("Item.Nome");
            nameColumn.Binding = columnNameBinding;
            nameColumn.Header = "Nome";
            nameColumn.IsReadOnly = true;
            dgClientes.Columns.Add(nameColumn);

            DataGridCheckBoxColumn dateColumn = new DataGridCheckBoxColumn();
            Binding columnDateBinding = new Binding("Item.Date");
            dateColumn.Binding = columnDateBinding;
            dateColumn.Header = "Data de Nascimento";
            dateColumn.IsReadOnly = true;
            dgClientes.Columns.Add(dateColumn);

            DataGridCheckBoxColumn cpfColumn = new DataGridCheckBoxColumn();
            Binding columnCpfBinding = new Binding("Item.Cpf");
            cpfColumn.Binding = columnDateBinding;
            cpfColumn.Header = "CPF";
            cpfColumn.IsReadOnly = true;
            dgClientes.Columns.Add(cpfColumn);

            DataGridCheckBoxColumn rgColumn = new DataGridCheckBoxColumn();
            Binding columnRgBinding = new Binding("Item.Rg");
            rgColumn.Binding = columnRgBinding;
            rgColumn.Header = "RG";
            rgColumn.IsReadOnly = true;
            dgClientes.Columns.Add(rgColumn);

            DataGridCheckBoxColumn cnhColumn = new DataGridCheckBoxColumn();
            Binding columnCnhBinding = new Binding("Item.Cnh");
            cnhColumn.Binding = columnCnhBinding;
            cnhColumn.Header = "CNH";
            cnhColumn.IsReadOnly = true;
            dgClientes.Columns.Add(cnhColumn);

            DataGridCheckBoxColumn emailColumn = new DataGridCheckBoxColumn();
            Binding columnEmailBinding = new Binding("Item.Email");
            emailColumn.Binding = columnEmailBinding;
            emailColumn.Header = "E-Mail";
            emailColumn.IsReadOnly = true;
            dgClientes.Columns.Add(emailColumn);

            DataGridCheckBoxColumn telefoneColumn = new DataGridCheckBoxColumn();
            Binding columnTelefoneBinding = new Binding("Item.Telefone");
            telefoneColumn.Binding = columnTelefoneBinding;
            telefoneColumn.Header = "Telefone";
            telefoneColumn.IsReadOnly = true;
            dgClientes.Columns.Add(telefoneColumn);

            DataGridCheckBoxColumn generoColumn = new DataGridCheckBoxColumn();
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
    }
}
