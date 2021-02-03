using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : GasPoweredVehicle
    {
        private const string k_TransportsHazardousMaterials = "Is the truck carrying dangerous materials?";
        private const string k_CargoCapacity = "Cargo Capacity";
        private const int k_NumberOfWheels = (int)Wheel.eNumberOfWheels.Truck;
        private const float k_MaxWheelAirPressure = (float)Wheel.eMaximumAirPressure.Truck;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_FuelTankLiterQuantity = (float)eFuelTankLiterQuantity.TruckTank;
        private bool m_TransportsHazardousMaterials;
        private float m_CargoCapacity;

        public Truck(string i_VehicleLicenseNumber)
            : base(i_VehicleLicenseNumber, k_FuelType, k_FuelTankLiterQuantity)
        {
        }

        public enum eHazardousMaterials
        {
            isHazardous = 1,
            isNotHazardous = 2
        }

        public bool TransportsHazardousMaterials
        {
            get
            {
                return m_TransportsHazardousMaterials;
            }

            set
            {
                m_TransportsHazardousMaterials = value;
            }
        }

        public float CargoCapacity
        {
            get
            {
                return m_CargoCapacity;
            }

            set
            {
                m_CargoCapacity = value;
            }
        }

        protected override void InitializeVehicleWheels(List<Wheel> i_Wheels)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                i_Wheels.Add(new Wheel(k_MaxWheelAirPressure));
            }
        }

        public override Dictionary<string, string> GetSpecificInfo()
        {
            Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();
            vehicleInfo.Add(
                k_TransportsHazardousMaterials,
                @"1.Yes 2.No");
            vehicleInfo.Add(k_CargoCapacity, null);

            return vehicleInfo;
        }

        public override void SetSpecificInfo(string i_OutputMember, string i_UserInput)
        {
            if (string.IsNullOrEmpty(i_UserInput))
            {
                string msg = string.Format("No input was entered for '{0}'", i_OutputMember);
                throw new ArgumentException(msg);
            }

            switch (i_OutputMember)
            {
                case k_TransportsHazardousMaterials:
                    int hazardousMaterialsInput;

                    if (!int.TryParse(i_UserInput, out hazardousMaterialsInput))
                    {
                        string msg = string.Format(
                            "The input for '{0}', not suitable in terms of type - should be digit",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }
                    else if (hazardousMaterialsInput != (int)eHazardousMaterials.isHazardous
                            && hazardousMaterialsInput != (int)eHazardousMaterials.isNotHazardous)
                    {
                        throw new ValueOutOfRangeException(1, 2);
                    }

                    m_TransportsHazardousMaterials = hazardousMaterialsInput == (int)eHazardousMaterials.isHazardous;
                
                    break;

                case k_CargoCapacity:
                    float cargoCapicity;

                    if (!float.TryParse(i_UserInput, out cargoCapicity) || cargoCapicity < 0)
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be positive float",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }

                    m_CargoCapacity = cargoCapicity;
                    break;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format(
                @"
Cargo capacity: {0}           
Is Transports hazardous materials: {1}",
                m_CargoCapacity.ToString(),
                m_TransportsHazardousMaterials.ToString());
            return vehicleDetails;
        }
    }
}