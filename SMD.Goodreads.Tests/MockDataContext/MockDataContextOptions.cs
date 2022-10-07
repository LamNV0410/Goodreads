using Microsoft.EntityFrameworkCore;

namespace SMD.Goodreads.Tests.MockDataContext
{
    public class MockDataContextOptions
    {
        public static DbContextOptions<T> GetContextOptions<T>(string name) where T : DbContext
        {
            return new DbContextOptionsBuilder<T>()
                .UseInMemoryDatabase("name")
                .Options;
        }
    }
}
