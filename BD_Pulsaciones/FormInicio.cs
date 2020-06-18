using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entity;
using BLL;

namespace BD_Pulsaciones
{
    public partial class FormInicio : Form
    {

        Persona persona;
        ServicePerson servicePerson;
        string mensaje;

        public FormInicio()
        {
            InitializeComponent();
            servicePerson = new ServicePerson();
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            Guardar();
        }

        private void Guardar()
        {
            if (RevisarCampos())
            {
                if (persona != null)
                {
                    servicePerson.Guardar(persona);
                    mensaje = "Persona Guardada en BD_SQL Server";
                    MessageBox.Show(mensaje, "",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    ListarPersonas();

                }
                else
                {
                    mensaje = "Primero debe Calcular la pulsacion";
                    btnCalcularPulsacion.Focus();
                    MessageBox.Show(mensaje,"",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                mensaje = "Existen Campos de Informacion Vacios";
                MessageBox.Show(mensaje, "",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }



        private bool RevisarCampos()
        {
            if(txtIdentificacion.Text.Trim().Equals("") || txtNombre.Text.Trim().Equals("") || txtApellido.Text.Trim().Equals("")
                || txtEdad.Text.Trim().Equals("") || cmbGenero.SelectedIndex == 0)
            {
                return false;
            }

            return true;
        }

        private void LimpiarCampos()
        {
            txtIdentificacion.Text = "";
            txtApellido.Text = "";
            txtNombre.Text = "";
            txtEdad.Text = "";
            txtPulsacion.Text = "";
            cmbGenero.SelectedIndex = 0;
        }

        private void BtnCalcularPulsacion_Click(object sender, EventArgs e)
        {
            if (RevisarCampos())
            {
                persona = MapearPersona();
                if (persona != null)
                {
                    txtPulsacion.Text = persona.Pulsacion.ToString();
                }
            }
            else
            {
                mensaje = "Existen campos de informacion vacios";
                MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        //--------------- RECUPERAR DATOS DE LOS COMBOBOX
        private Persona MapearPersona()
        {
            try
            {
                persona = new Persona();
                persona.Identificacion = txtIdentificacion.Text.Trim();
                persona.Nombre = txtNombre.Text.Trim();
                persona.Apellido = txtApellido.Text.Trim();
                persona.Edad = Convert.ToInt32(txtEdad.Text.Trim());
                persona.Genero = Convert.ToChar(cmbGenero.SelectedItem.ToString());
                persona.Email = txtEmail.Text;
                return persona;
            }
            catch (Exception e)
            {
                mensaje = $"Se ha Producido un error {e.Message}";
                MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

            return null;

        }


        private void BtnConsultar_Click(object sender, EventArgs e)
        {
            ListarPersonas();

        }

        private void ListarPersonas()
        {
            dgvPersonas.DataSource = null;
            dgvPersonas.DataSource = servicePerson.Consultar();
        }

        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
            
        }

        private void Buscar()
        {
            string id = txtIdentificacion.Text;
            persona = servicePerson.Buscar(id);
            if (persona != null)
            {
                txtNombre.Text = persona.Nombre;
                txtApellido.Text = persona.Apellido;
                txtEdad.Text = persona.Edad.ToString();
                cmbGenero.Text = persona.Genero.ToString();
                txtPulsacion.Text = persona.Pulsacion.ToString();

            }
            else
            {
                mensaje = " Identificacion no encontrada ";
                MessageBox.Show(mensaje, "",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            //string id = txtIdentificacion.Text;
            //persona = servicePerson.Buscar(id);
            if (persona!= null)
            {
                mensaje = "Desea Eliminar a esta Persona ";
                DialogResult respuesta = MessageBox.Show(mensaje,"",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta.Equals(DialogResult.Yes))
                {
                    servicePerson.Eliminar(persona.Identificacion);
                    LimpiarCampos();
                }

            }
            else
            {
                mensaje = "Primero Debes buscar Identificacion";
                MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }


        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if(persona!= null)
            {
                mensaje = "Desea modificara a esta persona";
                DialogResult respuesta = MessageBox.Show(mensaje,"", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (respuesta.Equals(DialogResult.Yes))
                {
                    persona.Nombre = txtNombre.Text;
                    persona.Apellido = txtApellido.Text;
                    persona.Edad = Convert.ToInt32(txtEdad.Text);
                    persona.Genero = Convert.ToChar(cmbGenero.SelectedItem.ToString());
                    servicePerson.Modificar(persona);
                    LimpiarCampos();

                }

            }
            else
            {
                mensaje = "Primero Debes buscar Identificacion";
                MessageBox.Show(mensaje, "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != 0)
            {
                dgvPersonas.DataSource = null;
                dgvPersonas.DataSource = servicePerson.ConsultaPorSexo(Convert.ToChar(comboBox1.SelectedItem.ToString()));

            }
            

        }

        private void txtPulsacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void FormInicio_Load(object sender, EventArgs e)
        {

        }

        private void txtIdentificacion_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDocumento_Click(object sender, EventArgs e)
        {
            DocumentoExcelService documentoExcelService = new DocumentoExcelService();
            documentoExcelService.CrearDocumento();
            documentoExcelService.LlenarDocumento(servicePerson.Consultar());
        }
    }
}
