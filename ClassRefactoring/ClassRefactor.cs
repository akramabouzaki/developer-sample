using System;

namespace DeveloperSample.ClassRefactoring
{
    public enum SwallowType
    {
        African, European
    }

    public enum SwallowLoad
    {
        None, Coconut
    }

    public class SwallowFactory
    {
        public Swallow GetSwallow(SwallowType swallowType) => new Swallow(swallowType);
    }

    public class Swallow
    {
        public SwallowType Type { get; }
        public SwallowLoad Load { get; private set; }
        private int airspeedVelocity = 22;

        public Swallow(SwallowType swallowType)
        {
            Type = swallowType;
            Load = SwallowLoad.None;
            if (swallowType == SwallowType.European)
                airspeedVelocity = 20;
        }

        public void ApplyLoad(SwallowLoad load)
        {
            updateAirspeedVelocity(Load, load);
            Load = load;
        }

        private void updateAirspeedVelocity(SwallowLoad oldLoad, SwallowLoad newLoad)
        {
            if (oldLoad == SwallowLoad.None && newLoad == SwallowLoad.Coconut)
                airspeedVelocity = airspeedVelocity - 4;
            else if (oldLoad == SwallowLoad.Coconut && newLoad == SwallowLoad.None)
                airspeedVelocity = airspeedVelocity + 4;
        }

        public int GetAirspeedVelocity()
        {
            return airspeedVelocity;
        }
    }
}
