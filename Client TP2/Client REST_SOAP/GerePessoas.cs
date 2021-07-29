using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class GerePessoas : Form
    {
        // Inicializar instancia de RootPessoa para receber informaçao do metodo getPessoas
        RootPessoa listPessoas = new RootPessoa();

        public GerePessoas()
        {
            InitializeComponent();
        }

        private void GerePessoas_Load(object sender, EventArgs e)
        {
            
            string url = "https://speedwayrentalapi-apim.azure-api.net/api/pessoas/getPessoas";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            
            
            //Prepara Pedido
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //Faz pedido e analisa resposta
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                StreamReader reader = new StreamReader(response.GetResponseStream());

                listPessoas = JsonConvert.DeserializeObject<RootPessoa>(reader.ReadToEnd());

                foreach (Pessoa pessoa in listPessoas.Pessoa)
                {
                    ListViewItem item = new ListViewItem(pessoa.id_pessoa.ToString());
                    item.SubItems.Add(pessoa.primeiro_nome);
                    item.SubItems.Add(pessoa.ultimo_nome);
                    item.SubItems.Add(pessoa.password_pessoa);
                    item.SubItems.Add(pessoa.email_pessoa);

                    listView1.Items.Add(item);
                }

            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login novoLogin = new Login();
            this.Hide();
            novoLogin.ShowDialog();
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {
                string url = "https://speedwayrentalapi-apim.azure-api.net/api/pessoas/updatePessoa";

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(url);


                Pessoa pessoa = new Pessoa();
                pessoa.id_pessoa = int.Parse(textBox1.Text);
                pessoa.primeiro_nome = textBox3.Text;
                pessoa.ultimo_nome = textBox4.Text;
                pessoa.email_pessoa = textBox6.Text;
                pessoa.password_pessoa = textBox5.Text;

                var pessoaJson = JsonConvert.SerializeObject(pessoa);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var stringContent = new StringContent(pessoaJson, Encoding.UTF8, "application/json");
                //Prepara Pedido
                //HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (client)
                {
                    var res = client.PutAsync(url, stringContent);

                    if (res.Result.StatusCode == HttpStatusCode.OK)
                    {
                        MessageBox.Show("Pessoa Atualizada!");

                    }
                    else if (res.Result.StatusCode == HttpStatusCode.NotFound)
                    {
                        MessageBox.Show("Essa pessoa não foi encontrada");
                    }
                    else MessageBox.Show("Processo não foi concluido");
                }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string url = "https://speedwayrentalapi-apim.azure-api.net/api/pessoas/deletePessoa/[EMAIL]";

            // Construção da uri
            StringBuilder uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[EMAIL]", HttpUtility.UrlEncode(textBox2.Text));

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri.ToString());

            using (client)
            {
                var res = client.DeleteAsync(uri.ToString());

                if (res.Result.StatusCode == HttpStatusCode.OK)
                {
                    MessageBox.Show("Pessoa Atualizada!");

                }
                else if (res.Result.StatusCode == HttpStatusCode.NotFound)
                {
                    MessageBox.Show("Essa pessoa não foi encontrada");
                }
                else MessageBox.Show("Processo não foi concluido");
            }


        }
    }
}
