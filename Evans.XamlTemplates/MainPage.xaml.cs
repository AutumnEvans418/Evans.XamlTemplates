﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Evans.XamlTemplates
{
    public partial class MainPage 
    {
        public MainPage()
        {
            BindingContext = new SandboxViewModel();

            InitializeComponent();
        }
    }
}
