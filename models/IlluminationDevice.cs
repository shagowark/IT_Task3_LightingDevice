using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightingDevice.models
{
    public abstract class IlluminationDevice
    {
        public bool IsOn { get; protected set; }

        public event EventHandler Broken;

        protected virtual void OnBroken()
        {
            Broken?.Invoke(this, EventArgs.Empty);
            IsOn = false;
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
    }
}
