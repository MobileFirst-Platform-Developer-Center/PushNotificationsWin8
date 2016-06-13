/**
* Copyright 2016 IBM Corp.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using IBMMobileFirstPlatformFoundationPush;
using System;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace PushNotifictionsWin8
{
    internal class NotificationListner : MFPPushNotificationListener
    {
        public async void onReceive(String properties, String payload)
        {
            try
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    async () =>
                    {
                        await new MessageDialog("Received Message\nproperties: " + properties + "\npayload: " + payload).ShowAsync();
                    });
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }
    }
}
