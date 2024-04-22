using System;

namespace LightingDevice.models
{
    public class DeskLamp : IlluminationDevice
    {
        private readonly Random random = new Random();
        private const double failureProbability = 0.2; 
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
