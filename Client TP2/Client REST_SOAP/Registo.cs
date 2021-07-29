using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class Registo : Form
    {
        public Registo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {

                //Adiciona novo registo na base de dados
                string novourl = "https://speedwayrentalapi-apim.azure-api.net/api/pessoas/inserePessoa";
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(novourl);



                Pessoa pessoa = new Pessoa();
                pessoa.primeiro_nome = textBox1.Text;
                pessoa.ultimo_nome = textBox2.Text;
                pessoa.email_pessoa = textBox3.Text;
                pessoa.password_pessoa = textBox4.Text;

                var pessoaJson = JsonConvert.SerializeObject(pessoa);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(pessoaJson, Encoding.UTF8, "application/json");

                //Prepara Pedido
                using (client)
                {
                    var res = client.PostAsync(novourl, stringContent);

                    if (res.Result.StatusCode.Equals(HttpStatusCode.OK))
                    {
                        MessageBox.Show("Pessoa Adicionada!");
                        Login login = new Login();
                        this.Hide();
                        login.ShowDialog();
                        this.Close();
                    }
                    else if (res.Result.StatusCode.Equals(HttpStatusCode.NotAcceptable))
                    {
                        MessageBox.Show("Este utilizador ja existe");
                    }
                }

            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void Registo_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Login novoLogin = new Login();
            this.Hide();
            novoLogin.ShowDialog();
            this.Close();
        }
    }
}
