﻿using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace DesktopApp
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
            Greeting();
        }

        public async void Greeting()
        {
            var mediaElement = new MediaElement();
            var stream = await new SpeechSynthesizer().SynthesizeTextToStreamAsync("Welcome!");
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }
    }
}