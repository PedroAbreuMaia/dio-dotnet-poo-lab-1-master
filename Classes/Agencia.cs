using System;
using System.Collections.Generic;

namespace DIO.Bank
{
    public class Agencia
    {
        private List<Conta> listContas;
        private long numAgencia;
        private long numContas;

        // Métodos
        public Agencia (long numAgencia) 
        {
            this.listContas = new List<Conta>();
            this.numAgencia = numAgencia;
            this.numContas = 0;
        }

        public bool ValidarSenha(int indiceConta, string senha) 
        {
            return listContas[indiceConta].ValidarSenha(senha);
        }
        
        public void Depositar(int indiceConta, double valorDeposito)
		{
			listContas[indiceConta].Depositar(valorDeposito);
		}

		public void Sacar(int indiceConta, double valorSaque)
		{
            listContas[indiceConta].Sacar(valorSaque);
		}

		public void Transferir(int indiceContaOrigem, Agencia agenciaDestino, int indiceContaDestino, double valorTransferencia)
		{
			listContas[indiceContaOrigem].Sacar(valorTransferencia);
            agenciaDestino.Depositar(indiceContaDestino, valorTransferencia);
		}

		public void InserirConta(int entradaTipoConta, string entradaNome, string entradaSenha, double entradaSaldo, double entradaCredito)
		{
			Conta novaConta = new Conta(tipoConta: (TipoConta)entradaTipoConta,
										agencia: this.numAgencia, 
                                        numConta: this.numContas++,											 
										saldo: entradaSaldo,
										credito: entradaCredito,
										nome: entradaNome, 
                                        senha: entradaSenha);

			listContas.Add(novaConta);
		}

		public void ListarContas()
		{
			Console.WriteLine("Listar contas");

			if (listContas.Count == 0)
			{
				Console.WriteLine("Nenhuma conta cadastrada.");
				return;
			}

			for (int i = 0; i < listContas.Count; i++)
			{
				Conta conta = listContas[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(conta);
			}
		}

        public override string ToString()
		{
            string retorno = "";
			retorno += "Nº Agência " + this.numAgencia + " | ";
            retorno += "Nº de Contas " + this.numContas + " | ";
			return retorno;
		}
        
    }
}