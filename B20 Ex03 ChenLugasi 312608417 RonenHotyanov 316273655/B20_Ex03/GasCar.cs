using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GasCar : GasPoweredVehicle
    {
        private const int k_NumberOfWheels = (int)Wheel.eNumberOfWheels.Car;
        private const float k_MaxWheelAirPressure = (float)Wheel.eMaximumAirPressure.Car;
        private const eFuelType k_FuelType = eFuelType.Octan96;
        private const float k_FuelTankLiterQuantity = (float)eFuelTankLiterQuantity.CarTank;
        private readonly Car r_CarData = new Car();

        public GasCar(string i_VehicleLicenseNumber)
            : base(i_VehicleLicenseNumber, k_FuelType, k_FuelTankLiterQuantity)
        {
        }

        public override Dictionary<string, string> GetSpecificInfo()
        {
            return r_CarData.GetSpecificInfo();
        }

        public override void SetSpecificInfo(string i_OutputMember, string i_UserInput)
        {
            r_CarData.SetSpecificInfo(i_OutputMember, i_UserInput);
        }

        protected override void InitializeVehicleWheels(List<Wheel> i_Wheels)
        {
            for (int i = 0; i < k_NumberOfWheels; i++)
            {
                i_Wheels.Add(new Wheel(k_MaxWheelAirPressure));
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();
            vehicleData.Append(base.ToString());
            vehicleData.Append(r_CarData.ToString());
            return vehicleData.ToString();
        }
    }
}
