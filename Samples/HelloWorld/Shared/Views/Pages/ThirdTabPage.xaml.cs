﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace atlas.samples.helloworld.Shared.Views.Pages
{
    public partial class ThirdTabPage : ContentPage
    {
        public ThirdTabPage()
        {
            InitializeComponent();
            Label.Text = Guid.NewGuid().ToString();
        }
    }
}