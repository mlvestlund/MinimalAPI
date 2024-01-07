using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class PersonInterest
    {
        public int PersonID { get; set; }
        public int InterestID { get; set; }

        public Person Person { get; set; }
        public Interest Interest { get; set; }
    }
}
