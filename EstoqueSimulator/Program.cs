using System.Net.Http.Json;

var http = new HttpClient();
int index = 0;

while (true)
{
    var listaFornecedores = new List<string> { "NAD534", "DFD234", "CCD234", "FER444", "TEY196", "YUI195" };

    var listaProdutos = new List<string> { "Frutas", "Doces", "Vegetais", "Salgadinhos", "Biscoitos", "Bebidas", "Pão" 
        , "Carnes", "Laticínios", "Enlatados", "Produtos de Limpeza", "Higiene Pessoal" }
;

var estoqueData = new
    {
        id = $"Id: {index}",
        Codigo_Fornecedor = $"Fornecedor: {listaFornecedores[new Random().Next(listaFornecedores.Count)]}",
        Nome_Produto = $"Produto: {listaProdutos[new Random().Next(listaProdutos.Count)]}",
        Quantidade = $"Quantidade: {new Random().Next(1, 100)}",
        Preco = $"Preço: R${new Random().NextDouble() * 100}",
        Data_Entrada = $"Data de Entrada: {DateTime.Now}",
        Data_Saida = $"Data de Saída: {DateTime.Now.AddDays(30)}"
    };
   
    var response = await http.PostAsJsonAsync("http://localhost:7101/api/estoque", estoqueData);
    if (!response.IsSuccessStatusCode)
    {
        var erro = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Erro ao enviar dados: {response.StatusCode} - {erro}");
    }
    else
    {
        Console.WriteLine($"Dados enviados com sucesso: {estoqueData}");
    }

    await Task.Delay(2000); // Aguarda 2 segundos antes de enviar o próximo dado
    index++;
}
