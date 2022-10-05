namespace SMD.Goodreads.API.Models.Entities
{
    public class UserBook
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsCompleted { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}
