﻿#pragma checksum "C:\Users\TAKHIR LOVAZOV\Documents\Visual Studio 2017\Projects\App4\App4\WebApi.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "9D620B109C31F0C2069944A25B81AE1C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace App4
{
    partial class WebApi : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                {
                    this.Webapi_Text = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 2:
                {
                    this.Id = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 3:
                {
                    this.Name = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 4:
                {
                    this.CoordinateX = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 5:
                {
                    this.CoordinateY = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 6:
                {
                    this.Webapi_Id = (global::Windows.UI.Xaml.Controls.TextBox)(target);
                }
                break;
            case 7:
                {
                    this.Get = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 27 "..\..\..\WebApi.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Get).Click += this.Getapi_Click;
                    #line default
                }
                break;
            case 8:
                {
                    this.Post = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 28 "..\..\..\WebApi.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Post).Click += this.Postapi_Click;
                    #line default
                }
                break;
            case 9:
                {
                    this.Put = (global::Windows.UI.Xaml.Controls.Button)(target);
                    #line 29 "..\..\..\WebApi.xaml"
                    ((global::Windows.UI.Xaml.Controls.Button)this.Put).Click += this.Putapi_Click;
                    #line default
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 14.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}

