using System;
using System.Security.Principal;

public class DTOBanka
{
    public string BankaAdi { get; set; }
    public string BankaLinki { get; set; }

  

    public DTOBanka(string bankaadi, string bankalinki)
    {

        BankaAdi = bankaadi;
        BankaLinki = bankalinki;
    }
}