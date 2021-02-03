using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class GasPoweredVehicle : Vehicle
    {
        private const string k_CurrentFuelQuantity = "Current Fuel Quantity";
        private readonly eFuelType r_FuelType;
        private readonly float r_MaximumFuelQuantity;
        private float m_CurrentFuelQuantity;

        protected GasPoweredVehicle(string i_VehicleLicenseNumber, eFuelType i_FuelType, float i_MaximumFuelQuantity)
            : base(i_VehicleLicenseNumber)
        {
            r_MaximumFuelQuantity = i_MaximumFuelQuantity;
            r_FuelType = i_FuelType;
        }

        protected float CurrentFuelQuantity
        {
            get
            {
                return m_CurrentFuelQuantity;
            }

            set
            {
                m_CurrentFuelQuantity = value;
            }
        }

        public float MaximumFuelQuantity
        {
            get
            {
                return r_MaximumFuelQuantity;
            }
        }

        public eFuelType FuelType
        {
            get
            {
                return r_FuelType;
            }
        }

        public float RemainingFuelQuantity()
        {
            return m_CurrentFuelQuantity;
        }

        public string GetFuelType()
        {
            int indexValue = 1;
            StringBuilder vehicleFuel = new StringBuilder();
            foreach (eFuelType fuelType in Enum.GetValues(typeof(eFuelType)))
            {
                vehicleFuel.Append(indexValue + "." + fuelType.ToString());
                vehicleFuel.AppendLine();
                indexValue++;
            }

            return vehicleFuel.ToString();
        }

        public void AddFuelToTheTank(float i_FuelQuantityToAdd)
        {
            if (m_CurrentFuelQuantity + i_FuelQuantityToAdd <= r_MaximumFuelQuantity && i_FuelQuantityToAdd >= 0)
            {
                m_CurrentFuelQuantity += i_FuelQuantityToAdd;
            }
            else
            {
                throw new ValueOutOfRangeException(0, r_MaximumFuelQuantity - m_CurrentFuelQuantity);
            }
        }

        public enum eFuelType
        {
            Octan98 = 1,
            Octan96 = 2,
            Octan95 = 3,
            Soler = 4
        }

        public enum eFuelTankLiterQuantity
        {
            MotorcycleTank = 7,
            CarTank = 60,
            TruckTank = 120
        }

        public override Dictionary<string, string> GetGeneralInfo()
        {
            Dictionary<string, string> vehicleInfo = base.GetGeneralInfo();
            vehicleInfo.Add(k_CurrentFuelQuantity, null);
            return vehicleInfo;
        }

        public override void SetGeneralInfo(string i_OutputMember, string i_UserInput)
        {
            if(string.IsNullOrEmpty(i_UserInput))
            {
                string msg = string.Format("No input was entered for '{0}'", i_OutputMember);
                throw new ArgumentException(msg);
            }

            switch(i_OutputMember)
            {
                case k_CurrentFuelQuantity:

                    float fuelQuantity;
                    if (!float.TryParse(i_UserInput, out fuelQuantity))
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be float",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }
                    else if(fuelQuantity < 0 || fuelQuantity > r_MaximumFuelQuantity)
                    {
                        throw new ValueOutOfRangeException(0, r_MaximumFuelQuantity);
                    }

                    m_CurrentFuelQuantity = fuelQuantity;
                    break;

                default:
                    base.SetGeneralInfo(i_OutputMember, i_UserInput);
                    break;
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();
            vehicleData.Append(base.ToString());
            string thisData = string.Format(
                @"
Current fuel quantity: {0}
Fuel Type: {1}",
                m_CurrentFuelQuantity.ToString(),
                r_FuelType.ToString());
            vehicleData.Append(thisData);
            return vehicleData.ToString();
        }
    }
}
