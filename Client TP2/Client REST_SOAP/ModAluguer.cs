using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Windows.Forms;


namespace Client_REST_SOAP
{
    public partial class ModAluguer : Form
    {
        // Criar instancia de serviços
        WCF_SOAP_Services_CloudVersion.AluguerClient aluguerClient = new WCF_SOAP_Services_CloudVersion.AluguerClient();
        //Objeto Pessoa que mantem os dados do utilizador que fez login
        Pessoa preservaUtilizador;

        public ModAluguer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// //Construtor que recebe objeto RootPessoa como parametro
        /// </summary>
        /// <param name="utilizador"></param>
        public ModAluguer(Pessoa utilizador) : this()
        {
            this.preservaUtilizador = utilizador;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ModAluguer_Load(object sender, EventArgs e)
        {
            string url = "https://speedwayrentalapi-apim.azure-api.net/api/veiculos/GetMarcas";


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


            //Recebe string json que contem todos os alugueres pertencentes a uma pessoa retornada de um metodo soap
            string jsonBruto = aluguerClient.ProcuraAlugueres(this.preservaUtilizador.email_pessoa);
            label1.Text = "Alugueres de " + this.preservaUtilizador.primeiro_nome + " " + this.preservaUtilizador.ultimo_nome + "";
            //Deserialização dos dados json para um objeto Root para colocar na listview
            Root listAluguer = JsonConvert.DeserializeObject<Root>(jsonBruto);
            

            foreach (Alugueres alugueres in listAluguer.Alugueres)
            {
                ListViewItem item = new ListViewItem(alugueres.id_aluguer.ToString());
                item.SubItems.Add(alugueres.primeiro_nome);
                item.SubItems.Add(alugueres.ultimo_nome);
                item.SubItems.Add(alugueres.nome_modelo);
                item.SubItems.Add(alugueres.nome_marca);
                item.SubItems.Add(alugueres.data_inicio.ToString());
                item.SubItems.Add(alugueres.data_final.ToString());
                listView1.Items.Add(item);
            }


        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            Menu novoMenu = new Menu(this.preservaUtilizador);
            this.Hide();
            novoMenu.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text) || string.IsNullOrEmpty(comboBox2.Text) || string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {
                try
                {
                    if (aluguerClient.UpdateAluguer(int.Parse(textBox1.Text), comboBox1.Text, comboBox2.Text, dateTimePicker1.Value, dateTimePicker2.Value) == true)
                    {
                        MessageBox.Show("Aluguer Eliminado com Successo");
                    }
                    else MessageBox.Show("O Processo não foi concluido");
                }
                catch
                {
                    MessageBox.Show("Formato ID inserido é invalido");
                }               
            }
        }
    }
}
