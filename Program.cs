using System;
using System.Linq;

namespace LP2_Atividade2
{
    class Program
    {
        static void Main(string[] args)
        {
            InitOperator();
            int menu = 0;
            int opt = 1;
            decimal taxa;
            taxa = 0.10M;
            for(opt = opt; opt!=0; opt = opt)
            {
                
                if(menu == 0) menu = MenuInicial(opt);
                opt = menu;
                if(menu == 1) menu = MenuContaSelec(opt);
                else if(menu == 2) menu = CriarConta();
                
            }
            Console.WriteLine("Volte sempre!");
            // Console.WriteLine("Hello World!");
        }

        public static void InitOperator()
        {
            using (var context = new BancoContext()) 
            {   
                if(!context.Bancos.Any())
                {
                    var newBanco = new Banco() { Nome = "Banco do Brasil" };     
                    context.Add(newBanco);     
                    context.SaveChanges();

                    if(!context.Agencias.Any())
                    {
                        var newAgencia = new Agencia() { Numero = "0121" , Banco = newBanco};     
                        context.Add(newAgencia);     
                        context.SaveChanges();
                    }
                    var bancos = context.Set<Banco>(); 
                    foreach (var p in bancos) 
                    {     
                        Console.WriteLine("Nome: " + p.Nome); 
                    } 

                }
                
            }
            
        }

        public static int MenuInicial(int opt)
        {
            Console.WriteLine(" ");
            Console.WriteLine("------------Menu------------");
            Console.WriteLine("Acessar Conta            - 1");
            Console.WriteLine("Criar Conta              - 2");
            Console.WriteLine("Sair                     - 0");
            Console.WriteLine(" ");
            opt = Int32.Parse(Console.ReadLine());
            return opt;
        }

        static int MenuContaSelec(int opt)
        {
            for(opt=opt;opt!=0;opt=opt)
            {
                Console.WriteLine(" ");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Conta Corrente           - 1");
                Console.WriteLine("Conta Poupanca           - 2");
                Console.WriteLine("Sair                     - 0");
                Console.WriteLine(" ");
                opt = Int32.Parse(Console.ReadLine());
                switch(opt)
                {
                    case 1 :
                        MenuContaCorrente(opt);
                    break;

                    case 2 :
                        MenuContaPoupanca(opt);
                    break;

                    case 0 :break;

                    default : 
                        Console.WriteLine("Opção Invalida");
                    break;
                }
            }

            return opt;
        }

        static void MenuContaCorrente(int opt)
        {
            for(opt=opt;opt!=0;opt=opt)
            {
                decimal qnt;
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Corrente-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar         - 1");
                Console.WriteLine("Depositar     - 2");
                Console.WriteLine("Olhar Saldo   - 3");
                Console.WriteLine("Voltar        - 4");
                Console.WriteLine(" ");
                opt = Int32.Parse(Console.ReadLine());
                Console.WriteLine(" ");
                switch(opt)
                {
                    case 1: 
                        Console.WriteLine("Quantidade a sacar:");
                        qnt = decimal.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                        // sacarCorrente(contaC,qnt);
                    break;

                    case 2: 
                        Console.WriteLine("Quantidade a depositar:");
                        qnt = decimal.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                        // depositarCorrente(contaC,qnt);
                    break;

                    case 3: 
                        // printarCorrente(contaC);
                    break;
                    case 4 : 
                        opt = 0;
                    break;
                    default :
                        Console.WriteLine("Opção Invalida");
                    break;
                }
            }
        }

        static void MenuContaPoupanca(int opt)
        {  
            for(opt=opt;opt!=0;opt=opt)
            {
                decimal qnt;
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Poupanca-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar         - 1");
                Console.WriteLine("Depositar     - 2");
                Console.WriteLine("Olhar Saldo   - 3");
                Console.WriteLine("Voltar        - 4");
                Console.WriteLine(" ");
                opt = Int32.Parse(Console.ReadLine());
                Console.WriteLine(" ");
                switch(opt)
                {
                    case 1: 
                        Console.WriteLine("Quantidade a sacar:");
                        qnt = decimal.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                        // sacarPoupanca(contaP,qnt);
                    break;

                    case 2: 
                        Console.WriteLine("Quantidade a depositar:");
                        qnt = decimal.Parse(Console.ReadLine());
                        Console.WriteLine(" ");
                        // depositarPoupanca(contaP,qnt);
                    break;

                    case 3: 
                        // printarPoupanca(contaP);
                    break;
                    case 4 :
                        opt = 0;
                    break;
                    default :
                        Console.WriteLine("Opção Invalida");
                    break;
                }
            }
        }
        public static int CriarConta()
        {
            using (var context = new BancoContext())
            {
                string cpf;
                int idade;
                string agencia;
                Agencia agenciaCliente;
                int saldo = 0;
                string titular;
                string nome;
                    Console.WriteLine("Digite seu cpf");
                    cpf = Console.ReadLine();
                    Console.WriteLine("Digite seu Nome");
                    nome = Console.ReadLine();
                    Console.WriteLine("Digite sua idade");
                    idade =Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Selecione uma de nossas Agencias:");
                    Banco banco;
                    var bancos = context.Set<Banco>(); 
                    foreach (var b in bancos) 
                    {     
                        if(b.Nome == "Banco do Brasil")
                            Console.WriteLine("--------" + b.Nome + "--------");
                    } 
                    var agencias = context.Set<Agencia>(); 
                    foreach (var a in agencias) 
                    {     
                        Console.WriteLine(a.Numero);
                    } 
                    Console.WriteLine(" ");
                    bool error = false;
                    for(;error==false;)
                    {
                        Console.WriteLine("Digite a agencia que deseja");
                        agencia = Console.ReadLine();
                        try{
                        var agenciaSelecionada = context.Agencias.Where(b => b.Numero == agencia)
                                                                 .FirstOrDefault();
                        agenciaCliente = agenciaSelecionada;
                        error = false;
                        }catch(Exception e)
                        {
                            Console.WriteLine("Agencia Não Encontrada");
                            error = true;
                        }
                    }
                    Console.WriteLine("Escolha um tipo de conta:");
                    Console.WriteLine(" ");
                    Console.WriteLine("Conta Corrente          1");
                    Console.WriteLine("Conta Poupanca          2");
            }
            return 0;
        }
    }
}
