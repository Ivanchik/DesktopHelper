using System;
using System.Xml;

namespace DesktopHelper
{
    public class WeatherTemplate
    {

        #region Warning. Singleton

        private static WeatherTemplate _instance;

        private WeatherTemplate()
        {
        }

        private static void SetNewLocation(string location)
        {
            XmlDocument xmlConditions = new XmlDocument();
            xmlConditions.Load(string.Format("http://api.openweathermap.org/data/2.5/weather?q={0}&mode=xml", location));

            //temperature
            XmlNode selectSingleNode = xmlConditions.SelectSingleNode("/current/temperature");
            if (selectSingleNode != null)
                if (selectSingleNode.Attributes != null)
                    _instance.TempC = selectSingleNode.Attributes["value"].InnerText;

            selectSingleNode = xmlConditions.SelectSingleNode("/current/humidity");
            if (selectSingleNode != null)
                if (selectSingleNode.Attributes != null)
                    _instance.Humidity = selectSingleNode.Attributes["value"].InnerText;
        }

        public static WeatherTemplate GetInstance(string location)
        {
            if (_instance != null) return _instance;
            if (_instance != null && _instance.City != location)
            {
                SetNewLocation(location);
                return _instance;
            }

            _instance = new WeatherTemplate();
            SetNewLocation(location);
           

            return _instance;
        }
        
        #endregion

       


            string _city = "No Data";
            string _dayOfWeek = DateTime.Now.DayOfWeek.ToString();
            string _condition = "No Data";
            string _tempF = "No Data";
            string _tempC = "No Data";
            string _humidity = "No Data";
            string _wind = "No Data";
            string _high = "No Data";
            string _low = "No Data";

            public string City
            {
                get { return _city; }
                set { _city = value; }
            }

            public string Condition
            {
                get { return _condition; }
                set { _condition = value; }
            }

            public string TempF
            {
                get { return _tempF; }
                set { _tempF = value; }
            }

            public string TempC
            {
                get { return _tempC; }
                set { _tempC = value; }
            }

            public string Humidity
            {
                get { return _humidity; }
                set { _humidity = value; }
            }

            public string Wind
            {
                get { return _wind; }
                set { _wind = value; }
            }

            public string DayOfWeek
            {
                get { return _dayOfWeek; }
                set { _dayOfWeek = value; }
            }

            public string High
            {
                get { return _high; }
                set { _high = value; }
            }

            public string Low
            {
                get { return _low; }
                set { _low = value; }
            }
        


    }
}
