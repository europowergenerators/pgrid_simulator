using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatorInterfaces
{
    public interface IPowerStorage
    {
        public void chargeToSOC(double power, double soc_pct);
        public void charge(double power);

        public void stopCharge();
    }
}
