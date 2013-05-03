using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class LoadMarsWeatherXML : MonoBehaviour
{
	private bool itIsWindy = false;
	private bool itIsHazy = false;
	private bool itIsNight = false;
	
    IEnumerator Start()
    {
        //Load XML data from a URL
        string url = "http://cab.inta-csic.es/rems/rems_weather.xml";
        WWW www = new WWW(url);
		
		//Load the data and yield (wait) till it's ready before we continue executing the rest of this method.
        yield return www;
        if (www.error == null)
        {
            //Sucessfully loaded the XML
            Debug.Log("Loaded following XML " + www.text);

            //Create a new XML document out of the loaded data
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(www.text);

            //Point to the book nodes and process them
            ProcessMarsWeather(xmlDoc.SelectNodes("weather_report"));
        }
        else
        {
            Debug.Log("ERROR: " + www.error);
        }

    }

    //Converts an XmlNodeList into Book objects and shows a book out of it on the screen
    private void ProcessMarsWeather(XmlNodeList nodes)
    {
		MarsWeather MarsWeather;
		
        foreach (XmlNode node in nodes)
        {
            MarsWeather = new MarsWeather();
            //book.id = Convert.ToInt16(node.Attributes.GetNamedItem("id").Value);
            MarsWeather.title = node.SelectSingleNode("title").InnerText;
            MarsWeather.sol = node.SelectSingleNode("sol").InnerText;
			MarsWeather.terrestrial_date = node.SelectSingleNode("terrestrial_date").InnerText;
			MarsWeather.min_temp = node.SelectSingleNode("magnitudes").ChildNodes.Item(0).InnerText;
			MarsWeather.max_temp = node.SelectSingleNode("magnitudes").ChildNodes.Item(1).InnerText;
			MarsWeather.pressure = node.SelectSingleNode("magnitudes").ChildNodes.Item(2).InnerText;
			MarsWeather.pressure_string = node.SelectSingleNode("magnitudes").ChildNodes.Item(3).InnerText;
			MarsWeather.abs_humidity = node.SelectSingleNode("magnitudes").ChildNodes.Item(4).InnerText;
			MarsWeather.wind_speed = node.SelectSingleNode("magnitudes").ChildNodes.Item(5).InnerText;
			MarsWeather.wind_direction = node.SelectSingleNode("magnitudes").ChildNodes.Item(6).InnerText;
			MarsWeather.atmo_opacity = node.SelectSingleNode("magnitudes").ChildNodes.Item(7).InnerText;
			MarsWeather.season = node.SelectSingleNode("magnitudes").ChildNodes.Item(8).InnerText;
			MarsWeather.ls = node.SelectSingleNode("magnitudes").ChildNodes.Item(9).InnerText;
			MarsWeather.sunrise = node.SelectSingleNode("magnitudes").ChildNodes.Item(10).InnerText;
			MarsWeather.sunset = node.SelectSingleNode("magnitudes").ChildNodes.Item(11).InnerText;
    		
			
            //MarsWeather.magnitudes = new ArrayList();*/

            //Loop the magnitudes
            /*foreach (XmlNode magnitudes in node.SelectNodes("magnitudes"))
            {
                //book.authors.Add(author.InnerText);
				MarsWeather.magnitudes.Add(min_temp.InnerText);
				MarsWeather.min_temp
				//MarsWeather.magnitudes.min_temp = Convert.ToInt16(node.SelectSingleNode("min_temp").InnerText);
            }
			//Debug.Log(MarsWeather.magnitudes.min_temp.toString());*/
            //Update3dText(MarsWeather);
			SendWeatherData(MarsWeather);
        }
    }

    //Finds book object in application and send the Book as parameter.
    //Currently only works with two books
   /* private void Update3dText(MarsWeather MarsWeather)
    {
        GameObject Temp3DText = GameObject.Find("Temp");
		//(TextMesh)Temp3DText.guiText.text = "testing!";
        Temp3DText.SendMessage("UpdateText", MarsWeather.sunset.ToString());
    }*/
	
	private void SendWeatherData(MarsWeather MarsWeather1)
    {
        GameObject WeatherData = GameObject.Find("Scripts");
		//(TextMesh)Temp3DText.guiText.text = "testing!";
        //WeatherData.SendMessage("GetWeatherData", MarsWeather1);
		WeatherData.SendMessage("GetSol", MarsWeather1.sol.ToString());
		WeatherData.SendMessage("GetTerrestrial_date", MarsWeather1.terrestrial_date.ToString());
		WeatherData.SendMessage("GetMinTemp", MarsWeather1.min_temp.ToString());
		WeatherData.SendMessage("GetMaxTemp", MarsWeather1.max_temp.ToString());
		WeatherData.SendMessage("GetPressure", MarsWeather1.pressure.ToString());
		WeatherData.SendMessage("GetPressure_string", MarsWeather1.pressure_string.ToString());
		WeatherData.SendMessage("GetWind_speed", MarsWeather1.wind_speed.ToString());
		WeatherData.SendMessage("GetWind_direction", MarsWeather1.wind_direction.ToString());
		WeatherData.SendMessage("GetAtmo_opacity", MarsWeather1.atmo_opacity.ToString());
		WeatherData.SendMessage("GetSeason", MarsWeather1.season.ToString());
		WeatherData.SendMessage("GetLs", MarsWeather1.ls.ToString());
		WeatherData.SendMessage("GetSunrise", MarsWeather1.sunrise.ToString());
		WeatherData.SendMessage("GetSunset", MarsWeather1.sunset.ToString());
		
		//Debug.Log(MarsWeather1.sunrise.ToString() + "::EQUALS::" + System.DateTime.Now.Hour.ToString());
		
		//checking to make sure the values exist
		if(MarsWeather1.sunrise.ToString() != null && !MarsWeather1.sunset.ToString().Contains("--") && MarsWeather1.sunset.ToString() != null && !MarsWeather1.sunrise.ToString().Contains("--")){
			int SunriseValue = returnTimeValue(MarsWeather1.sunrise.ToString());
			int SunsetValue = returnTimeValue(MarsWeather1.sunset.ToString());
			
			//sun is up
			if(System.DateTime.UtcNow.Hour > SunriseValue && System.DateTime.UtcNow.Hour < SunsetValue && !itIsNight){
				GameObject Light1 = GameObject.Find("Directional light1");
				Light1.light.intensity = 0.62f;
				GameObject Light2 = GameObject.Find("Directional light2");
				Light2.light.intensity = 0.46f;
				GameObject Light3 = GameObject.Find("Directional light3");
				Light3.light.intensity = 0.69f;
			}
			//sun set or sun rise is going on == slightly darker scene
			else if((System.DateTime.UtcNow.Hour == SunriseValue || System.DateTime.UtcNow.Hour == SunsetValue) && !itIsNight){
				GameObject Light1 = GameObject.Find("Directional light1");
				Light1.light.intensity = 0.4f;
				GameObject Light2 = GameObject.Find("Directional light2");
				Light2.light.intensity = 0.4f;
				GameObject Light3 = GameObject.Find("Directional light3");
				Light3.light.intensity = 0.4f;
			}
			//darken scene if the sun is down
			else{
				GameObject Light1 = GameObject.Find("Directional light1");
				Light1.light.intensity = 0.2f;
				GameObject Light2 = GameObject.Find("Directional light2");
				Light2.light.intensity = 0.2f;
				GameObject Light3 = GameObject.Find("Directional light3");
				Light3.light.intensity = 0.2f;
				
				GameObject MoveScript = GameObject.Find("Scripts");
				MoveScript.SendMessage("isNightButtonOn", true);
			}
		}
		
		GameObject MarsWindDust = GameObject.Find("MarsDustStorm");
		//blow wind if the wind speed is up
		//kind of windy (dusty)
		if(MarsWeather1.wind_speed.ToString() != null && !MarsWeather1.wind_speed.ToString().Contains("--")){
			if(int.Parse(MarsWeather1.wind_speed.ToString()) > 8 && int.Parse(MarsWeather1.wind_speed.ToString()) < 25 || itIsWindy){
				MarsWindDust.particleEmitter.emit = true;
				MarsWindDust.particleEmitter.maxEnergy = 25;
				
				GameObject MoveScript = GameObject.Find("Scripts");
				MoveScript.SendMessage("isWindButtonOn", true);
			}
			//really windy (dusty)
			else if(int.Parse(MarsWeather1.wind_speed.ToString()) >= 20){
				MarsWindDust.particleEmitter.emit = true;
				MarsWindDust.particleEmitter.maxEnergy = 45;
				
				GameObject MoveScript = GameObject.Find("Scripts");
				MoveScript.SendMessage("isWindButtonOn", true);
			}
			//not windy (dusty)
			else{
				MarsWindDust.particleEmitter.emit = false;
				MarsWindDust.particleEmitter.maxEnergy = 3;
			}
		}
		//remove default tint the screen if it is "Sunny"
		//Debug.Log(MarsWeather1.atmo_opacity.ToString());
		if (MarsWeather1.atmo_opacity.ToString().Contains("Sunny") && !itIsHazy){
			GameObject GUIScript = GameObject.Find("Scripts");
			GUIScript.SendMessage("isItSunnyToggle", true);
		}
		else {
			
			GameObject GUIScript = GameObject.Find("Scripts");
			GUIScript.SendMessage("isItSunnyToggle", false);
			
			GameObject MoveScript = GameObject.Find("Scripts");
			MoveScript.SendMessage("isHazeButtonOn", true);
		}
	}
	
	int returnTimeValue(string timeString)
	{
		int timeInt;
		if(timeString.Length >= 5){
			//timeString = timeString.Remove(2);
			timeInt = int.Parse(timeString.Remove(2));
			if(timeString.Contains("pm")){
				timeInt = timeInt + 12;
			}
			Debug.Log("int!!! "+timeInt);
			}
		else{
			timeInt = int.Parse(timeString.Remove(1));
			if(timeString.Contains("pm")){
				timeInt = timeInt + 12;
			}
			Debug.Log("int!!! "+timeInt);
		}
		return timeInt;
	}
	
	public void toggleWind(bool windEnergy){
		GameObject MarsWindDust = GameObject.Find("MarsDustStorm");
		if(windEnergy){
			MarsWindDust.particleEmitter.emit = true;
			MarsWindDust.particleEmitter.maxEnergy = 45;
		}
		else{
			MarsWindDust.particleEmitter.emit = false;
			MarsWindDust.particleEmitter.maxEnergy = 3;
		}
		
		
		GameObject RoverMoveScript = GameObject.Find("Scripts");
		RoverMoveScript.SendMessage("toggleMoveScript", true);
	}
	public void toggleHaze(bool hazy){
		if(hazy){
			GameObject GUIScript = GameObject.Find("Scripts");
			GUIScript.SendMessage("isItSunnyToggle", false);
		}
		else{
			GameObject GUIScript = GameObject.Find("Scripts");
			GUIScript.SendMessage("isItSunnyToggle", true);
		}
		
		GameObject RoverMoveScript = GameObject.Find("Scripts");
		RoverMoveScript.SendMessage("toggleMoveScript", true);

	}
	public void toggleNight(bool night){
		if(night){
			GameObject Light1 = GameObject.Find("Directional light1");
			Light1.light.intensity = 0.2f;
			GameObject Light2 = GameObject.Find("Directional light2");
			Light2.light.intensity = 0.2f;
			GameObject Light3 = GameObject.Find("Directional light3");
			Light3.light.intensity = 0.2f;
		}
		else{
			GameObject Light1 = GameObject.Find("Directional light1");
			Light1.light.intensity = 0.62f;
			GameObject Light2 = GameObject.Find("Directional light2");
			Light2.light.intensity = 0.46f;
			GameObject Light3 = GameObject.Find("Directional light3");
			Light3.light.intensity = 0.69f;
		}
		
		GameObject RoverMoveScript = GameObject.Find("Scripts");
		RoverMoveScript.SendMessage("toggleMoveScript", true);
	}
}
