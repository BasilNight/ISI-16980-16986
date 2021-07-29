/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 * Desc: A pasta de Models contem todas as classes modelo que contem as propriedades das mesmas como tambem metodos a ser chamados como serviços nos seus respetivos controladores
 * 
 */


using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;



namespace RestAPI.Model
{
    /// <summary>
    /// Classe que gere todos os dados e metodos de uma pessoa
    /// </summary>
    public class Pessoa
    {

        // String usada para iniciar a conexao com a base de dados
        //string connect = "Data Source=DESKTOP-5R50HVU;Initial Catalog=&quot;Car Rental&quot;;Persist Security Info=True;User ID=sa;password_pessoa=123";
        string connectionString = "Server=speedwayrental.database.windows.net;Database=Car Rental;User Id = admn; password=Mememaster101; Trusted_Connection=False; Encrypt=True;";
        

        #region Propriedades
        public int ID_Pessoa { get; set; }
        public string Primeiro_Nome { get; set; }
        public string Ultimo_Nome { get; set; }
        public string password_pessoa { get; set; }
        public string email_pessoa { get; set; }
        #endregion

        #region Metodos
        
        /// <summary>
        /// Metodo que recebe objeto "Pessoa" e adiciona na base de dados
        /// </summary>
        /// <param name="pessoanova"></param>
        /// <returns></returns>
        public bool InserePessoaObjDB(Pessoa pessoanova)
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
                //Verificar se a pessoa ja existe
                if (ExistePessoa(pessoanova.email_pessoa) == false)
                {
                    //Construçao da query...             
                    string comando;

                    comando = "Insert into pessoa (primeiro_nome, ultimo_nome, password_pessoa, email_pessoa) values(@primeiro_nome, @ultimo_nome, @password, @email);";

                    SqlCommand cmdins = new SqlCommand(comando, connection);

                    cmdins.Parameters.AddWithValue("@primeiro_nome", pessoanova.Primeiro_Nome);
                    cmdins.Parameters.AddWithValue("@ultimo_nome", pessoanova.Ultimo_Nome);
                    cmdins.Parameters.AddWithValue("@password", pessoanova.password_pessoa);
                    cmdins.Parameters.AddWithValue("@email", pessoanova.email_pessoa);


                    //Executa a query
                    int res = cmdins.ExecuteNonQuery();
                    if (res > 0)
                    {
                        connection.Close();
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Metodo que devolve todas as pessoa registadas na BD
        /// </summary>
        /// <returns></returns>
        public ListPessoas GetPessoas()
        {
            ListPessoas listPessoas = new ListPessoas();
            DataSet ds = new DataSet();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                //Conexao falhou
                connection.Close();               
                return null;
            }
            if (connection.State.ToString() == "Open")
            {
                //Construçao da query
                SqlCommand cmdins = new SqlCommand();
                string comando;
                cmdins.Connection = connection;
             
                comando = "SELECT * FROM pessoa";
                cmdins.CommandText = comando;

                SqlDataAdapter da = new SqlDataAdapter(cmdins.CommandText, connection);
                da.Fill(ds, "Pessoa");

                var json = JsonConvert.SerializeObject(ds);
                listPessoas = JsonConvert.DeserializeObject<ListPessoas>(json);
                connection.Close();
                return listPessoas;
            }
            else return null;
        }

        /// <summary>
        /// Verifica se uma pessoa existe na base de dados
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistePessoa(string email)
        {
            DataTable dataTable = new DataTable();
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
                string comando = "SELECT * from pessoa WHERE email_pessoa = @email;";

                SqlCommand cmd = new SqlCommand(comando, connection);

                cmd.Parameters.AddWithValue("@email", email);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);

                if (dataTable.Rows.Count == 1)
                {

                    return true;
                }
                else return false;

            }
            else return false;
            
        }

        /// <summary>
        /// Metodo que procura 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Pessoa Login(string email, string password)
        {
            
            DataTable dataTable = new DataTable();
            Pessoa pessoa = new Pessoa();
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
            }
            catch
            {
                connection.Close();
                return null;
            }
            if (connection.State.ToString() == "Open")
            {
                string comando = "SELECT id_pessoa, primeiro_nome, ultimo_nome, password_pessoa, email_pessoa from pessoa WHERE email_pessoa = @email and password_pessoa = @password";

                SqlCommand cmd = new SqlCommand(comando, connection);

                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);

                if (dataTable.Rows.Count != 0)
                {
                    pessoa.ID_Pessoa = int.Parse(dataTable.Rows[0]["id_pessoa"].ToString());
                    pessoa.Primeiro_Nome = dataTable.Rows[0]["primeiro_nome"].ToString();
                    pessoa.Ultimo_Nome = dataTable.Rows[0]["ultimo_nome"].ToString();
                    pessoa.password_pessoa = dataTable.Rows[0]["password_pessoa"].ToString();
                    pessoa.email_pessoa = dataTable.Rows[0]["email_pessoa"].ToString();
                   
                    return pessoa;
                }
                else return null;

            }
            else return null;
        }
       
        /// <summary>
        /// Faz update de dados de uma pessoa usando um objeto pessoa
        /// </summary>
        /// <param name="pessoaUpdate"></param>
        /// <returns></returns>
        public bool UpdatePessoaObj(Pessoa pessoaUpdate)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            //Tenta verificar se a conexao é efetuada sem problema
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
                //Construçao da query           
                string comando;

                comando = "UPDATE pessoa SET primeiro_nome = @primeiro_nome, ultimo_nome = @ultimo_nome, password_pessoa = @password, email_pessoa = @email WHERE id_pessoa = @idpessoa";
                SqlCommand cmdins = new SqlCommand(comando, connection);

                cmdins.Parameters.AddWithValue("@primeiro_nome", pessoaUpdate.Primeiro_Nome);
                cmdins.Parameters.AddWithValue("@ultimo_nome", pessoaUpdate.Ultimo_Nome);
                cmdins.Parameters.AddWithValue("@password", pessoaUpdate.password_pessoa);
                cmdins.Parameters.AddWithValue("@email", pessoaUpdate.email_pessoa);
                cmdins.Parameters.AddWithValue("@idpessoa", pessoaUpdate.ID_Pessoa);

                int res = cmdins.ExecuteNonQuery();
                if (res > 0)
                {
                    connection.Close();
                    return true;               
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Metodo para eliminar uma pessoa dos registos da base de dados
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool DeletePessoa(string email)
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
                //Construçao da query             
                string comando;
                             
                comando = "DELETE FROM pessoa WHERE email_pessoa = @email;";
                SqlCommand cmdins = new SqlCommand(comando, connection);
                cmdins.Parameters.AddWithValue("@email", email);

                int res = cmdins.ExecuteNonQuery();
                if (res > 0)
                {
                    connection.Close();
                    return true;
                }
                else return false;
            }
            else return false;
        }


        public int WowCoolSoma(int a, int b)
        {
            return a + b;
        }

        #endregion

    }

    public class ListPessoas
    {
        public List<Pessoa> Pessoa { get; set; }
    }
}
