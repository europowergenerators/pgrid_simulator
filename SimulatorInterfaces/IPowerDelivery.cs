using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IPowerDelivery
    {
        public bool isAvailable();

        public double pricePerkWh();
        public double getMaximumPower();


        public void deliverPower(double power);
        public void stopDelivery(); 
    }
}
