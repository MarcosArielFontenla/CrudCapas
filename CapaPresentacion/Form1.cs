using CapaDatos;
using System;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Form1 : Form
    {

        DbProvider DbProvider = new DbProvider();

        private string idProducto = null;
        private bool Editar = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        private void MostrarProductos()
        {
            dataGridView1.DataSource = DbProvider.GetAllEntities();
        }

        private Entity MapEntityFromControls()
        {
            return new Entity()
            {
                Name = txtNombre.Text,
                Descripcion = txtDesc.Text,
                Marca = txtMarca.Text,
                Precio = txtPrecio.Text,
                Stock = txtStock.Text
            };
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Agregar
            if (Editar == false)
            {
                try
                {
                    DbProvider.ExecuteCommand(MapEntityFromControls(), DbActionType.InsertarProductos);
                    MessageBox.Show("Se insertó correctamente!");
                    MostrarProductos();
                    LimpiarForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pude insertar los datos por: " + ex);
                }
            }

            // Editar
            if (Editar == true)
            {
                try
                {
                    DbProvider.ExecuteCommand(MapEntityFromControls(), DbActionType.EditarProductos);
                    MessageBox.Show("Se editó correctamente!");
                    MostrarProductos();
                    LimpiarForm();
                    Editar = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("No se pudieron editar los datos por: " + ex);
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                Editar = true;

                txtNombre.Text = dataGridView1.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDesc.Text = dataGridView1.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtMarca.Text = dataGridView1.CurrentRow.Cells["Marca"].Value.ToString();
                txtPrecio.Text = dataGridView1.CurrentRow.Cells["Precio"].Value.ToString();
                txtStock.Text = dataGridView1.CurrentRow.Cells["Stock"].Value.ToString();
                idProducto = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void LimpiarForm()
        {
            txtDesc.Clear();
            txtMarca.Text = "";
            txtNombre.Clear();
            txtPrecio.Clear();
            txtStock.Clear();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id;
                int.TryParse(dataGridView1.CurrentRow.Cells["id"].Value.ToString(), out id);
                DbProvider.ExecuteCommand(new Entity() { Id = id }, DbActionType.EliminarProducto);
                MessageBox.Show("Eliminado correctamente!");
                MostrarProductos();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }
    }
}
