﻿using GroceryList.Interfaces;
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
      HttpClient http = new HttpClient();
      http.BaseAddress = new Uri(string.Format("{0}/GroceryItems/{1}.json",m_firebaseUrl, key));
      var response = await http.GetAsync(http.BaseAddress);
      response.EnsureSuccessStatusCode();
      var jsonResult = response.Content.ReadAsStringAsync().Result;
      return JsonConvert.DeserializeObject<GroceryItem>(jsonResult);
    }

    public Task<List<GroceryItem>> ReadGroceryList()
    {
      throw new NotImplementedException();
    }

    public Task<ShoppingList> ReadShoppingList(string key)
    {
      throw new NotImplementedException();
    }

    public async Task<StorageResponse> WriteGroceryItem(GroceryItem item)
    {
      var jsonEncodedData = JsonConvert.SerializeObject(item);
      HttpClient http = new HttpClient();
      http.BaseAddress = new Uri(string.Format("{0}/GroceryItems/{1}.json", m_firebaseUrl, item.Id));
      StringContent content = new StringContent(jsonEncodedData);
      var response = await http.PutAsync(http.BaseAddress, content);
      return response.IsSuccessStatusCode ? StorageResponse.Success : StorageResponse.Failure;
    }

    public Task<StorageResponse> WriteGroceryList(List<GroceryItem> list)
    {
      throw new NotImplementedException();
    }

    public async Task<StorageResponse> WriteShoppingList(ShoppingList shoppingList)
    {
      var jsonEncodedData = JsonConvert.SerializeObject(shoppingList);
      HttpClient http = new HttpClient();
      http.BaseAddress = new Uri(string.Format("{0}/GroceryItems/{1}.json", m_firebaseUrl, shoppingList.Id));
      StringContent content = new StringContent(jsonEncodedData);
      var response = await http.PutAsync(http.BaseAddress, content);
      return response.IsSuccessStatusCode ? StorageResponse.Success : StorageResponse.Failure;
    }

    private string m_firebaseUrl;
  }
}
