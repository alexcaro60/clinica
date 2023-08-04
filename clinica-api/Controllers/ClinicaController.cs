using clinica_api.Entities;
using clinica_api.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace clinica_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClinicaController : ControllerBase
    {
        // GET: api/<ClinicaController>
        [HttpGet("ObtenerMedicos")]
        public IEnumerable<Medico> ObtenerMedicos()
        {
            List<Medico> listMedicos = new List<Medico>();
            MySQLConnection mySQLConnection = new MySQLConnection();
            var conecction = mySQLConnection.Connect();
            MySqlCommand sqlQuery = new MySqlCommand();
            sqlQuery.Connection = conecction;
            sqlQuery.CommandText = "SELECT * FROM medicos";

            using (var reader = sqlQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    Medico medico = new Medico();
                    medico.Id = Convert.ToInt32(reader["Id"]);
                    medico.Nombre = reader["Nombre"].ToString();
                    medico.Especialidad = reader["Especialidad"].ToString();

                    listMedicos.Add(medico);
                }
            }
            conecction.Close();
            return listMedicos;
        }


        [HttpPost("InsertarMedico")]
        public async Task<IActionResult> InsertarMedico(InsertMedico medico)
        {
            try
            {
                MySQLConnection mySQLConnection = new MySQLConnection();
                var connection = mySQLConnection.Connect();
                MySqlCommand sqlQuery = new MySqlCommand();
                sqlQuery.Connection = connection;

                sqlQuery.CommandText = "INSERT INTO medicos (nombre, especialidad) VALUES (@nombre, @especialidad)";
                sqlQuery.Parameters.AddWithValue("@nombre", medico.Nombre);
                sqlQuery.Parameters.AddWithValue("@especialidad", medico.Especialidad);
                sqlQuery.ExecuteNonQuery();
                var successObject = new
                {
                    Mensaje = "Insertado Correctamente",
                    MedicoInsertado = medico  
                };

                return Ok(successObject);
            }
            catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ObtenerDataMedico")]
        public async Task<IActionResult> ObtenerDataMedico([FromBody] int Id)
        {
            try
            {
                Medico medico = new Medico();
                MySQLConnection mySQLConnection = new MySQLConnection();
                var connection = mySQLConnection.Connect();
                MySqlCommand sqlQuery = new MySqlCommand();
                sqlQuery.Connection = connection;

                sqlQuery.CommandText = "SELECT * FROM medicos WHERE id = @Id";
                sqlQuery.Parameters.AddWithValue("@Id", Id);

                using (var reader = sqlQuery.ExecuteReader())
                {
                    while (reader.Read())
                    { 
                        medico.Id = Convert.ToInt32(reader["Id"]);
                        medico.Nombre = reader["Nombre"].ToString();
                        medico.Especialidad = reader["Especialidad"].ToString();
                    }
                }
 
                return Ok(medico);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("EditarMedico")]
        public async Task<IActionResult> EditarMedico(EditMedico medico)
        {
            try
            {
                MySQLConnection mySQLConnection = new MySQLConnection();
                var connection = mySQLConnection.Connect();
                MySqlCommand sqlQuery = new MySqlCommand();
                sqlQuery.Connection = connection;

                // Utiliza parámetros para evitar ataques de inyección SQL
                sqlQuery.CommandText = "UPDATE medicos SET nombre = @nombre, especialidad = @especialidad WHERE id = @id";
                sqlQuery.Parameters.AddWithValue("@nombre", medico.Nombre);
                sqlQuery.Parameters.AddWithValue("@especialidad", medico.Especialidad);
                sqlQuery.Parameters.AddWithValue("@id", medico.Id);
                sqlQuery.ExecuteNonQuery();
                var successObject = new
                {
                    Mensaje = "Editado Correctamente",
                    MedicoActualizado = medico
                };

                return Ok(successObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Citas
        [HttpGet("ObtenerCitas")]
        public IEnumerable<Cita> ObtenerCitas()
        {
            List<Cita> listCitas = new List<Cita>();
            MySQLConnection mySQLConnection = new MySQLConnection();
            var conecction = mySQLConnection.Connect();
            MySqlCommand sqlQuery = new MySqlCommand();
            sqlQuery.Connection = conecction;
            sqlQuery.CommandText = "select * from citas inner join medicos on citas.IdMedico = medicos.id";

            using (var reader = sqlQuery.ExecuteReader())
            {
                while (reader.Read())
                {
                    Cita cita = new Cita();
                    cita.Id = Convert.ToInt32(reader["Id"]);
                    cita.fecha = (DateTime)reader["fecha"];
                    cita.IdMedico = Convert.ToInt32(reader["IdMedico"]);
                    cita.NombreMedico = reader["nombre"].ToString();
                    cita.Especialidad = reader["especialidad"].ToString();

                    listCitas.Add(cita);
                }
            }
            conecction.Close();
            return listCitas;
        }
        [HttpPost("InsertarCitas")]
        public async Task<IActionResult> InsertarCitas(InsertCita cita)
        {
            try
            {
                MySQLConnection mySQLConnection = new MySQLConnection();
                var connection = mySQLConnection.Connect();
                MySqlCommand sqlQuery = new MySqlCommand();
                sqlQuery.Connection = connection;

                sqlQuery.CommandText = "INSERT INTO citas (fecha, IdMedico) VALUES (@fecha, @IdMedico)";
                sqlQuery.Parameters.AddWithValue("@fecha", cita.fecha);
                sqlQuery.Parameters.AddWithValue("@IdMedico", cita.IdMedico);
                sqlQuery.ExecuteNonQuery();
                var successObject = new
                {
                    Mensaje = "Insertado Correctamente",
                    CitaInsertada = cita
                };

                return Ok(successObject);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
