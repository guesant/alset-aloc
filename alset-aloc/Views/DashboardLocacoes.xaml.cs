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
    /// Interação lógica para DashboardLocacoes.xam
    /// </summary>
    /// 

    class DashboardLocacoesItem
    {
        public Locacao Locacao { get; set; }
        public Veiculo Veiculo {get;set;}
        public Cliente Cliente {get;set;}
        public Funcionario Funcionario {get;set;}

        public string Situacao
        {
            get
            {
                return this.Locacao.Status ? "Concluído" : "Locado";
            }
        }
    }

    public partial class DashboardLocacoes : UserControl
    {

            public List<long> selectedIds = new List<long>();
        public DashboardLocacoes()
        {
            InitializeComponent();

            DefineColumns();

            LoadSearch();

        }

        private void DefineColumns()
        {
            dgLocacoes.Columns.Clear();

            DataGridCheckBoxColumn selectedColumn = new DataGridCheckBoxColumn();
            Binding columnSelectBinding = new Binding("isSelected");
            selectedColumn.Binding = columnSelectBinding;
            selectedColumn.Header = "#";
            selectedColumn.IsReadOnly = false;
            dgLocacoes.Columns.Add(selectedColumn);

            DataGridTextColumn idColumn = new DataGridTextColumn();
            Binding columnIdBinding = new Binding("Item.Locacao.Id");
            idColumn.Binding = columnIdBinding;
            idColumn.Header = "Código";
            idColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(idColumn);

            DataGridTextColumn clienteColumn = new DataGridTextColumn();
            Binding columnClienteBinding = new Binding("Item.Cliente.Nome");
            clienteColumn.Binding = columnIdBinding;
            clienteColumn.Header = "Cliente";
            clienteColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(clienteColumn);

            DataGridTextColumn vehicleColumn = new DataGridTextColumn();
            Binding columnVehicleBinding = new Binding("Item.Veiculo.Placa");
            vehicleColumn.Binding = columnVehicleBinding;
            vehicleColumn.Header = "Veículo";
            vehicleColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(vehicleColumn);


            DataGridTextColumn valueDailyColumn = new DataGridTextColumn();
            Binding columnValueDailyBinding = new Binding("Item.Locacao.ValorDiaria");

            columnValueDailyBinding.StringFormat = "R$ ";
            valueDailyColumn.Binding = columnValueDailyBinding;
            valueDailyColumn.Header = "Valor Diária";
            valueDailyColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(valueDailyColumn);

            DataGridTextColumn dateColumn = new DataGridTextColumn();
            Binding columnDateBinding = new Binding("Item.Locacao.DataLocacao");
            columnDateBinding.StringFormat = "dd/MM/yyyy";
            dateColumn.Binding = columnDateBinding;
            dateColumn.Header = "Data da Locação";
            dateColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(dateColumn);


            DataGridTextColumn statusColumn = new DataGridTextColumn();
            Binding columnStatusBinding = new Binding("Item.Situacao");
            statusColumn.Binding = columnStatusBinding;
            statusColumn.Header = "Status";
            statusColumn.IsReadOnly = true;
            dgLocacoes.Columns.Add(statusColumn);


        }

        private void LoadSearch()
        {
            var locacoesDAO = new LocacaoDAO();
            var locacoes = locacoesDAO.List();

            var dataRequired = locacoes.Select(locacao =>
            {

                var item = new DashboardLocacoesItem();
                item.Locacao = locacao;

                item.Cliente = locacao.ClienteId != null ? new ClienteDAO().GetById((int)locacao.ClienteId):null;
                item.Veiculo = locacao.VeiculoId != null ? new VeiculoDAO().GetById((int)locacao.VeiculoId) : null;
                item.Funcionario = locacao.FuncionarioId != null ? new FuncionarioDAO().GetById((int)locacao.FuncionarioId) : null;

                return new TableEntry<DashboardLocacoesItem>(item, this.selectedIds);
            }).ToList();

            dgLocacoes.ItemsSource = dataRequired;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarLocacao();
            form.ShowDialog();
            this.LoadSearch();
        }

        private void dgLocacoes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                  e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null)
                return;

            var tableEntry = row.DataContext as TableEntry<DashboardLocacoesItem>;

            var func = tableEntry.Item;

            var window = new CadastrarLocacao(func.Locacao.Id);
            window.ShowDialog();

            LoadSearch();
        }
    }
        
}
