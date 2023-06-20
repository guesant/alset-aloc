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
        public CadastrarVeiculo()
        {
            InitializeComponent();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
           
            var veiculo = new Veiculo();


            veiculo.Modelo = txtVeiculoModelo.Text;
            veiculo.Marca = txtVeiculoMarca.Text;
            veiculo.Ano = Convert.ToInt32(txtVeiculoAno.Text);
            veiculo.Placa = txtVeiculoPlaca.Text;
            veiculo.NumeroChassi= txtVeiculoNumeroChassi.Text;
            veiculo.Cor = txtVeiculoCor.Text;
            veiculo.DataCompra = txtVeiculoDataCompra.DisplayDate;
            veiculo.Descricao= txtVeiculoDescricao.Text;




            // veiculo.CargoId = txtveiculoNome.Text;

            var veiculoDao = new VeiculoDAO();

            veiculoDao.Insert(veiculo);

            MessageBox.Show("Veículo cadastrado com sucesso!", "ALOC - Alset");

            this.Close();
        }

        }
}
