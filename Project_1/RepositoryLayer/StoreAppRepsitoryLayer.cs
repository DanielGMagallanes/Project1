using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ModelLayer;
using ModelLayer.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Logging;
using ModelLayer.ViewModels;

namespace RepositoryLayer
{
    public class StoreAppRepsitoryLayer : InventoryDecrement
    {
        private readonly StoreAppContext _SA_DbContext;
        private readonly ILogger<StoreAppRepsitoryLayer> _logger;
        DbSet<Store> stores;
        DbSet<Orders> orders;
        DbSet<OrderedItem> orderedItems;
        DbSet<Item> items;
        DbSet<Customer> customers;
        DbSet<Product> products;
        DbSet<Cart> carts;
        public StoreAppRepsitoryLayer() { }
        public StoreAppRepsitoryLayer(StoreAppContext storeAppContext, ILogger<StoreAppRepsitoryLayer> logger)
        {
            this._SA_DbContext = storeAppContext;
            this.stores = _SA_DbContext.stores;
            this.orders = _SA_DbContext.orders;
            this.orderedItems = _SA_DbContext.orderedItems;
            this.items = _SA_DbContext.ItemsAtStore;
            this.customers = _SA_DbContext.customers;
            this.products = _SA_DbContext.products;
            this.carts = _SA_DbContext.cart;
            this._logger = logger;

        }
        public StoreAppRepsitoryLayer(StoreAppContext storeAppContext)
        {
            this._SA_DbContext = storeAppContext;
            this.stores = _SA_DbContext.stores;
            this.orders = _SA_DbContext.orders;
            this.orderedItems = _SA_DbContext.orderedItems;
            this.items = _SA_DbContext.ItemsAtStore;
            this.customers = _SA_DbContext.customers;
            this.products = _SA_DbContext.products;
            this.carts = _SA_DbContext.cart;
        }

        /// <summary>
        /// Creates a Customer after verifying that Customer does not already
        /// exist. defalult have been set. Returns Customer object.
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        public Customer CreateCustomer(string fName = "null", string lName = "null")
        {
            Customer c1 = new Customer();
            c1 = customers.Where(x => x.firstName == fName && x.lastName == lName).FirstOrDefault();

            if (c1 == null)
            {
                c1 = new Customer()
                {
                    firstName = fName,
                    lastName = lName,
                    Addmin = false
                };
                customers.Add(c1);
                _SA_DbContext.SaveChanges();
            }
            return c1;
        }
        /// <summary>
        /// This is where the order get added to database and the sales added
        /// to the store.
        /// </summary>
        /// <param name="order"></param>
        /// <param name="list"></param>
        /// <param name="store"></param>
        public void ProcessOrder(Orders order, List<OrderedItem> list, int store,string guid)
        {
            int OrderID = orders.Count() + 1;
            orders.Add(order);
            foreach (var entry in list)
            {
                entry.OrderID = OrderID;
                orderedItems.Add(entry);
            }
            var query = from s in stores
                        where s.Id == store
                        select s;
            foreach (var a in query)
            {
                a.totalSales += order.total;
            }
            List<Cart> cart = carts.Where(x => x.customerGuild == guid).ToList();
            foreach(var c in cart)
            {
                carts.Remove(c);
            }
            _SA_DbContext.SaveChanges();
        }
        /// <summary>
        /// Returns store based on store id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Store SelectTheStore(int id)
        {
            Store select = new Store();
            select = stores.Where(x => x.Id == id).FirstOrDefault();
            return select;
        }
        /// <summary>
        /// List of past orders for a store
        /// </summary>
        /// <param name="store"></param>
        /// <returns></returns>
        public List<Orders> GetAllStorePastOrders(int store)
        {
            List<Orders> allorders = new List<Orders>();

            var listallorders = from o in orders
                                where o.storeLocationID == store
                                select o;
            foreach (var q in listallorders)
            {
                allorders.Add(q);
            }
            return allorders;
        }
        /// <summary>
        /// List of orders for a customer
        /// </summary>
        /// <param name="customerid"></param>
        /// <returns></returns>
        public List<Orders> GetAllPastOrders(string customerid)
        {
            List<Orders> allorders = new List<Orders>();

            var listallorders = from o in orders
                                where o.customerGuid == customerid
                                select o;
            foreach (var q in listallorders)
            {
                allorders.Add(q);
            }
            return allorders;
        }
        /// <summary>
        /// Takes an int for the store selection. Using a join to combine two tables. It returns a List of StoreViewModel
        /// and the values are StoreViewModel.
        /// </summary>
        /// <param name="id"></param>
        public List<StoreViewModel> GetItemForStore(int id)
        {
            var itemandproduct = from i in items
                                 join p in products on i.productId equals p.productId
                                 where i.Id_TO_S == id
                                 select new { PId = i.productId, Product = p.productName, Price = p.price, Qty = i.qty , Sale = i.sale};
            List<StoreViewModel> temp = new List<StoreViewModel>();
            foreach (var entry in itemandproduct)
            {
                StoreViewModel storeView = new StoreViewModel();
                storeView.productId = entry.PId;
                storeView.productName = entry.Product;
                storeView.price = entry.Price;
                storeView.qty = entry.Qty;
                storeView.ID_store = id;
                storeView.sale = entry.Sale;
                temp.Add(storeView);
            }
            return temp;
        }
        public Product GetProduct(int itemid)
        {
            Item item = items.Where(x => x.Id == itemid).FirstOrDefault();
            Product p = products.Where(x => x.productId == item.productId).FirstOrDefault();
            return p;
        }

        public List<FullOrderViewModel> FullOrderDisplay(int ord)
        {
            
            List<FullOrderViewModel> list = new List<FullOrderViewModel>();
            //Havind a issue with getting store info
            //
            //Store temp = stores.Where(x => x.Id == ord.stroeLocation.Id).FirstOrDefault();
            var query = from orderitem in orderedItems
                        join od in orders on orderitem.OrderID equals od.orderID
                        where od.orderID == ord
                        select new { time = od.dateTime, PN = orderitem.ProductName, PP = orderitem.pricePaid, QT = orderitem.qtyOrdered };
            Orders orde = orders.Where(x => x.orderID == ord).FirstOrDefault();
            Store store = new Store();
            var query2 = from s in stores where s.Id == ord select s;
            foreach (var s in query2)
            {
                store = s;
            }
            foreach (var fd in query)
            {
                FullOrderViewModel fullOrderViewModel = new FullOrderViewModel()
                {
                    dateTime = fd.time,
                    OrderID = ord,
                    ProductName = fd.PN,
                    pricePaid = fd.PP,
                    qtyOrdered = fd.QT,
                    storeName = store.storeName,
                    location = store.location
                };
                list.Add(fullOrderViewModel);
            }
            return list;

        }

        public List<Item> GetItems()
        {
            return items.ToList();
        }

        public List<Store> GetStores()
        {
            return stores.ToList();
        }
        /// <summary>
        /// should return any customer with a first or last name match the search
        /// passed in.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public List<Customer> GetListMatchingName(string search)
        {
            List<Customer> match = new List<Customer>();
            string[] elements = search.Trim().Split(' ');
            if (elements.Count() == 1)
            {
                var query1 = from c in customers
                             where c.firstName == elements[0] || c.lastName == elements[0]
                             select c;

                foreach (var q in query1)
                {
                    match.Add(q);
                }

            }
            else if (elements.Count() == 2)
            {
                var query2 = from c in customers
                             where c.firstName == elements[0] && c.lastName == elements[1] || c.firstName == elements[1] && c.lastName == elements[0]
                             select c;
                foreach (var q in query2)
                {
                    match.Add(q);
                }
            }

            return match;
        }


        /// <summary>
        /// There are Item which are made from items within the store.
        /// If one of those items are picked by the customer than each of 
        /// the ingreadents will also decrese by 1
        /// </summary>
        /// <param name="it"></param>
        public void PremadeItemGrabed(Item it)
        {
            if (it.productId == 12)
            {
                int[] ingreadents = { 1, 2, 3, 8, 13 };
                var query = from i in items
                            where i.Id_TO_S == it.Id_TO_S
                            select i;
                foreach (var component in query)
                {
                    foreach (var num in ingreadents)
                    {
                        if (component.productId == num)
                        {
                            //what if qty is 0
                            component.qty -= 1;
                        }
                    }
                }
            }
            else if (it.productId == 9)
            {
                //1,2,3
                int[] ingreadents = { 1, 2, 3 };
                var query2 = from i in items
                             where i.Id_TO_S == it.Id_TO_S
                             select i;
                foreach (var component in query2)
                {
                    foreach (var num in ingreadents)
                    {
                        if (component.productId == num)
                        {
                            //what if qty is 0
                            component.qty -= 1;
                        }
                    }
                }

            }
            _SA_DbContext.SaveChanges();
        }

        public void ItemWasGrabed(Item item)
        {
            int amountGrabed = item.qty;
            Item temp = items.Where(x => x.Id_TO_S == item.Id_TO_S && x.productId == item.productId).FirstOrDefault();
            temp.qty -= amountGrabed;
            _SA_DbContext.SaveChanges();
        }

        public void TooManyGrabed(Item item, int wants)
        {
            Item temp = items.Where(x => x.Id_TO_S == item.Id_TO_S && x.productId == item.productId).FirstOrDefault();
            int agreed = 0;
            int maxCanGet = 0;
            try
            {
                if (item.qty != 0)
                {
                    maxCanGet = (item.qty / 2) + 1;

                    if (maxCanGet >= wants)
                    {
                        agreed = wants;
                    }
                    else
                    {
                        throw new Exception("You are asking for to many.\nWe need some for other people.\nThanks for your understanding!");
                    }
                }
                else
                {
                    throw new Exception("Sorry! We sold out of that Item.");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _SA_DbContext.SaveChanges();
        }
        public ClaimsIdentity Authenticate(string username, string password)
        {
             Customer user = CreateCustomer(username, password);
             if (user == null || user.lastName != password) return null;

             var claims = new List<Claim>
             {
                 new Claim("Id", user.Customer_Id.ToString()),
                 new Claim(ClaimTypes.Name, user.firstName),
                 new Claim(ClaimTypes.NameIdentifier, user.firstName)
             };
             if (user.Addmin) claims.Add(new Claim("IsAdmin", "Yes"));

            return new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public void AddToCart(Item i,Cart a)
        {
            Item temp = items.Where(x => x.Id_TO_S == i.Id_TO_S && x.productId == i.productId).FirstOrDefault();
            TooManyGrabed(temp,i.qty);
            ItemWasGrabed(i);
            PremadeItemGrabed(i);
            a.the_store_id = temp.Id_TO_S;
            a.InShoppingCart = (int)temp.Id;
            carts.Add(a);
            _SA_DbContext.SaveChanges();
        }

        public List<Cart> GetCartItems(string CustGuidString)
        {
            var itemincart = carts.Where(x => x.customerGuild == CustGuidString);
            List<Cart> cart = new List<Cart>();
            foreach(var c in itemincart)
            {
                cart.Add(c);
            }
            return cart;
        }
    }
}
