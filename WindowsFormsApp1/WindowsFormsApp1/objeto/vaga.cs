using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.objeto
{
    internal class vaga
    {
        public int idvaga { get; set; }
        public int numero { get; set; }
        public DateTime? horario { get; set; }
        public string status { get; set; } // "LIVRE" ou "OCUPADA"
    }
}
