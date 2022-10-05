using System.Collections;
using System.Collections.Generic;

namespace SMD.Goodreads.API.Models
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
