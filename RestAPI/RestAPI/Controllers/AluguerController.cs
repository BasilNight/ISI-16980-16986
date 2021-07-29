/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 * Desc: A pasta de Controllers contem todos os controladores dos modelos
 * 
 * 
 */

using Microsoft.AspNetCore.Mvc;
using RestAPI.Models;


namespace RestAPI.Controllers
{
    [ApiController]
    [Route("api/aluguer")]
    /// <summary>
    /// Controller para "Aluguer"
    /// </summary>
    public class AluguerController : Controller
    {
        Aluguer aluguer = new Aluguer();

        /// <summary>
        /// Metodo POST que insere dados de um aluguer na base de dados
        /// </summary>
        /// <param name="email"></param>
        /// <param name="marca"></param>
        /// <param name="modelo"></param>
        /// <returns></returns>
        [HttpPost("insereAluguer")]
        public ActionResult InsereAluguerDB(string email, string marca, string modelo, string dataIn, string dataOut)
        {
            if( aluguer.AddAluguer(email, marca, modelo, dataIn, dataOut) == true)
            {
                return Ok(); //Retorna Codigo 200 visto que foi executado com sucesso
            }
            else return new StatusCodeResult(406); //Status code 406: Not Acceptable
        }

        /// <summary>
        /// Metodo GET que devolve os alugueres de uma pessoa usando o seu email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet("procuraAlugueres/{email}")]
        public ActionResult<string> procuraAlugueres(string email)
        {
            string resultado = aluguer.ProcuraAlugueres(email);
            if (resultado == null)
            {
                return NotFound();
            }
            else return resultado;
        }

        /// <summary>
        /// Metodo PUT que atualiza um aluguer na base de dados usando o seu ID
        /// </summary>
        /// <param name="id_aluguer"></param>
        /// <param name="nome_marca"></param>
        /// <param name="nome_modelo"></param>
        /// <param name="datain"></param>
        /// <param name="dataout"></param>
        /// <returns></returns>
        [HttpPut("updateAluguer")]
        public ActionResult UpdateAluguer(string id_aluguer, string nome_marca, string nome_modelo, string datain, string dataout)
        {
            if (aluguer.UpdateAluguer(id_aluguer, nome_marca, nome_modelo, datain, dataout) == true)
            {
                return Ok();
            }
            else return NotFound();
        }

        /// <summary>
        /// Metodo DELETE que elimina um aluguer na base de dados usando o seu ID
        /// </summary>
        /// <param name="id_aluguer"></param>
        /// <returns></returns>
        [HttpDelete("deleteAluguer/{id_aluguer}")]
        public ActionResult EliminaAluguer(string id_aluguer)
        {
            if (aluguer.EliminaAluguer(id_aluguer) == true)
            {
                return Ok();
            }
            else return NotFound();
        }
    }
}
