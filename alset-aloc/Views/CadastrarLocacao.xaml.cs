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
    /// Interação lógica para CadastrarLocacao.xam
    /// </summary>
    public partial class CadastrarLocacao : Window
    {
        private long? _id;

        public CadastrarLocacao()
        {
            InitializeComponent();

            BuscarDados();
        }

        public CadastrarLocacao(long? id)
        {
            if (id <= 0)
            {
                id = null;
            }

            _id = id;

            InitializeComponent();

            BuscarDados();

            if (id != null)
            {
                FillForm();
            }
        }

        private void BuscarDados()
        {
            cbCliente.ItemsSource = new ClienteDAO().List();
            cbVeiculo.ItemsSource = new VeiculoDAO().List();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var locacaoAtual = _id != null ? (new LocacaoDAO()).GetById((int)_id) : null;


            if (cbVeiculo.SelectedValue == null || cbCliente.SelectedValue == null)
            {
                MessageBox.Show("Preencha todos os campos corretamente!");
                return;
            }

            var locacao = new Locacao();

            if (locacaoAtual != null)
            {
                locacao = (new LocacaoDAO()).GetById((int)locacaoAtual.Id);
            }

            locacao.ValorDiaria = Convert.ToDouble(txtLocacaoValorDiaria.Text);
           
            locacao.DataLocacao = (DateTime)dtLocacaoData.SelectedDate;
            
            locacao.DataDevolucaoPrevista = dtLocacaoDevolucao.SelectedDate;
            locacao.DataDevolucaoEfetivada = dtLocacaoDevolucaoEfetivada.SelectedDate;

            locacao.VeiculoId = Convert.ToInt32(cbVeiculo.SelectedValue);
            locacao.ClienteId = Convert.ToInt32(cbCliente.SelectedValue);
            
            // TODO
            // locacao.FuncionarioId = null;

            if (locacao.Id == 0)
            {
                var locacaoDAO = new LocacaoDAO();
                locacaoDAO.Insert(locacao);
                MessageBox.Show("Locação cadastrado com sucesso!", "ALOC - Alset");
            }
            else
            {
                var locacaoDAO = new LocacaoDAO();
                locacaoDAO.Update(locacao);
                MessageBox.Show($"Locação atualizadoa com sucesso!", "ALOC - Alset");
            }

            this.Close();

        }

        private void FillForm()
        {
            try
            {
                if (_id != 0 && _id != null)
                {
                    var locacaoDAO = new LocacaoDAO();
                    var _locacao = locacaoDAO.GetById((int)_id);                   

                    dtLocacaoData.SelectedDate = _locacao.DataLocacao;
                    dtLocacaoDevolucao.SelectedDate = _locacao.DataDevolucaoPrevista;
                    dtLocacaoDevolucaoEfetivada.SelectedDate = _locacao.DataDevolucaoEfetivada;

                    txtLocacaoValorDiaria.Text = _locacao.ValorDiaria.ToString();

                    if (_locacao.VeiculoId != null)
                    {
                        var _veiculo = new VeiculoDAO().GetById((int)_locacao.VeiculoId);
                        cbVeiculo.SelectedValue = _veiculo.Id;                        
                    }


                    if (_locacao.ClienteId != null)
                    {
                        var _cliente = new VeiculoDAO().GetById((int)_locacao.ClienteId);
                        cbCliente.SelectedValue = _cliente.Id;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
