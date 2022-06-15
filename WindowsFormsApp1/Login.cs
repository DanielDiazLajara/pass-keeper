using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pass
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            label4.Text = "";
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                label4.ForeColor = Color.Red;
                label4.Text = "Rellene todos los campos";
            }
            try
            {
                Usuario user = new Usuario(textBox1.Text, textBox2.Text);
                label4.Text = "todo correcto";
                user.CargaContraseñas();
                new Perfil(user).Show();
                Hide();
            }
            catch (Exception e1) {
                label4.Text = "Login incorrecto: ";
                label4.ForeColor = Color.Red;
                Console.WriteLine(e1.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Inicio().Show();
            Hide();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
