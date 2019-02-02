using System;
using System.Linq;

namespace LP2_Atividade2
{
    class Program
    {
        static void Main(string[] args)
        {
            InitOperator();
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
        public void CriarConta()
        {

        }
    }
}
