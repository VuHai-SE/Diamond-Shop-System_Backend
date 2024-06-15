using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.DTOs.Request
{
    public class ProductFilterCriteria
    {
        public FourC? FourC { get; set; }
        public List<string> materialList = new List<string>();
        public List<string> genders = new List<string>();
        public List<string> origins = new List<string>();
    }
}
