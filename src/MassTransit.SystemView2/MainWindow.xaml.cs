﻿// Copyright 2007-2008 The Apache Software Foundation.
//  
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace MassTransit.SystemView
{
    using System.Windows;
    using System.Windows.Controls;
    using ViewModel;
    
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tvSubscriptions.ItemsSource = LocalSubscriptionCache.Endpoints;
        }

        private void itemRemove_Click(object sender, RoutedEventArgs e)
        {
            if (e != null)
            {
                var source = e.Source as MenuItem;
                if (source != null)
                {
                    var message = source.CommandParameter as Message;
                    if (message != null)
                    {
                        if (MessageBox.Show("Are you sure you want to remove this subscription?", "Confirm", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            var sdc = App.SubscriptionDataConsumer;

                            sdc.RemoveSubscription(message.ClientId, message.MessageName, message.CorrelationId, message.EndpointUri);
                        }
                    }
                }
            }
        }
    }
}