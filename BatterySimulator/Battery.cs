using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BatterySimulator
{
    public class Battery : IPowerStorage
    {
        public double state_of_charge_Wh { get; set; }

        public double capacity_Wh { get; set; }
        public double state_of_health_pct { get; set; }

        private double charge_power;
        private double charge_to_soc_point;


        public void charge(double power)
        {
            charge_power = power;
        }

        public void chargeToSOC(double power, double soc_pct)
        {
            chargeToSOC_wh(power, capacity_Wh * soc_pct / 100);
        }
        private void chargeToSOC_wh(double power, double soc_wh)
        {
            charge_power = power;
            charge_to_soc_point = soc_wh;
        }

        public void discharge(double power)
        {
            charge_power = power * -1;
            charge_to_soc_point = -1.0f;
        }

        public void dischargeToSOC(double power, double soc_pct)
        {
            dischargeToSOC_wh(power, capacity_Wh * soc_pct / 100);
        }
        private void dischargeToSOC_wh(double power, double soc_wh)
        {
            charge_to_soc_point = soc_wh;
            charge_power = power * -1;
        }

        public void stopCharge()
        {
            charge_power = 0;
            charge_to_soc_point = -1.0d;
        }

        public void stopDischarge()
        {
            stopCharge();
        }

        public Battery(double capacity)
        {
            this.capacity_Wh = capacity;
            state_of_charge_Wh = 0.0d;
            state_of_health_pct = 100.0d;

            charge_power = 0.0d;
            charge_to_soc_point = -1.0d;
        }

        public void onClockTick(object source, ElapsedEventArgs e)
        {
            double powerDiff = charge_power / 3600 * (((System.Timers.Timer)source).Interval / 1000);

            double newSoC = state_of_charge_Wh + powerDiff;
            if (newSoC < capacity_Wh && newSoC > 0)
            {
                if (charge_to_soc_point > 0 &&
                        ((powerDiff > 0 && newSoC > charge_to_soc_point)
                        || (powerDiff < 0 && newSoC < charge_to_soc_point)))
                {
                    state_of_charge_Wh = charge_to_soc_point;
                    stopCharge();
                }
                else
                {
                    state_of_charge_Wh = newSoC;
                }
            }
            else if (newSoC <= 0)
            {
                state_of_charge_Wh = 0;
                stopCharge();

            }
            else
            {
                state_of_charge_Wh = capacity_Wh;
                stopCharge();

            }
            Console.WriteLine(ToString());

        }

        public override string ToString()
        {
            return "Battery: Cap(kWh): " + Math.Round(capacity_Wh / 1000, 2) 
                + " SOC(kWh): " + Math.Round(state_of_charge_Wh / 1000, 5) 
                + " Charge Power (kW):" + Math.Round(charge_power / 1000, 1) 
                + " Charge to SoC (%):" + (charge_to_soc_point < 0 ? -1 : Math.Round(charge_to_soc_point/capacity_Wh*100,0));
        }
    }
}
