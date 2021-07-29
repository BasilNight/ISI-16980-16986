/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 * Desc: A pasta de Models contem todas as classes modelo que contem as propriedades das mesmas como tambem metodos a ser chamados como serviços nos seus respetivos controladores
 * 
 */

using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Text;
using System.Web;



namespace RestAPI.Model
{
    /// <summary>
    /// Classe veiculo define todas a propriedades atribuidas a um veiculo como também os seus metodos
    /// </summary>
    public class Veiculo
    {
        
        #region METODOS

        /// <summary>
        /// Metodo que pesquisa uma certa marca na API externa e mostra todos o
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public Veiculos SearchModelsAPI(string marca)
        {
            // String com url template para a pesquisa de modelos de uma marca na API
            string url = "https://cis-automotive.p.rapidapi.com/getModels?brandName=[MARCA]";

            // Construção da uri
            StringBuilder uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[MARCA]", HttpUtility.UrlEncode(marca));

            // Realização de um "request" à API externa
            var client = new RestClient(uri.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "ae6829c890mshafb5aa6e5e9eb4ap1c6a85jsn985f80de20a5");
            request.AddHeader("x-rapidapi-host", "cis-automotive.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            // Faz a desserialização dos dados em Json para um objeto
            Veiculos listveiculos = JsonConvert.DeserializeObject<Veiculos>(response.Content);
            
            return listveiculos;
        }
        

        /// <summary>
        /// Metodo que retorna lista de modelos de uma marca em forma de objeto veiculos
        /// </summary>
        /// <param name="marca"></param>
        /// <returns></returns>
        public Veiculos SearchModelsAPIObj(string marca)
        {
            // String com url template para a pesquisa de modelos de uma marca na API
            string url = "https://cis-automotive.p.rapidapi.com/getModels?brandName=[MARCA]";

            // Construção da uri
            StringBuilder uri = new StringBuilder();
            uri.Append(url);
            uri.Replace("[MARCA]", HttpUtility.UrlEncode(marca));

            // Realização de um "request" à API externa
            var client = new RestClient(uri.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "ae6829c890mshafb5aa6e5e9eb4ap1c6a85jsn985f80de20a5");
            request.AddHeader("x-rapidapi-host", "cis-automotive.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            // Faz a desserialização dos dados em Json para um objeto
            Veiculos listveiculos = JsonConvert.DeserializeObject<Veiculos>(response.Content);


            return listveiculos;
        }

        /// <summary>
        /// Metodo que retorna todas as marcas de carros na API externa
        /// </summary>
        /// <returns></returns>
        public Marcas SearchBrandAPI()
        {
            
            var client = new RestClient("https://cis-automotive.p.rapidapi.com/getBrands");
            var request = new RestRequest(Method.GET);
            request.AddHeader("x-rapidapi-key", "ae6829c890mshafb5aa6e5e9eb4ap1c6a85jsn985f80de20a5");
            request.AddHeader("x-rapidapi-host", "cis-automotive.p.rapidapi.com");
            IRestResponse response = client.Execute(request);

            // Faz a desserialização dos dados em Json para um objeto
            Marcas listveiculos = JsonConvert.DeserializeObject<Marcas>(response.Content);

            return listveiculos;

        }
        #endregion

    }

    /// <summary>
    /// Classe que recebe dados sobre o nome da marca do carro pesquisado
    /// </summary>
    public class Veiculos
    {
        public string brandName { get; set; }
        public object modelName { get; set; }
        public object regionName { get; set; }
        public object condition { get; set; }
        public object msg { get; set; }
        public int cacheTimeLimit { get; set; }
        public List<Modelos> data { get; set; }

    }

    /// <summary>
    /// Classe que contem os modelos de marcas de carros
    /// </summary>
    public class Modelos
    {
        public string modelName { get; set; }
    }

    /// <summary>
    /// Classe que contem dados de marcas de carros retirados da API externa
    /// </summary>
    public class Marcas
    {
        public object brandName { get; set; }
        public object modelName { get; set; }
        public object regionName { get; set; }
        public object condition { get; set; }
        public object msg { get; set; }
        public int cacheTimeLimit { get; set; }
        public List<string> data { get; set; }
    }
}
