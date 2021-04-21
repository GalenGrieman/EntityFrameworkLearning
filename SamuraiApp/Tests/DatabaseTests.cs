using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
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
         * * ****/
        [TestMethod]
        public void CanInsertSamuraiIntoDatabase()
        {
            using (var context = new SamuraiContext())
            {
                //context.Database.EnsureDeleted();
                //context.Database.EnsureCreated();
                var samurai = new Samurai();
                context.Samurais.Add(samurai);
                Debug.WriteLine($"Before Save: {samurai.Id}");

                context.SaveChanges();
                Debug.WriteLine($"After Save: {samurai.Id}");

                Assert.AreNotEqual(0, samurai.Id);
            }
        }
    }
}
