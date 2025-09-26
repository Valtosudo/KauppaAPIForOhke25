using Microsoft.Data.Sqlite;

class Kauppakanta
{
    //Tietokantayhteyttä _connectionstring
    private static string _connectionstring = "Data Source=kauppa.db";

    //Rakentaja
    public Kauppakanta()
    {
        //Luodaan tietokantayhteys
        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();

            //Luodaan Asiakkaat-taulut jos sitä ei ole
            var CommandForCreateTable = connection.CreateCommand();
            CommandForCreateTable.CommandText = @"CREATE TABLE IF NOT EXISTS Asiakkaat (id INTEGER PRIMARY KEY, nimi TEXT)";
            CommandForCreateTable.ExecuteNonQuery();
        }
    }
}