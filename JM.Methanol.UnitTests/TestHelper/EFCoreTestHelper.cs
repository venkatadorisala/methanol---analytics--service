using JM.Integration.Methanol.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace JM.Integration.Methanol.Services.UnitTests.TestHelper
{
    public class EFCoreTestHelper
    {
        private readonly LevoMethanolDBContext levoMethanolDBContext;

        public EFCoreTestHelper()
        {
            var builder = new DbContextOptionsBuilder<LevoMethanolDBContext>();
            builder.UseInMemoryDatabase(databaseName: "levoMethanolDbInMemory");
            var dbContextOptions = builder.Options;

            levoMethanolDBContext = new LevoMethanolDBContext(dbContextOptions);
            levoMethanolDBContext.Database.EnsureDeleted();
            levoMethanolDBContext.Database.EnsureCreated();
        }
    }
}