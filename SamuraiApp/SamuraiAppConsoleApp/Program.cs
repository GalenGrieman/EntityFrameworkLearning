using System;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace SamuraiAppConsoleApp
{
    internal class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            //GetSamurais("Before Add:");
            //AddSamurai();
            //GetSamurais("After Add:");
            InsertMultipleSamurais();
            //GetSamuraisSimpler();
            //QueryFilters();
            //RetrieveAndUpdateSamurai();
            //RetrieveAndUpdateMultipleSamurais();
            //RetrieveAndDeleteSamurai();
            //InsertBattle();
            //QueryandUpdateBattle_Disconnected();
            //InsertNewSamuraiWithAQuote();
            //InsertNewSamuraiWithManyQuotes();
            //AddQuoteToExistingSamuraiWhileTracked();
            //AddQuoteToExistingSamuraiNotTracked(2);
            //AddQuoteToExistingSamuraiNotTracked_Easy(2);
            //EagerLoadSamuraiWithQuotes();
            //ProjectSomeProperties();
            //ProjectSamuraisWithQuotes();
            //ExplicitLoadQuotes();
            //FilteringWithRelatedData();
            //ModifyingRelatedDataWhenTracked();
            //ModifyingRelatedDataWhenNotTracked();
            //JoinBattleAndSamurai();
            //EnlistSamuraiIntoBattle();
            //RemoveJoinBetweenSamuraiAndBattleSimple();
            //GetSamuraiWithBattles();
            //AddNewSamuraiWithHorse();
            //AddNewHorseToSamuraiUsingId();
            //AddNewHorseToSamuraiObject();
            //AddNewHorseToDisconnectedSamuraiObject();
            //ReplaceAHorse();
            //QuerySamuraiBattleStats();
            //QueryUsingRawSQL();
            //QueryUsingRawSQLWithInterpolation();
            //QueryUsingFromRawSQLStoredProc();
            //ExecuteSamRawSQL();
            Console.WriteLine("Press Any key");
            Console.ReadKey();
        }
        
        private static void InsertMultipleSamurais()
        {
            //var samurai = new Samurai { Name = "Tasha" };
            //var samurai2 = new Samurai { Name = "Tasha2" };
            //var samurai3 = new Samurai { Name = "Tasha3" };
            //var samurai4 = new Samurai { Name = "Tasha4" };
            var _bizData = new BusinessDataLogic();
            var samuraiNames = new string[] { "Sampson", "Tasha", "Sampson2", "Tasha2" };
            var newSamuraisCreated = _bizData.AddMultipleSamurais(samuraiNames);
        }
        private static void InsertVariousTypes()
        {
            var samurai = new Samurai { Name = "Kikuchio" };
            var clan = new Clan { ClanName = "Imperial Clan" };
            _context.AddRange(samurai, clan);
            _context.SaveChanges();
        }
        private static void AddSamurai()
        {
            var samurai = new Samurai { Name = "Sampson" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void GetSamuraisSimpler()
        {
            var samurais = _context.Samurais.ToList();
            foreach (var s in samurais)
            {
                Console.WriteLine(s.Name);
            }
        }
        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai Count is {samurais.Count}");
         //   foreach (var samurai in samurais)
         //   {
         //       Console.WriteLine(samurai.Name);
         //   }

        }
        private static void QueryFilters()
        {
            var name = "Sampson";
            var samurais = _context.Samurais.Where(s => s.Name == name).ToList();
        }
        private static void RetrieveAndUpdateSamurai()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Name += "San";
            _context.SaveChanges();
        }
        private static void RetrieveAndUpdateMultipleSamurais()
        {
            var samurais = _context.Samurais.Skip(1).Take(4).ToList();
            samurais.ForEach(s => s.Name += "San");
            _context.SaveChanges();
        }
        private static void RetrieveAndDeleteSamurai()
        {
            var samurai = _context.Samurais.Find(18);
            _context.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
        private static void InsertBattle()
        {
            _context.Battles.Add(new Battle
            {
                Name = "Battle of Okehazama",
                StartDate = new DateTime(1560, 05, 01),
                EndDate = new DateTime(1560, 06, 15),
            });
            _context.SaveChanges();
        }
        private static void QueryandUpdateBattle_Disconnected()
        {
            var battle = _context.Battles.AsNoTracking().FirstOrDefault();
            battle.EndDate = new DateTime(1560, 06, 30);
            using (var newContextInstance = new SamuraiContext())
            {
                newContextInstance.Battles.Update(battle);
                newContextInstance.SaveChanges();
            }
        }
        private static void InsertNewSamuraiWithAQuote()
        {
            var samurai = new Samurai
            {
                Name = "Kambei Shimada",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "I've come to save you"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void InsertNewSamuraiWithManyQuotes()
        {
            var samurai = new Samurai
            {
                Name = "Kyuzo",
                Quotes = new List<Quote>
                {
                    new Quote { Text = "Watch out for my sharp sword!"},
                    new Quote { Text = "I told you to watch out for the sharp sword! Oh well!"}
                }
            };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiWhileTracked()
        {
            var samurai = _context.Samurais.FirstOrDefault();
            samurai.Quotes.Add(new Quote
            {
                Text = "I bet you're happy that I've saved you!"
            });
            _context.SaveChanges();
        }
        private static void AddQuoteToExistingSamuraiNotTracked(int samuraiId)
        {
            var samurai = _context.Samurais.Find(samuraiId);
            samurai.Quotes.Add(new Quote
            {
                Text = "Now that I saved you, will you feed me dinner?"
            });
            using (var newContext = new SamuraiContext())
            {
                newContext.Samurais.Update(samurai);
                newContext.SaveChanges();
            }
        }
        private static void AddQuoteToExistingSamuraiNotTracked_Easy(int samuraiId)
        {
            var quote = new Quote
            {
                Text = "Now that I saved you, will you feed me dinner again?",
                SamuraiId = samuraiId
            };
            using (var newConext = new SamuraiContext())
            {
                newConext.Quotes.Add(quote);
                newConext.SaveChanges();
            }
        }
        private static void EagerLoadSamuraiWithQuotes()
        {
            var samuraiWithQuotes = _context.Samurais.Include(s => s.Quotes).ToList();
        }
        private static void ProjectSomeProperties()
        {
            var someProperties = _context.Samurais.Select(s => new { s.Id, s.Name }).ToList();
            var idsAndName = _context.Samurais.Select(s => new IdAndName(s.Id, s.Name)).ToList();
        }
        public struct IdAndName
        {
            public IdAndName(int id, string name)
            {
                Id = id;
                Name = name;
            }
            public int Id;
            public string Name;
        }
        private static void ProjectSamuraisWithQuotes()
        {
            //var somePropertiesWithQuotes = _context.Samurais
            //    .Select(s => new { s.Id, s.Name, s.Quotes.Count })
            //    .ToList();
            //var somePropertiesWithQuotes = _context.Samurais
            // .Select(s => new { s.Id, s.Name,
            //   HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) })
            // .ToList();
            var samuraisWithHappyQuotes = _context.Samurais
             .Select(s => new {
                 Samurai = s,
                 HappyQuotes = s.Quotes.Where(q => q.Text.Contains("happy")) 
             })
             .ToList();
            var firstSamurai = samuraisWithHappyQuotes[0].Samurai.Name += " The Happiest";
        }
        private static void ExplicitLoadQuotes()
        {
            //can only use the .Load command for a single object
            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Tasha"));
            _context.Entry(samurai).Collection(s => s.Quotes).Load();
            _context.Entry(samurai).Reference(s => s.Horse).Load();
        }
        private static void LazyLoadQuotes()
        {
            //LazyLoading is generally not used and is turned off by default as it has the following negative implications:
            //-Easy to abuse
            //-Create Perfomnce issues
            //-Unexpected Results
            //
            //+But it is easy
            //Must have the following for it to trigger
            //Everything must be in scope
            //Every navigation property must be virtual
            //Microsoft.EntityFramework.Proxies package must be added to project
            //Db Context class model builder must be told to use proxies I.E. ModelBuilder.UseLazyLoadingProxies()
            // Julie Lerman doesnt go into great detail about this as she does not like it in practice for the above reasons but she did give examples
            /*******************************************************************************************************************************
             * foreach(var q in samurai.Quotes)
             * {
             *      Console.WriteLine(q.Text);
             * }
             * 
             * "This will send one command to retrieve all of the Quotes for that samurai, then iterate through them
             * 
             * This is the only good one she recommonded.
             * 
             * These are the bad ones.
             * 
             * var qCount=samurai.Quotes.Count();
             * "This will retrieve all of the quote ovjects from the database and materialize them and then give you the count
             * 
             * Data bind a grid to lazy-loaded data
             * 
             * "This happned a lot in ASP.NET pages.  The grid populate one row at a time and lazy loads the related data for that row, then the next,
             * then the next. N+1 commands sent to the database!
             * 
             * Lazy loading when no context in place
             * 
             * "No Data is retrieved"
             * *********************************/

            var samurai = _context.Samurais.FirstOrDefault(s => s.Name.Contains("Tasha:"));

            var quoteCount = samurai.Quotes.Count();
        }
        private static void FilteringWithRelatedData()
        {
            var samurais = _context.Samurais
                .Where(s => s.Quotes.Any(q => q.Text.Contains("happy")))
                .ToList();
        }
        private static void ModifyingRelatedDataWhenTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            samurai.Quotes[0].Text = " Did you hear that?";
            _context.SaveChanges();
        }
        private static void ModifyingRelatedDataWhenNotTracked()
        {
            var samurai = _context.Samurais.Include(s => s.Quotes).FirstOrDefault(s => s.Id == 2);
            var quote = samurai.Quotes[0];
            quote.Text += " Did you hear that again?";
            using (var newContext = new SamuraiContext())
            {
                //newContext.Quotes.Update(quote); 
                //this will update all the quotes if there are more than 1, causing a lot of "lag" or
                // or time spent on this simple process if there was like 100 quotes for this specific samurai.

                //The following makes it so it is only tracking var quote = samuri.Quotes[0] and therefore only saves that change as we 
                //set the state to modified.  Within this scope, it only tracks, changes, and saves this specific quote (samurai.Quotes[0])
                // and not the potential 100 other quotes it would have to process through as it would not know if there were changed or not as this
                // is disconnected/Not tracked mode
                newContext.Entry(quote).State = EntityState.Modified;
                newContext.SaveChanges();
            }
        }
        private static void JoinBattleAndSamurai()
        {
            //Samurai and Battle already exist and we have their IDs **SideNote** i only had 1 battle entered so i had to use BattleID of 1 instead of Julie's 3.
            var sbJoin = new SamuraiBattle { SamuraiId = 1, BattleId = 1 };
            _context.Add(sbJoin);
            _context.SaveChanges();
        }
        private static void EnlistSamuraiIntoBattle()
        {
            //this will find the battle and then since we already have the battle ID
            //we can use a new instance of SamuraiBattle to find the samurai ID to have them be added to that battle
            var battle = _context.Battles.Find(1);
            battle.SamuraiBattles.Add(new SamuraiBattle { SamuraiId = 11 });
            _context.SaveChanges();
        }
        private static void RemoveJoinBetweenSamuraiAndBattleSimple()
        {
            //this works if you already know the ID's of both the Battle and Samurai, otherwise, it will fail
            var join = new SamuraiBattle { BattleId = 1, SamuraiId = 11 };
            _context.Remove(join);
            _context.SaveChanges();
        }
        private static void GetSamuraiWithBattles()
        {
            //var samuraiWithBattle = _context.Samurais
            //    .Include(s => s.SamuraiBattles)
            //    .ThenInclude(sb => sb.Battle)
            //    .FirstOrDefault(samurai => samurai.Id == 12);
            // Shows all the Samurais => Samurai Battles => Battle // Parent => Child => grandchild
            var samuraiWithBattlesCleaner = _context.Samurais.Where(s => s.Id == 12)
                .Select(s => new
                {
                    Samurai = s,
                    Battles = s.SamuraiBattles.Select(sb => sb.Battle)
                })
                .FirstOrDefault();
        }
        private static void AddNewSamuraiWithHorse()
        {
            var samurai = new Samurai { Name = "Jina Ujichika" };
            samurai.Horse = new Horse { Name = "Silver" };
            _context.Samurais.Add(samurai);
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiUsingId()
        {
            //This only works if you have the internal ID of the object to add the horse to
            var horse = new Horse { Name = "Scout", SamuraiId = 2 };
            _context.Add(horse);
            _context.SaveChanges();
        }
        private static void AddNewHorseToSamuraiObject()
        {
            var samurai = _context.Samurais.Find(3);
            samurai.Horse = new Horse { Name = "Black Beauty" };
            _context.SaveChanges();
        }
        private static void AddNewHorseToDisconnectedSamuraiObject()
        {
            var samurai = _context.Samurais.AsNoTracking().FirstOrDefault(s => s.Id == 4);
            samurai.Horse = new Horse { Name = "Mr. Ed" };
            using (var newContext = new SamuraiContext())
            {
                newContext.Attach(samurai);
                newContext.SaveChanges();
            }
        }
        private static void ReplaceAHorse()
        {
            //var samurai = _context.Samurais.Include(s=> s.Horse).FirstOrDefault(s=> s.Id == 2)
            var samurai = _context.Samurais.Find(2); //has a horse
            samurai.Horse = new Horse { Name = "Trigger" }; //This throws an exception because samurai already has a horse.  Need to delete old horse before adding a new one other INSERT will throw handling exception
            _context.SaveChanges();
        }
        private static void GetSamuraisWithHorse()
        {
            var samurai = _context.Samurais.Include(s => s.Horse).ToList();
        }
        private static void GetHorseWithSamurai()
        {
            var horseWithoutSamurai = _context.Set<Horse>().Find(2); //No Db set for Horse so SET will work

            var horseWithSamurai = _context.Samurais.Include(s => s.Horse) //This looks for samurais with horses INCLUDE
                .FirstOrDefault(s => s.Horse.Id == 2);

            var horseswithSamurais = _context.Samurais //Filerting where where horses are null and then selecting them with horses
                .Where(s => s.Horse != null)
                .Select(s => new { Horse = s.Horse, Samurai = s })
                .ToList();
           
        }
        private static void GetSamuraiWithClan()
        {
            var samurai = _context.Samurais.Include(s => s.Clan).FirstOrDefault(); //Get a samurai with his clan
        }
        private static void GetClanWithSamurais()
        {
            // You can not navigate from Clan to its samurai as we did not add a property in clan to connect clan to samurai
            // We added the Clan property into Samurai, but we did not add the SamuraiID property into clan
            // var clan = _context.Clans.Include(c=>c.??????)
            var clan = _context.Clans.Find(3);
            var samuraisForClan = _context.Samurais.Where(s => s.Clan.Id == 3).ToList();
        }
        private static void QuerySamuraiBattleStats()
        {
            var stats = _context.SamuraiBattleStats.ToList();
            var firstStat = _context.SamuraiBattleStats.FirstOrDefault();
            var tashaStat = _context.SamuraiBattleStats.Where(s => s.Name == "Tasha").FirstOrDefault();
            //var findone = _context.SamuraiBattleStats.Find(2); //This makes no sense as it is a DBset and can run it, but it has no key as we set it to have no key
        }
        private static void QueryUsingRawSQL()
        {
            var samurais = _context.Samurais.FromSqlRaw("Select * FROM Samurais").ToList();
            //using raw SQL Rules
            // Entities have to be known from the _context; horse is not known as it has no DbSet therefore cannot be fround
            // Entites have to have same properties; I.E. all the names are different and in samurais so you cannot use Select Name FROM Samurais as it is expecting ID's
            //
        }
        private static void QueryUsingRawSQLWithInterpolation()
        {
            string name = "Kikuchyo";
            var samurais = _context.Samurais
                .FromSqlInterpolated($"Select * FROM Samurais WHERE Name = {name}")
                .ToList();
            //DO NOT DO THE ABOVE WITH FromSqlRaw!!! THIS CAUSES VULNERABTILITES FOR SQL INJECTION ATTACKS
        }
        private static void QueryUsingFromRawSQLStoredProc()
        {
            var text = "Happy";
            var samurais = _context.Samurais.FromSqlRaw(
                "EXEC dbo.SamuraisWhoSaidAWord {0}", text).ToList();
        }
        private static void ExecuteSamRawSQL()
        {
            var samuraidId = 16; //Lesson used 22, I had to use a samurai id that had a quote in it
            var x = _context.Database
                .ExecuteSqlRaw("EXEC DeleteQuotesForSamurai {0}", samuraidId);
        }
    }
}
