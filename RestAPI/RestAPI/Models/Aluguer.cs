/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 * Desc: A pasta de Models contem todas as classes modelo que contem as propriedades das mesmas como tambem metodos a ser chamados como serviços nos seus respetivos controladores
 * 
 */

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace RestAPI.Models
{
    /// <summary>
    /// Classe de aluguer feita em REST para testes
    /// </summary>
    public class Aluguer
    {

        // String usada para iniciar a conexao com a base de dados
        //string connect = "Data Source=DESKTOP-5R50HVU;Initial Catalog=&quot;Car Rental&quot;;Persist Security Info=True;User ID=sa;password_pessoa=123";
        string connectionString = "Server=speedwayrental.database.windows.net;Database=Car Rental;User Id = admn; password=Mememaster101; Trusted_Connection=False; Encrypt=True;";

        #region Propriedades
        public int ID_Aluguer { get; set; }
        public int ID_Pessoa { get; set; }
        public string Nome_Marca { get; set; }
        public string Nome_Modelo { get; set; }
        public DateTime Data_Inicio { get; set; }
        public DateTime Data_Final { get; set; }
        #endregion

        #region Metodos

        /// <summary>
        /// Metodo que adiciona um registo de aluguer usando o email da pessoa para identificar
        /// </summary>
        /// <param name="email"></param>
        /// <param name="marca"></param>
        /// <param name="modelo"></param>
        /// <param name="dataIn"></param>
        /// <param name="dataOut"></param>
        /// <returns></returns>
        public bool AddAluguer(string email, string marca, string modelo, string dataIn, string dataOut)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

            }
            catch
            {
                //Conexao falhou
                connection.Close();
                return false;
            }
            if (connection.State.ToString() == "Open")
            {
                string comando;
                comando = "Insert into aluguer (id_pessoa, nome_modelo, nome_marca, data_inicio, data_final) values((select id_pessoa from pessoa where email_pessoa = @email), @nomeMarca, @nomeModelo, @dataIn, @dataOut);";

                SqlCommand cmdins = new SqlCommand(comando, connection);

                cmdins.Parameters.AddWithValue("@email", email);
                cmdins.Parameters.AddWithValue("@nomeMarca", marca);
                cmdins.Parameters.AddWithValue("@nomeModelo", modelo);
                cmdins.Parameters.AddWithValue("@dataIn", DateTime.ParseExact(dataIn, "dd/MM/yyyy HH:mm:ss", null));
                cmdins.Parameters.AddWithValue("@dataOut", DateTime.ParseExact(dataOut, "dd/MM/yyyy HH:mm:ss", null));

                int res = cmdins.ExecuteNonQuery();
                if (res > 0)
                {
                    return true;
                }
                else return false;
            }
            return false;
        }

        /// <summary>
        /// Metodo que retorna todos os alugueres atribuidos a uma pessoa
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string ProcuraAlugueres(string email)
        {
            string json;
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                connection.Close();
                return json = null;
            }
            if (connection.State.ToString() == "Open")
            {
                string comando = "SELECT aluguer.id_aluguer ,pessoa.primeiro_nome, pessoa.ultimo_nome,pessoa.email_pessoa ,aluguer.nome_marca, aluguer.nome_modelo, aluguer.data_inicio, aluguer.data_final from aluguer INNER JOIN pessoa on aluguer.id_pessoa = pessoa.id_pessoa where pessoa.email_pessoa = @email;";

                SqlCommand cmd = new SqlCommand(comando, connection);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.ExecuteNonQuery();
                DataSet dataSet = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataSet, "Alugueres");

                json = JsonConvert.SerializeObject(dataSet);
                connection.Close();
                return json;
            }
            else return json = null;
        }

        /// <summary>
        /// Metodo que atualiza um aluguer com informaçoes novas
        /// </summary>
        /// <param name="id_aluguer"></param>
        /// <param name="nome_marca"></param>
        /// <param name="nome_modelo"></param>
        /// <param name="datain"></param>
        /// <param name="dataout"></param>
        /// <returns></returns>
        public bool UpdateAluguer(string id_aluguer, string nome_marca, string nome_modelo, string datain, string dataout)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                connection.Close();
                return false;
            }
            if (connection.State.ToString() == "Open")
            {
                string comando = "Update aluguer SET nome_modelo = @nomemarca, nome_marca = @nomemodelo, data_inicio = @datain , data_final = @dataout WHERE id_aluguer = @idaluguer";

                SqlCommand cmd = new SqlCommand(comando, connection);

                cmd.Parameters.AddWithValue("@nomemarca", nome_marca);
                cmd.Parameters.AddWithValue("@nomemodelo", nome_modelo);
                cmd.Parameters.AddWithValue("@datain", DateTime.Parse(datain));
                cmd.Parameters.AddWithValue("@dataout", DateTime.Parse(dataout));
                cmd.Parameters.AddWithValue("@idaluguer", id_aluguer);

                cmd.ExecuteNonQuery();
                DataSet dataSet = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataSet, "Alugueres");

                string json = JsonConvert.SerializeObject(dataSet);
                connection.Close();
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Metodo que Elimina um Aluguer da base de dados
        /// </summary>
        /// <param name="id_aluguer"></param>
        /// <returns></returns>
        public bool EliminaAluguer(string id_aluguer)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                connection.Close();
                return false;
            }
            if (connection.State.ToString() == "Open")
            {
                string comando = "DELETE FROM aluguer WHERE id_aluguer = @idaluguer;";

                SqlCommand cmd = new SqlCommand(comando, connection);

                cmd.Parameters.AddWithValue("@idaluguer", id_aluguer);

                cmd.ExecuteNonQuery();
                DataSet dataSet = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(dataSet, "Alugueres");

                string json = JsonConvert.SerializeObject(dataSet);
                connection.Close();
                return true;
            }
            else return false;
        }
        #endregion
    }

    /// <summary>
    /// Classe modelo Root que contém uma lista composta de objetos Aluguer
    /// </summary>
    public class Root
    {
        public List<Aluguer> Alugueres { get; set; }
    }
}
