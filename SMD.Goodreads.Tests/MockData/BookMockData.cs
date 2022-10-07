using SMD.Goodreads.API.Models.Entities;

namespace SMD.Goodreads.Tests.MockData
{
    public class BookMockData
    {
        public static List<Book> GetBooks()
        {
            return new List<Book>()
            {
                new Book()
                {
                   Id = 1,
                   Name="Coven",
                   Description= @"
Emsy has always lived in sunny California, 
and she'd much rather spend her days surfing with her friends or hanging out with her girlfriend than honing her powers as a fire elemental.
But when members of her family's coven back east are murdered under mysterious circumstances that can only be the result of powerful witchcraft, 
her family must suddenly return to dreary upstate New York. There, Emsy will have to master her neglected craft in order to find the killer . . . 
before her family becomes their next target",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 1,
                            IsCompleted = true,
                            UserId = 1
                       },
                   }
        },
                new Book()
                {
                   Id = 2,
                   Name="My Favorite Thing Is Monsters, Vol. 2",
                   Description= @"In the conclusion of this two-part graphic novel, 
set in 1960s Chicago, dark mysteries past and present abound, and 10-year-old Karen tries to solve them.
\r\n\r\nKaren attends the Yippie-organized Festival of Life in Chicago, and finds herself swept up in a police stomping. 
Privately, she wrestles with her sexual identity, and she continues to investigate her neighbor’s recent death. 
She discovers one last cassette tape, which sheds light on Anka’s heroic activities.",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 2,
                            IsCompleted = true,
                            UserId = 1
                       },
                   }
                }
            };
        }
        
        public static Book NewBook()
        {
            return new Book
            {
                Id = 1,
                Name = "One Piece",
                Description = " One Piece Description"
            };
        }

        public static Book GetById(int id)
        {
            return GetBooks().FirstOrDefault(x => x.Id == id);
        }
    
        public static T GetEmptyBook<T>()
        {
            return default;
        }

        public static List<Book> GetBooksWithUncompletedReading()
        {
            return new List<Book>()
            {
                new Book()
                {
                   Id = 1000,
                   Name="One Piece",
                   Description= @"As a child, Monkey D. Luffy was inspired to become a pirate by listening to the tales of the buccaneer
""Red-Haired"" Shanks. But Luffy's life changed when he accidentally ate the Gum-Gum 
Devil Fruit and gained the power to stretch like rubber...
at the cost of never being able to swim again! Years later, still vowing to become the king of the pirates, 
Luffy sets out on his adventure...one guy alone in a rowboat, in search of the legendary ""One Piece,"" 
said to be the greatest treasure in the world...",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 1000,
                            IsCompleted = false,
                            UserId = 3
                       }
                   }
                },
                new Book()
                {
                    Id = 2000,
                   Name="Monster",
                   Description= @"
A man working a job far removed from his childhood dreams gets wrapped up in an unexpected situation…! 
Becoming a monster, he aims once again to fulfill his lifelong dream…!",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 2000,
                            IsCompleted = false,
                            UserId = 3
                       }
                   }
                }
            };
        }

    }
}
