using System;
using System.Linq;

namespace LP2_Atividade2
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BancoContext();
            InitOperator(context);
            int menu = 0;
            int opt = 1;
            decimal taxa;
            taxa = 0.10M;
            for(; opt!=0;)
            {
                
                if(menu == 0) menu = MenuInicial(opt);
                opt = menu;
                if(menu == 1) menu = MenuContaSelec(opt,context);
                else if(menu == 2) menu = CriarConta(context);
                
            }
            Console.WriteLine("Volte sempre!");
        }

        public static void InitOperator(BancoContext context)
        {  
                if(!context.Bancos.Any())
                {
                    Console.WriteLine("aqui");
                    var newBanco = new Banco() { Nome = "Banco do Brasil" };     
                    context.Add(newBanco);     
                    context.SaveChanges();

                    if(!context.Agencias.Any())
                    {
                        var newAgencia = new Agencia() { Numero = "0121" , Banco = newBanco};     
                        context.Add(newAgencia);     
                        context.SaveChanges();
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

        static int MenuContaSelec(int opt, BancoContext context)
        {
            Conta conta = new Conta();
            bool access = false;
            for(;opt!=0;)
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
                        Console.WriteLine(" ");
                        conta = VerificarConta(opt,context);
                        if(conta==null) {
                            access=false;
                        }
                        else {
                            access=true;
                        }
                        if(access==true)MenuContaCorrente(opt,context,conta);
                    break;

                    case 2 :
                        Console.WriteLine(" ");
                        conta = VerificarConta(opt,context);
                        if(conta==null) {
                            access=false;
                        }
                        else {
                            access=true;
                        }
                        if(access==true)MenuContaPoupanca(opt,context,conta);
                    break;

                    case 0 :break;

                    default : 
                        Console.WriteLine("Opção Invalida");
                    break;
                }
            }

            return opt;
        }

        static void MenuContaCorrente(int opt, BancoContext context, Conta conta)
        {
            for(;opt!=0;)
            {
                decimal qnt;
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Corrente-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar                - 1");
                Console.WriteLine("Depositar            - 2");
                Console.WriteLine("Olhar Saldo          - 3");
                Console.WriteLine("Atualizar dados      - 4");
                Console.WriteLine("Fechar esta conta    - 5");
                Console.WriteLine("Voltar               - 6");
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
                    case 6 : 
                        opt = 0;
                    break;
                    default :
                        Console.WriteLine("Opção Invalida");
                    break;
                }
            }
        }

        static void MenuContaPoupanca(int opt, BancoContext context, Conta conta)
        {  
            for(;opt!=0;)
            {
                decimal qnt;
                Console.WriteLine(" ");
                Console.WriteLine("-------Conta Poupanca-------");
                Console.WriteLine("------------Menu------------");
                Console.WriteLine("Sacar                - 1");
                Console.WriteLine("Depositar            - 2");
                Console.WriteLine("Olhar Saldo          - 3");
                Console.WriteLine("Atualizar dados      - 4");
                Console.WriteLine("Fechar esta conta    - 5");
                Console.WriteLine("Voltar               - 6");
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
        public static int CriarConta(BancoContext context)
        {
                string cpf;
                int idade;
                string agencia;
                Agencia agenciaCliente = new Agencia();
                string nome;
                    Console.WriteLine("Digite seu cpf");
                    cpf = Console.ReadLine();
                    Console.WriteLine("Digite seu Nome");
                    nome = Console.ReadLine();
                    Console.WriteLine("Digite sua idade");
                    idade =Int32.Parse(Console.ReadLine());
                    Console.WriteLine("Selecione uma de nossas Agencias:");
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
                    bool error = true;
                    for(;error!=false;)
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
                    int contaType = 0;
                    for(;contaType!=1 && contaType!=2;)
                    {
                        Console.WriteLine("Escolha um tipo de conta:");
                        Console.WriteLine(" ");
                        Console.WriteLine("Conta Corrente          1");
                        Console.WriteLine("Conta Poupanca          2");
                        contaType = Int32.Parse(Console.ReadLine());
                        switch(contaType)
                        {
                            case 1 :
                                CriarContaCorrente(cpf,nome,idade,agenciaCliente,context);
                            break;

                            case 2 :
                                CriarContaPoupanca(cpf,nome,idade,agenciaCliente,context);
                            break;

                            default :
                                Console.WriteLine("Tipo de conta Invalido");
                            break;
                        }
                    }
            return 0;
        }

        public static void CriarContaCorrente(string cpf,string nome, int idade,Agencia agencia,BancoContext context)
        {
                try
                {
                    var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };     
                    context.Add(newCliente);     
                    context.SaveChanges();
                    decimal saldo = 0;
                    var newConta = new Conta() {Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome};
                    context.Add(newConta);     
                    context.SaveChanges();
                    var newContaCorrente = new ContaCorrente() { Conta = newConta, Taxa = 0.10M};
                    context.Add(newContaCorrente);     
                    context.SaveChanges();
                    Console.WriteLine("Operação Realizada com sucesso!");
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Não foi possivel realizar esta ação!");
                }
            
        }

        public static void CriarContaPoupanca(string cpf,string nome, int idade,Agencia agencia,BancoContext context)
        {
                try
                {
                    var newCliente = new Cliente() { Nome = nome, Cpf = cpf, Idade = idade };     
                    context.Add(newCliente);     
                    context.SaveChanges();
                    decimal saldo = 0;
                    var newConta = new Conta() {Agencia = agencia, Cliente = newCliente, Saldo = saldo, Titular = newCliente.Nome};
                    context.Add(newConta);     
                    context.SaveChanges();
                    decimal taxaJuros = 0;
                    var newContaPoupanca = new ContaPoupanca() { Conta = newConta, TaxaJuros = taxaJuros};
                    context.Add(newContaPoupanca);     
                    context.SaveChanges();
                    Console.WriteLine("Operação Realizada com sucesso!");
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine("Não foi possivel realizar esta ação!");
                }
            
        }

        public static Conta VerificarConta(int opt, BancoContext context)
        {   
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            bool valid = false;
            Console.WriteLine("Digite o Titular da conta");
            nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            cpf = Console.ReadLine();
                try{
                    var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente)
                                              .FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    }catch(Exception e)
                    {   
                        Console.WriteLine("Conta não encontrada");
                        valid = false;
                    }
            return conta;
        }

        static void sacarCorrente(Conta conta,decimal saque)
        {
            decimal saldoAnterior;
            try
            {
                conta.Sacar(saque,conta);

            } catch(Exception error)
            {
                Console.WriteLine("Não foi possivel efetuar esta ação");
            }
        }
    }
}
