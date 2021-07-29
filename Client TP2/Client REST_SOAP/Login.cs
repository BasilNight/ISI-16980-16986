using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class Login : Form
    {
        Pessoa novaPessoa = new Pessoa();

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {
                string url = "https://speedwayrentalapi-apim.azure-api.net/api/pessoas/Login/[EMAIL]/[PASSWORD]";

                // Construção da uri
                StringBuilder uri = new StringBuilder();
                uri.Append(url);
                uri.Replace("[EMAIL]", HttpUtility.UrlEncode(textBox1.Text));
                uri.Replace("[PASSWORD]", HttpUtility.UrlEncode(textBox2.Text));

                //Prepara Pedido
                HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;


                //Faz pedido e analisa resposta
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        MessageBox.Show("Os dados introduzidos estão incorretos");
                    }
                    else if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //Recebe dados do utilizador como json
                        StreamReader reader = new StreamReader(response.GetResponseStream());
                        string jsonBruto = reader.ReadToEnd();

                        // Deserializa json para um objeto Pessoa
                        novaPessoa = JsonConvert.DeserializeObject<Pessoa>(jsonBruto);

                        MessageBox.Show("Login Efetuado com Sucesso");
                        Menu novoMenu = new Menu(novaPessoa);
                        this.Hide();
                        novoMenu.ShowDialog();
                        this.Close();
                    }
                }


            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Registo novoRegisto = new Registo();
            this.Hide();
            novoRegisto.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GerePessoas gerePessoas = new GerePessoas();
            this.Hide();
            gerePessoas.ShowDialog();
            this.Close();
        }
    }
}
