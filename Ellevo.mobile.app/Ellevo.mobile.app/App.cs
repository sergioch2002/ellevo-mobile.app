﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
using Xamarin.Forms;

namespace Ellevo.mobile.app
{
    public class App : Application
    {
        public App()
        {
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));
            NavigationPage navigationPage = new NavigationPage(new StartPage());
            navigationPage.BarBackgroundColor = Color.FromHex("#2DBDB6");
            navigationPage.BarTextColor = Color.White;
            MainPage = navigationPage;
        }

        protected override void OnStart()
        {
            MobileCenter.Start(typeof(Analytics), typeof(Crashes));
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
