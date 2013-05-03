using System;
using System.Collections;

class MarsWeather
{
    public string title;
    public string sol;
	public string terrestrial_date;
    //public ArrayList magnitudes;
    public string min_temp;
	public string max_temp;
	public string pressure;
	public string pressure_string;
	public string abs_humidity;
	public string wind_speed;
	public string wind_direction;
	public string atmo_opacity;
	public string season;
	public string ls;
	public string sunrise;
	public string sunset;

    override public string ToString()
    {
        /*string min_tempValue = "";
        if(magnitudes!=null)
        {
        	foreach(string min_temp in magnitudes)
	        {
            	min_tempValue += min_temp + ", ";
        	}
        }*/
        string returnString = "\r\nTitle: " + title;
        returnString += "\r\nSol: " + sol;
		returnString += "\r\nTerrestrial_Date: " + terrestrial_date;
        returnString += "\r\nMin_temp: " + min_temp;
		returnString += "\r\nMax_temp: " + max_temp;
		returnString += "\r\nPressure: " + pressure;
		returnString += "\r\nPressure_string: " + pressure_string;
		returnString += "\r\nAbs_humidity: " + abs_humidity;
		returnString += "\r\nWind_speed: " + wind_speed;
		returnString += "\r\nWind_direction: " + wind_direction;
		returnString += "\r\nAtmo_opacity: " + atmo_opacity;
		returnString += "\r\nSeason: " + season;
		returnString += "\r\nLs: " + ls;
		returnString += "\r\nSunrisep: " + sunrise;
		returnString += "\r\nSunset: " + sunset;
		
        return returnString;
    }
}