using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DAL;
using Entity;
using Infraestructura;

namespace BLL
{
    public class ServicePerson
    {
        RepositoryPerson repositoryPerson;
        IList<Persona> personas;
        SqlConnection conexion;
#pragma warning disable CS0169 // El campo 'ServicePerson.Persona' nunca se usa
        Persona persona;
#pragma warning restore CS0169 // El campo 'ServicePerson.Persona' nunca se usa

        public ServicePerson()
        {
            conexion = new SqlConnection(@"SERVER=CPE;DATABASE=Persona;Integrated security=true");
            repositoryPerson = new RepositoryPerson(conexion);

        }

        public void Guardar(Persona persona)
        {
            Email email = new Email();
            string mensajeEmail = string.Empty;
            try
            {
                conexion.Open();
                repositoryPerson.Guardar(persona);
                mensajeEmail = email.EnviarEmail(persona);
                
                conexion.Close();
                
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }

        }

        public IList<Persona> Consultar()
        {
            try
            {
                conexion.Open();
                personas = repositoryPerson.Consultar();
                conexion.Close();
                return personas;
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            return null;
        }

        public IList<Persona> ConsultaPorSexo(char genero)
        {
            try
            {
                conexion.Open();
                personas = repositoryPerson.ConsultaPorSexo(genero);
                conexion.Close();
                return personas;

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }

            return null;

        }

        public Persona Buscar(string identificacion)
        {
            try
            {
                conexion.Open();
                persona = repositoryPerson.Buscar(identificacion);
                conexion.Close();
                return persona;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public void Eliminar(string identificacion)
        {
            try
            {
                conexion.Open();
                repositoryPerson.Eliminar(identificacion);
                conexion.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
           
        }

        public void Modificar(Persona persona)
        {
            try
            {
                conexion.Open();
                repositoryPerson.Modificar(persona);
                conexion.Close();

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
        }



        

    }


}
