using System;
using System.Collections.Generic;
using System.Threading;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    internal class GarageManagement
    {
        private const int k_MaxSelectionMenu = 8;
        private readonly Garage r_VehicleManagement = new Garage();

        internal void Garage()
        {
            Console.WriteLine("Welcome to our garage.");
            Thread.Sleep(1000);
            displayGarageOptions();
        }

        private void displayGarageOptions()
        {
            bool isValidDecision = false;
            while(!isValidDecision)
            {
                try
                {
                    printMenu();
                    eUserDecision userDecision = checkUserDecision();
                    string vehicleLicenseNumber = string.Empty;
                    if(eUserDecision.DisplayListOfVehiclesInGarage != userDecision
                       && eUserDecision.Exit != userDecision)
                    {
                        vehicleLicenseNumber = receiveLicenseNumber();

                        while (string.IsNullOrEmpty(vehicleLicenseNumber))
                        {
                            Console.WriteLine("Invalid input, please try again:");
                            vehicleLicenseNumber = Console.ReadLine();
                        }
                    }

                    menuDecision(userDecision, vehicleLicenseNumber);
                    isValidDecision = true;
                }
                catch(FormatException)
                {
                    Console.WriteLine(
                        @"Invalid input:
you must select a section from the menu (only numbers).");
                }
                catch(ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                Thread.Sleep(1500);
            }
        }

        internal enum eUserDecision
        {
            InsertNewVehicle = 1,
            DisplayListOfVehiclesInGarage = 2,
            ChangeVehicleStatus = 3,
            MaximizeVolume = 4,
            FuelVehicle = 5,
            ChargeVehicle = 6,
            DisplayFullDataOfVehicle = 7,
            Exit = 8
        }

        private void printMenu()
        {
            Console.Clear();
            Console.WriteLine(
                @"Please choose one of the options below:
Menu:
-----
1. Inserting a new vehicle into the garage.
2. Display list of vehicle license number in the garage
3. Change the status of vehicle in the garage.
4. Maximize the volume of air in the wheels of a vehicle.
5. Fuel a vehicle whose type of engine is fuel.
6. Charge an electric vehicle
7. Display full data of vehicle by license number.
8. Exit. ");
        }

        private eUserDecision checkUserDecision()
        {
            int userDecision;
            if(!int.TryParse(Console.ReadLine(), out userDecision))
            {
                throw new FormatException();
            }
            else if(!isLegalMenuChoice(userDecision))
            {
                throw new ValueOutOfRangeException(1, k_MaxSelectionMenu);
            }

            return (eUserDecision)userDecision;
        }

        private bool isLegalMenuChoice(int i_Choice)
        {
            return i_Choice > 0 && i_Choice <= k_MaxSelectionMenu;
        }

        private void menuDecision(eUserDecision i_UserDecision, string i_VehicleLicenseNumber)
        {
            Console.Clear();
            switch(i_UserDecision)
            {
                case eUserDecision.InsertNewVehicle:
                    insertVehicle(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.DisplayListOfVehiclesInGarage:
                    displayListOfVehiclesInGarage();
                    break;
                case eUserDecision.ChangeVehicleStatus:
                    changeVehicleStatus(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.MaximizeVolume:
                    maximizeWheelAirVolume(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.FuelVehicle:
                    fuelVehicle(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.ChargeVehicle:
                    chargeVehicle(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.DisplayFullDataOfVehicle:
                    displayFullDataOfVehicle(i_VehicleLicenseNumber);
                    break;
                case eUserDecision.Exit:
                    Console.WriteLine("Goodbye, hope you enjoyed the garage service.");
                    break;
            }
        }

        private void insertVehicle(string i_VehicleLicenseNumber)
        {
            if(isTheLicenseNumberAlreadyExists(i_VehicleLicenseNumber))
            {
                VehicleInTheGarage newVehicle = r_VehicleManagement[i_VehicleLicenseNumber];
                newVehicle.changeStatusToInRepair();
                Console.WriteLine("The vehicle is in repair");
                Thread.Sleep(2000);
            }
            else
            {
                insertNewVehicle(i_VehicleLicenseNumber);
                Console.WriteLine("The vehicle entered the garage successfully");
                Thread.Sleep(2000);
            }

            displayGarageOptions();
        }

        private void insertNewVehicle(string i_VehicleLicenseNumber)
        {
            string ownerName;
            string phoneNumber;
            string userVehicleTypeDecision;
            bool isValidType = false;
            getConsumerDetails(out ownerName, out phoneNumber);
            while(!isValidType)
            {
                try
                {
                    Console.Clear();
                    string vehicleType = r_VehicleManagement.GetVehicleType();
                    Console.WriteLine("Please select the vehicle type:");
                    Console.WriteLine(vehicleType);
                    userVehicleTypeDecision = Console.ReadLine();
                    r_VehicleManagement.VehicleType = userVehicleTypeDecision;
                    isValidType = true;
                }
                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                    Thread.Sleep(1500);
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                    Thread.Sleep(1500);
                }
            }

            VehicleInTheGarage newVehicle = r_VehicleManagement.AddNewVehicle(
                i_VehicleLicenseNumber,
                ownerName,
                phoneNumber);

            insertNewVehicleInfo(newVehicle);
        }

        private void insertNewVehicleInfo(VehicleInTheGarage i_NewVehicle)
        {
            Dictionary<string, string> vehicleGeneralInfo = r_VehicleManagement.GetVehicleGeneralInfo(i_NewVehicle);
            getVehicleInfo(vehicleGeneralInfo, i_NewVehicle, eInfoType.GeneralInfo);

            Dictionary<string, string> vehicleSpecificInfo = r_VehicleManagement.GetVehicleSpecificInfo(i_NewVehicle);
            getVehicleInfo(vehicleSpecificInfo, i_NewVehicle, eInfoType.SpecificInfo);
        }

        internal enum eInfoType
        {
            GeneralInfo = 1,
            SpecificInfo = 2
        }

        private void getVehicleInfo(
            Dictionary<string, string> i_VehicleDetails,
            VehicleInTheGarage i_NewVehicle,
            eInfoType i_InfoType)
        {
            string userInput = string.Empty;
            string Description = string.Empty;
            Dictionary<string, string>.KeyCollection vehicleDetails = i_VehicleDetails.Keys;
            Console.Clear();
            Console.WriteLine("Please enter:");
            foreach(string vehicleDetail in vehicleDetails)
            {
                bool isValidDetail = false;
                Console.WriteLine(vehicleDetail);
                if(i_VehicleDetails.TryGetValue(vehicleDetail, out Description) && !string.IsNullOrEmpty(Description))
                {
                    Console.WriteLine(Description);
                }

                while(!isValidDetail)
                {
                    try
                    {
                        userInput = Console.ReadLine();
                        if(i_InfoType == eInfoType.GeneralInfo)
                        {
                            r_VehicleManagement.SetVehicleGeneralInfo(vehicleDetail, userInput, i_NewVehicle);
                        }
                        else
                        {
                            r_VehicleManagement.SetVehicleSpecificInfo(vehicleDetail, userInput, i_NewVehicle);
                        }

                        isValidDetail = true;
                    }
                    catch(FormatException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                    catch(ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.WriteLine(valueOutOfRangeException.Message);
                    }
                    catch(ArgumentException exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
            }
        }

        private void getConsumerDetails(out string o_OwnerName, out string o_PhoneNumber)
        {
            getConsumerName(out o_OwnerName);
            getConsumerPhone(out o_PhoneNumber);
        }

        private void getConsumerName(out string o_OwnerName)
        {
            Console.WriteLine("Please enter vehicle owner name:");
            o_OwnerName = Console.ReadLine();
            while (string.IsNullOrEmpty(o_OwnerName))
            {
                Console.WriteLine("Invalid input, please try again:");
                o_OwnerName = Console.ReadLine();
            }
        }

        private void getConsumerPhone(out string o_PhoneNumber)
        {
            Console.WriteLine("Please enter phone number:");
            o_PhoneNumber = Console.ReadLine();
            while (string.IsNullOrEmpty(o_PhoneNumber))
            {
                Console.WriteLine("Invalid input, please try again:");
                o_PhoneNumber = Console.ReadLine();
            }
        }

        private bool isTheLicenseNumberAlreadyExists(string i_VehicleLicenseNumber)
        {
            return r_VehicleManagement.isAlreadyInTheGarage(i_VehicleLicenseNumber);
        }

        private string receiveLicenseNumber()
        {
            Console.WriteLine("Please enter vehicle license number:");
            string vehicleLicenseNumber = Console.ReadLine();
            return vehicleLicenseNumber;
        }

        internal enum eDisplayConditionInGarage
        {
            VehiclesInRepair = 1,
            RepairedVehicles = 2,
            Paid = 3,
            DisplayAll = 4
        }

        private void displayListOfVehiclesInGarage()
        {
            if (r_VehicleManagement.AreThereAnyVehiclesInTheGarage())
            {
                bool isValidDecision = false;
                while(!isValidDecision)
                {
                    try
                    {
                        printOptionsInGarage();
                        eDisplayConditionInGarage userDecision = checkDisplayStatusDecision();
                        statusInGarageMenu(userDecision);
                        isValidDecision = true;
                    }
                    catch (ValueOutOfRangeException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Thread.Sleep(1500);
                    }
                }
            }
            else
            {
                Console.WriteLine("There are no vehicles in the garage.");
                Thread.Sleep(2000);
            }

            displayGarageOptions();
        }

        private void statusInGarageMenu(eDisplayConditionInGarage i_UserDecision)
        {
            Console.Clear();
            string displayLicenseNumbers = string.Empty;
            switch(i_UserDecision)
            {
                case eDisplayConditionInGarage.VehiclesInRepair:
                    displayLicenseNumbers = r_VehicleManagement.DisplayVehiclesInRepair();
                    break;
                case eDisplayConditionInGarage.RepairedVehicles:
                    displayLicenseNumbers = r_VehicleManagement.DisplayFixedVehicles();
                    break;
                case eDisplayConditionInGarage.Paid:
                    displayLicenseNumbers = r_VehicleManagement.DisplayPaidVehicles();
                    break;
                case eDisplayConditionInGarage.DisplayAll:
                    displayLicenseNumbers = r_VehicleManagement.DisplayAllVehicles();
                    break;
            }

            Console.WriteLine(displayLicenseNumbers);
            Thread.Sleep(2000);
        }

        private eDisplayConditionInGarage checkDisplayStatusDecision()
        {
            int userDecision;
            if(!int.TryParse(Console.ReadLine(), out userDecision))
            {
                throw new FormatException();
            }
            else if(!isLegalDisplayStatusChoice(userDecision))
            {
                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eDisplayConditionInGarage)).Length);
            }

            return (eDisplayConditionInGarage)userDecision;
        }

        private bool isLegalDisplayStatusChoice(int i_Choice)
        {
            return i_Choice > 0 && i_Choice <= Enum.GetValues(typeof(eDisplayConditionInGarage)).Length;
        }

        private void printOptionsInGarage()
        {
            Console.WriteLine(
                @"Please choose one of the options below:
Menu:
-----
1. Display vehicles in repair
2. Display vehicles that have already been repaired
3. Display paid vehicles
4. Display all vehicles");
        }

        private void changeVehicleStatus(string i_VehicleLicenseNumber)
        {
            VehicleInTheGarage changeStatusToVehicle = r_VehicleManagement[i_VehicleLicenseNumber];
             if (changeStatusToVehicle != null)
             {
                VehicleInTheGarage.eVehicleStatus userDecision;
                bool isValidDecision = false;

                while (!isValidDecision)
                {
                    try
                    {
                        printVehicleStatusMenu();
                        userDecision = checkStatusDecision();
                        changeStatusInGarageMenu(userDecision, changeStatusToVehicle);
                        isValidDecision = true;
                    }
                    catch (ValueOutOfRangeException valueOutOfRangeException)
                    {
                        Console.WriteLine(valueOutOfRangeException.Message);
                        Thread.Sleep(1500);
                    }
                }

                Console.WriteLine("Garage vehicle status changed successfully.");
            }
            else
            {
                Console.WriteLine("The vehicle does not in the garage.");
            }

            Thread.Sleep(1500);
            displayGarageOptions();
        }

        private void changeStatusInGarageMenu(
            VehicleInTheGarage.eVehicleStatus i_UserDecision,
            VehicleInTheGarage i_Vehicle)
        {
            Console.Clear();
            switch(i_UserDecision)
            {
                case VehicleInTheGarage.eVehicleStatus.InRepair:
                    i_Vehicle.changeStatusToInRepair();
                    break;
                case VehicleInTheGarage.eVehicleStatus.RepairIsDone:
                    i_Vehicle.changeStatusToRepairIsDone();
                    break;
                case VehicleInTheGarage.eVehicleStatus.PaidForAdjustment:
                    i_Vehicle.changeStatusToPaid();
                    break;
            }
        }

        private VehicleInTheGarage.eVehicleStatus checkStatusDecision()
        {
            int userDecision;

            if(!int.TryParse(Console.ReadLine(), out userDecision))
            {
                throw new FormatException();
            }
            else if(!isLegalStatusChoice(userDecision))
            {
                throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(VehicleInTheGarage.eVehicleStatus)).Length);
            }

            return (VehicleInTheGarage.eVehicleStatus)userDecision;
        }

        private bool isLegalStatusChoice(int i_Choice)
        {
            return i_Choice > 0 && i_Choice <= Enum.GetValues(typeof(VehicleInTheGarage.eVehicleStatus)).Length;
        }

        private void printVehicleStatusMenu()
        {
            Console.WriteLine(
                @"To change vehicle status in the garage,
please choose one of the options below:
-----
1. In repair
2. Was repaired
3. paid");
        }

        private void maximizeWheelAirVolume(string i_VehicleLicenseNumber)
        {
            VehicleInTheGarage vehicleToMaximizeVolume = r_VehicleManagement[i_VehicleLicenseNumber];
            if (vehicleToMaximizeVolume != null)
            {
                try
                {
                    r_VehicleManagement.maximizeWheelAirVolume(vehicleToMaximizeVolume);
                    Console.WriteLine("The wheels in the vehicle were inflated to the maximum.");
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            else
            {
                Console.WriteLine("The vehicle does not exists in the garage.");
            }
        
            Thread.Sleep(1500);
            displayGarageOptions();
        }

        private void fuelVehicle(string i_VehicleLicenseNumber)
        {
            VehicleInTheGarage vehicleToFuel = r_VehicleManagement[i_VehicleLicenseNumber];
            if(vehicleToFuel != null)
            {
                try
                {
                    string typeOfVehicle = r_VehicleManagement.CheckGasPoweredVehicle(vehicleToFuel);

                    Console.WriteLine(
                        string.Format(
                            @"Please choose the type of the fuel:
{0}",
                            typeOfVehicle));
                    chooseFuelType(vehicleToFuel);
                }
                catch (FormatException formatException)
                {
                    Console.WriteLine(formatException.Message);
                }
            }
            else
            {
                Console.WriteLine("The vehicle does not exists in the garage.");
            }

            Thread.Sleep(1500);
            displayGarageOptions();
        }

        private void chooseFuelType(VehicleInTheGarage i_VehicleToFuel)
        {
            bool isValidChoice = false;
            while (!isValidChoice)
            {
                try
                {
                    string fuelChoice = Console.ReadLine();

                    if (r_VehicleManagement.CheckUserFuelTypeChoice(fuelChoice, i_VehicleToFuel))
                    {
                        Console.WriteLine("Please enter the requested quantity to add:");
                        checkQuantityFuel(i_VehicleToFuel);
                        isValidChoice = true;
                    }
                }
                catch (ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch (ArgumentException exception)
                {
                    Console.WriteLine(exception.Message);
                }

                if (!isValidChoice)
                {
                    Console.WriteLine("Please choose again:");
                }
            }

            Console.WriteLine("Fueling successfully completed.");
        }

        private void checkQuantityFuel(VehicleInTheGarage i_VehicleToFuel)
        {
            float quantityOfFuelToAdd;

            bool isValidInput = false;
            while(!isValidInput)
            {
                try
                {
                    checkQuantityFuelValidation(out quantityOfFuelToAdd);
                    r_VehicleManagement.AddFuelToTheTank(i_VehicleToFuel, quantityOfFuelToAdd);
                    isValidInput = true;
                }
                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch(ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void checkQuantityFuelValidation(out float o_QuantityOfFuelToAdd)
        {
            if(!float.TryParse(Console.ReadLine(), out o_QuantityOfFuelToAdd))
            {
                string msg = "The input not suitable in terms of type - should be float";
                throw new FormatException(msg);
            }
        }

        private void chargeVehicle(string i_VehicleLicenseNumber)
        {
            VehicleInTheGarage vehicleToCharge = r_VehicleManagement[i_VehicleLicenseNumber];
            if (vehicleToCharge != null)
            {
                if (r_VehicleManagement.CheckElectricVehicle(vehicleToCharge))
                {
                    chargeElectricVehicle(vehicleToCharge);
                }
                else
                {
                    string msg = "It is not electric vehicle! you can not charge it.";
                    Console.WriteLine(msg);
                }
            }
            else
            {
                Console.WriteLine("The vehicle does not exists in the garage.");
            }

            Thread.Sleep(1500);
            displayGarageOptions();
        }

        private void chargeElectricVehicle(VehicleInTheGarage i_VehicleToCharge)
        {
            float timeToCharge;
            bool isValidInput = false;
            while(!isValidInput)
            {
                try
                {
                    Console.WriteLine("Please enter the time of charge you would like to load:");
                    checkTimeToChargeValidation(out timeToCharge);
                    r_VehicleManagement.LoadingBattery(i_VehicleToCharge, timeToCharge);
                    isValidInput = true;
                }
                catch(FormatException exception)
                {
                    Console.WriteLine(exception.Message);
                }
                catch(ValueOutOfRangeException exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }

            Console.WriteLine("Charging successfully completed.");
        }

        private void checkTimeToChargeValidation(out float o_TimeToCharge)
        {
            if (!float.TryParse(Console.ReadLine(), out o_TimeToCharge))
            {
                string msg = "The input not suitable in terms of type - should be float";
                throw new FormatException(msg);
            }
        }

        private void displayFullDataOfVehicle(string i_VehicleLicenseNumber)
        {
            VehicleInTheGarage vehicleToDisplay = r_VehicleManagement[i_VehicleLicenseNumber];
            if(vehicleToDisplay != null)
            {
                string vehicleDetails = r_VehicleManagement.DisplayVehicleData(vehicleToDisplay);
                Console.WriteLine("Vehicle Details:");
                Console.WriteLine(vehicleDetails);
            }
            else
            {
                Console.WriteLine("The vehicle does not exists in the garage.");
            }

            Thread.Sleep(1500);
            displayGarageOptions();
        }
    }
}