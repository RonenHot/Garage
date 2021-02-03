using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle
    {
        private const string k_LicenseType = "The license Type";
        private const string k_EngineCapacity = "Engine Capacity in CC";
        private int m_EngineCapacity /*InCC*/;
        private eLicenseType m_LicenseType;

        internal eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                m_LicenseType = value;
            }
        }

        internal int EngineCapacityInCc
        {
            get
            {
                return m_EngineCapacity;
            }

            set
            {
                m_EngineCapacity = value;
            }
        }

        internal enum eLicenseType
        {
            A,
            A1,
            AA,
            B
        }

        public static string GetLicenseType()
        {
            int indexValue = 1;
            StringBuilder license = new StringBuilder();
            foreach(eLicenseType door in Enum.GetValues(typeof(eLicenseType)))
            {
                license.Append(indexValue + "." + door.ToString());
                license.AppendLine();
                indexValue++;
            }

            return license.ToString();
        }

        public Dictionary<string, string> GetSpecificInfo()
        {
            Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();

            vehicleInfo.Add(k_LicenseType, GetLicenseType());
            vehicleInfo.Add(k_EngineCapacity, null);

            return vehicleInfo;
        }

        public void SetSpecificInfo(string i_OutputMember, string i_UserInput)
        {
            if(string.IsNullOrEmpty(i_UserInput))
            {
                string msg = string.Format("No input was entered for '{0}'", i_OutputMember);
                throw new ArgumentException(msg);
            }

            int selectedInput;
            switch(i_OutputMember)
            {
                case k_LicenseType:
                    if(!int.TryParse(i_UserInput, out selectedInput))
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be positive integer",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }

                    if(!(selectedInput > 0 && selectedInput <= Enum.GetValues(typeof(eLicenseType)).Length))
                    {
                        throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eLicenseType)).Length);
                    }

                    m_LicenseType = (eLicenseType)selectedInput;
                    break;

                case k_EngineCapacity:

                    if(!int.TryParse(i_UserInput, out selectedInput) || selectedInput < 0)
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be positive integer",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }

                    m_EngineCapacity = selectedInput;
                    break;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format(
                @"
Engine Capacity: {0}           
License Type: {1}",
                m_EngineCapacity.ToString(),
                m_LicenseType.ToString());
            return vehicleDetails;
        }
    }
}