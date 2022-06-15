using System;
using System.Windows.Forms;

namespace Pass
{
    public partial class Perfil : Form
    {
        Usuario user;
        public Perfil(Usuario user)
        {
            this.user = user;
            InitializeComponent();
            label1.Text = this.user.nick;
            label3.Text = "";
            label4.Text = "";

            for (int i = 0; i < user.mypasswords.Count; i++)
            {
                comboBox1.Items.Add(user.mypasswords[i].App);
            }
        }

        private void Perfil_Load(object sender, EventArgs e)
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.PasswordChar = checkBox2.Checked ? '\0' : '*';
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox1.Text == "" || textBox2.Text == "")
            {
                label3.Text = "Error; hay campos vacíos";
            }
            else if (textBox2.Text.Length < 10 || !Usuario.ValidaContraseña(textBox2.Text))
            {
                label3.Text = "La contraseña no cumple los parámetros de seguridad:\n" +
                    "- Una mayúscula\n" +
                    "- Una minúscula\n" +
                    "- Números y caracteres especiales\n" +
                    "- Longitud mínima de 10";
                return;
            }
            else
            {
                try
                {
                    user.AddContraseña(textBox1.Text, textBox2.Text, textBox5.Text);
                }
                catch (Exception e1)
                {
                    label3.Text = "Contraseña incorrecta";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                textBox4.Text = user.GetPassword(comboBox1.SelectedItem.ToString(), textBox3.Text).Substring(0,14);
            }catch (Exception ex) { textBox4.Text = ex.Message; }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "") { label3.Text = "Contraseña vacía"; }
            else
            {
                if (Password.CheckPawned(textBox2.Text))
                {
                    label3.Text = "Contraseña comprometida";
                    label3.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    label3.Text = "Contraseña no comprometida";
                    label3.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Perfil(user).Show();
        }
    }
}
