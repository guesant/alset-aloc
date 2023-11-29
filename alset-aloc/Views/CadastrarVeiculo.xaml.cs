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
    /// Lógica interna para CadastrarVeiculo.xaml
    /// </summary>
    public partial class CadastrarVeiculo : Window
    {

        private long? _id;

        public CadastrarVeiculo()
        {
            InitializeComponent();
        }

        public CadastrarVeiculo(long? id = null)
        {
            if (id <= 0)
            {
                id = null;
            }

            _id = id;

            InitializeComponent();

            if (id != null)
            {
                Title = "Visualizar Veículo";
                btCadastrar.Content = "Atualizar";
                FillForm();
            }
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var veiculoAtual = _id != null ? (new VeiculoDAO()).GetById((int)_id) : null;

            var veiculo = new Veiculo();

            if (veiculoAtual != null)
            {
                veiculo = (new VeiculoDAO()).GetById((int)veiculoAtual.Id);
            }

            veiculo.Modelo = txtVeiculoModelo.Text;
            veiculo.Marca = txtVeiculoMarca.Text;
            veiculo.Ano = Convert.ToInt32(txtVeiculoAno.Text);
            veiculo.Placa = txtVeiculoPlaca.Text;
            veiculo.NumeroChassi= txtVeiculoNumeroChassi.Text;
            veiculo.Cor = txtVeiculoCor.Text;
            veiculo.DataCompra = txtVeiculoDataCompra.DisplayDate;
            veiculo.Descricao = txtVeiculoDescricao.Text;

            if (veiculo.Id == 0)
            {
                var veiculoDao = new VeiculoDAO();
                veiculoDao.Insert(veiculo);
                MessageBox.Show("Veículo cadastrado com sucesso!", "ALOC - Alset");
            } else
            {
                var veiculoDao = new VeiculoDAO();
                veiculoDao.Update(veiculo);
                MessageBox.Show("Veículo atualizado com sucesso!", "ALOC - Alset");
            }

            this.Close();
        }

        private void FillForm()
        {
            try
            {
                if (_id != 0 && _id != null)
                {
                    var veiculoDAO = new VeiculoDAO();
                    var _veiculo = veiculoDAO.GetById((int)_id);

                    txtVeiculoModelo.Text = _veiculo.Modelo;
                    txtVeiculoMarca.Text = _veiculo.Marca;
                    txtVeiculoAno.Text = Convert.ToString(_veiculo.Ano);
                    txtVeiculoPlaca.Text = _veiculo.Placa;
                    txtVeiculoNumeroChassi.Text = _veiculo.NumeroChassi;
                    txtVeiculoCor.Text = _veiculo.Cor;
                    txtVeiculoDataCompra.SelectedDate = _veiculo.DataCompra;
                    txtVeiculoDescricao.Text = _veiculo.Descricao;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro! {ex}");
            }
        }

    }
}
