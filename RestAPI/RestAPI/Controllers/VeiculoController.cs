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
    [Route("api/veiculos")]
    /// <summary>
    /// Controller para "Veiculo"
    /// </summary>
    public class VeiculoController : ControllerBase
    {
        Veiculo v = new Veiculo();

        #region Serviços Rest

        // Serviço GET para buscar todas as marcas na API externa
        [HttpGet("GetMarcas")]
        public Marcas SearchMarcasAPI()
        {
            return v.SearchBrandAPI();
        }

        // Serviço GET para a busca de dados de modelos de uma certa marca na API externa
        [HttpGet("GetModelos/{marca}")]
        public Veiculos SearchAPI(string marca)
        {
            return v.SearchModelsAPI(marca);
        }
        #endregion

    }
}
