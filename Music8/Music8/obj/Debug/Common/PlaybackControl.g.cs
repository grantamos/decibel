﻿

#pragma checksum "C:\Users\Grant\documents\visual studio 2012\Projects\Music8\Music8\Common\PlaybackControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "75241D39309579F476B643DEAD994A6D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Music8.Common
{
    partial class PlaybackControl : global::Windows.UI.Xaml.Controls.UserControl, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 24 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Repeat_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 25 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Shuffle_Click;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 26 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Previous_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 27 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Play_Click;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 28 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Next_Click;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 29 "..\..\Common\PlaybackControl.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Queue_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

