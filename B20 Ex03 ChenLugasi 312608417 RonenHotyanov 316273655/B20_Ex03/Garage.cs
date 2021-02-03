using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleInTheGarage> r_GarageVehicles;
        private eVehicleType m_VehicleType;

        public Garage()
        {
            r_GarageVehicles = new Dictionary<string, VehicleInTheGarage>();
        }

        public string VehicleType
        {
            set
            {
                int userDecision;
                checkValidType(value, out userDecision);
                m_VehicleType = (eVehicleType)userDecision;
            }
        }

        public Dictionary<string, VehicleInTheGarage> GarageVehicles
        {
            get
            {
                return r_GarageVehicles;
            }
        }

        // an Indexer
        public VehicleInTheGarage this[string i_Str]
        {
            get
            {
                VehicleInTheGarage vehicle;
                r_GarageVehicles.TryGetValue(i_Str, out vehicle);
                return vehicle;
            }
        }

        public string DisplayVehicleData(VehicleInTheGarage i_VehicleToDisplay)
        {
            StringBuilder vehicleToDisplay = new StringBuilder();
            vehicleToDisplay.Append(i_VehicleToDisplay.ToString());
            vehicleToDisplay.Append(i_VehicleToDisplay.Vehicle.ToString());
            return vehicleToDisplay.ToString();
        }

        public void maximizeWheelAirVolume(VehicleInTheGarage i_VehicleToMaximizeVolume)
        {
            foreach (Wheel vehicleWheel in i_VehicleToMaximizeVolume.Vehicle.WheelsCollection)
            {
                float amountOfAirToInflate = vehicleWheel.MaximumAirPressure - vehicleWheel.CurrentAirPressure;
                vehicleWheel.InflatingWheel(amountOfAirToInflate);
            }
        }

        public string CheckGasPoweredVehicle(VehicleInTheGarage i_VehicleToFuel)
        {
            if(!(i_VehicleToFuel.Vehicle is GasPoweredVehicle))
            {
                string msg = "It is not gas powered vehicle!you can not fuel it.";
                throw new FormatException(msg);
            }

            return (i_VehicleToFuel.Vehicle as GasPoweredVehicle).GetFuelType();
        }

        private void checkUserChoiceFuelTypeValidity(string i_UserFuelTypeChoice, out int o_FuelTypeChoice)
        {
            int maxFuelTypeSelection = Enum.GetValues(typeof(GasPoweredVehicle.eFuelType)).Length;

            if(!int.TryParse(i_UserFuelTypeChoice, out o_FuelTypeChoice))
            {
                string message = "Invalid Input, please enter only integer";
                throw new FormatException(message);
            }
            else if(!isLegalFuelTypeChoice(o_FuelTypeChoice, maxFuelTypeSelection))
            {
                throw new ValueOutOfRangeException(1, maxFuelTypeSelection);
            }
        }

        public bool CheckUserFuelTypeChoice(string i_UserFuelTypeChoice, VehicleInTheGarage i_VehicleToFuel)
        {
            int fuelTypeChoice;
            GasPoweredVehicle typeOfVehicle = i_VehicleToFuel.Vehicle as GasPoweredVehicle;

            checkUserChoiceFuelTypeValidity(i_UserFuelTypeChoice, out fuelTypeChoice);
            return isCompatibleFuelType(fuelTypeChoice, typeOfVehicle);
        }

        private bool isLegalFuelTypeChoice(int i_FuelTypeChoice, int i_MaxFuelTypeSelection)
        {
            return i_FuelTypeChoice > 0 && i_FuelTypeChoice <= i_MaxFuelTypeSelection;
        }

        private bool isCompatibleFuelType(
            int i_UserFuelTypeChoice,
            GasPoweredVehicle i_TypeOfVehicle)
        {
            bool isCompatibleFuelType = i_UserFuelTypeChoice == (int)i_TypeOfVehicle.FuelType;

            if (!isCompatibleFuelType)
            {
                throw new ArgumentException("There is no match for the fuel type of the vehicle");
            }

            return isCompatibleFuelType; 
        }

       public void AddFuelToTheTank(VehicleInTheGarage i_VehicleToFuel, float i_FuelQuantityToAdd)
        {
            GasPoweredVehicle gasVehicle = i_VehicleToFuel.Vehicle as GasPoweredVehicle;
            
            gasVehicle.AddFuelToTheTank(i_FuelQuantityToAdd);
        }

       public bool CheckElectricVehicle(VehicleInTheGarage i_VehicleToCharge)
       {
         return i_VehicleToCharge.Vehicle is ElectricVehicle;
       }

       public void LoadingBattery(VehicleInTheGarage i_VehicleToCharge, float i_TimeToCharge)
        {
            ElectricVehicle typeOfVehicle = i_VehicleToCharge.Vehicle as ElectricVehicle;
            if(typeOfVehicle != null)
            {
                typeOfVehicle.LoadingBattery(i_TimeToCharge);
            }
        }

        public bool AreThereAnyVehiclesInTheGarage()
        {
            return GarageVehicles.Count > 0;
        }

        public bool isAlreadyInTheGarage(string i_VehicleLicenseNumber)
        {
            return r_GarageVehicles.Count > 0 && r_GarageVehicles.ContainsKey(i_VehicleLicenseNumber);
        }

        public VehicleInTheGarage AddNewVehicle(string i_VehicleLicenseNumber, string i_OwnerName, string i_PhoneNumber)
        {
            VehicleInTheGarage vehicleConsumer = new VehicleInTheGarage(i_OwnerName, i_PhoneNumber);
            CreateNewVehicle newVehicle = new CreateNewVehicle(i_VehicleLicenseNumber, m_VehicleType, vehicleConsumer);
            r_GarageVehicles.Add(i_VehicleLicenseNumber, vehicleConsumer);
            return vehicleConsumer;
        }

        public Dictionary<string, string> GetVehicleGeneralInfo(VehicleInTheGarage i_Vehicle)
        {
            return i_Vehicle.Vehicle.GetGeneralInfo();
        }

        public void SetVehicleGeneralInfo(string i_OutputMember, string i_UserInput, VehicleInTheGarage i_Vehicle)
        {
            i_Vehicle.Vehicle.SetGeneralInfo(i_OutputMember, i_UserInput);
        }

        public Dictionary<string, string> GetVehicleSpecificInfo(VehicleInTheGarage i_Vehicle)
        {
            i_Vehicle.Vehicle.UpdateWheelsCollectionData();
            Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();
            vehicleInfo = i_Vehicle.Vehicle.GetSpecificInfo();
            return vehicleInfo;
        }

        public void SetVehicleSpecificInfo(string i_OutputMember, string i_UserInput, VehicleInTheGarage i_Vehicle)
        {
            i_Vehicle.Vehicle.SetSpecificInfo(i_OutputMember, i_UserInput);
        }

        public string DisplayVehiclesInRepair()
        {
            StringBuilder LicenseNumbersForVehicleInGarage = new StringBuilder();
            int indexValue = 1;
            foreach(VehicleInTheGarage vehicle in this.r_GarageVehicles.Values)
            {
                if(vehicle.VehicleStatus == VehicleInTheGarage.eVehicleStatus.InRepair)
                {
                    LicenseNumbersForVehicleInGarage.AppendLine(indexValue.ToString() + ". " + vehicle.Vehicle.LicenseNumber);
                    indexValue++;
                }
            }

            if(indexValue == 1)
            {
                LicenseNumbersForVehicleInGarage.AppendLine("There are no vehicles in Repair");
            }

            return LicenseNumbersForVehicleInGarage.ToString();
        }

        public string DisplayFixedVehicles()
        {
            int indexValue = 1;
            StringBuilder LicenseNumbersForVehicleInGarage = new StringBuilder();
            foreach(VehicleInTheGarage vehicle in this.r_GarageVehicles.Values)
            {
                if(vehicle.VehicleStatus == VehicleInTheGarage.eVehicleStatus.RepairIsDone)
                {
                    LicenseNumbersForVehicleInGarage.AppendLine(
                        indexValue.ToString() + ". " + vehicle.Vehicle.LicenseNumber);
                    indexValue++;
                }
            }

            if (indexValue == 1)
            {
                LicenseNumbersForVehicleInGarage.AppendLine("There are no repaired vehicles in the garage.");
            }

            return LicenseNumbersForVehicleInGarage.ToString();
        }

        public string DisplayPaidVehicles()
        {
            int indexValue = 1;
            StringBuilder LicenseNumbersForVehicleInGarage = new StringBuilder();
            foreach(VehicleInTheGarage vehicle in this.r_GarageVehicles.Values)
            {
                if(vehicle.VehicleStatus == VehicleInTheGarage.eVehicleStatus.PaidForAdjustment)
                {
                    LicenseNumbersForVehicleInGarage.AppendLine(indexValue + ". " + vehicle.Vehicle.LicenseNumber);
                    indexValue++;
                }
            }

            if (indexValue == 1)
            {
                LicenseNumbersForVehicleInGarage.AppendLine("There are no paid vehicles in the garage.");
            }

            return LicenseNumbersForVehicleInGarage.ToString();
        }

        public string DisplayAllVehicles()
        {
            int indexValue = 1;
            StringBuilder LicenseNumbersForVehicleInGarage = new StringBuilder();
            foreach(VehicleInTheGarage vehicle in this.r_GarageVehicles.Values)
            {
                LicenseNumbersForVehicleInGarage.AppendLine(indexValue + ". " + vehicle.Vehicle.LicenseNumber);
            }

            return LicenseNumbersForVehicleInGarage.ToString();
        }

        public enum eVehicleType
        {
            Truck = 1,
            ElectricMotorcycle = 2,
            GasMotorcycle = 3,
            ElectricCar = 4,
            GasCar = 5,
        }

        private void checkValidType(string i_UserTypeDecision, out int o_UserDecision)
        {
            if(!int.TryParse(i_UserTypeDecision, out o_UserDecision))
            {
                string msg = "The input not suitable in terms of type - should be an integer.";
                throw new FormatException(msg);
            }
            else if(!(o_UserDecision > 0 && o_UserDecision <= Enum.GetValues(typeof(eVehicleType)).Length))
            {
                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eVehicleType)).Length);
            }
        }

        public string GetVehicleType()
        {
            int indexValue = 1;
            StringBuilder vehicleType = new StringBuilder();
            foreach(eVehicleType type in Enum.GetValues(typeof(eVehicleType)))
            {
                vehicleType.AppendLine(indexValue + "." + type);
                indexValue++;
            }

            return vehicleType.ToString();
        }
    }
}
