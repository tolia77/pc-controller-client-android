using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;
using SocketApp;

namespace PCControllerMobile
{
    public partial class MainPage : ContentPage
    {
        private SocketRequests socketRequests;
        public MainPage()
        {
            InitializeComponent();
            MoveMouseButton.IsEnabled = false;
            OpenLinkButton.IsEnabled = false;
            CmdExecuteButton.IsEnabled = false;
            ShutdownPcButton.IsEnabled = false;
            RestartPcButton.IsEnabled = false;
            GetScreenshotButton.IsEnabled = false;
        }
        public void InputIPPressed(object sender, EventArgs args)
        {
            try
            {
                socketRequests = new SocketRequests(IPEntry.Text);
                MoveMouseButton.IsEnabled = true;
                OpenLinkButton.IsEnabled = true;
                CmdExecuteButton.IsEnabled = true;
                ShutdownPcButton.IsEnabled = true;
                RestartPcButton.IsEnabled = true;
                GetScreenshotButton.IsEnabled = true;
            }
            catch (Exception)
            {
                DisplayAlert("ERROR", "Connection error", "OK");
            }
        }
        public void MoveMousePressed(object sender, EventArgs args)
        {
            if (MoveMouseButton.Text == "Submit")
            {
                Entry intervalEntry = (Entry)MoveMouseInputField.Children[0];
                Entry repeatsEntry = (Entry)MoveMouseInputField.Children[1];
                if
                (
                !string.IsNullOrEmpty(repeatsEntry.Text) &&
                !string.IsNullOrEmpty(intervalEntry.Text) &&
                long.TryParse(repeatsEntry.Text, out long temp) && temp > 0 &&
                long.TryParse(intervalEntry.Text, out temp) && temp > 0
                )
                {
                    try
                    {
                        string result = socketRequests.MoveMouseRequest(repeatsEntry.Text, intervalEntry.Text);
                        if (result == "ERROR")
                        {
                            DisplayAlert("ERROR", "Trouble on server", "OK");
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Fatal error", ex.ToString(), "OK");
                    }
                }
                MoveMouseButton.Text = "Move mouse";
                MoveMouseInputField.Children.Remove(MoveMouseInputField.Children[0]);
                MoveMouseInputField.Children.Remove(MoveMouseInputField.Children[0]);
            }
            else
            {
                Entry intervalEntry = new Entry { Placeholder = "Interval" };
                Entry timesEntry = new Entry { Placeholder = "How many times" };
                MoveMouseInputField.Children.Add(intervalEntry);
                MoveMouseInputField.Children.Add(timesEntry);
                MoveMouseButton.Text = "Submit";
            }
        }
        public void GetScreenshotPressed(object sender, EventArgs args)
        {
            try
            {
                byte[] bytes = socketRequests.GetScreenshotRequest();
                ScreenshotImage.Source = ImageSource.FromStream(() => new MemoryStream(bytes));
            }
            catch (Exception ex)
            {
                DisplayAlert("Fatal error", ex.ToString(), "OK");
            }

        }
        public void OpenLinkPressed(object sender, EventArgs args)
        {
            if (OpenLinkButton.Text == "Submit")
            {
                Entry linkEntry = (Entry)OpenLinkInputField.Children[0];
                try
                {
                    string result = socketRequests.OpenLinkRequest(linkEntry.Text);
                    if (result == "ERROR")
                    {
                        DisplayAlert("ERROR", "Trouble on server", "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Fatal error", ex.ToString(), "OK");
                }
                OpenLinkButton.Text = "Open link";
                OpenLinkInputField.Children.Remove(OpenLinkInputField.Children[0]);
            }
            else
            {
                Entry linkEntry = new Entry { Placeholder = "Link" };
                OpenLinkInputField.Children.Add(linkEntry);
                OpenLinkButton.Text = "Submit";
            }
        }
        public void CMDExecutePressed(object sender, EventArgs args)
        {
            if (CmdExecuteButton.Text == "Submit")
            {
                Entry commandEntry = (Entry)CMDExecuteInputField.Children[0];
                try
                {
                    string result = socketRequests.CmdExecuteRequest(commandEntry.Text);
                    if (result == "ERROR")
                    {
                        DisplayAlert("ERROR", "Trouble on server", "OK");
                    }
                }
                catch (Exception ex)
                {
                    DisplayAlert("Fatal error", ex.ToString(), "OK");
                }
                CMDExecuteInputField.Children.Remove(CMDExecuteInputField.Children[0]);
                CmdExecuteButton.Text = "Execute in CMD";
            }
            else
            {
                Entry commandEntry = new Entry { Placeholder = "Command" };
                CMDExecuteInputField.Children.Add(commandEntry);
                CmdExecuteButton.Text = "Submit";
            }
        }
        public void ShutdownPCPressed(object sender, EventArgs args)
        {
            try
            {
                socketRequests.ShutdownPcRequest();
            }
            catch (Exception ex)
            {
                DisplayAlert("Fatal error", ex.ToString(), "OK");
            }

        }
        public void RestartPCPressed(object sender, EventArgs args)
        {
            try
            {
                socketRequests.RestartPcRequest();
            }
            catch (Exception ex)
            {
                DisplayAlert("Fatal error", ex.ToString(), "OK");
            }
        }
    }
}
