using System.Collections.Generic;
public class ContaCorrente
{
    public int Id{get;set;}
    public decimal Taxa{get;set;}
    public virtual Conta Conta{get;set;}
}