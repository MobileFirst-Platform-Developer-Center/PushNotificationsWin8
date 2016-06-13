using IBMMobileFirstPlatformFoundationPush;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Worklight;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace PushNotifictionsWin8
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage _this;
        public bool isLoggedOut { get; set; }
        UserLoginChallengeHandler userChallengeHandler;
        IWorklightClient _newClient;
        private MFPPush PushClient = null;
        public MessageDialog Message = null;

        public MainPage()
        {
            this.InitializeComponent();
            _this = this;
            userChallengeHandler = new UserLoginChallengeHandler("UserLogin");
            userChallengeHandler.SetShouldSubmitChallenge(false);
            userChallengeHandler.SecurityCheck = "UserLogin";
            userChallengeHandler.SetSubmitFailure(false);

            _newClient = WorklightClient.CreateInstance();
            _newClient.RegisterChallengeHandler(userChallengeHandler);
            getAccessToken();

            try
            {
                PushClient = MFPPush.GetInstance();
                PushClient.Initialize();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void getAccessToken()
        {
            WorklightAccessToken accessToken = await _newClient.AuthorizationManager.ObtainAccessToken(userChallengeHandler.SecurityCheck);

            if (accessToken.IsValidToken && accessToken.Value != null && accessToken.Value != "")
            {
                Debug.WriteLine("Success");
                _this.hideChallenge();
                userChallengeHandler.SetShouldSubmitChallenge(false);
            }
            else
            {
                Debug.WriteLine("Failure");
            }


        }

        public async void AddUserName(String userName)
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    if (userName != "")
                    {
                        _this.UserName.Text = "Hello " + userName;
                    }
                    else
                    {
                        _this.UserName.Text = "";
                    }
                });
        }

        private async void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (username.Text != "" && password.Text != "")
            {
                JObject userJSON = new JObject();
                userJSON.Add("username", username.Text);
                userJSON.Add("password", password.Text);
                userJSON.Add("rememberMe", RememberMe.IsChecked);

                userChallengeHandler.SetShouldSubmitChallenge(true);
                userChallengeHandler.challengeAnswer = userJSON;
                UserLoginChallengeHandler.waitForPincode.Set();
                if (this.isLoggedOut)
                {
                    userChallengeHandler.login(userJSON);
                }
                hideChallenge();
                this.AddUserName(username.Text);
            }
            else
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     _this.HintText.Text = "Username and password are required";
                 });
            }
        }

        public async void showChallenge(Object challenge)
        {
            String errorMsg = "";
            JObject challengeJSON = (JObject)challenge;

            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                 () =>
                 {
                     _this.HintText.Text = "";
                     _this.LoginGrid.Visibility = Visibility.Visible;
                     if (challengeJSON != null)
                     {
                         if (errorMsg != "")
                         {
                             _this.HintText.Text = errorMsg + "Remaining Attempts: " + challengeJSON.GetValue("remainingAttempts");
                         }
                         else
                         {
                             _this.HintText.Text = challengeJSON.GetValue("errorMsg") + "\n" + "Remaining Attempts: " + challengeJSON.GetValue("remainingAttempts");
                         }
                     }
                     else
                     {
                         _this.username.Text = "";
                         _this.password.Text = "";
                     }
                     _this.RegisterDevice.IsEnabled = false;
                     _this.GetTags.IsEnabled = false;
                     _this.Subscribe.IsEnabled = false;
                     _this.GetSubscriptions.IsEnabled = false;
                     _this.Unsubscribe.IsEnabled = false;
                     _this.UnregisterDevice.IsEnabled = false;
                     _this.Logout.IsEnabled = false;
                 });
        }

        public async void hideChallenge()
        {
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    _this.LoginGrid.Visibility = Visibility.Collapsed;
                    _this.RegisterDevice.IsEnabled = true;
                    _this.GetTags.IsEnabled = true;
                    _this.Subscribe.IsEnabled = true;
                    _this.GetSubscriptions.IsEnabled = true;
                    _this.Unsubscribe.IsEnabled = true;
                    _this.UnregisterDevice.IsEnabled = true;
                    _this.Logout.IsEnabled = true;
                });
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            userChallengeHandler.SetSubmitFailure(true);
            userChallengeHandler.SetShouldSubmitChallenge(false);
            UserLoginChallengeHandler.waitForPincode.Set();

        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            userChallengeHandler.logout(userChallengeHandler.SecurityCheck);
        }

        private async void IsPushSupported_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PushClient.IsPushSupported())
                {
                    Message = new MessageDialog("Push is supported!");
                }
                else
                {
                    Message = new MessageDialog("Push is not supported!");
                }

                await Message.ShowAsync();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void RegisterDevice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject Options = new JObject();
                Options["phoneNumber"] = "9999999999";
                MFPPushMessageResponse Response = await PushClient.RegisterDevice(Options);
                NotificationListner Listner = new NotificationListner();
                PushClient.Listen(Listner);

                if (Response.Success == true)
                {
                    Message = new MessageDialog("Registration Successful");
                }
                else
                {
                    Message = new MessageDialog("Registration Failed");
                }

                await Message.ShowAsync();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void GetTags_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MFPPushMessageResponse Response = await PushClient.GetTags();

                if (Response.Success == true)
                {
                    Message = new MessageDialog("Avalibale Tags: " + Response.ResponseJSON["tagNames"]);
                }
                else
                {
                    Message = new MessageDialog("Failed to get Tags list");
                }

                await Message.ShowAsync();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void Subscribe_Click(object sender, RoutedEventArgs e)
        {
            string[] Tags = null;
            try
            {
                MFPPushMessageResponse Response = await PushClient.GetTags();

                if (Response.Success == true)
                {
                    Tags = JsonConvert.DeserializeObject<string[]>(Response.ResponseJSON["tagNames"].ToString());
                }

                if (Tags != null && !Tags[0].Equals("Push.All"))
                {
                    if ((await PushClient.Subscribe(Tags)).Success == true)
                    {
                        Message = new MessageDialog("Successfully Subscribed to Tags: " + string.Join(", ", Tags));
                    }
                    else
                    {
                        Message = new MessageDialog("Failed to Subscribe!");
                    }

                    await Message.ShowAsync();
                }
                else
                {
                    Message = new MessageDialog("There are no tags to subscribe to");
                    await Message.ShowAsync();
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void GetSubscriptions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MFPPushMessageResponse Response = await PushClient.GetSubscriptions();

                if (Response.Success == true)
                {
                    Message = new MessageDialog("Subscriptions: " + Response.ResponseJSON["tagNames"]);
                }
                else
                {
                    Message = new MessageDialog("Failed to get subcription list...");
                }

                await Message.ShowAsync();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void Unsubscribe_Click(object sender, RoutedEventArgs e)
        {
            string[] Tags = null;
            try
            {
                MFPPushMessageResponse Response = await PushClient.GetSubscriptions();

                if (Response.Success == true)
                {
                    Tags = JsonConvert.DeserializeObject<string[]>(Response.ResponseJSON["tagNames"].ToString());
                }

                if (Tags != null && !Tags[0].Equals("Push.All"))
                {
                    if ((await PushClient.Unsubscribe(Tags)).Success == true)
                    {
                        Message = new MessageDialog("Successfully Unsubscribed to Tags: " + string.Join(", ", Tags));
                    }
                    else
                    {
                        Message = new MessageDialog("Failed to Unsubscribe!");
                    }

                    await Message.ShowAsync();
                }
                else
                {
                    Message = new MessageDialog("There are no tags to unsubscribe to");
                    await Message.ShowAsync();
                }
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }

        private async void UnregisterDevice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MFPPushMessageResponse Response = await PushClient.UnregisterDevice();

                if (Response.Success == true)
                {
                    Message = new MessageDialog("Successfully Unregistered Device!");
                }
                else
                {
                    Message = new MessageDialog("Failed to Unrgister the Device!");
                }

                await Message.ShowAsync();
            }
            catch (Exception Ex)
            {
                Debug.WriteLine(Ex.StackTrace);
            }
        }
    }
}
