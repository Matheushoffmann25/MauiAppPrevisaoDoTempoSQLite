using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

public class PrevisaoService
{
    private static readonly string ApiUrl = "https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}&units=metric&lang=pt_br";
    private static readonly string ApiKey = "3e09ba159f5345fad9938fc51dbec294";  

    public async Task<Previsao> ObterPrevisao(string cidade)
    {
        using var client = new HttpClient();
        var url = string.Format(ApiUrl, cidade, ApiKey);
        var response = await client.GetStringAsync(url);

        dynamic resultado = JsonConvert.DeserializeObject(response);
        return new Previsao
        {
            Cidade = cidade,
            Data = DateTime.Now.ToString("dd/MM/yyyy"),
            Descricao = resultado.weather[0].description,
            Temperatura = resultado.main.temp + "°C"
        };
    }
}
