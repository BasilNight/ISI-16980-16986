/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 * Desc: A pasta de Controllers contem todos os controladores dos modelos
 * 
 */

using Microsoft.AspNetCore.Mvc;
using RestAPI.Model;


namespace RestAPI.Controllers
{

    [ApiController]
    [Route("api/pessoas")]

    /// <summary>
    /// Controller para "Pessoa"
    /// </summary>
    public class PessoaController : Controller
    {
        Pessoa pessoa = new Pessoa();

        #region Serviços REST

        /// <summary>
        /// Metodo POST que insere dados de uma pessoa na base de dados
        /// </summary>
        /// <param name="primeiro_nome"></param>
        /// <param name="ultimo_nome"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPost("inserePessoa")]
        public ActionResult InserePessoaObjDB(Pessoa pessoaNova)
        {

            if (pessoa.InserePessoaObjDB(pessoaNova) == true)
            {
                return Ok(); //Retorna Codigo 200 visto que foi executado com sucesso
            }
            else return new StatusCodeResult(406); //Status code 406: Not Acceptable
            
        }

        /// <summary>
        /// Metodo GET que vai buscar todos as pessoas registadas à base de dados
        /// </summary>
        /// <returns></returns>
        [HttpGet("getPessoas")]
        public ActionResult<ListPessoas> GetPessoas()
        {
            ListPessoas listPessoas = new ListPessoas();
            listPessoas = pessoa.GetPessoas();
            if (listPessoas == null)
            {
                return NotFound(); // Retorna Codigo 404 visto que nao foi encontrada uma lista de pessoas
            }
            else return listPessoas;
            
        }

        /// <summary>
        /// Metodo GET que, utilizando um email e password verifica a existencia de um utilizador e devolve os seus dados
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("Login/{email}/{password}")]
        public ActionResult<Pessoa> Login(string email, string password)
        {
            pessoa = pessoa.Login(email, password);
            if (pessoa == null)
            {
                return NotFound(); // Retorna Codigo 404 visto que nao foi encontrada uma pessoa
            }
            else return pessoa;
        }

        /// <summary>
        /// Metodo GET que verifica se existe uma pessoa na base de dados
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("ExistePessoa/{email}")]
        public ActionResult ExistePessoa(string email)
        {
            if (pessoa.ExistePessoa(email) == true)
            {
                return Ok(); //Retorna Codigo 200 visto que foi executado com sucesso
            }
            else return NotFound(); // Retorna Codigo 404 visto que nao foi encontrada uma pessoa
        }

        /// <summary>
        /// Metodo PUT que atualiza as informaçoes de uma certa pessoa na base de dados utilizando o seu email para a identificar
        /// </summary>
        /// <param name="primeiro_nome"></param>
        /// <param name="ultimo_nome"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpPut("updatePessoa")]
        public ActionResult UpdatePessoaObj(Pessoa pessoaUpdate)
        {
            if (pessoa.UpdatePessoaObj(pessoaUpdate) == true)
            {
                return Ok(); //Retorna Codigo 200 visto que foi executado com sucesso
            }
            else return NotFound(); // Retorna Codigo 404 visto que nao foi encontrada uma pessoa
        }

        /// <summary>
        /// Metodo DELETE que elimina informaçoes de uma certa pessoa na base de dados usando o seu email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpDelete("deletePessoa/{email}")]
        public ActionResult DeletePessoa(string email)
        {
            if (pessoa.DeletePessoa(email) == true)
            {
                return Ok(); //Retorna Codigo 200 visto que foi executado com sucesso
            }
            else return NotFound(); // Retorna Codigo 404 visto que nao foi encontrada uma pessoa
        }

        [HttpGet("getSoma/{val1}/{val2}")]
        public int WowCoolSoma(int val1, int val2)
        {
            return pessoa.WowCoolSoma(val1, val2);
        }

        #endregion
    }
}
