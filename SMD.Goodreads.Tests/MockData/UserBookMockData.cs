using SMD.Goodreads.API.Models.Entities;
using System.Net;

namespace SMD.Goodreads.Tests.MockData
{
    public class UserBookMockData
    {
        public static List<Book> GetBooksWithcompletedReading()
        {
            return new List<Book>()
            {
                new Book()
                {
                   Id = 1,
                   Name="Boneshaker",
                   Description= @"In the early days of the Civil War, 
rumors of gold in the frozen Klondike brought hordes of newcomers to the Pacific Northwest. 
Anxious to compete, Russian prospectors commissioned inventor Leviticus Blue to create a great machine that could mine through Alaska’s ice. 
Thus was Dr. Blue’s Incredible Bone-Shaking Drill Engine born.",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 1,
                            IsCompleted = true,
                            UserId = 1
                       }
                   }
                },
                new Book()
                {
                   Id = 2,
                   Name="The Anubis Gates",
                   Description= @"Brendan Doyle, a specialist in the work of the early-nineteenth century poet William Ashbless,
reluctantly accepts an invitation from a millionaire to act as a guide to time-travelling tourists. 
But while attending a lecture given by Samuel Taylor Coleridge in 1810,
he becomes marooned in Regency London, where dark and dangerous forces know about the gates in time.",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 2,
                            IsCompleted = false,
                            UserId = 1
                       }
                   }
                }
            };
        }

        public static List<Book> GetBooksWithUncompletedReading()
        {
            return new List<Book>()
            {
                new Book()
                {
                   Id = 1,
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
                            BookId = 1,
                            IsCompleted = false,
                            UserId = 1
                       }
                   }
                },
                new Book()
                {
                    Id = 2,
                   Name="Monster",
                   Description= @"
A man working a job far removed from his childhood dreams gets wrapped up in an unexpected situation…! 
Becoming a monster, he aims once again to fulfill his lifelong dream…!",
                   UserBooks = new List<UserBook>()
                   {
                       new UserBook()
                       {
                            BookId = 2,
                            IsCompleted = false,
                            UserId = 1
                       }
                   }
                }
            };
        }

        public static List<Book> GetEmptyBook()
        {
            return new List<Book>();
        }
        
        public static UserBook GetUserBook()
        {
            return new UserBook()
            {
                BookId = 2,
                IsCompleted = true,
                UserId = 2
            };
        }
        
        public static UserBook NewUserBook()
        {
            return new UserBook()
            {
                BookId = 2,
                IsCompleted = true,
                UserId = 3
            };
        }
        
        public static UserBook GetEmptyUserBook()
        {
            return null;
        }
    
    
    }
}
