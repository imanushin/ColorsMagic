﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ColorsMagic.WP.Common
{
    public sealed class NavigationService
    {
        public static readonly NavigationService Instance = new NavigationService();
            
        private NavigationService()
        {
        }

        public void Navigate(Type sourcePage)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage);
        }

        public void Navigate(Type sourcePage, object parameter)
        {
            var frame = (Frame)Window.Current.Content;
            frame.Navigate(sourcePage, parameter);
        }

        public void GoBack()
        {
            var frame = (Frame)Window.Current.Content;

            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
            else
            {
                Application.Current.Exit();
            }
        }
    }
}