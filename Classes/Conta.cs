using System;

namespace DIO.Bank
{
	public class Conta
	{
		// Atributos
		private TipoConta TipoConta { get; set; }
		private long Agencia { get; set; }
		private long NumConta { get; set; }
		private double Saldo { get; set; }
		private double Credito { get; set; }
		private string Nome { get; set; }
		private string Senha { get; set; }

		// Métodos
		public Conta(TipoConta tipoConta, long agencia, long numConta, double saldo, double credito, string nome, string senha)
		{
			this.TipoConta = tipoConta;
			this.Agencia = agencia;
			this.NumConta = numConta;
			this.Saldo = saldo;
			this.Credito = credito;
			this.Nome = nome;
			this.Senha = senha;
		}

		public bool ValidaSenhas(string senha)
		{
			return this.Senha.Equals(senha);
		}

		public bool Sacar(double valorSaque)
		{
            // Validação de saldo suficiente
            if (this.Saldo - valorSaque < (this.Credito *-1)){
                Console.WriteLine("Saldo insuficiente!");
                return false;
            }
            this.Saldo -= valorSaque;

            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
            // https://docs.microsoft.com/pt-br/dotnet/standard/base-types/composite-formatting

            return true;
		}

		public void Depositar(double valorDeposito)
		{
			this.Saldo += valorDeposito;

            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.Nome, this.Saldo);
		}

        public override string ToString()
		{
            string retorno = "";
            retorno += "TipoConta " + this.TipoConta + " | ";
			retorno += "Agência " + this.Agencia + " | ";
			retorno += "Nº Conta " + this.NumConta + " | ";
            retorno += "Nome " + this.Nome + " | ";
			retorno += "Senha " + this.Senha + " | ";
            retorno += "Saldo " + this.Saldo + " | ";
            retorno += "Crédito " + this.Credito;
			return retorno;
		}
	}
}