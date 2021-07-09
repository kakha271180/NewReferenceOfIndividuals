using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewReferenceOfIndividuals.Models
{
    public class Person
    {
        public int PeopelId { get; set; }

        [Display(Name = "იდენთიფიკატორი")]
        public string Identificator { get; set; }

        [DisplayName("სახელი ქართულად")]
        [Required(ErrorMessage ="შეავსეთ ველი, აუცილებელია")]
        public string FirstNameGeo { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [DisplayName("სახელი ინგლისურად")]
        public string FirstNameEn { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [Display(Name = "გვარი ქართულად")]
        public string LastNameGeo { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [Display(Name = "გვარი ინგლისურად")]
        public string LastNameEn { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [Display(Name = "პირადი ნომერი")]
        public string PersonalNumber { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [Display(Name = "დაბადების თარიღი")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "შეავსეთ ველი, აუცილებელია")]
        [Display(Name = "მისამართი")]
        public string Addres { get; set; }

        [DisplayName("მობილური")]
        public string Mobile1 { get; set; }

        [DisplayName("მობილური")]
        public string Mobile2 { get; set; }

        [DisplayName("სურათი")]
        public string Picture { get; set; }

        [DisplayName("ნათესაური კავშირის იდენთიფიკატორი")]
        public string KinshipIdentificator { get; set; }

        [DisplayName("ნათესაური კავშირი")]
        public string WhoIs { get; set; }
    }
}
