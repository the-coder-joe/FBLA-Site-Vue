﻿// Posting.cs
namespace FBLA_Site
{
    public class Posting: IEquatable<Posting>
    {
        public int Id { get; set; }
        public int SubmittedById { get; set; }
        public string Title { get; set; }
        public string Employer { get; set; }
        public string Requirements { get; set; }
        public string Description { get; set; }
        public string AdditionalInformation { get; set; }
        public string ContactInformation { get; set; }
        public List<string> Questions { get; set; }
        public bool Equals(Posting? other)
        {
            return other?.Id == this.Id;
        }
    }
}
