using GroceryList.Services;
using GroceryList.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace GroceryList
{
	public class App : Application
	{
		public App()
		{
      // The root page of your application
      var mainView = new MainViewPage();
      mainView.Init(new FirebaseStorageService(FirebaseStorageService.FIREBASE_URL));
      MainPage = mainView;
    }

    protected override void OnStart()
		{
      // Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
