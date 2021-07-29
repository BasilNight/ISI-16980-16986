using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class Aluguer : Form
    {
        // Criar instancia de serviços
        //WCF_Soap_Services.AluguerClient aluguerClient = new WCF_Soap_Services.AluguerClient();
        WCF_SOAP_Services_CloudVersion.AluguerClient aluguerClient = new WCF_SOAP_Services_CloudVersion.AluguerClient();

        //Objeto Pessoa que mantem os dados do utilizador que fez login
        Pessoa preservaUtilizador;

        public Aluguer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// //Construtor que recebe objeto Pessoa como parametro
        /// </summary>
        /// <param name="utilizador"></param>
        public Aluguer(Pessoa utilizador) : this()
        {
            this.preservaUtilizador = utilizador;
        }

        private void Aluguer_Load(object sender, EventArgs e)
        {
            string url = "https://speedwayrentalapi-apim.azure-api.net/api/veiculos/GetMarcas";

            textBox1.Text = this.preservaUtilizador.email_pessoa;

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

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Marcas));
                Marcas marcas = (Marcas)jsonSerializer.ReadObject(response.GetResponseStream());

                foreach (string marca in marcas.data)
                {
                    comboBox1.Items.Add(marca);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string url = "https://speedwayrentalapi-apim.azure-api.net/api/veiculos/GetModelos/[MARCA]";

            comboBox2.Items.Clear();
            comboBox2.Text = null;

            // Construção da uri
            StringBuilder uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[MARCA]", HttpUtility.UrlEncode(comboBox1.Text));
            uri.Replace("+", "%20");
            //Prepara Pedido
            HttpWebRequest request = WebRequest.Create(uri.ToString()) as HttpWebRequest;


            //Faz pedido e analisa resposta
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string message = String.Format("GET falhou. Recebido HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                StreamReader reader = new StreamReader(response.GetResponseStream());

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Modelos));
                Modelos veiculos = (Modelos)jsonSerializer.ReadObject(response.GetResponseStream());

                
                foreach (Datum nomeModelo in veiculos.data)
                {
                    comboBox2.Items.Add(nomeModelo.modelName);
                }

            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {
                if (aluguerClient.AddAluguer(textBox1.Text, comboBox1.Text, comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value) == true)
                {
                    MessageBox.Show("Aluguer Adicionado com Successo");
                }
                else MessageBox.Show("O Processo não foi concluido");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Menu novoMenu = new Menu(this.preservaUtilizador);
            this.Hide();
            novoMenu.ShowDialog();
            this.Close();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
