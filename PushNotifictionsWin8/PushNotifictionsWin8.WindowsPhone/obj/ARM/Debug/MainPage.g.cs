﻿

#pragma checksum "C:\Users\Bharat Devdas\Documents\Visual Studio 2015\Projects\PushNotifictionsWin8\PushNotifictionsWin8\PushNotifictionsWin8.WindowsPhone\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D874A9159C08532AA65585E9D0B484D4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PushNotifictionsWin8
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 15 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.IsPushSupported_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 16 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.RegisterDevice_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 17 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GetTags_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 18 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Subscribe_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 19 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.GetSubscriptions_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 20 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Unsubscribe_Click;
                 #line default
                 #line hidden
                break;
            case 7:
                #line 21 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.UnregisterDevice_Click;
                 #line default
                 #line hidden
                break;
            case 8:
                #line 22 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Logout_Click;
                 #line default
                 #line hidden
                break;
            case 9:
                #line 33 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.OKButton_Click;
                 #line default
                 #line hidden
                break;
            case 10:
                #line 34 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.CancelButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


