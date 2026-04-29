using Shared;
using System.Net.Http.Json;

var http = new HttpClient();
int index = 0;

var listaFornecedores = new List<string> { "NAD534", "DFD234", "CCD234", "FER444", "TEY196", "YUI195" };

var listaProdutos = new List<string> { "Frutas", "Doces", "Vegetais", "Salgadinhos", "Biscoitos", "Bebidas", "Pão"
        , "Carnes", "Laticínios", "Enlatados", "Produtos de Limpeza", "Higiene Pessoal" };

while (true)
{ 

    var estoque = new EstoqueData
    {
        Id = index,
        Codigo_Fornecedor = listaFornecedores[new Random().Next(listaFornecedores.Count)],
        Nome_Produto = listaProdutos[new Random().Next(listaProdutos.Count)],
        Quantidade = new Random().Next(1, 100),
        Preco = Math.Round(new Random().NextDouble() * 100, 2),
        Data_Entrada = DateTime.Now,
        Data_Saida = DateTime.Now.AddDays(30)
    };

    try
    {
        /// Envia os dados do sensor para a API usando uma requisição POST.
        /// Se a resposta indicar sucesso, exibe os dados enviados; caso contrário, exibe o erro retornado pela API.
        var response = await http.PostAsJsonAsync("https://localhost:7101/api/v1/estoque", estoque);

        if (!response.IsSuccessStatusCode)
        {
            var erro = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Erro ao enviar dados: {response.StatusCode} - {erro}");
        }
        else
        {
            Console.WriteLine($"Dados enviados: \n Código do Fornecedor: {estoque.Codigo_Fornecedor}, \n Nome do Produto: {estoque.Nome_Produto}, \n " +
                $"Quantidade: {estoque.Quantidade}, \n Preço: {estoque.Preco}, \n Data de Entrada: {estoque.Data_Entrada}, \n Data de Saída: {estoque.Data_Saida}");
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao enviar dados: {ex.Message}");
    }

    await Task.Delay(2000); // Aguarda 2 segundos antes de enviar o próximo dado
    index++;
}
