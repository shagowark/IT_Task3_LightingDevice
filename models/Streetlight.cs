using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightingDevice.models
{
    public class Streetlight : IlluminationDevice
    {
        private readonly Random random = new Random();
        private const double failureProbability = 0.2; 

        public override void TurnOn()
        {
            if (random.NextDouble() > failureProbability)
                IsOn = true;
            else
                OnBroken();
        }

        public override void TurnOff()
        {
            IsOn = false;
        }
    }
}
