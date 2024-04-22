using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightingDevice.models
{
    class Torchere : IlluminationDevice
    {
        private readonly Random random = new Random();
        private const double failureProbability = 0.3;
        private bool isConnectedToPower = false;

        public void ConnectToPower()
        {
            isConnectedToPower = true;
        }

        public override void TurnOn()
        {
            if (isConnectedToPower)
            {
                if (random.NextDouble() > failureProbability)
                {
                    IsOn = true;
                }
                else
                {
                    OnBroken();
                }
            }
        }

        public override void TurnOff()
        {
            IsOn = false;
        }
    }
}
