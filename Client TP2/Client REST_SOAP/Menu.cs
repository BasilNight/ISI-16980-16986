using System;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class Menu : Form
    {

        // Criar instancia de serviços
        WCF_Soap_Services.AluguerClient aluguerClient = new WCF_Soap_Services.AluguerClient();
        //Objeto Pessoa que mantem os dados do utilizador que fez login
        Pessoa preservaUtilizador;

        public Menu()
        {

            InitializeComponent();
        }

        /// <summary>
        /// //Construtor que recebe objeto Pessoa como parametro
        /// </summary>
        /// <param name="utilizador"></param>
        public Menu(Pessoa utilizador) : this()
        {
            this.preservaUtilizador = utilizador;
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            label1.Text = "Bem vindo " + this.preservaUtilizador.primeiro_nome + "";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            VerAlugueres novoVeralugueres = new VerAlugueres(this.preservaUtilizador);
            this.Hide();
            novoVeralugueres.ShowDialog();
            this.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Aluguer novoAluguer = new Aluguer(this.preservaUtilizador);
            this.Hide();
            novoAluguer.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ModAluguer novomodAluguer = new ModAluguer(this.preservaUtilizador);
            this.Hide();
            novomodAluguer.ShowDialog();
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            EliAluguer novoEliAluguer = new EliAluguer(this.preservaUtilizador);
            this.Hide();
            novoEliAluguer.ShowDialog();
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Login novoLogin = new Login();
            this.Hide();
            novoLogin.ShowDialog();
            this.Close();
        }
    }
}
