namespace MinimalAPI.Models
{
    public class Person
    {
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public List<PersonInterest> PersonInterests { get; set; }
        public List<Link> Links { get; set; }
    }
}
