using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace adonet_db_videogame
{
    internal class VideogameManager
    {
        const string connectionString = "Data Source=localhost;Initial Catalog=mssqlserver-db_videogame;Integrated Security=True";   //definisce una stringa di connessione che viene utilizzata per connettersi a un database Microsoft SQL Server tramite l'oggetto SqlConnection

        public static int InsertVideogame(Videogame videogame)
        {
            int res = 0;
            // istanzio la risorsa nello using
            using (SqlConnection connessioneSql = new SqlConnection(connectionString))
            {
                // da qui in poi posso usare la risorsa 
                try
                {
                    connessioneSql.Open();

                    string sqlQuery =
                        "INSERT INTO videogames(name, overview, release_date, created_at, updated_at, software_house_id) " +
                        "VALUES(@Name, @Overview, @Release_date, GETDATE(), GETDATE(), @Software_house_id)";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, connessioneSql))
                    {
                        cmd.Parameters.AddWithValue("@Name", videogame.Name);
                        cmd.Parameters.AddWithValue("@Overview", videogame.Overview);
                        cmd.Parameters.AddWithValue("@Release_date", videogame.Release_date);
                        cmd.Parameters.AddWithValue("@Software_house_id", videogame.Software_house_id);
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


        public static Videogame SearchById(int id)
        {
            // istanzio la risorsa nello using
            using (SqlConnection connessioneSql = new SqlConnection(connectionString))
            {
                // da qui in poi posso usare la risorsa 
                try
                {
                    connessioneSql.Open();
                    // Console.WriteLine("Connessione effettuata!");

                    string sqlQuery =
                        "SELECT name, overview, release_date, software_house_id FROM videogames WHERE id=@Id";

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, connessioneSql))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Id", id));
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader.GetString(reader.GetOrdinal("name"));
                                string overview = reader.GetString(reader.GetOrdinal("overview"));
                                DateTime release_date = reader.GetDateTime(reader.GetOrdinal("release_date"));


                                int software_house_id = reader.GetOrdinal("software_house_id");

                                Console.WriteLine("Name: " + name);
                                Console.WriteLine("Overview: " + overview);
                                string dateString = release_date.ToString("dd/MM/yyyy");
                                Console.WriteLine("Release date: " + dateString);
                                Console.WriteLine("Software house id: " + software_house_id);

                                Videogame newVideogame = new Videogame(name, overview, release_date, software_house_id);

                                return newVideogame;

                            }
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return null;
                }
            }
        }

        public static void DeleteById(int id)
        {
            // istanzio la risorsa nello using
            using (SqlConnection connessioneSql = new SqlConnection(connectionString))
            {
                try
                {
                    connessioneSql.Open();

                    
                    using (SqlTransaction transaction = connessioneSql.BeginTransaction())
                    {

                        try
                        {
                            //DEVO ELIMINARE TUTTE LE COMPARSE DEL VIDEOGIOCO NELLE ALTRE TABELLE PER MANTENERE COERENZA NEL DATABASE
                            
                            // Elimina le righe dalla tabella ponte
                            string queryPonte = "DELETE FROM tournament_videogame WHERE videogame_id = @id;" +
                                                "DELETE FROM reviews WHERE videogame_id = @id;" +
                                                "DELETE FROM pegi_label_videogame WHERE videogame_id = @id;" +
                                                "DELETE FROM device_videogame WHERE videogame_id = @id;" +
                                                "DELETE FROM category_videogame WHERE videogame_id = @id;" +
                                                "DELETE FROM award_videogame WHERE videogame_id = @id;";
                            using (SqlCommand cmdPonte = new SqlCommand(queryPonte, connessioneSql))
                            {
                                cmdPonte.Transaction = transaction;
                                cmdPonte.Parameters.AddWithValue("@id", id);
                                cmdPonte.ExecuteNonQuery();
                            }

                            // Elimina la riga dalla tabella principale
                            string queryPrincipale = "DELETE FROM videogames WHERE id = @id";
                            using (SqlCommand cmdPrincipale = new SqlCommand(queryPrincipale, connessioneSql))
                            {
                                cmdPrincipale.Transaction = transaction;
                                cmdPrincipale.Parameters.AddWithValue("@id", id);
                                cmdPrincipale.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            Console.WriteLine("Riga eliminata con successo");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            Console.WriteLine("Errore durante l'eliminazione: {0}", ex.Message);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
    }
}