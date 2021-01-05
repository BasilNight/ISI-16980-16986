/*	Descriçao classe:
 * 
 * 
 * 
 * 
 */

using System.ServiceModel;


[ServiceContract]
public interface IAluguer
{

	[OperationContract]
	bool AddAluguer(string email, string marca, string modelo, string dataIn, string dataOut);

	[OperationContract]
	string ProcuraAlugueres(string email);

	[OperationContract]
	bool UpdateAluguer(string id_aluguer, string nome_marca, string nome_modelo, string datain, string dataout);

	[OperationContract]
	bool EliminaAluguer(string id_aluguer);

}


