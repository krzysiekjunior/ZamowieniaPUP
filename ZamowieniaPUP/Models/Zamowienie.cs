using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZamowieniaPUP.Models
{
    public class Zamowienie
    {
        public int Id { get; set; }
        public string UslugaDostawa { get; set; }
        public int Ilosc { get; set; }
        public float CenaNetto { get; set; }
        public string Kontrahent { get; set; }
        public bool CzyUmowa { get; set; }

        public virtual RokZamowienie TenRokZamowienie { get; set; }
    }
}
