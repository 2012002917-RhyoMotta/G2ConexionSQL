using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//LIBRERIA
using System.Data.SqlClient;

namespace G1ConexionSQL
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source = LAB-C-PC-17\SQLEXPRESS; Initial Catalog = Producto; Integrated Security = True");
        public Form1()
        {
            InitializeComponent();
            ObtenerRegistros();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = LAB-C-PC-17\SQLEXPRESS; Initial Catalog = Producto; Integrated Security = True");
            con.Open();
            MessageBox.Show("Conexion Exitosa");
            con.Close();
            MessageBox.Show("Conexion Cerrada");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == null || textBox2.Text == null || textBox3 == null)
                    MessageBox.Show("Llene los comapos necesarios");
                else
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("Insert Into Postres(nombre, precio, stock) values ('"+textBox1.Text+"', '"+textBox2.Text+"', '"+textBox3.Text+"')",conn);
                    adapter.SelectCommand.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Datos Almacenados Correctamente");
                    ObtenerRegistros();
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("ERROR de SQL Verificar en" + ex.ToString());
            }
        }

        //Metodo
        private void ObtenerRegistros()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select * from Postres", conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "nombre");
            dataGridView1.DataSource = ds.Tables["nombre"].DefaultView;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Relizar la consulta para visualizar el rtegistro
            string sql = "Select * from Postres WHERE ID=" + textBox5.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            conn.Open();

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox5.Text = reader[0].ToString();
                    textBox1.Text = reader[1].ToString();
                    textBox2.Text = reader[2].ToString();
                    textBox3.Text = reader[3].ToString();
                }
                else
                {
                    MessageBox.Show("Ningun Registro Encontrado con el ID");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.ToString());
            }
            finally 
            { 
                conn.Close();
            }
            textBox5.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string sql = "Delete from Postres WHERE ID=" + textBox4.Text;
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Registro Eliminado Correctamente");
                    ObtenerRegistros();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("ERROR: " + ex.ToString());
            }
            finally 
            { 
                conn.Close(); 
            }
            textBox4.Text = "";
        }
    }
}
