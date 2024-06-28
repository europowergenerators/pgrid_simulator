using BatterySimulatorLib;
using System;
using System.Timers;

namespace BatterySimulatorConsole
{
    class Program
    {
        public static Battery battery { get; set; }

        static void Main(string[] args)
        {
            System.Timers.Timer outputTimer = new System.Timers.Timer(1000);
            outputTimer.Elapsed += (sender, e) => Console.WriteLine(battery.ToString());
            outputTimer.AutoReset = true;
            outputTimer.Start();
        
            battery = new Battery(10000); //10kWh
            
            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                switch (input)
                {
                    case "c":
                        battery.charge(10000); // 10kW charging
                        break;
                    case "d":
                        battery.discharge(1000); //10kW discharging
                        break;
                    case "cts":
                        battery.chargeToSOC(10000, 50); // 10kW charging to 80% SOC
                        break;
                    case "dts":
                        battery.dischargeToSOC(10000, 50); // 10kW discharging to 20% SOC
                        break;

                    case "s":
                        battery.stopCharge();
                        battery.stopDischarge();
                        break;
                }
            }
        }

    }
}