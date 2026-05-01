using ApiVendas.Models;
using EstoqueInterface.Commands;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EstoqueInterface.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        // Propriedades para os campos do formulário
        private string _codigoFornecedor;
        public string CodigoFornecedor
        {
            get => _codigoFornecedor;
            set
            {
                _codigoFornecedor = value; OnPropertyChanged(nameof(CodigoFornecedor));
            }
        }

        private string _nomeProduto;
        public string NomeProduto
        {
            get => _nomeProduto;
            set
            {
                _nomeProduto = value; OnPropertyChanged(nameof(NomeProduto));
            }
        }

        private int _quantidade;
        public int Quantidade
        {
            get => _quantidade;
            set
            {
                _quantidade = value; OnPropertyChanged(nameof(Quantidade));
            }
        }

        private double _preco;
        public double Preco
        {
            get => _preco;
            set
            {
                _preco = value; OnPropertyChanged(nameof(Preco));
            }
        }

        private DateTime _dataEntrada;
        public DateTime DataEntrada
        {
            get => _dataEntrada;
            set
            {
                _dataEntrada = value; OnPropertyChanged(nameof(DataEntrada));
            }
        }

        private DateTime _dataSaida;
        public DateTime DataSaida
        {
            get => _dataSaida;
            set
            {
                _dataSaida = value; OnPropertyChanged(nameof(DataSaida));
            }
        }

        public ICommand SalvarCommand { get; set; }

        public MainViewModel()
        {
            SalvarCommand = new RelayCommand(Salvar);
        }

        public async void Salvar()
        {
            try
            {
                var http = new HttpClient();

                // Criar um objeto DTO com os dados do formulário
                var novoProduto = new EstoqueDataDTO
                {
                    Codigo_Fornecedor = CodigoFornecedor,
                    Nome_Produto = NomeProduto,
                    Quantidade = Quantidade,
                    Preco = Preco,
                    Data_Entrada = DataEntrada,
                    Data_Saida = DataSaida,
                };

                // Enviar os dados para a API
                var response = await http.PostAsJsonAsync("https://localhost:7101/api/v1/estoque", novoProduto);

                if (response.IsSuccessStatusCode)
                {
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro ao salvar os dados. Por favor, tente novamente.", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LimparCampos()
        {
            CodigoFornecedor = string.Empty;
            NomeProduto = string.Empty;
            Quantidade = 0;
            Preco = 0;
            DataEntrada = DateTime.Now;
            DataSaida = DateTime.Now;
        }
    }
}
