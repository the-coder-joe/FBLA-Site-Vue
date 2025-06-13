namespace FBLA_Site

{
    public class Application
    {
        public int Id { get; set; } = -1; 
        public int ForPostingId { get; set; }
        public string Name { get; set; }
        public string ContactInfo { get; set; }
        public List<string> Answers { get; set; }

    }
}
