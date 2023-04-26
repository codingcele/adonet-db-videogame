
using System;   //il namespace System contiene le classi fondamentali e le funzionalità del framework .NET

using System.Data.SqlClient;   //il namespace System.Data.SqlClient contiene le classi che permettono di connettersi e comunicare con un database Microsoft SQL Server

const string connectionString = "Data Source=localhost;Initial Catalog=mssqlserver-db_videogame;Integrated Security=True";   //definisce una stringa di connessione che viene utilizzata per connettersi a un database Microsoft SQL Server tramite l'oggetto SqlConnection

static int insertVideogame(string name, string overview, string release_date, int software_house_id)
{
    int res = 0;
    // istanzio la risorsa nello using
    using (SqlConnection connessioneSql = new SqlConnection(connectionString))
    {
        // da qui in poi posso usare la risorsa 
        try
        {
            connessioneSql.Open();
            // Console.WriteLine("Connessione effettuata!");

            string sqlQuery =
                "INSERT INTO videogames(name, overview, release_date, software_house_id) " +
                "VALUES(@Name, @Overview, @Release_date, @Software_house_id)";

            using (SqlCommand cmd = new SqlCommand(sqlQuery, connessioneSql))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Overview", overview);
                cmd.Parameters.AddWithValue("@Release_date", release_date);
                cmd.Parameters.AddWithValue("@Software_house_id", software_house_id);
                res = cmd.ExecuteNonQuery();

            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    return res;
}


insertVideogame("Fifa 2020", "Gioco molto bello.", "2020/05/14", 3);