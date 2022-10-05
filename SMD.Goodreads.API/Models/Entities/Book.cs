using System.Collections.Generic;

namespace SMD.Goodreads.API.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UserBook> UserBooks { get; set; }
    }
}
