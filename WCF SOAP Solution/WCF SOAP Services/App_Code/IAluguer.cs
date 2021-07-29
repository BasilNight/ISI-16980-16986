/*
 * Trabalho Pratico 2 ISI
 * 
 * Autores: Luís Martins nº16980, Carlos Ribeiro º16986
 * 
 */

using System;
using System.ServiceModel;
using System.Web.Services;


[ServiceContract]
public interface IAluguer
{

	/// <summary>
	/// Metodo que adiciona um aluguer
	/// </summary>
	/// <param name="email"></param>
	/// <param name="marca"></param>
	/// <param name="modelo"></param>
	/// <param name="dataIn"></param>
	/// <param name="dataOut"></param>
	/// <returns></returns>
	[OperationContract]
	bool AddAluguer(string email, string marca, string modelo, DateTime dataIn, DateTime dataOut);

	/// <summary>
	/// Metodo que devolve lista de alugueres 
	/// </summary>
	/// <param name="email"></param>
	/// <returns></returns>
	[OperationContract]
	string ProcuraAlugueres(string email);

	/// <summary>
	/// Metodo que faz atualização de dados de um aluguer
	/// </summary>
	/// <param name="id_aluguer"></param>
	/// <param name="nome_marca"></param>
	/// <param name="nome_modelo"></param>
	/// <param name="datain"></param>
	/// <param name="dataout"></param>
	/// <returns></returns>
	[OperationContract]
	bool UpdateAluguer(int id_aluguer, string nome_marca, string nome_modelo, DateTime datain, DateTime dataout);

	/// <summary>
	/// Metodo que elimina uma aluguer
	/// </summary>
	/// <param name="id_aluguer"></param>
	/// <returns></returns>
	[OperationContract]
	bool EliminaAluguer(int id_aluguer);

}


