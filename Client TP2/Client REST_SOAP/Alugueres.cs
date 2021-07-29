using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_REST_SOAP
{

    /// <summary>
    /// Classe modelo de um aluguer
    /// </summary>
    public class Alugueres
    {
        public int id_aluguer { get; set; }
        public string primeiro_nome { get; set; }
        public string ultimo_nome { get; set; }
        public string email_pessoa { get; set; }
        public string nome_marca { get; set; }
        public string nome_modelo { get; set; }
        public string data_inicio { get; set; } // Datas em string devido a um imprevisto na deserialização Json
        public string data_final { get; set; }
    }

    /// <summary>
    /// Classe modelo de uma lista de objetos Alugueres
    /// </summary>
    public class Root
    {
        public List<Alugueres> Alugueres { get; set; }
    }
}
