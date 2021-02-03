namespace Ex03.GarageLogic
{
    public class CreateNewVehicle
    {
        public CreateNewVehicle(
            string i_VehicleLicenseNumber,
            Garage.eVehicleType i_VehicleType,
            VehicleInTheGarage i_NewVehicle)
        {
            switch(i_VehicleType)
            {
                case Garage.eVehicleType.Truck:
                    i_NewVehicle.Vehicle = new Truck(i_VehicleLicenseNumber);
                    break;
                case Garage.eVehicleType.ElectricCar:
                    i_NewVehicle.Vehicle = new ElectricCar(i_VehicleLicenseNumber);
                    break;
                case Garage.eVehicleType.GasCar:
                    i_NewVehicle.Vehicle = new GasCar(i_VehicleLicenseNumber);
                    break;
                case Garage.eVehicleType.ElectricMotorcycle:
                    i_NewVehicle.Vehicle = new ElectricMotorcycle(i_VehicleLicenseNumber);
                    break;
                case Garage.eVehicleType.GasMotorcycle:
                    i_NewVehicle.Vehicle = new GasMotorcycle(i_VehicleLicenseNumber);
                    break;
            }
        }
    }
}
