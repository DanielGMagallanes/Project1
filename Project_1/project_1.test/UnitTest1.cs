using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using ModelLayer;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace project_1.test
{
    public class UnitTest1
    {
        static StoreAppContext cont = new StoreAppContext();
        static StoreAppRepsitoryLayer repo = new StoreAppRepsitoryLayer(cont);
        static StoreLevelPrograms storeLevelPrograms = new StoreLevelPrograms(repo);


        /*[Fact]
        public void CheckOutTotalTest()
        {
            //var options = new DbContextOptionsBuilder<StoreAppContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            double tmep = 0;
            List<Cart> carts = new List<Cart>();
            carts.Add(new Cart() { amountPicked = 3, InShoppingCart = 31 });
            carts.Add(new Cart() { amountPicked = 3, InShoppingCart = 33 });
            carts.Add(new Cart() { amountPicked = 5, InShoppingCart = 32 });
           // using (var context = new StoreAppContext(options))
           // {
          //      context.Database.EnsureDeleted();
          //      context.Database.EnsureCreated();

          //      StoreAppRepsitoryLayer repo = new StoreAppRepsitoryLayer(context);
          //      StoreLevelPrograms storeLevelPrograms = new StoreLevelPrograms(repo);

                tmep = storeLevelPrograms.CheckOutTotal(carts);

          //      context.SaveChanges();
          //  }

          //  using (var context = new StoreAppContext(options))
         //   {
                Assert.Equal(1816.22, tmep);
         //   }


        }*/

        [Fact]//the name of the function should tell the user what the function is doing
        public void CreateCustomerSavesANewPlayerToTheDb()
        {
            // arrange
            // Creating the in-memory Db
            var options = new DbContextOptionsBuilder<StoreAppContext>()
                .UseInMemoryDatabase(databaseName: "TestDb").Options;

            // act
            // add to the In-memory Db
            Customer p1 = new Customer();
            using (var context = new StoreAppContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                StoreAppRepsitoryLayer repo = new StoreAppRepsitoryLayer(context);

                p1 = repo.CreateCustomer("sparky", "jones");
                context.SaveChanges();
            }


            // assert
            // verify the results was as expected
            using (var context = new StoreAppContext(options))
            {
                StoreAppRepsitoryLayer repo = new StoreAppRepsitoryLayer(context);
                Customer result = repo.CreateCustomer("sparky", "jones");
                Assert.True(p1.Customer_Id.Equals(result.Customer_Id));
            }
        }
        
    }
}
