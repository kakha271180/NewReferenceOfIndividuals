using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NewReferenceOfIndividuals.Data;
using NewReferenceOfIndividuals.IReferenceIndividualService;
using NewReferenceOfIndividuals.Models;
using NewReferenceOfIndividuals.ReferenceIndividualService.Helper;

namespace NewReferenceOfIndividuals.ReferenceIndividualService
{
    public class ReferenceIndividual : IReferenceIndividual
    {
       private readonly ApplicationDbContext _application;


        public ReferenceIndividual(ApplicationDbContext application)
        {
            _application = application;
        }

        public string AddPerson(Person person)
        {
            if (GetOnePerson(person.PersonalNumber))
            {
                return "ასეთი პირადი ნომრის პიროვნება არსებობს!";
            }

            if (!GetOnePerson(person.PersonalNumber))
            {


                _application.Persons.Add(person);
                _application.SaveChanges();

            }
            if (GetOnePerson(person.PersonalNumber))
            {
                return "მონაცემები წარმატებით ჩაიწერა!";
            }
            else
            {
                return "მონაცემები ვერ ჩაიწერა!";
            }
        }

        public PersonList GetAllPersons(GetFilter getFilter)
        {
            IQueryable<Person> filterPerson = _application.Persons.Where(p => p.Identificator == getFilter.FindIdentificator || p.PersonalNumber == getFilter.FindPersonalNumber || getFilter.FindIdentificator == null);


            int size = getFilter.PageSize;

           
            int totalCount = filterPerson.Count();

            var xx = getFilter.PageSize * (getFilter.Page-1);

            IQueryable<PersonList.Persons> persons = filterPerson
                                                    .Skip(xx)
                                                    .Take(getFilter.PageSize)
                                                    .Select(q => new PersonList.Persons
                                                    {
                                                        Identificator = q.Identificator,
                                                        FirstNameGeo = q.FirstNameGeo,
                                                        FirstNameEn = q.FirstNameEn,
                                                        LastNameGeo = q.LastNameGeo,
                                                        LastNameEn = q.LastNameEn,
                                                        PersonalNumber = q.PersonalNumber
                                                    });
            return new PersonList
            {
                PageSize = size,
                people = persons.ToList(),
                TotalCount = totalCount,
                Page = getFilter.Page,
                FindIdentificator= getFilter.FindIdentificator,
                FindPersonalNumber= getFilter.FindPersonalNumber
            };
        }

        public List<PersonList.Persons> GetFullPerson(int x, int y)
        {
            
            List<PersonList.Persons> persons = _application.Persons
                                                    .Skip(x)
                                                    .Take(y)
                                                    .Select(q => new PersonList.Persons
                                                    {
                                                        Identificator = q.Identificator,
                                                        FirstNameGeo = q.FirstNameGeo,
                                                        FirstNameEn = q.FirstNameEn,
                                                        LastNameGeo = q.LastNameGeo,
                                                        LastNameEn = q.LastNameEn,
                                                        PersonalNumber = q.PersonalNumber
                                                    }).ToList();

            return persons;
        }

        public bool GetOnePerson(string PersonNumber)
        {
            return _application.Persons.Any(p => p.PersonalNumber == PersonNumber || p.Identificator == PersonNumber);
        }

        public Person GetPerson(string identity)
        {
            return _application.Persons.SingleOrDefault(p => p.Identificator == identity || p.PersonalNumber == identity); ;
        }

        public string EditPerson(Person person)
        {
            if (person == null)
            {
                return "Is null";
            }

            Person personOld = _application.Persons.FirstOrDefault(p => p.PeopelId == person.PeopelId);

            personOld.Identificator = person.Identificator;
            personOld.FirstNameGeo = person.FirstNameGeo;
            personOld.FirstNameEn = person.FirstNameEn;
            personOld.LastNameGeo = person.LastNameGeo;
            personOld.LastNameEn = person.LastNameEn;
            personOld.PersonalNumber = person.PersonalNumber;
            personOld.BirthDate = person.BirthDate;
            personOld.Addres = person.Addres;
            personOld.Mobile1 = person.Mobile1;
            personOld.Mobile2 = person.Mobile2;
            personOld.Picture = person.Picture;
            personOld.KinshipIdentificator = person.KinshipIdentificator;
            personOld.WhoIs = person.WhoIs;
               
                   
            _application.SaveChanges();
            

            Person personNew = _application.Persons.FirstOrDefault(p => p.PersonalNumber == person.PersonalNumber);

            if(personOld!=personNew)
            {
                return "მონაცემი წარმაწებით განახლდა!";
            }
            else
            {
                return "მონაცემი ვერ განახლდა!";
            }
        }

        public string DeletePerson(string identity)
        {
            
            if (identity == null)
            {
                
                return "იდენთიფიკატორი არ არსებობს!";
            }

            Person person = _application.Persons.SingleOrDefault(p => p.Identificator == identity);

            if (person != null)
            {
                DeletePicture(person.Picture);
                _application.Persons.Remove(person);
                _application.SaveChanges();
                return "მონაცემი წარმატებით წაიშალა";
            }
            else
            {
                return "პიროვნებ ვერ წაიშალა, რადგან არ არსებობს ბაზაში!";
            }

        }


        public string name(string mm)
        {
            var tt = mm.Split("/");

            string addresPathe = "";


            ///Resources/Images/20170625_135101.jpg


            for (var i = 1; i < tt.Length; i++)
            {
                addresPathe += "\\" + tt[i];
            };



            string addres = addresPathe;

            return addres;
        }

        public void DeletePicture(string fileAdres)
        {
            var fileName = "wwwroot" + name(fileAdres);

            var path = Path.Combine(Directory.GetCurrentDirectory(), /*"wwwroot",*/ fileName);

            var tt = System.IO.File.Exists(path);
            if (tt)
            {
                System.IO.File.Delete(path);
            }
        }

    }
}
