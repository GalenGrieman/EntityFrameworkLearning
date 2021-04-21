using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    [TestClass]
    public class InMemoryTests
    {
        /****
         * 
         * Common Types of Automated Tests:
         * Unit Test - Tests small units of your own code
         * Intergration Test - One step further and Tests your logic and interacts with other servicces or modules
         * Functional Test - One step further and verifies results of interaction
         * 
         * What does testing EF Core Mean?
         * - Validate your DbContext against the database
         *      Verifing results - Such as primary keys
         *      
         * - Validate your business logic against the DbContext
         * 
         * - Validate your business logic that ues the DbContext and Database
         * 
         * - EF Core raw SQL Methods, mentioned below, only work with relational database providers and therefore cannot be tested within EF Core test methods
         *   as their features only work within the database themselves.
         *  //QuerySamuraiBattleStats();
         *  //QueryUsingRawSQL();
         *  //QueryUsingRawSQLWithInterpolation();
         *  //QueryUsingFromRawSQLStoredProc();
         *  //ExecuteSamRawSQL();
         * 
         * 
         * 
         * * ****/
        [TestMethod]
        public void CanInsertSamuraiIntoDatabaseInMemory()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using (var context = new SamuraiContext(builder.Options))
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Assert.AreEqual(EntityState.Added, context.Entry(samurai).State); ;
            }
        }
    }
}
