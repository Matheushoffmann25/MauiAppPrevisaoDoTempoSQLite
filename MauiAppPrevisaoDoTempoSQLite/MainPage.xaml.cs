public partial class MainPage : ContentPage
{
    private DatabaseService _databaseService;
    private PrevisaoService _previsaoService;

    public MainPage()
    {
        InitializeComponent();
        _databaseService = new DatabaseService();
        _previsaoService = new PrevisaoService();
        CarregarPrevisoes();
    }

    private async void CarregarPrevisoes()
    {
        var previsoes = await _databaseService.GetPrevisoesAsync();
        previsoesListView.ItemsSource = previsoes;
    }

    private async void OnBuscarPrevisaoClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(cidadeEntry.Text))
        {
            var previsao = await _previsaoService.ObterPrevisao(cidadeEntry.Text);
            await _databaseService.SavePrevisaoAsync(previsao);
            CarregarPrevisoes();
        }
        else
        {
            await DisplayAlert("Erro", "Digite uma cidade.", "OK");
        }
    }

    private async void OnBuscarPrevisaoPorDataClicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(dataEntry.Text))
        {
            var previsoes = await _databaseService.GetPrevisaoPorDataAsync(dataEntry.Text);
            previsoesListView.ItemsSource = previsoes;
        }
        else
        {
            await DisplayAlert("Erro", "Digite uma data.", "OK");
        }
    }
}
