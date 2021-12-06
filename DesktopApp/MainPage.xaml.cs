﻿using System;
using System.Linq;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace DesktopApp
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            Greeting();
        }

        public async void Greeting()
        {
            var mediaElement = new MediaElement();
            var stream = await new SpeechSynthesizer{ 
                Voice = (
                    from voice in SpeechSynthesizer.AllVoices
                    where voice.Gender == VoiceGender.Female
                    select voice
                ).FirstOrDefault() ?? SpeechSynthesizer.DefaultVoice
            }.SynthesizeTextToStreamAsync("Welcome!");

            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (HackatonWebView.CanGoBack)
            {
                HackatonWebView.GoBack();
            }
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (HackatonWebView.CanGoForward)
            {
                HackatonWebView.GoForward();
            }
        }
    }
}
