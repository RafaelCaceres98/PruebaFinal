using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using System.Data.SqlClient;

namespace DAL
{
    public class RepositoryPerson
    {
        SqlConnection conexion;
        SqlDataReader datos;
        SqlCommand cmd;
        Persona persona;
        IList<Persona> personas;

        public RepositoryPerson(SqlConnection connection)
        {
            this.conexion = connection;
        }

        public void Guardar(Persona persona)
        {
            using (cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Persona (Identificacion,Nombre,Apellido,Edad,Genero,Pulsacion) VALUES"
                    + "(@Identificacion,@Nombre,@Apellido,@Edad,@Genero,@Pulsacion)";

                cmd.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar).Value = persona.Identificacion;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = persona.Nombre;
                cmd.Parameters.Add("@Apellido", System.Data.SqlDbType.VarChar).Value = persona.Apellido;
                cmd.Parameters.Add("@Edad", System.Data.SqlDbType.Int).Value = persona.Edad;
                cmd.Parameters.Add("@Genero", System.Data.SqlDbType.Char).Value = persona.Genero;
                cmd.Parameters.Add("@Pulsacion", System.Data.SqlDbType.Decimal).Value = persona.Pulsacion;
                cmd.ExecuteNonQuery();

            }
        }

        public IList<Persona> Consultar()
        {
            using (cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Persona";
                datos = cmd.ExecuteReader();
            }
            return RealizarConsulta(datos);
        }


        public IList<Persona> ConsultaPorSexo(char genero)
        {
            using (cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Persona WHERE Genero = @Genero";
                cmd.Parameters.Add("@Genero", System.Data.SqlDbType.Char).Value = genero;
                datos = cmd.ExecuteReader();

            }
            return RealizarConsulta(datos);
        }


        public Persona Buscar(string identificacion)
        {
            using (cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Persona WHERE Identificacion = @Identificacion";
                cmd.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar).Value = identificacion;
                datos = cmd.ExecuteReader();
                if (datos.Read())
                {
                    return persona = MapearPersona(datos);
                }
                return null;
            }
         
        }
    

        public void Eliminar(string identificacion)
        {
            using(cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Persona  WHERE Identificacion = @Identificacion";
                cmd.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar).Value = identificacion;
                cmd.ExecuteNonQuery();
            }
        }

        public void Modificar(Persona persona)
        {
            using (cmd = conexion.CreateCommand())
            {
                cmd.CommandText = "UPDATE persona SET"
                    + "Nombre = @Nombre, Apellido = @Apellido, Edad = @Edad, Genero = @Genero, Pulsacion = @Pulsacion"
                    + "WHERE Identificacion = @Identificacion";

                cmd.Parameters.Add("@Identificacion", System.Data.SqlDbType.VarChar).Value = persona.Identificacion;
                cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.VarChar).Value = persona.Nombre;
                cmd.Parameters.Add("@Apellido", System.Data.SqlDbType.VarChar).Value = persona.Apellido;
                cmd.Parameters.Add("@Edad", System.Data.SqlDbType.Int).Value = persona.Edad;
                cmd.Parameters.Add("@Genero", System.Data.SqlDbType.Char).Value = persona.Genero;
                cmd.Parameters.Add("@Pulsaicon", System.Data.SqlDbType.Decimal).Value = persona.Pulsacion;
                cmd.ExecuteNonQuery();

            }

        }



        private IList<Persona> RealizarConsulta(SqlDataReader datos)
            {
                personas = new List<Persona>();
                while (datos.Read())
                {
                    persona = MapearPersona(datos);
                    personas.Add(persona);
                }
                return personas;
            }

            private  Persona MapearPersona(SqlDataReader datos)
            {
                persona = new Persona();
                persona.Identificacion = datos.GetString(0);
                persona.Nombre = datos.GetString(1);
                persona.Apellido = datos.GetString(2);
                persona.Edad = Convert.ToInt32(datos.GetInt32(3));
                persona.Genero = Convert.ToChar(datos.GetString(4));
                return persona;

            }   




        



    }
}
