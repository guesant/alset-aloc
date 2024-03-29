﻿using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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

  

    public delegate UserControl? ControlDelegate();

    public class DashboardMenuButton
    {
        public string Id { get; set; }
        public string Label { get; set; }
        public PackIconKind Icon { get; set; }

        public ControlDelegate? Control { get; internal set; }
    }

    public class DashboardModel : INotifyPropertyChanged
    {
        List<DashboardMenuButton> GetMenuButtons()
        {
            return new List<DashboardMenuButton>()
            {
                new DashboardMenuButton()
                {
                    Id = "funcionarios",
                    Icon = PackIconKind.Worker,
                    Label = "Funcionários",
                    Control = () => new DashboardFuncionarios(),
                },

                new DashboardMenuButton()
                {
                    Id = "fornecedores",
                    Icon = PackIconKind.Business,
                    Label = "Fornecedores",
                    Control = () => new DashboardFornecedor(),

                },

                // ...

                // Financeiro

                new DashboardMenuButton()
                {
                    Id = "compras",
                    Icon = PackIconKind.ReceiptText,
                    Label = "Compras"
                },

                // ...

                new DashboardMenuButton()
                {
                    Id = "clientes",
                    Icon = PackIconKind.People,
                    Label = "Clientes",
                    Control = () => new DashboardClientes(),

                },

                new DashboardMenuButton()
                {
                    Id = "veiculos",
                    Icon = PackIconKind.LocalShipping,
                    Label = "Veículos",
                    Control = () => new DashboardVeiculo(),

                },

                new DashboardMenuButton()
                {
                    Id = "locacoes",
                    Icon = PackIconKind.Key,
                    Label = "Locações",
                    Control = () => new DashboardLocacoes(),
                },


                // ...


                
                new DashboardMenuButton()
                {
                    Id = "relatorios",
                    Icon = PackIconKind.ChartBox,
                    Label = "Relatórios"
                },

                //

                new DashboardMenuButton()
                {
                    Id = "recebimentos",
                    Icon = PackIconKind.PointOfSale,
                    Label = "Recebimentos",
                    Control = () => new DashboardRecebimentos(),
                }
            };

        }

        private UserControl? _dashboardContent;

        public UserControl? DashboardContent
        {
            get { return _dashboardContent; }
            set
            {
                _dashboardContent = value;
                OnPropertyChanged(nameof(DashboardContent));
            }
        }


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
            MenuButtons = GetMenuButtons();
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

            ChangeView(null);
        }

        public void ChangeView(string? id)
        {
            UserControl control = new DashboardHome();

            var menuButton = this.model.FindMenuButtonById(id);

            if (menuButton != null)
            {
                var menuButtonControl = menuButton.Control != null ? menuButton.Control() : null;

                if (menuButtonControl != null)
                {
                    control = menuButtonControl;
                }
            }

            this.model.DashboardContent = control;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var id = (string)((Button)sender).Tag;
            this.ChangeView(id);
        }
        
    }
}
