using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
   public class Wheel
    {
        private const string k_ManufacturerName = "Manufacturer name of the wheels";
        private const string k_CurrentAirPressure = "Wheels current air pressure";
        private readonly float r_MaximumAirPressure;
        private float m_CurrentAirPressure;
        private string m_ManufacturerName;

        public Wheel(float i_MaximumAirPressure)
        {
            r_MaximumAirPressure = i_MaximumAirPressure;
        }

        public void InflatingWheel(float i_AmountOfAirToInflate)
        {
            if(m_CurrentAirPressure + i_AmountOfAirToInflate <= r_MaximumAirPressure && i_AmountOfAirToInflate >= 0)
            {
                m_CurrentAirPressure += i_AmountOfAirToInflate;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaximumAirPressure);
            }
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }

            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get
            {
                return m_CurrentAirPressure;
            }

            set
            {
                m_CurrentAirPressure = value;
            }
        }

        public float MaximumAirPressure
        {
            get
            {
                return r_MaximumAirPressure;
            }
        }

        public enum eMaximumAirPressure
        {
            Motorcycle = 30,
            Car = 32,
            Truck = 28
        }

        public enum eNumberOfWheels
        {
            Motorcycle = 2,
            Car = 4,
            Truck = 16
        }

        public void GetGeneralInfo(Dictionary<string, string> i_VehicleInfo)
        {
            i_VehicleInfo.Add(k_ManufacturerName, null);
            i_VehicleInfo.Add(k_CurrentAirPressure, null);
        }

        public void SetGeneralInfo(string i_OutputMember, string i_UserInput)
        {
            switch(i_OutputMember)
            {
                case k_ManufacturerName:
                    ManufacturerName = i_UserInput;
                    break;
                case k_CurrentAirPressure:
                    if(!float.TryParse(i_UserInput, out m_CurrentAirPressure))
                    {
                        string msg = string.Format(
                            "The input for '{0}', not suitable in terms of type - should be float",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }
                    else if(m_CurrentAirPressure > r_MaximumAirPressure || m_CurrentAirPressure < 0)
                    {
                        throw new ValueOutOfRangeException(0, r_MaximumAirPressure);
                    }

                    break;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format(
                @"
Wheels manufacturer name: {0}
Current air pressure: {1}",
                m_ManufacturerName,
                m_CurrentAirPressure.ToString());
            return vehicleDetails;
        }
    }
}