using Microsoft.AspNetCore.Http;
using ModelLayer;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class MapperClass
    {

		private readonly StoreAppRepsitoryLayer _storeAppRepsitoryLayer;
		public MapperClass() { }
		public MapperClass(StoreAppRepsitoryLayer storeAppRepsitory)
		{
			this._storeAppRepsitoryLayer = storeAppRepsitory;
		}

		public OrderViewModel ConvertOrdersToOrderViewModel(Orders order)
        {
			OrderViewModel orderViewModel = new OrderViewModel()
			{
				orderID=order.orderID,
				dateTime = order.dateTime,
				total = order.total
			};
			return orderViewModel;
        }


		public CustomerViewModel ConvertPlayerToPlayerViewModel(Customer customer)
		{
			CustomerViewModel customerViewModel = new CustomerViewModel()
			{
				Customer_Id = customer.Customer_Id,
				firstName = customer.firstName,
				lastName = customer.lastName,
				JpgStringImage = ConvertByteArrayToJpgString(customer.ByteArrayImage)
			};

			return customerViewModel;
		}
		public byte[] ConvertIformFileToByteArray(IFormFile iformFile)
		{
			using (var ms = new MemoryStream())
			{
				// convert the IFormFile into a byte[]
				iformFile.CopyTo(ms);

				if (ms.Length > 2097152)// if it's bigger that 2 MB
				{
					return null;
				}
				else
				{
					byte[] a = ms.ToArray(); // put the string into the Image property
					return a;
				}
			}
		}

		public Customer ConvertPlayerViewModelToPlayer(CustomerViewModel customerViewModel)
		{
			Customer customer = new Customer()
			{
				Customer_Id = customerViewModel.Customer_Id,
				firstName = customerViewModel.firstName,
				lastName = customerViewModel.lastName,
				ByteArrayImage = ConvertImageStringToByteArray(customerViewModel.JpgStringImage)
			};

			return customer;
		}

		private string ConvertByteArrayToJpgString(byte[] byteArray)
		{
			if (byteArray != null)
			{
				string imageBase64Data = Convert.ToBase64String(byteArray, 0, byteArray.Length);
				string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64Data}");
				return imageDataURL;
			}
			else return null;
		}

		public byte[] ConvertImageStringToByteArray(string base64Image)
		{
			//take everything after the ,
			string base64Image1 = base64Image.Split(',')[1];
			byte[] bytes = Convert.FromBase64String(base64Image1);
			return bytes;
		}

		public Customer GetCustomer(string username, string password)
        {
			return _storeAppRepsitoryLayer.CreateCustomer(username, password);
        }

		public ClaimsIdentity Authenticate(string username, string password)
		{
			
			return _storeAppRepsitoryLayer.Authenticate(username, password);
		}
	}
}
