﻿using SMD.Goodreads.API.Models.Entities;
using System.Collections.Generic;

namespace SMD.Goodreads.API.Context
{
    public class BooksContextSeed
    {
        public static void SeedData(GoodReadsDbcontext context)
        {
            var books = new List<Book>()
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
                }
            };

            context.Books.AddRange(books);

            var userBooks = new List<UserBook>()
            {
                new UserBook()
                {
                    BookId = 1,
                    IsCompleted = false,
                    UserId = 2
                },
                new UserBook()
                {
                    BookId = 2,
                    IsCompleted = false,
                    UserId = 1
                }
            };

            context.UserBooks.AddRange(userBooks);
            context.SaveChanges();
        }
}
}
