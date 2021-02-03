using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class ElectricVehicle : Vehicle
    {
        private const string k_CurrentTimeLeftOfBatteryInHours = "Current Time Left Of Battery In Hours";
        private readonly float r_MaximumTimeOfBatteryInHours;
        private float m_CurrentTimeLeftOfBatteryInHours;

        protected ElectricVehicle(string i_VehicleLicenseNumber, float i_MaximumTimeOfBatteryInHours)
            : base(i_VehicleLicenseNumber)
        {
            r_MaximumTimeOfBatteryInHours = i_MaximumTimeOfBatteryInHours;
        }

        public float CurrentTimeLeftOfBatteryInHours
        {
            get
            {
                return m_CurrentTimeLeftOfBatteryInHours;
            }

            set
            {
                m_CurrentTimeLeftOfBatteryInHours = value;
            }
        }

        public float MaximumTimeOfBatteryInHours
        {
            get
            {
                return r_MaximumTimeOfBatteryInHours;
            }
        }

        public float RemainingTimeOfBattery()
        {
            return m_CurrentTimeLeftOfBatteryInHours;
        }

        public void LoadingBattery(float i_BatteryTimeToAdd)
        {
            if((m_CurrentTimeLeftOfBatteryInHours + i_BatteryTimeToAdd <= r_MaximumTimeOfBatteryInHours) && i_BatteryTimeToAdd >= 0)
            {
                m_CurrentTimeLeftOfBatteryInHours += i_BatteryTimeToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(
                    0,
                    r_MaximumTimeOfBatteryInHours - m_CurrentTimeLeftOfBatteryInHours);
            }
        }

        public override Dictionary<string, string> GetGeneralInfo()
        {
            Dictionary<string, string> vehicleInfo = base.GetGeneralInfo();
            vehicleInfo.Add(k_CurrentTimeLeftOfBatteryInHours, null);
            return vehicleInfo;
        }

        public override void SetGeneralInfo(
            string i_OutputMember,
            string i_UserInput)
        {
            if(string.IsNullOrEmpty(i_UserInput))
            {
                string msg = string.Format("No input was entered for '{0}'", i_OutputMember);
                throw new ArgumentException(msg);
            }

            switch (i_OutputMember)
            {
                case k_CurrentTimeLeftOfBatteryInHours:
                    float timeLeftOfBattey;
                    if (!float.TryParse(i_UserInput, out timeLeftOfBattey))
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be float",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }
                    else if(timeLeftOfBattey > r_MaximumTimeOfBatteryInHours
                            || timeLeftOfBattey < 0)
                    {
                        throw new ValueOutOfRangeException(0, r_MaximumTimeOfBatteryInHours);
                    }

                    m_CurrentTimeLeftOfBatteryInHours = timeLeftOfBattey;
                    break;
                default:
                    base.SetGeneralInfo(i_OutputMember, i_UserInput);
                    break;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = base.ToString();
            string thisData = string.Format(
                @"
Current time left of battery in hours: {0}",
                m_CurrentTimeLeftOfBatteryInHours.ToString());
            return vehicleDetails + thisData;
        }
    }
}
