using System.Collections.Generic;

namespace Client_REST_SOAP
{
    //Classe modelo Pessoa de acordo com o modelo da API criada
    public class Pessoa
    {
        public int id_pessoa { get; set; }
        public string primeiro_nome { get; set; }
        public string ultimo_nome { get; set; }
        public string password_pessoa { get; set; }
        public string email_pessoa { get; set; }

    }

    //Classe que contem uma lista de objetos Pessoa
    public class RootPessoa
    {
        public List<Pessoa> Pessoa { get; set; }
    }
}
