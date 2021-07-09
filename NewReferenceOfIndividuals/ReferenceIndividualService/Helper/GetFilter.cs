using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewReferenceOfIndividuals.ReferenceIndividualService.Helper
{
    public class GetFilter
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string FindIdentificator { get; set; }
        public string FindPersonalNumber { get; set; }
    }
}
