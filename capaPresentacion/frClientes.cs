using capaEntidad;
using capaNegocio;

namespace capaPresentacion
{
    public partial class frClientes : Form
    {
        CNCliente cNCliente = new CNCliente();
        public frClientes()
        {
            InitializeComponent();
        }

        private void frClientes_Load(object sender, EventArgs e)
        {
            CargarDatos();
        }
        private void CargarDatos()
        {
            gridDatos.DataSource = cNCliente.ObtenerDatos().Tables["tbl"];
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            txtID.Value = 0;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            picFoto.Image = null;
        }
        private void LimpiarForm()
        {
            txtID.Value = 0;
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            picFoto.Image = null;
        }

        private void lnkFoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdFoto.FileName = string.Empty;


            if (ofdFoto.ShowDialog() == DialogResult.OK)
            {
                picFoto.Load(ofdFoto.FileName);
            }
            ofdFoto.FileName = string.Empty;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            bool Resultado;
            CEClientes cEClientes = new CEClientes();
            cEClientes.id = (int)txtID.Value;
            cEClientes.Nombre = txtNombre.Text;
            cEClientes.Apellido = txtApellido.Text;
            cEClientes.Foto = picFoto.ImageLocation;

            Resultado = cNCliente.ValidarDatos(cEClientes);

            if (Resultado == false)
            {
                return;
            }

            if (cEClientes.id == 0)
            {
                cNCliente.CrearCliente(cEClientes);
            }
            else
            {
                cNCliente.EditarCliente(cEClientes);
            }
           
            CargarDatos();
            LimpiarForm();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (txtID.Value == 0) return;
            if (MessageBox.Show("¿Deseas eleminar el registro?", "Titulo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CEClientes cE = new CEClientes();
                cE.id = (int)txtID.Value;
                cNCliente.EliminarCliente(cE);
            }
            CargarDatos();
            LimpiarForm();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gridDatos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            txtID.Value = (int) gridDatos.CurrentRow.Cells["id"].Value;
            txtNombre.Text = gridDatos.CurrentRow.Cells["nombre"].Value.ToString();
            txtApellido.Text = gridDatos.CurrentRow.Cells["apellido"].Value.ToString();
            picFoto.Load(gridDatos.CurrentRow.Cells["foto"].Value.ToString());
            
        }
    }
}  