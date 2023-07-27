using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLayer
{
    public class B2B
    {
        public string ctin { get; set; }
        //public string inv { get; set; }
        //public string idt { get; set; }
        public string val { get; set; }
        //public string pos { get; set; }
        //public string rchrg { get; set; }
        //public string inv_typ { get; set; }       
        //public string num { get; set; }
        //public string txval { get; set; }
        //public string rt { get; set; }
        //public string camt { get; set; }
        //public string samt { get; set; }
        //public string csamt { get; set; }
        public List<B2BItems> itms { get; set; }
       // public List<B2BInv> inv { get; set; }
    }
}
