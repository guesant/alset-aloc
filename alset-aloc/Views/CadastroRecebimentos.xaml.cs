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
    /// Lógica interna para CadastroRecebimentos.xaml
    /// </summary>
    public partial class CadastroReecbimentos : Window
    {

        private long? _id;

        public CadastroReecbimentos()
        {
            InitializeComponent();
            BuscarDados();
        }

        public CadastroReecbimentos(long? id = null)
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
                Title = "Visualizar Fornecedor";
                btCadastrar.Content = "Atualizar";

                FillForm();
            }
        }

        private void BuscarDados()
        {
            cbRecebidoDe.ItemsSource = new ClienteDAO().List();
            
            
        }
        private void FillForm()
        {
            {
                try
                {
                    if(_id != 0 && +_id != null)
                    {
                        var recebimentoDAO = new RecebimentoDAO();
                        var _recebimento = recebimentoDAO.GetById((int)_id);

                        txtDescricao.Text = _recebimento.Descricao;
                        txtDataCredenciamento.Text = _recebimento.DataCredenciamento.ToString();
                        txtDataDeVencimento.Text = _recebimento.DataVencimento.ToString();
                        txtValor.Text = _recebimento.Valor.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro! {ex}");
                }
            }
            
        }

        private void btCadastrar_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescricao.Text) || string.IsNullOrEmpty(txtValor.Text))
                {
                    MessageBox.Show("Descrição e Valor não podem estar vazios.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var recebimento = new Recebimento();
                var recebimentoDAO = new RecebimentoDAO();
                recebimento.Descricao = txtDescricao.Text;
                recebimento.Valor = Convert.ToDouble(txtValor.Text); // Consider using double.TryParse to avoid FormatException
                recebimento.Parcelas = 1; // Ensure this is correct logic for your app
                recebimento.Pagador = ((Cliente)cbRecebidoDe.SelectedItem).Nome;
                recebimento.DataVencimento = txtDataDeVencimento.DisplayDate; 
                recebimento.DataCredenciamento = txtDataCredenciamento.DisplayDate;

                if (!_id.HasValue || _id == 0)
                {
                    recebimentoDAO.Insert(recebimento);
                    MessageBox.Show("Recebimento cadastrado com sucesso!", "ALOC - Alset");
                }
                else
                {
                    recebimento.Id = _id.Value;
                    recebimentoDAO.Update(recebimento);
                    MessageBox.Show($"Recebimento {txtDescricao.Text} atualizado com sucesso!", "ALOC - Alset");
                }

                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Erro de formato: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar o recebimento: {ex.Message}", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}