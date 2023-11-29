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
    /// Interação lógica para DashboardVeiculo.xam
    /// </summary>
    public partial class DashboardVeiculo : UserControl
    {
        public List<long> idsSelecionados = new List<long>();

        public DashboardVeiculo()
        {
            InitializeComponent();

            DefinirColunas();

            CarregarBusca();
        }

        private void DefinirColunas()
        {
            dgVeiculo.Columns.Clear();

            // ...

            DataGridCheckBoxColumn colunaSelect = new DataGridCheckBoxColumn();
            Binding colunaSelectBinding = new Binding("IsSelected");
            colunaSelect.Binding = colunaSelectBinding;
            colunaSelect.Header = "#";
            colunaSelect.IsReadOnly = false;
            dgVeiculo.Columns.Add(colunaSelect);

            // ...

            DataGridTextColumn colunaId = new DataGridTextColumn();
            Binding colunaIdBinding = new Binding("Item.Id");
            colunaId.Binding = colunaIdBinding;
            colunaId.Header = "ID";
            colunaId.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaId);

            // ...

            DataGridTextColumn colunaModelo = new DataGridTextColumn();
            Binding colunaModeloBinding = new Binding("Item.Modelo");
            colunaModelo.Binding = colunaModeloBinding;
            colunaModelo.Header = "Modelo";
            colunaModelo.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaModelo);


            // ...

            DataGridTextColumn colunaMarca = new DataGridTextColumn();
            Binding colunaMarcaBinding = new Binding("Item.Marca");
            colunaMarca.Binding = colunaMarcaBinding;
            colunaMarca.Header = "Marca";
            colunaMarca.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaMarca);

            // ...

            DataGridTextColumn colunaAno = new DataGridTextColumn();
            Binding colunaAnoBinding = new Binding("Item.Ano");
            colunaAno.Binding = colunaAnoBinding;
            colunaAno.Header = "Ano";
            colunaAno.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaAno);

            // ...

            DataGridTextColumn colunaPlaca = new DataGridTextColumn();
            Binding colunaPlacaBinding = new Binding("Item.Placa");
            colunaPlaca.Binding = colunaPlacaBinding;
            colunaPlaca.Header = "Placa";
            colunaPlaca.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaPlaca);

            // ...

            DataGridTextColumn colunaNumeroChassi = new DataGridTextColumn();
            Binding colunaNumeroChassiBinding = new Binding("Item.NumeroChassi");
            colunaNumeroChassi.Binding = colunaNumeroChassiBinding;
            colunaNumeroChassi.Header = "Numero Chassi";
            colunaNumeroChassi.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaNumeroChassi);


            //...

            DataGridTextColumn colunaCor = new DataGridTextColumn();
            Binding colunaCorBinding = new Binding("Item.Cor");
            colunaCor.Binding = colunaCorBinding;
            colunaCor.Header = "Cor";
            colunaCor.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaCor);

            // ...

            DataGridTextColumn colunaDataCompra = new DataGridTextColumn();
            Binding colunaDataCompraBinding = new Binding("Item.DataCompra");
            colunaDataCompraBinding.StringFormat = "dd/MM/yyyy";
            colunaDataCompra.Binding = colunaDataCompraBinding;
            colunaDataCompra.Header = "Data de Compra";
            colunaDataCompra.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaDataCompra);

            //...

            DataGridTextColumn colunaDescricao = new DataGridTextColumn();
            Binding colunaDescricaoBinding = new Binding("Item.Descricao");
            colunaDescricao.Binding = colunaDescricaoBinding;
            colunaDescricao.Header = "Descricao";
            colunaDescricao.IsReadOnly = true;
            dgVeiculo.Columns.Add(colunaDescricao);
        }


        private void CarregarBusca()
        {
            var veiculoDAO = new VeiculoDAO();
            var veiculos= veiculoDAO.List();

            var data = veiculos.Select(veiculo=> new TableEntry<Veiculo>(veiculo , this.idsSelecionados)).ToList();

            dgVeiculo.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarVeiculo();
            form.ShowDialog();
            this.CarregarBusca();
        }

        private void dgVeiculo_MouseDoubleClick(object sender , MouseButtonEventArgs e)
            {
                var row = ItemsControl.ContainerFromElement((DataGrid) sender ,
                                        e.OriginalSource as DependencyObject) as DataGridRow;

                if (row == null)
                    return;

                var tableEntry = row.DataContext as TableEntry<Veiculo>;

                var veiculo = tableEntry.Item;

                var window = new CadastrarVeiculo(veiculo.Id);
                window.ShowDialog();
                
                CarregarBusca();


            }

        private void Button_Click_1(object sender , RoutedEventArgs e)
            {
            var result = MessageBox.Show("Deseja excluir os registros?" , "Confirm" , MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
                {
                foreach (TableEntry<Veiculo> tableEntry in dgVeiculo.Items)
                    {
                    if (tableEntry.IsSelected)
                        {
                        // A linha foi selecionada, você pode acessar o objeto Funcionario associado a esta linha.
                        Veiculo veiculo = tableEntry.Item;

                        var veiculoDAO = new VeiculoDAO();

                        veiculoDAO.Delete(veiculo);

                        // Faça o que precisar com o objeto funcionario.
                        }
                    }
                } //ao clicar neste botão ele verifica todos os campos que possuem checkbox marcada e retorna a linha em que em que o checkbox se encontra
            CarregarBusca();
            }

    }
}
