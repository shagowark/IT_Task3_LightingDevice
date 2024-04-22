using LightingDevice.models;
using System.Collections.Generic;
using System.Windows;

namespace LightingDevice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<IlluminationDevice> devices;

        public MainWindow()
        {
            InitializeComponent();

            devices = new List<IlluminationDevice>
            {
                new Streetlight(),
                new DeskLamp(),
                new Chandelier(),
                new Torchere()
            };

            foreach (var device in devices)
            {
                device.Broken += (sender, args) =>
                {
                    MessageBox.Show($"The {sender.GetType().Name} is broken.");
                };
                deviceListBox.Items.Add(device.GetType().Name);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = deviceListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                var selectedDevice = devices[selectedIndex];
                if (!selectedDevice.IsOn)
                {
                    selectedDevice.TurnOn();
                }
                else
                {
                    selectedDevice.TurnOff();
                }
                if (selectedDevice.IsOn)
                {
                    statusText.Text = $"{selectedDevice.GetType().Name} is now ON";
                }
                else
                {
                    statusText.Text = $"{selectedDevice.GetType().Name} is now OFF";
                }

            }
            else
            {
                MessageBox.Show("Please select a device to test.");
            }
        }
        private void ConnectDeskLampButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = deviceListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (devices[selectedIndex] is DeskLamp deskLamp)
                {
                    deskLamp.ConnectToPower();
                    statusText.Text = $"DeskLamp is connected to power.";
                }
                else
                {
                    MessageBox.Show("DeskLamp must be selected to connect it to power.");
                }
            }
            else
            {
                MessageBox.Show("Please select a device to connect to power.");
            }
        }

        private void ConnectTorchereButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = deviceListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (devices[selectedIndex] is Torchere torchere)
                {
                    torchere.ConnectToPower();
                    statusText.Text = $"Torchere is connected to power.";
                }
                else
                {
                    MessageBox.Show("Torchere must be selected to connect it to power.");
                }
            }
            else
            {
                MessageBox.Show("Please select a device to connect to power.");
            }
        }

        private void TurnOnChandelierButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = deviceListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (devices[selectedIndex] is Chandelier chandelier)
                {
                    chandelier.TurnOn();
                    statusText.Text = $"Chandelier is turned on. Current mode: {chandelier.CurrentMode}";
                }
                else
                {
                    MessageBox.Show("Chandelier must be selected to switch its mode.");
                }
            }
            else
            {
                MessageBox.Show("Please select a device to switch its mode.");
            }
        }
        private void TurnOffChandelierButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndex = deviceListBox.SelectedIndex;
            if (selectedIndex != -1)
            {
                if (devices[selectedIndex] is Chandelier chandelier)
                {
                    chandelier.TurnOff();
                    if (!chandelier.IsOn)
                    {
                        statusText.Text = $"Chandelier is turned off.";
                    } else
                    {
                        statusText.Text = $"Chandelier is turned on. Current mode: {chandelier.CurrentMode}";
                    }
                }
                else
                {
                    MessageBox.Show("Chandelier must be selected to turn it off.");
                }
            }
            else
            {
                MessageBox.Show("Please select a device to turn off.");
            }
        }

    }
}
