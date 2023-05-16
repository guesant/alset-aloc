using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica interna para Dashboard.xaml
    /// </summary>
    /// 

   public class DashboardMenuButton
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public string Icon { get; set; }
        public UserControl? Control { get; internal set; }
    }

    public class DashboardModel : INotifyPropertyChanged
    {

        private List<DashboardMenuButton> _menuButtons;

        public List<DashboardMenuButton> MenuButtons
        {
            get { return _menuButtons; }
            set
            {
                _menuButtons = value;
                OnPropertyChanged(nameof(MenuButtons));
            }
        }

        public DashboardModel()
        {
            List<DashboardMenuButton> menuButtons = new();

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "funcionarios",
                  Icon = "/Images/Icons/Funcionarios.png",
                  Label = "Funcionários"
              }
            );

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "veiculos",
                  Icon = "/Images/Icons/carro.png",
                  Label = "Veículos"
              }
            );


            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "clientes",
                  Icon = "/Images/Icons/Cliente.png",
                  Label = "Clientes",
                  Control = new DashboardClientes(),
              }
            );

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "fornecedores",
                  Icon = "/Images/Icons/Fornecedor.png",
                  Label = "Fornecedores"
              }
            );

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "compras",
                  Icon = "/Images/Icons/Compra.png",
                  Label = "Compras"
              }
            );

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "locacoes",
                  Icon = "/Images/Icons/locacoes.png",
                  Label = "Locações"
              }
            );

            menuButtons.Add(
              new DashboardMenuButton()
              {
                  Id = "relatorios",
                  Icon = "/Images/Icons/relatorios.png",
                  Label = "Relatórios"
              }
            );

            MenuButtons = menuButtons;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public DashboardMenuButton? FindMenuButtonById(string id)
        {
            var menuButton = this.MenuButtons.Find((i) => i.Id == id);

            return menuButton;
        }
    }

    public partial class Dashboard : Window
    {
        private DashboardModel model = new DashboardModel();

        public Dashboard()
        {
            InitializeComponent();

            DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = (string)((Button)sender).Tag;

            var menuButton = this.model.FindMenuButtonById(id);

            if(menuButton != null)
            {
                //Dashboard_Content.Ci = menuButton.Control;
            }

            MessageBox.Show(menuButton == null ? "nulo" : "encontrado");
        }
    }
}
