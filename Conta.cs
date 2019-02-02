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

        public void Sacar(decimal saque, Conta conta)
        {
            
        }
    }
}