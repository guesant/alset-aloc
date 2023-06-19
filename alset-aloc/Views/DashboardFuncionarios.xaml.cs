using alset_aloc.Helpers;
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
    /// Interação lógica para DashboardFuncionarios.xam
    /// </summary>
    public partial class DashboardFuncionarios : UserControl
    {
        public DashboardFuncionarios()
        {
            InitializeComponent();
            CarregarBusca();
        }


        private void CarregarBusca()
        {
            var funcionarioDAO = new FuncionarioDAO();

            var funcionarios = funcionarioDAO.List();

            var data = funcionarios
                .Select(
                    t => new 
                    {
                        Id = t.Id,
                        Nome = t.Nome,
                        DataNascimento = t.DataNascimento.ToString("dd/MM/yyyy"),
                        Email = t.Email,
                        Genero = t.Genero,
                        Cpf = Mascaras.FormatarCPF(t.Cpf),
                        Rg = t.Rg,
                        Telefone = Mascaras.FormatarTelefone(t.Telefone)
                    }
                )
                .ToArray();

            dgFuncionarios.ItemsSource = data;
        }

        private void dgFuncionarios_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            switch (e.Column.Header.ToString()) {
                case "Id":
                    {
                        e.Column.Header = "ID";
                        break;
                    }
                case "Nome":
                    {
                        e.Column.Header = "Nome";
                        break;
                    }
                case "DataNascimento":
                    {
                        e.Column.Header = "Data de Nascimento";
                        break;
                    }
                case "Cpf":
                    {
                        e.Column.Header = "CPF";
                        break;
                    }
            }
        }
    }
}
