using ACDeliverySimulatorLib;
using System;
using System.Timers;

namespace ACDeliverySimulator
{
    class Program
    {
        public static ACDelivery acDelivery { get; set; }

        static void Main(string[] args)
        {
            System.Timers.Timer outputTimer = new System.Timers.Timer(1000);
            outputTimer.Elapsed += (sender, e) => Console.WriteLine(acDelivery.ToString());
            outputTimer.AutoReset = true;
            outputTimer.Start();

            acDelivery = new ACDelivery(50000); //50kWh

            while (true)
            {
                string input = Console.ReadLine();
                if (input == "exit")
                {
                    break;
                }
                switch (input)
                {
                    case "p":
                        acDelivery.deliverPower(10000); // 10kW charging
                        break;
                    
                    case "s":
                        acDelivery.stopDelivery();
                        break;
                }
            }
        }

    }
}