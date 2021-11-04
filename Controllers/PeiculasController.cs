using ApiManopla.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace ApiManopla.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    
    public class PeliculasController : ControllerBase 
    {
        public const string ConnectionString = "Data Source=DESKTOP-TI1DTD5\\SQLEXPRESS;Initial Catalog=Peliculas;Integrated Security=True";
        public SqlConnection connection = new SqlConnection(ConnectionString);


        [HttpGet]
        public List<Pelicula> Get() 
        {
            List<Pelicula> peliculas = new List<Pelicula>();
            connection.Open();
            const string query = "SELECT * FROM pelicula ORDER BY id ASC";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read()) 
            {
                Pelicula pelicula = new Pelicula()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nombre = Convert.ToString(reader["nombre"]),
                    Genero = Convert.ToString(reader["genero"]),
                    Director = Convert.ToString(reader["director"]),
                    Fecha = Convert.ToDateTime(reader["fecha"]),
                };
                peliculas.Add(pelicula);
            }
            connection.Close();
            return peliculas;
        }

        [HttpPost]
        public string Post(Pelicula pelicula)
        {
            connection.Open();
            const string query = "INSERT INTO pelicula (nombre,genero,director,fecha) VALUES (@nombre,@genero,@director,@fecha)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@nombre", pelicula.Nombre);
            cmd.Parameters.AddWithValue("@genero", pelicula.Genero);
            cmd.Parameters.AddWithValue("@director", pelicula.Director);
            cmd.Parameters.AddWithValue("@fecha", pelicula.Fecha);
            cmd.ExecuteNonQuery();
            connection.Close();
            return pelicula.ToString();
        }


        // PUT

        [HttpPut("{id}")]
        public string Put(Pelicula pelicula ) 
        {
            
            connection.Open();
            const string query = "UPDATE pelicula SET nombre=@nombre,genero=@genero,director=@director,fecha=@fecha WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", pelicula.Id);
            cmd.Parameters.AddWithValue("@nombre", pelicula.Nombre);
            cmd.Parameters.AddWithValue("@genero", pelicula.Genero);
            cmd.Parameters.AddWithValue("@director", pelicula.Director);
            cmd.Parameters.AddWithValue("@fecha", pelicula.Fecha);
            cmd.ExecuteNonQuery();
            connection.Close();
            return pelicula.ToString();
        }

        //DELETE

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            connection.Open();
            const string query = "DELETE FROM pelicula WHERE id=@id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            connection.Close();
            return id.ToString();
        }


    }
}
