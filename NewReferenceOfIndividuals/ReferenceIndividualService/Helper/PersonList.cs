using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewReferenceOfIndividuals.Models;

namespace NewReferenceOfIndividuals.ReferenceIndividualService.Helper
{
    public class PersonList
    {
        public IEnumerable<Persons> people { get; set; }
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string FindIdentificator { get; set; }
        public string FindPersonalNumber { get; set; }

        public class Persons
        {
            public string Identificator { get; set; }
            public string FirstNameGeo { get; set; }
            public string FirstNameEn { get; set; }
            public string LastNameGeo { get; set; }
            public string LastNameEn { get; set; }
            public string PersonalNumber { get; set; }
        }
    }
}
