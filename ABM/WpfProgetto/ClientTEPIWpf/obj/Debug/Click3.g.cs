﻿#pragma checksum "..\..\Click3.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3C2E22DBE965CF7A89824AE674B481E4F212B842FE5B0D6C6FF5E627718D2D1D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ClientTEPIWpf;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ClientTEPIWpf {
    
    
    /// <summary>
    /// Click3
    /// </summary>
    public partial class Click3 : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\Click3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DPFirst;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Click3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker DPFinally;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\Click3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBSettore;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\Click3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBArgomento;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Click3.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TBArea;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ClientTEPIWpf;component/click3.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Click3.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.DPFirst = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 2:
            this.DPFinally = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.TBSettore = ((System.Windows.Controls.TextBox)(target));
            
            #line 18 "..\..\Click3.xaml"
            this.TBSettore.GotFocus += new System.Windows.RoutedEventHandler(this.txtSettore_GotFocus);
            
            #line default
            #line hidden
            
            #line 19 "..\..\Click3.xaml"
            this.TBSettore.LostFocus += new System.Windows.RoutedEventHandler(this.txtSettore_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TBArgomento = ((System.Windows.Controls.TextBox)(target));
            
            #line 20 "..\..\Click3.xaml"
            this.TBArgomento.GotFocus += new System.Windows.RoutedEventHandler(this.txtArgomento_GotFocus);
            
            #line default
            #line hidden
            
            #line 21 "..\..\Click3.xaml"
            this.TBArgomento.LostFocus += new System.Windows.RoutedEventHandler(this.txtArgomento_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.TBArea = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\Click3.xaml"
            this.TBArea.GotFocus += new System.Windows.RoutedEventHandler(this.txtArea_GotFocus);
            
            #line default
            #line hidden
            
            #line 23 "..\..\Click3.xaml"
            this.TBArea.LostFocus += new System.Windows.RoutedEventHandler(this.txtArea_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 24 "..\..\Click3.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 31 "..\..\Click3.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

