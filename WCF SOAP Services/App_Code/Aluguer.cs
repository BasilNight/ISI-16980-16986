using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


public class Aluguer : IAluguer
{
	
    /// <summary>
    /// Metodo que adiciona um registo de aluguer usando o email da pessoa para identificar
    /// </summary>
    /// <param name="email"></param>
    /// <param name="marca"></param>
    /// <param name="modelo"></param>
    /// <returns></returns>
    public bool AddAluguer(string email, string marca, string modelo, string dataIn, string dataOut)
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureCarRentalConnectionString"].ConnectionString);

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
            cmdins.Parameters.AddWithValue("@dataIn", DateTime.Parse(dataIn)); // Dps adicionar forma de mudar datas and stuff, isto é temporario!!!
            cmdins.Parameters.AddWithValue("@dataOut", DateTime.Parse(dataOut));

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
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureCarRentalConnectionString"].ConnectionString);

        try
        {
            connection.Open();
        }
        catch
        {
            connection.Close();
            return "Fail";
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

            string json = JsonConvert.SerializeObject(dataSet);
            connection.Close();
            return json;
        }
        else return "fail";
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
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureCarRentalConnectionString"].ConnectionString);

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
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["AzureCarRentalConnectionString"].ConnectionString);

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
}

//Classes teste
public class Alugueres
{
    public string primeiro_nome { get; set; }
    public string ultimo_nome { get; set; }
    public string nome_marca { get; set; }
    public string nome_modelo { get; set; }
    public DateTime data_inicio { get; set; }
    public DateTime data_final { get; set; }
}

public class AluguerList
{
    public List<Alugueres> Table { get; set; }
}
