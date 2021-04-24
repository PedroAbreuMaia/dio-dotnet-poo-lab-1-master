using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography; 

namespace DIO.Bank
{
	class Program
	{
		static List<Agencia> listAgencia = new List<Agencia>();
		
		static void Main(string[] args)
		{
			listAgencia.Add(new Agencia(0));
			listAgencia.Add(new Agencia(1));
			listAgencia.Add(new Agencia(2));
			listAgencia.Add(new Agencia(3));

			string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarAgencias();
						break;
					case "2":
						ListarContas();
						break;
					case "3":
						InserirConta();
						break;
					case "4":
						Transferir();
						break;
					case "5":
						Sacar();
						break;
					case "6":
						Depositar();
						break;
                    case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}
			
			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
		}

		private static void Depositar()
		{
			Console.Write("Digite o número da agência: ");
			int indiceAgencia = int.Parse(Console.ReadLine());

			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o valor a ser depositado: ");
			double valorDeposito = double.Parse(Console.ReadLine());

            listAgencia[indiceAgencia].Depositar(indiceConta, valorDeposito);
		}

		private static void Sacar()
		{
			Console.Write("Digite o número da agência: ");
			int indiceAgencia = int.Parse(Console.ReadLine());

			Console.Write("Digite o número da conta: ");
			int indiceConta = int.Parse(Console.ReadLine());

			Console.Write("Digite a senha: ");
			string entradaSenha = Console.ReadLine(); 

			string senhaSHA256 = ComputeSha256Hash(entradaSenha);
			if (listAgencia[indiceAgencia].ValidarSenha(indiceConta, senhaSHA256)) {
				Console.Write("Digite o valor a ser sacado: ");
				double valorSaque = double.Parse(Console.ReadLine());

				listAgencia[indiceAgencia].Sacar(indiceConta, valorSaque);
			} else {
				Console.Write("Senha inválida! Operação cancelada.");
			}
		}

		private static void Transferir()
		{
			Console.Write("Digite o número da agência de origem: ");
			int indiceAgenciaOrigem = int.Parse(Console.ReadLine());

			Console.Write("Digite o número da conta de origem: ");
			int indiceContaOrigem = int.Parse(Console.ReadLine());

			Console.Write("Digite a senha: ");
			string entradaSenha = Console.ReadLine(); 

			string senhaSHA256 = ComputeSha256Hash(entradaSenha);
			if (listAgencia[indiceAgenciaOrigem].ValidarSenha(indiceContaOrigem, senhaSHA256)) {

				Console.Write("Digite o número da agência de destino: ");
				int indiceAgenciaDestino = int.Parse(Console.ReadLine());

				Console.Write("Digite o número da conta de destino: ");
				int indiceContaDestino = int.Parse(Console.ReadLine());

				Console.Write("Digite o valor a ser transferido: ");
				double valorTransferencia = double.Parse(Console.ReadLine());

				listAgencia[indiceAgenciaOrigem].Transferir(indiceContaOrigem, listAgencia[indiceAgenciaDestino], indiceContaDestino, valorTransferencia);
			} else {
				Console.Write("Senha inválida! Operação cancelada.");
			}
		}

		private static void InserirConta()
		{
			Console.WriteLine("Inserir nova conta");

			Console.Write("Digite o número da agência: ");
			int indiceAgencia = int.Parse(Console.ReadLine());

			Console.Write("Digite 1 para Conta Fisica ou 2 para Juridica: ");
			int entradaTipoConta = int.Parse(Console.ReadLine());

			Console.Write("Digite o Nome do Cliente: ");
			string entradaNome = Console.ReadLine();

			Console.Write("Digite a senha: ");
			string entradaSenha = Console.ReadLine();

			Console.Write("Digite o saldo inicial: ");
			double entradaSaldo = double.Parse(Console.ReadLine());

			Console.Write("Digite o crédito: ");
			double entradaCredito = double.Parse(Console.ReadLine());

			string senhaSHA256 = ComputeSha256Hash(entradaSenha);

			listAgencia[indiceAgencia].InserirConta(entradaTipoConta: entradaTipoConta, entradaNome: entradaNome, 
														entradaSenha: senhaSHA256, entradaSaldo: entradaSaldo, entradaCredito: entradaCredito);
		}

		private static void ListarContas()
		{
			Console.WriteLine("Listar contas");

			Console.Write("Digite o número da agência: ");
			int indiceAgencia = int.Parse(Console.ReadLine());

			listAgencia[indiceAgencia].ListarContas();			
		}

		private static void ListarAgencias() 
		{
			Console.WriteLine("Listar Agências");

			if (listAgencia.Count == 0)
			{
				Console.WriteLine("Nenhuma agência cadastrada.");
				return;
			}

			for (int i = 0; i < listAgencia.Count; i++)
			{
				Agencia agencia = listAgencia[i];
				Console.Write("#{0} - ", i);
				Console.WriteLine(agencia);
			}
		}

		private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Bank a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar agências");
			Console.WriteLine("2- Listar contas");
			Console.WriteLine("3- Inserir nova conta");
			Console.WriteLine("4- Transferir");
			Console.WriteLine("5- Sacar");
			Console.WriteLine("6- Depositar");
            Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}

		private static string ComputeSha256Hash(string rawData)  
        {  
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())  
            {  
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));  
  
                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();  
                for (int i = 0; i < bytes.Length; i++)  
                {  
                    builder.Append(bytes[i].ToString("x2"));  
                }  
                return builder.ToString();  
            }  
        }  
	}
}
