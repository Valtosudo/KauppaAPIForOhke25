using Microsoft.Data.Sqlite;

public record Asiakas(int Id, string Nimi);
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
    //Metodi, jolla lisätään asiakas tietokantaan
    public void LisaaAsiakas(string nimi)
    {
        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();
            //Lisätään asiakas tietokantaan
            var CommandForAddingCustomer = connection.CreateCommand();
            CommandForAddingCustomer.CommandText = @"INSERT INTO Asiakkaat (nimi) VALUES ($nimi)";
            CommandForAddingCustomer.Parameters.AddWithValue("$nimi", nimi);
            CommandForAddingCustomer.ExecuteNonQuery();
        }
    }

    public Dictionary<int, string> HaeAsiakkaat()
    {
        using (var connection = new SqliteConnection(_connectionstring))
        {
            connection.Open();
            //Haetaan kaikki asiakkaat tietokannasta
            var CommandForGettingCustomers = connection.CreateCommand();
            CommandForGettingCustomers.CommandText = @"SELECT * FROM Asiakkaat";
            using (var reader = CommandForGettingCustomers.ExecuteReader())
            {
                var asiakkaat = new Dictionary<int, string>();
                while (reader.Read())
                {
                    asiakkaat.Add(reader.GetInt32(0), reader.GetString(1));
                }
                return asiakkaat;
            }

        }
    }
}