using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Domain.Specifications
{
    public class SpecificationParams
    {
        public string? sort { get; set; }
        public int? brandId { get; set; }
        public int? typeId { get; set; }

        private string? search;

        public string? Search
        {
            get { return search; }
            set { search = value.ToLower(); }
        }




        private int pageSize = 5;
        private const int Max = 10; 
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize  = value > Max ? Max : value; }
        }
        public int Index { get; set; } = 1;

        //private string search;

        //public string Search
        //{
        //    get { return search; }
        //    set { search = value.ToLower(); }
        //}


    }
}
