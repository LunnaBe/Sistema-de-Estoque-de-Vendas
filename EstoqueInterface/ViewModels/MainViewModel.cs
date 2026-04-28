using EstoqueInterface.Commands;
using Shared;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public ICommand AddItemCommand { get; set; }

        public MainViewModel()
        {
            Itens = new ObservableCollection<EstoqueData>();
            AddItemCommand = new RelayCommand(AddItem);
        }

        private async void AddItem()
        {
            var http = new HttpClient();

            var dados = await http.GetFromJsonAsync<List<EstoqueData>>(
                "http://localhost:7101/api/estoque");

            foreach (var item in dados)
            {
                Itens.Add(item);
            }
        }
    }
}
