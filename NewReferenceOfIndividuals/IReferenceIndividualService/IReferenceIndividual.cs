using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewReferenceOfIndividuals.Models;
using NewReferenceOfIndividuals.ReferenceIndividualService.Helper;

namespace NewReferenceOfIndividuals.IReferenceIndividualService
{
    public interface IReferenceIndividual
    {
        public PersonList GetAllPersons(GetFilter getFilter);

        public string AddPerson(Person person);

        public Boolean GetOnePerson(string PersonNumber);

        public Person GetPerson(string identity);

        public string EditPerson(Person person);

        public string DeletePerson(string identity);

        public List<PersonList.Persons> GetFullPerson(int x, int y);

        public string name(string name);

        public void DeletePicture(string name);
    }
}
