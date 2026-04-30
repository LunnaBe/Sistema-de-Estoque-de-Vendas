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
using System.Windows.Input;

namespace EstoqueInterface.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<string> CodigoFornecedor { get; set; }
        public ObservableCollection<string> NomeProduto { get; set; }
        public ObservableCollection<int> Quantidade { get; set; }
        public ObservableCollection<double> Preco { get; set; }
        public ObservableCollection<DateTime> DataEntrada { get; set; }
        public ObservableCollection<DateTime> DataSaida{ get; set; }
        public ICommand SalvarCommand { get; set; }

        public MainViewModel()
        {
            CodigoFornecedor = new ObservableCollection<string>();
            NomeProduto = new ObservableCollection<string>();
            Quantidade = new ObservableCollection<int>();
            Preco = new ObservableCollection<double>();
            DataEntrada = new ObservableCollection<DateTime>();
            DataSaida = new ObservableCollection<DateTime>();
            SalvarCommand = new RelayCommand(Salvar);
        }

        private async void Salvar()
        {
            try
            {
                var http = new HttpClient();

                var dados = await http.GetFromJsonAsync<List<EstoqueData>>(
                    "https://localhost:7101/api/v1/estoque");

                if (dados != null)
                {
                    CodigoFornecedor.Clear();
                    NomeProduto.Clear();
                    Quantidade.Clear();
                    Preco.Clear();
                    DataEntrada.Clear();
                    DataSaida.Clear();

                    foreach (var item in dados)
                    {
                        CodigoFornecedor.Add(item.Codigo_Fornecedor);
                        NomeProduto.Add(item.Nome_Produto);
                        Quantidade.Add(item.Quantidade);
                        Preco.Add(item.Preco);
                        DataEntrada.Add(item.Data_Entrada);
                        DataSaida.Add(item.Data_Saida);

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar dados: {ex.Message}");
            }
        }

    }
}
