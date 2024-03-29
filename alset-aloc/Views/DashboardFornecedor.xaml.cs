﻿using alset_aloc.Helpers;
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
    /// Interação lógica para DashboardFornecedor.xam
    /// </summary>
    public partial class DashboardFornecedor : UserControl
    {
        public List<long> idsSelecionados = new List<long>();

        public DashboardFornecedor()
        {
            InitializeComponent();

            DefinirColunas();

            CarregarBusca();
        }

        private void DefinirColunas()
        {
            dgFornecedor.Columns.Clear();

            // ...

            DataGridCheckBoxColumn colunaSelect = new DataGridCheckBoxColumn();
            Binding colunaSelectBinding = new Binding("IsSelected");
            colunaSelect.Binding = colunaSelectBinding;
            colunaSelect.Header = "#";
            colunaSelect.IsReadOnly = false;
            dgFornecedor.Columns.Add(colunaSelect);

            // ...

            DataGridTextColumn colunaId = new DataGridTextColumn();
            Binding colunaIdBinding = new Binding("Item.Id");
            colunaId.Binding = colunaIdBinding;
            colunaId.Header = "ID";
            colunaId.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaId);

            // ...

            DataGridTextColumn colunaCNPJ = new DataGridTextColumn();
            Binding colunaCNPJBinding = new Binding("Item.CNPJ");
            colunaCNPJ.Binding = colunaCNPJBinding;
            colunaCNPJ.Header = "CNPJ";
            colunaCNPJ.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaCNPJ);

            // ...

            DataGridTextColumn colunaRazaoSocial = new DataGridTextColumn();
            Binding colunaRazaoSocialBinding = new Binding("Item.RazaoSocial");
            colunaRazaoSocial.Binding = colunaRazaoSocialBinding;
            colunaRazaoSocial.Header = "Razão Social";
            colunaRazaoSocial.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaRazaoSocial);

            // ...

            DataGridTextColumn colunaNomeFantasia = new DataGridTextColumn();
            Binding colunaNomeFantasiaBinding = new Binding("Item.NomeFantasia");
            colunaNomeFantasia.Binding = colunaNomeFantasiaBinding;
            colunaNomeFantasia.Header = "Nome Fantasia";
            colunaNomeFantasia.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaNomeFantasia);

            // ...

            DataGridTextColumn colunaEmail = new DataGridTextColumn();
            Binding colunaEmailBinding = new Binding("Item.Email");
            colunaEmail.Binding = colunaEmailBinding;
            colunaEmail.Header = "E-mail";
            colunaEmail.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaEmail);

            // ...

            DataGridTextColumn colunaTelefone = new DataGridTextColumn();
            Binding colunaTelefoneBinding = new Binding("Item.Telefone");
            colunaTelefone.Binding = colunaTelefoneBinding;
            colunaTelefone.Header = "Telefone";
            colunaTelefone.IsReadOnly = true;
            dgFornecedor.Columns.Add(colunaTelefone);

            // ...
        }

     

        private void CarregarBusca()
        {
            var fornecedorDAO = new FornecedorDAO();
            var fornecedores = fornecedorDAO.List();

            var data = fornecedores.Select(fornecedor => new TableEntry<Fornecedor>(fornecedor, this.idsSelecionados)).ToList();

            dgFornecedor.ItemsSource = data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var form = new CadastrarFornecedor();
            form.ShowDialog();
            this.CarregarBusca();
        }
        private void dgFornecedor_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var row = ItemsControl.ContainerFromElement((DataGrid)sender,
                                    e.OriginalSource as DependencyObject) as DataGridRow;

            if (row == null) return;

            var tableEntry = row.DataContext as TableEntry<Fornecedor>;

            var forn = tableEntry.Item;

            var window = new CadastrarFornecedor(forn.Id);
            window.ShowDialog();

            CarregarBusca();

        }

        private void Button_Click_1(object sender , RoutedEventArgs e)
            {
                var result = MessageBox.Show("Deseja excluir os registros?" , "Confirm" , MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                    {
                    foreach (TableEntry<Fornecedor> tableEntry in dgFornecedor.Items)
                        {
                        if (tableEntry.IsSelected)
                            {
                            // A linha foi selecionada, você pode acessar o objeto Funcionario associado a esta linha.
                            Fornecedor fornecedor = tableEntry.Item;

                            var fornecedorDAO = new FornecedorDAO();

                        fornecedorDAO.Delete(fornecedor);

                            // Faça o que precisar com o objeto funcionario.
                            }
                        }
                    } //ao clicar neste botão ele verifica todos os campos que possuem checkbox marcada e retorna a linha em que em que o checkbox se encontra
                CarregarBusca();
                }
            
        }
        }

