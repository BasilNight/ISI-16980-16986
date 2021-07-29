using System.Collections.Generic;

namespace Client_REST_SOAP
{
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


    /// <summary>
    /// Classe que recebe dados sobre o nome da marca do carro pesquisado
    /// </summary>
    public class Modelos
    {
        public string brandName { get; set; }
        public object modelName { get; set; }
        public object regionName { get; set; }
        public object condition { get; set; }
        public object msg { get; set; }
        public int cacheTimeLimit { get; set; }
        public List<Datum> data { get; set; }

    }

    /// <summary>
    /// Classe que contem os modelos de marcas de carros
    /// </summary>
    public class Datum
    {
        public string modelName { get; set; }
    }

}
