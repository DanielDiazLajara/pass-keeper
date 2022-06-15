using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pass
{
    public partial class Registro : Form
    {
        public Registro()
        {
            InitializeComponent();
            label5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length < 10 ||  !Usuario.ValidaContraseña(textBox2.Text))
            {
                label5.Text = "La contraseña no cumple los parámetros de seguridad:\n" +
                    "- Una mayúscula\n" +
                    "- Una minúscula\n" +
                    "- Números y caracteres especiales\n" +
                    "- Longitud mínima de 10";
                label5.ForeColor = Color.Red;
                return;
            }
            try
            {
                Usuario user = new Usuario(textBox1.Text, textBox2.Text, 10);
                user.EscribirFichero(textBox2.Text);
                label5.ForeColor = Color.Green;
                label5.Text = "Registro correcto";

            }
            catch (Exception ex)
            {
                label5.ForeColor = Color.Red;
                label5.Text = "Hubo un error en el registro: "+ex.Message;
            }
        }

        private void Registro_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = Usuario.CrearContraseña(14);
        }



        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2.PasswordChar = checkBox1.Checked ? '\0' : '*';
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new Inicio().Show();
            Hide();
        }
    }
}
