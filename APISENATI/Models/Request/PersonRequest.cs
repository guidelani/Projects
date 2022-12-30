using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APISENATI.Models.Request
{
    public class PersonRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Ciudad { get; set; }
    }
}