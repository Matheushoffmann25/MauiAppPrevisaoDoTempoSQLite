using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

public class DatabaseService
{
    private readonly SQLiteAsyncConnection _database;

    public DatabaseService()
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PrevisaoTempo.db3");
        _database = new SQLiteAsyncConnection(dbPath);
        _database.CreateTableAsync<Previsao>().Wait();
    }

    public Task<List<Previsao>> GetPrevisoesAsync()
    {
        return _database.Table<Previsao>().ToListAsync();
    }

    public Task<int> SavePrevisaoAsync(Previsao previsao)
    {
        return _database.InsertAsync(previsao);
    }

    public Task<List<Previsao>> GetPrevisaoPorDataAsync(string data)
    {
        return _database.Table<Previsao>().Where(p => p.Data == data).ToListAsync();
    }
}

public class Previsao
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Cidade { get; set; }
    public string Data { get; set; }
    public string Descricao { get; set; }
    public string Temperatura { get; set; }
}
