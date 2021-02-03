using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private const string k_Model = "Model name";
        private const string k_PercentageOfRemainingEnergy = "Percentage Of Remaining Energy";
        private readonly string r_VehicleLicenseNumber;
        private readonly List<Wheel> r_WheelsCollection;
        private string m_Model;
        private float m_PercentageOfRemainingEnergy;

        public Vehicle(string i_VehicleLicenseNumber)
        {
            r_VehicleLicenseNumber = i_VehicleLicenseNumber;
            m_Model = string.Empty;
            r_WheelsCollection = new List<Wheel>();
            InitializeVehicleWheels(r_WheelsCollection);
        }

        public string Model
        {
            get
            {
                return m_Model;
            }

            set
            {
                m_Model = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return r_VehicleLicenseNumber;
            }
        }

        public float PercentageOfRemainingEnergy
        {
            get
            {
                return m_PercentageOfRemainingEnergy;
            }

            set
            {
                m_PercentageOfRemainingEnergy = value;
            }
        }

        public List<Wheel> WheelsCollection
        {
            get
            {
                return r_WheelsCollection;
            }
        }

        protected virtual void InitializeVehicleWheels(List<Wheel> i_Wheels)
        {
        }

        public virtual Dictionary<string, string> GetSpecificInfo()
        {
            return null;
        }
     
        public virtual void SetSpecificInfo(string i_OutputMember, string i_UserInput)
        {
        }

        public virtual Dictionary<string, string> GetGeneralInfo()
        {
            Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();
            vehicleInfo.Add(k_Model, null);
            vehicleInfo.Add(k_PercentageOfRemainingEnergy, null);
            r_WheelsCollection[0].GetGeneralInfo(vehicleInfo);
            return vehicleInfo;
        }

        public virtual void SetGeneralInfo(string i_OutputMember, string i_UserInput)
        {
            switch (i_OutputMember)
            {
                case k_Model:
                    m_Model = i_UserInput;
                    break;
                case k_PercentageOfRemainingEnergy:
                    float userInput;
                    if (!float.TryParse(i_UserInput, out userInput))
                    {
                        string msg = string.Format(
                            "The input for '{0}', not suitable in terms of type - should be float.",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }
                    else if (userInput < 0 || userInput > 100)
                    {
                        throw new ValueOutOfRangeException(0, 100);
                    }

                    m_PercentageOfRemainingEnergy = userInput;
                    break;
                default:
                    r_WheelsCollection[0].SetGeneralInfo(i_OutputMember, i_UserInput);
                    break;
            }
        }

        public void UpdateWheelsCollectionData()
        {
            for(int i = 1; i < r_WheelsCollection.Count; i++)
            {
                r_WheelsCollection[i] = r_WheelsCollection[0];
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();
            string thisData = string.Format(
                @"
Model: {0}           
Percentage of remaining energy: %{1}",
                m_Model,
                m_PercentageOfRemainingEnergy.ToString());
            vehicleData.Append(thisData);
            vehicleData.Append(r_WheelsCollection[0].ToString());
            return vehicleData.ToString();
        }
    }
}