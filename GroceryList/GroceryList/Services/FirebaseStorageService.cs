using GroceryList.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryList.Model;
using System.Net.Http;
using Newtonsoft.Json;

namespace GroceryList.Services
{
  public class FirebaseStorageService : IStorageWrapper
  {
    public const string FIREBASE_URL = "https://luminous-fire-4986.firebaseio.com";

    public FirebaseStorageService(string firebaseUrl)
    {
      m_firebaseUrl = firebaseUrl;
    }

    public async Task<GroceryItem> ReadGroceryItem(string key)
    {
			HttpClient http = CreateHttpClient(key);
      var response = await http.GetAsync(http.BaseAddress);
      response.EnsureSuccessStatusCode();
      var jsonResult = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<GroceryItem>(jsonResult);
    }

    public async Task<List<GroceryItem>> ReadGroceryList()
    {
			HttpClient http = CreateHttpClient("GroceryTypes");
			var response = await http.GetAsync(http.BaseAddress);
			response.EnsureSuccessStatusCode();
			var jsonResult = response.Content.ReadAsStringAsync().Result;
			return JsonConvert.DeserializeObject<List<GroceryItem>>(jsonResult);
		}

		public async Task<ShoppingList> ReadShoppingList(string key)
    {
			HttpClient http = CreateHttpClient(key);
			var response = await http.GetAsync(http.BaseAddress);
			response.EnsureSuccessStatusCode();
			var jsonResult = response.Content.ReadAsStringAsync().Result;
			var shoppingListWrapper = JsonConvert.DeserializeObject<ShoppingListWrapper>(jsonResult);
      return shoppingListWrapper.ToShoppingList();
		}

		public async Task<StorageResponse> WriteGroceryItem(GroceryItem item)
    {
      var jsonEncodedData = JsonConvert.SerializeObject(item);
			HttpClient http = CreateHttpClient(item.Id);
      var response = await http.PutAsync(http.BaseAddress, new StringContent(jsonEncodedData));
      return response.IsSuccessStatusCode ? StorageResponse.Success : StorageResponse.Failure;
    }

    public async Task<StorageResponse> WriteGroceryList(List<GroceryItem> list)
    {
			var jsonEncodedData = JsonConvert.SerializeObject(list);
			HttpClient http = CreateHttpClient("GroceryTypes");
			var response = await http.PutAsync(http.BaseAddress, new StringContent(jsonEncodedData));
			return response.IsSuccessStatusCode ? StorageResponse.Success : StorageResponse.Failure;
		}

		public async Task<StorageResponse> WriteShoppingList(ShoppingList shoppingList)
		{
      var shoppingListWrapper = new ShoppingListWrapper(shoppingList);
			var jsonEncodedData = JsonConvert.SerializeObject(shoppingListWrapper);
			HttpClient http = CreateHttpClient(shoppingList.Id);
			var response = await http.PutAsync(http.BaseAddress, new StringContent(jsonEncodedData));
			return response.IsSuccessStatusCode ? StorageResponse.Success : StorageResponse.Failure;
		}

		private HttpClient CreateHttpClient(string storageItemkey)
		{
			HttpClient http = new HttpClient();
			http.BaseAddress = new Uri(string.Format("{0}/GroceryItems/{1}.json", m_firebaseUrl, storageItemkey));
			return http;
		}

		private string m_firebaseUrl;
  }

  class ShoppingListWrapper
  {
    public ShoppingListWrapper(ShoppingList list)
    {
      if (null == list) return;

      Name = list.Name;
      Id = list.Id;
      Items = list;
    }

    public ShoppingList ToShoppingList()
    {
      ShoppingList shoppingList = new ShoppingList(Name, Id);
      if (null != Items)
      {
        foreach (var item in Items)
        {
          shoppingList.Add(item);
        }
      }
      return shoppingList;
    }

    public string Name { get; set; }
    public string Id { get; set; }
    public IEnumerable<GroceryItem> Items { get; set; }
  }
}
