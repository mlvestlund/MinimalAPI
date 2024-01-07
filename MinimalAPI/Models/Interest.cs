namespace MinimalAPI.Models
{
    public class Interest
    {
        public int InterestID { get; set; }
        public string InterestName { get; set; }
        public string InterestDescription { get; set; }

        public List<PersonInterest> PersonInterests { get; set; }
        public List<Link> Links { get; set; }
    }
}
