
using EstoqueInterface.Context;
using SQLitePCL;
using System.Configuration;
using System.Data;
using System.Windows;

namespace EstoqueInterface
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            try
            {
                Batteries.Init();

                Database.Initialize();
            }
            catch (Exception ex)
            {
                // Mostrar uma mensagem de erro e encerrar a aplicação
                MessageBox.Show($"Erro ao iniciar: {ex.Message}");
                Shutdown();
            }
        }
    }

}
