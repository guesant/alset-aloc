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
        public CadastrarLocacao()
        {
            InitializeComponent();
        }

        private void btCadastrar_Click(object sender, RoutedEventArgs e)
        {
            var locacao = new Locacao();

            locacao.DataLocacao = dtLocacaoData.DisplayDate;
            locacao.DataDevolucaoPrevista = dtLocacaoDevolucao.DisplayDate;
            locacao.DataDevolucaoEfetivada = dtLocacaoDevolucaoEfetivada.DisplayDate;
            //locacao.ValorLocacao = txt.LocacaoValor.Text;

            var locacaoDao = new LocacaoDAO();

            locacaoDao.Insert(locacao);

            MessageBox.Show("Locação cadastrada com sucesso!",
                "ALOC - Alset");

            this.Close();


                /* 

                 public long Id { get; set; }

        public DateTime DataLocacao { get; set; }

        public DateTime DataDevolucaoPrevista { get; set;  }

        public DateTime? DataDevolucaoEfetivada { get; set; }

        public bool Status { get; set; }

        public long? VeiculoId { get; set; }

        public long? FuncionarioId { get; set; }
                */


        }

        private void btCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
