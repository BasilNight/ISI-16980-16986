using Newtonsoft.Json;
using System;
using System.Windows.Forms;

namespace Client_REST_SOAP
{
    public partial class EliAluguer : Form
    {
        // Criar instancia de serviços
        WCF_SOAP_Services_CloudVersion.AluguerClient aluguerClient = new WCF_SOAP_Services_CloudVersion.AluguerClient();
        //Objeto Pessoa que mantem os dados do utilizador que fez login
        Pessoa preservaUtilizador;

        public EliAluguer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// //Construtor que recebe objeto Pessoa como parametro
        /// </summary>
        /// <param name="utilizador"></param>
        public EliAluguer(Pessoa utilizador) : this()
        {
            this.preservaUtilizador = utilizador;
        }

        private void EliAluguer_Load(object sender, EventArgs e)
        {
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Um ou mais campos não foram preenchidos.");
            }
            else
            {
                try
                {
                    if (aluguerClient.EliminaAluguer(int.Parse(textBox1.Text)) == true)
                    {
                        MessageBox.Show("Aluguer Eliminado com Successo");
                    }
                    else MessageBox.Show("O Processo não foi concluido");
                }
                catch
                {
                    MessageBox.Show("Formato invalido inserido");
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
    }
}
