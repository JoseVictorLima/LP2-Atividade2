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
                    // Console.WriteLine("aqui");
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
                        conta = VerificarContaCorrente(opt,context);
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
                        conta = VerificarContaPoupanca(opt,context);
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
            Console.WriteLine(" ");
            Console.WriteLine("Bem Vindo " + conta.Titular);
            for(;opt!=0;)
            {
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
                        sacarCorrente(conta,context);
                    break;

                    case 2: 
                        depositarCorrente(conta,context);
                    break;

                    case 3: 
                        OlharSaldo(conta);
                    break;

                    case 4: 
                        AtualizarDados(conta,context,1);
                    break;

                    case 5: 
                        OlharSaldo(conta);
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
            Console.WriteLine(" ");
            Console.WriteLine("Bem Vindo " + conta.Titular);
            for(;opt!=0;)
            {
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
                        sacarPoupanca(conta,context);
                    break;

                    case 2: 
                        depositarPoupanca(conta,context);
                    break;

                    case 3: 
                        OlharSaldo(conta);
                    break;

                    case 4: 
                        AtualizarDados(conta,context,2);
                    break;

                    case 5: 
                        OlharSaldo(conta);
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

        public static Conta VerificarContaCorrente(int opt, BancoContext context)
        {   
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Digite o nome do Titular da conta");
            nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            cpf = Console.ReadLine();
                try{
                    var cliente = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    conta = context.Contas.Where(b => b.Titular == nome && b.Cliente == cliente)
                                              .FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    }catch(Exception e)
                    {   
                        Console.WriteLine("Conta não encontrada");
                    }
            return conta;
        }

        public static Conta VerificarContaPoupanca(int opt, BancoContext context)
        {   
            
            Conta conta = new Conta();
            string nome;
            string cpf;
            Console.WriteLine("Digite o nome do Titular da conta");
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
                    }
            return conta;
        }

        static void sacarCorrente(Conta conta, BancoContext context)
        {
                Console.WriteLine("Digite a quantidade a ser Sacada");
                decimal saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque,conta,context,1);
        }

        static void depositarCorrente(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Depositada");
            decimal deposito = Decimal.Parse(Console.ReadLine());
            conta.Depositar(deposito,conta,context,1);
        }

        static void sacarPoupanca(Conta conta, BancoContext context)
        {
                Console.WriteLine("Digite a quantidade a ser Sacada");
                decimal saque = Decimal.Parse(Console.ReadLine());
                conta.Sacar(saque,conta,context,2);
        }

        static void depositarPoupanca(Conta conta, BancoContext context)
        {
            Console.WriteLine("Digite a quantidade a ser Depositada");
            decimal deposito = Decimal.Parse(Console.ReadLine());
            conta.Depositar(deposito,conta,context,2);
        }

        static void OlharSaldo(Conta conta)
        {
            Console.WriteLine("Saldo disponivel: ");
            Console.WriteLine(conta.Saldo);
        }

        static void AtualizarDados(Conta conta, BancoContext context, int opt)
        {
            Console.WriteLine("Por Motivos de Segurança pedimos que redigite seu nome e cpf");
            Console.WriteLine("Digite o nome do Titular da conta");
            string nome = Console.ReadLine();
            Console.WriteLine("Digite o Cpf da conta");
            string cpf = Console.ReadLine();
                if(opt==1)
                {
                    try{
                    var clienteC = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    var contaCorrente = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    clienteC.atualizar(conta,clienteC,context);
                    }catch(Exception e)
                    {   
                        Console.WriteLine("Credenciais incorretas");
                    }
                } else if(opt==2)
                {
                    try{
                    var clienteP = context.Clientes.Where(b => b.Cpf == cpf && b.Nome == nome)
                                              .FirstOrDefault();
                    var contaPoupanca = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                    clienteP.atualizar(conta,clienteP,context);
                    }catch(Exception e)
                    {   
                        Console.WriteLine("Credenciais incorretas");
                    }
                }
        }
    }
}
