using System.Collections.Generic;
public class Banco
{
    public int Id{get;set;}
    public Banco()
    {
        Agencias = new List<Agencia>();
    }
    public string Nome{get;set;}

    public virtual ICollection<Agencia> Agencias{get;set;}
}