namespace MinimalAPI.Models
{
    public class Link
    {
        public int LinkID { get; set; }
        public string LinkURL { get; set; }
        public int PersonID { get; set; }
        public int InterestID { get; set; }

        public Person Person { get; set; }
        public Interest Interest { get; set; }
    }
}
