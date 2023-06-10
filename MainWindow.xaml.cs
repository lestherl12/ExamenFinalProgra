using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Examen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int Codigo;
        public MainWindow()
        {
            InitializeComponent();
            MostrarPrimerRegistro();
            

        }

        private void MostrarPrimerRegistro()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TOP 1 Nombre, Direccion, Codigo,Precio, Disponible FROM Datos";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["Nombre"].ToString();
                            string producto = reader["Direccion"].ToString();
                            string  precio= reader["Precio"].ToString();
                            string  disponible = reader["Disponible"].ToString();
                            string codigo = reader["codigo"].ToString();


                            texto2.Text = nombre;
                            texto3.Text = producto;
                            texto1.Text = codigo;
                            texto4.Text = precio;
                            texto5.Text = disponible;
                        }
                    }
                }
            }
        }



        private SqlConnection CrearConexion()
        {
            return new SqlConnection("C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string codigos = texto1.Text;

                string query = "SELECT Nombre, Direccion FROM Datos  WHERE Codigo = @Codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", codigos);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string descripcion = reader["Nombre"].ToString();
                            string producto = reader["Direccion"].ToString();

                          


                            texto2.Text = descripcion;
                            texto3.Text = producto;
                         
                            
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el código.");
                        }
                    }
                }
            }
    }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int codigo=Convert.ToInt32(texto1.Text);
            string nombre = texto2.Text;
            string direccion = texto3.Text;
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string codigos = texto1.Text;

                string query = "SELECT Nombre, Direccion FROM Datos  WHERE Codigo = @Codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Datos WHERE Codigo = @Codigo AND Nombre = @Nombre AND Direccion = @Direccion", connection);
                    cmd.Parameters.AddWithValue("@Codigo", codigo);
                    cmd.Parameters.AddWithValue("@Nombre", nombre);
                    cmd.Parameters.AddWithValue("@Direccion", direccion);

                    
                    cmd.ExecuteNonQuery();

                    texto1.Text = "";
                    texto2.Text = "";
                    texto3.Text = "";

                  
                    MessageBox.Show("Los datos se han eliminado correctamente.");
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int codigo = Convert.ToInt32(texto1.Text);
            string nombre = texto2.Text;
            string direccion = texto3.Text;

            
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string checkQuery = "SELECT COUNT(*) FROM Datos WHERE Codigo = @Codigo";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@Codigo", codigo);
                    int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                    if (count > 0)
                    {
                       
                        MessageBox.Show("El código ya existe en la base de datos.");
                        return;
                    }
                }

               
                SqlCommand cmd = new SqlCommand("INSERT INTO Datos (Codigo, Nombre, Direccion) VALUES (@Codigo, @Nombre, @Direccion)", connection);
                cmd.Parameters.AddWithValue("@Codigo", codigo);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Direccion", direccion);

            
                cmd.ExecuteNonQuery();

                // Limpiar los textos después de agregar los datos
                texto1.Text = "";
                texto2.Text = "";
                texto3.Text = "";

                //Mensaje de exito
                MessageBox.Show("Los datos se han agregado correctamente.");
            }

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            
            Codigo++;

            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT Nombre, Direccion FROM Datos WHERE Codigo = @Codigo";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Codigo", Codigo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string nombre = reader["Nombre"].ToString();
                            string producto = reader["Direccion"].ToString();

                            texto2.Text = nombre;
                            texto3.Text = producto;
                            texto1.Text = Codigo.ToString();
                        }
                        else
                        {
                            
                            MostrarPrimerRegistro();
                        }
                    }
                }
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Codigo--;

            if (Codigo >= 1)
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\leste\\OneDrive\\Desktop\\Examen\\Database1.mdf;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT Nombre, Direccion FROM Datos WHERE Codigo = @Codigo";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Codigo", Codigo);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string nombre = reader["Nombre"].ToString();
                                string direccion = reader["Direccion"].ToString();

                                texto2.Text = nombre;
                                texto3.Text = direccion;
                                texto1.Text = Codigo.ToString();
                            }
                        }
                    }
                }
            }
            else
            {
             
                MostrarPrimerRegistro();
            }
        }

        private void list1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
    }

