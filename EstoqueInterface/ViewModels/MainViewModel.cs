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
    internal class MainViewModel : BaseViewModel
    {
        public ObservableCollection<EstoqueData> Itens { get; set; }
        public ICommand SalvarCommand { get; set; }

        public MainViewModel()
        {
            Itens = new ObservableCollection<EstoqueData>();
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
                    Itens.Clear();
                    foreach (var item in dados)
                    {
                        Itens.Add(item);
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
