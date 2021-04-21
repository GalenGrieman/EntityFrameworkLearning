using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using SamuraiAppConsoleApp;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class BizDataLogicTests
    {
        [TestMethod]
        public void AddMultipleSamuraisReturnsCorrectNumberOfInsertedRows()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddMultipleSamurais");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogic = new BusinessDataLogic();
                var nameList = new string[] { "Kikuchiyo", "Kyuzo", "Rikchi" };
                var result = bizlogic.AddMultipleSamurais(nameList);
                Assert.AreEqual(nameList.Count(), result);
            }
        }

        public void CanInsertSingleSamurai()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("AddSingleSamurai");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bizlogiz = new BusinessDataLogic();
                bizlogiz.InsertNewSamurai(new Samurai());

            };
            using (var context2 = new SamuraiContext(builder.Options))
            {
                Assert.AreEqual(1, context2.Samurais.Count());
            }
            
        }
    }
}
