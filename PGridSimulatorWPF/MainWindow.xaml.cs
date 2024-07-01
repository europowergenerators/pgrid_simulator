using BatterySimulatorLib;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PGridSimulatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Battery _battery;
        private System.Timers.Timer _timer;
        
        public MainWindow()
        {
            InitializeComponent();
            
            _battery = new Battery(50000);
            _battery.state_of_charge_Wh = 25000; // 50% of capacity initially
            
            // Create and start the timer
            _timer = new System.Timers.Timer(1000); // 1000 milliseconds = 1 second
            _timer.Elapsed += TimerElapsed;
            _timer.Start();
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            // Update battery level
            Dispatcher.Invoke(() =>
            {
                UpdateBatteryLevel();
            });
        }

        private void UpdateBatteryLevel()
        {
            double level = _battery.state_of_charge_Wh / _battery.capacity_Wh * 100;
            BatteryLevel.Width = (190 * level) / 100;
            BatteryPercentage.Text = $"{Math.Round(level, 2)}%";

            if (level > 50)
                BatteryLevel.Fill = Brushes.Green;
            else if (level > 20)
                BatteryLevel.Fill = Brushes.Yellow;
            else
                BatteryLevel.Fill = Brushes.Red;
        }

        private void ButtonChargeBattery_Click(object sender, RoutedEventArgs e)
        {
            var chargePower = Double.Parse(BatteryChargePower.Text) * 1000;
            _battery.charge(chargePower);
        }

        private void ButtonDischargeBattery_Click(object sender, RoutedEventArgs e)
        {
            _battery.discharge(Double.Parse(BatteryDischargePower.Text)*1000);
        }

        private void ButtonStopDischargeBattery_Click(object sender, RoutedEventArgs e)
        {
            _battery.stopCharge();
            _battery.stopDischarge();
        }
    }
}