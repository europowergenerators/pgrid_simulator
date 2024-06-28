using SimulatorInterfaces;

namespace ACDeliverySimulatorLib
{
    public class ACDelivery : IPowerDelivery
    {
        public double maximum_power { get; set; }
        public double power { get; set; }

        public ACDelivery(double max_power)
        {
            maximum_power = max_power;
        }

        public void deliverPower(double power)
        {
            if (power < maximum_power)
            {
                this.power = power;
            }
            else
            {
                this.power = maximum_power;
            }
        }

        public void stopDelivery()
        {
            power = 0;
        }

        public override string ToString()
        {
            return "ACDelivery: (kWh) " + Math.Round(power / 1000, 2) + ", Max power: (kWh) " + Math.Round(maximum_power / 1000, 2);
        }

        public bool isAvailable()
        {
            return true;
        }

        public double pricePerkWh()
        {
            return 0.2;
        }

        public double getMaximumPower()
        {
            return maximum_power;
        }
    }
}
