﻿namespace ABP_7_2.MauiBlazor;
using Application = Microsoft.Maui.Controls.Application;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }
}
