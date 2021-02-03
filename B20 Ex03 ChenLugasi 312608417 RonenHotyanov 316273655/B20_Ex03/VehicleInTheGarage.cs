namespace Ex03.GarageLogic
{
    public class VehicleInTheGarage
    {
        private string m_VehicleOwnerName;
        private string m_VehicleOwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus = eVehicleStatus.InRepair;
        private Vehicle m_Vehicle = null;

        public VehicleInTheGarage(string i_VehicleOwner, string i_PhoneNumber)
        {
            m_VehicleOwnerName = i_VehicleOwner;
            m_VehicleOwnerPhoneNumber = i_PhoneNumber;
        }

        public enum eVehicleStatus
        {
            InRepair,
            RepairIsDone,
            PaidForAdjustment
        }

        public string OwnerName
        {
            get
            {
                return m_VehicleOwnerName;
            }

            set
            {
                m_VehicleOwnerName = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return m_VehicleOwnerPhoneNumber;
            }

            set
            {
                m_VehicleOwnerPhoneNumber = value;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                m_VehicleStatus = value;
            }
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }

            set
            {
                m_Vehicle = value;
            }
        }

        public void changeStatusToInRepair()
        {
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public void changeStatusToPaid()
        {
            m_VehicleStatus = eVehicleStatus.PaidForAdjustment;
        }

        public void changeStatusToRepairIsDone()
        {
            m_VehicleStatus = eVehicleStatus.RepairIsDone;
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format(
                @"          
Owner name: {0}
Phone Number: {1}
Type of Vehicle: {2}
Vehicle status in the garage: {3}",
                m_VehicleOwnerName,
                m_VehicleOwnerPhoneNumber,
                m_Vehicle.GetType().Name.ToString(),
                m_VehicleStatus.ToString());

            return vehicleDetails;
        }
    }
}
