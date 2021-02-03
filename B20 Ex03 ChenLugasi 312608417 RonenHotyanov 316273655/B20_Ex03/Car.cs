using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Car
    {
        private const string k_CarColor = "The color of the car";
        private const string k_CarDoors = "Number of doors in the car";
        private eColor m_Color;
        private eNumberOfDoors m_NumberOfDoors;

        internal enum eColor
        {
            Red = 1,
            White = 2,
            Black = 3,
            Silver = 4
        }

        internal enum eNumberOfDoors
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        internal eColor Color
        {
            get
            {
                return m_Color;
            }

            set
            {
                m_Color = value;
            }
        }

        internal eNumberOfDoors NumberOfDoors
        {
            get
            {
                return m_NumberOfDoors;
            }

            set
            {
                m_NumberOfDoors = value;
            }
        }

        public string GetCarColor()
        {
            int indexValue = 1;
            StringBuilder carcolor = new StringBuilder();
            foreach(eColor color in Enum.GetValues(typeof(eColor)))
            {
                carcolor.Append(indexValue + "." + color.ToString());
                carcolor.AppendLine();
                indexValue++;
            }

            return carcolor.ToString();
        }

        public string GetNumberOfDoors()
        {
            int indexValue = 1;
            StringBuilder carDoors = new StringBuilder();
            foreach(eNumberOfDoors door in Enum.GetValues(typeof(eNumberOfDoors)))
            {
                carDoors.Append(indexValue + "." + door.ToString());
                carDoors.AppendLine();
                indexValue++;
            }

            return carDoors.ToString();
        }

        public Dictionary<string, string> GetSpecificInfo()
        {
            Dictionary<string, string> vehicleInfo = new Dictionary<string, string>();

            vehicleInfo.Add(k_CarColor, GetCarColor());
            vehicleInfo.Add(k_CarDoors, GetNumberOfDoors());

            return vehicleInfo;
        }

        public void SetSpecificInfo(
            string i_OutputMember,
            string i_UserInput)
        {
            if (string.IsNullOrEmpty(i_OutputMember))
            {
                string msg = string.Format("No input was entered for '{0}'", i_OutputMember);
                throw new ArgumentException(msg);
            }

            int selectedInput;
            switch(i_OutputMember)
            {
                case k_CarColor:
                    if(!int.TryParse(i_UserInput, out selectedInput))
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be positive integer",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }

                    if(!(selectedInput > 0 && selectedInput <= Enum.GetValues(typeof(eColor)).Length))
                    {
                        throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eColor)).Length);
                    }

                    m_Color = (eColor)selectedInput;
                    break;

                case k_CarDoors:

                    if(!int.TryParse(i_UserInput, out selectedInput))
                    {
                        string msg = string.Format(
                            "The input for '{0}', Not suitable in terms of type - should be integer",
                            i_OutputMember);
                        throw new FormatException(msg);
                    }

                    if(!(selectedInput >= 1 && selectedInput <= Enum.GetValues(typeof(eNumberOfDoors)).Length))
                    {
                        throw new ValueOutOfRangeException(1, Enum.GetValues(typeof(eNumberOfDoors)).Length);
                    }

                    m_NumberOfDoors = (eNumberOfDoors)selectedInput;
                    break;
            }
        }

        public override string ToString()
        {
            string vehicleDetails = string.Format(
                @"
Color: {0}           
Number of doors: {1}",
                m_Color.ToString(),
                m_NumberOfDoors.ToString());
            return vehicleDetails;
        }
    }
}
