﻿

#pragma checksum "C:\Users\Grant\Documents\GitHub\decibel\Music8\Music8\Pages\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4C18B5B4DBCAB7CA45FB60BEED189900"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Music8.Pages
{
    partial class MainPage : global::Music8.Common.LayoutAwarePage, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 43 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).GotFocus += this.OnSearchTextBoxGotFocus;
                 #line default
                 #line hidden
                #line 44 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).LostFocus += this.OnSearchTextBoxLostFocus;
                 #line default
                 #line hidden
                #line 46 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.TextBox)(target)).TextChanged += this.searchTextBox_TextChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 31 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.RadioButton_Checked;
                 #line default
                 #line hidden
                #line 32 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerExited += this.RadioButton_PointerExited;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 34 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.RadioButton_Checked;
                 #line default
                 #line hidden
                #line 35 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerExited += this.RadioButton_PointerExited;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 37 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.RadioButton_Checked;
                 #line default
                 #line hidden
                #line 38 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).PointerExited += this.RadioButton_PointerExited;
                 #line default
                 #line hidden
                #line 38 "..\..\Pages\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Unchecked += this.nowPlayingRadioButton_Unchecked;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


