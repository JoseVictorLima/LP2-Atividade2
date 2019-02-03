using System;
using System.Linq;

namespace LP2_Atividade2
{
    public class Conta
    {
        public int Id{get;set;}

        public decimal Saldo{get;set;}
        public string Titular{get;set;}
        public virtual Agencia Agencia{get;set;}

        public virtual Cliente Cliente{get;set;}

        public void Sacar(decimal valor, Conta conta, BancoContext context,int type)
        {
            decimal desconto;
            if(conta.Saldo >= valor)
            {
                try
                {
                    if(type == 1)
                    {
                        var contaC = context.ContasCorrente.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                        desconto = valor * contaC.Taxa;
                        conta.Saldo = conta.Saldo - (valor + desconto);
                        context.SaveChanges();
                    }
                    else if(type==2)
                    {
                        var contaC = context.ContasPoupanca.Where(b => b.Conta == conta)
                                              .FirstOrDefault();
                        conta.Saldo = conta.Saldo - valor;
                        context.SaveChanges();
                    }
                    Console.WriteLine("Operação Realizada com sucesso!");
                }catch(Exception error)
                {
                    Console.WriteLine("Não foi possivel efetuar esta ação");
                }
            }
            else
            {
                Console.WriteLine("A conta não possui saldo suficiente");
            }
        }

        public void Depositar(decimal valor, Conta conta, BancoContext context,int type)
        {
            decimal desconto;
            try
            {
                if(type == 1)
                {
                    var contaC = context.ContasCorrente.Where(b => b.Conta == conta)
                                          .FirstOrDefault();
                    desconto = valor * contaC.Taxa;
                    conta.Saldo = conta.Saldo + (valor - desconto);
                    context.SaveChanges();
                }
                else if(type==2)
                {
                    var contaC = context.ContasPoupanca.Where(b => b.Conta == conta)
                                          .FirstOrDefault();
                    conta.Saldo = conta.Saldo + valor;
                    context.SaveChanges();
                }
                Console.WriteLine("Operação Realizada com sucesso!");
            }catch(Exception error)
            {
                Console.WriteLine("Não foi possivel efetuar esta ação");
            }
            
        }
    }
}