using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class GarageVehicles
    {
        private string m_VehicleOwnerName;
        private string m_VehicleOwnerPhoneNumber;
        private eVehicleStatement m_VehicleStatement = eVehicleStatement.InRepair;
        private Vehicles m_VehicleData;

        public enum eVehicleStatement
        {
            InRepair,
            RepairIsDone,
            PaidForAdjustment
        }

    }
}
