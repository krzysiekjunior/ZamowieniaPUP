using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZamowieniaPUP.Models
{
    public class ZamowienieViewModel
    {
        public RokZamowienie RokZamowienie { get; set; }
        public List<Zamowienie> Zamowienia { get; set; }

        public int RokID { get; set; }
        public string UslugaDostawa { get; set; }
        public int Ilosc { get; set; }
        public int CenaNetto { get; set; }
        public string Kontrahent { get; set; }
        public bool CzyUmowa { get; set; } 
    }
}
