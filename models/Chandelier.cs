using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightingDevice.models
{
    public class Chandelier : IlluminationDevice
    {
        private readonly Random random = new Random();
        private const double failureProbability = 0.3;
        private int currentMode = 0;
        public int CurrentMode => currentMode;

        public override void TurnOn()
        {
            if (random.NextDouble() > failureProbability)
            {
                currentMode = (currentMode % 3) + 1; 
                IsOn = true;
            } else
            {
                OnBroken();
            }
        }

        public override void TurnOff()
        {
            currentMode = (currentMode == 1) ? 0 : currentMode - 1;
            if (currentMode == 0)
                IsOn = false;
        }
    }
}
