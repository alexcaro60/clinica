using MySql.Data.MySqlClient;

namespace clinica_api
{
    public class MySQLConnection
    {
        private IConfiguration Configuration { get; }
        public MySQLConnection()
        {
        }

        public MySqlConnection Connect()
        {
            string ConnectionString = "Server=localhost;Database=uniminuto-clinica;Uid=root;Pwd=";
            try
            {
                MySqlConnection connection = new MySqlConnection(ConnectionString);
                connection.Open();

                return connection;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error al conectar a la base de datos: {ex.Message}");
                return null;
            }
        }
    }

}
