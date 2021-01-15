using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace BusinessLayer
{
    public class StoreLevelPrograms
    {
        private readonly StoreAppRepsitoryLayer _storeAppRepsitoryLayer;
        private readonly MapperClass _mapperClass;
        public StoreLevelPrograms() { }
        public StoreLevelPrograms(StoreAppRepsitoryLayer storeAppRepsitory,MapperClass mapper)
        {
            this._storeAppRepsitoryLayer = storeAppRepsitory;
            this._mapperClass = mapper;
        }
        public StoreLevelPrograms(StoreAppRepsitoryLayer storeAppRepsitory)
        {
            this._storeAppRepsitoryLayer = storeAppRepsitory;
        }

        public List<StoreViewModel> ProductSelection(int store_id)
        {
            List<StoreViewModel> list = _storeAppRepsitoryLayer.GetItemForStore(store_id);

            return list;
        }

        public double CheckOutTotal(List<Cart> loaded)
        {
            Product temp = new Product();
            List<OrderedItem> list = new List<OrderedItem>();
            double tempTotal = 0;
            foreach (var grab in loaded)
            {
                temp = _storeAppRepsitoryLayer.GetProduct(grab.InShoppingCart);
                OrderedItem orderedItem = new OrderedItem();
                orderedItem.ProductName = temp.productName;
                orderedItem.qtyOrdered = grab.amountPicked;
                orderedItem.pricePaid = temp.price;
                list.Add(orderedItem);
                tempTotal = tempTotal + (grab.amountPicked * temp.price);
            }
            return tempTotal;

        }

        public void CheckOutCounter(List<Cart> loaded,int storeid,string guid)
        {
            Product temp = new Product();
            Orders order = new Orders();
            order.storeLocationID = storeid;
            order.customerGuid = guid;
            List<OrderedItem> list = new List<OrderedItem>();
            double tempTotal = 0;
            foreach (var grab in loaded)
            {
                temp = _storeAppRepsitoryLayer.GetProduct(grab.InShoppingCart);
                OrderedItem orderedItem = new OrderedItem();
                orderedItem.ProductName = temp.productName;
                orderedItem.qtyOrdered = grab.amountPicked;
                orderedItem.pricePaid = temp.price;
                list.Add(orderedItem);
                tempTotal = tempTotal + (grab.amountPicked * temp.price);
            }
            order.total = tempTotal;
            order.dateTime = DateTime.Now;

            _storeAppRepsitoryLayer.ProcessOrder(order, list, storeid,guid);
        }
        public void AddToCart(StoreViewModel svm,HttpContext context)
        {
            Item item = new Item();
            item.Id_TO_S = svm.ID_store;
            item.productId = svm.productId;
            item.qty = svm.qty;
            item.sale = svm.sale;
            Cart cart = new Cart();
            cart.customerGuild = context.Session.GetString("Guid");
            cart.amountPicked = svm.qty;
            _storeAppRepsitoryLayer.AddToCart(item,cart);
        }

        public List<Store> GetStores()
        {
            return _storeAppRepsitoryLayer.GetStores();
        }

        public List<Cart> GetCartItems(string customerGuidtoString)
        {
            return _storeAppRepsitoryLayer.GetCartItems(customerGuidtoString);
        }

        public List<Orders> AllPastOrders(string customerid)
        {
            return _storeAppRepsitoryLayer.GetAllPastOrders(customerid);
        }

        public List<FullOrderViewModel> GetFullOrderDetails(int id)
        {
            List<FullOrderViewModel> fullorder = new List<FullOrderViewModel>(); 
            fullorder = _storeAppRepsitoryLayer.FullOrderDisplay(id);
            return fullorder;
        }

        public List<Orders> AllPastStoreOrders(int id)
        {
            return _storeAppRepsitoryLayer.GetAllStorePastOrders(id);
        }
        public List<Orders> DisplayPastOrders(List<Orders> lo,int sort)
        {
            List<Orders> temp3 = new List<Orders>();
            switch (sort)
            {
                case 2:
                    var temp = lo.OrderByDescending(x => x.dateTime);
                    foreach (var der in temp)
                    {
                        temp3.Add(der);
                    }
                    Console.WriteLine();
                    break;
                case 1:
                    var temp2 = lo.OrderBy(x => x.dateTime);
                    foreach (var der in temp2)
                    {
                        temp3.Add(der);
                    }
                    Console.WriteLine();
                    break;
                case 3:
                    temp3.Add(lo.Where(x => x.total ==  lo.Min(x => x.total)).FirstOrDefault());
                    break;
                case 4:
                    temp3.Add(lo.Where(x => x.total == lo.Max(x => x.total)).FirstOrDefault());
                    break;
            }
            return lo;
        }
    }
}
