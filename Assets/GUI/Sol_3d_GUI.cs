using UnityEngine;
using System.Collections;

public class Sol_3d_GUI : MonoBehaviour
{
	
	private bool availability;
	
    public GUISkin thisOrangeGUISkin;
	public Texture2D windButtonIcon;
	public Texture2D atmosphereButtonIcon;
	public Texture2D nightTimeButtonIcon;
	public Texture2D windButtonIcon_Off;
	public Texture2D atmosphereButtonIcon_Off;
	public Texture2D nightTimeButtonIcon_Off;
	
	
	private bool windOn = false;
	private bool atmosphereOn = false;
	private bool nightTime = false;
	private bool roverCanMove = true;
	
	private string ActiveGameObjectName = "FrameMarker0";//set Mar planet as 1st scene for dollar bill
    private Rect rctWindow1;
    private Rect rctWindow2;
    private Rect rctWindow3;
    private Rect rctWindow4;
    private GUIStyle subtitle = new GUIStyle();
	private GUIStyle redHeader = new GUIStyle();
	private GUIStyle redBody = new GUIStyle();
	private GUIStyle whiteBody = new GUIStyle();
	private GUIStyle blackSmall = new GUIStyle();
	//private Vector2 scrollPosition = Vector2.zero;
	private GUIStyle transparentButton = new GUIStyle();
	private bool showMenu = false;
	private bool showStartScreen = true;
	private bool showIconButtons = false;
	private bool showSubMenu = false;
	private bool showSceneMenu = false;
	private bool showSceneMenuIcon = false;
	private string SubMenuTitleText = "";
	private string SubMenuBodyText = "";
	private int w = 320;
	private int h = 480;
	public string dollarBillScenesLocal;
	
	private string sol_l = "no data";
	private string terrestrial_date_l = "no data"; 
	private string minTemp_l = "no data"; 
	private string maxTemp_l = "no data"; 
	private string pressure_l = "no data"; 
	private string pressure_string_l = "no data"; 
	private string windSpeed_l = "no data"; 
	private string wind_direction_l = "no data"; 
	private string atmo_opacity_l = "no data"; 
	private string season_l = "no data"; 
	private string ls_l = "no data";
	private string sunrise_l = "no data"; 
	private string sunset_l = "no data";
	
	Vector3 scale = new Vector3((float)Screen.width / 320.0f, 
                            (float)Screen.height / 480.0f, 
                            1.0f);
	
	Vector3 iPad3Scale = new Vector3((float)Screen.width / 640.0f, 
                            (float)Screen.height / 930.0f, 
                            1.0f);
	
	
    void Awake()
    {

		//twitterPlugin
		availability = TwitterPlugin.isAvailable;

		rctWindow1 = new Rect(0, 1648, Screen.width, 399);
		rctWindow2 = new Rect(685, 1905, 400, 400);
		rctWindow3 = new Rect(915, 1905, 400, 400);
		rctWindow4 = new Rect(1225, 1905, 400, 400);
		
       /* if (Screen.height==960)
		{
			
			rctWindow1 = new Rect((Screen.width-(w*2))/2, (Screen.height-(h*2))/2, w, h);
		}
		else if(Screen.height == 2048){
			//rctWindow1 = new Rect(0, 0, w, h);
			rctWindow1 = new Rect(((Screen.width-(w*4))/2)+40, ((Screen.height-(h*4))/2)+165, w, h);
			//rctWindow1 = new Rect((Screen.width-(w*4.35F))/2, (Screen.height-(h*4.22F))/2, w, h);
		}
		else{
			rctWindow1 = new Rect((Screen.width-w)/2, (Screen.height-h)/2, w, h);
		}*/
		
    }
    void OnGUI()
    {
		/*if(!roverCanMove && showIconButtons){
			//let touches move the rover if the marker is found
			GameObject RoverMoveScript = GameObject.Find("Scripts");
			RoverMoveScript.SendMessage("toggleMoveScript", true);
		}*/

		/*if (Screen.height==960)
		{
			GUI.matrix = Matrix4x4.Scale(scale);
		}
		else if(Screen.height == 2048)
		{
			GUI.matrix = Matrix4x4.Scale(iPad3Scale);
		}*/
		
		GUI.skin = thisOrangeGUISkin;
		
		GUI.skin = thisOrangeGUISkin;

		if(showIconButtons){
			//wind alert/toggle
			if(windOn){
				if (GUI.Button (new Rect (20,20, 100, 100), windButtonIcon, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);
					roverCanMove = false;

					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleWind", false);
					windOn = false;
					
					
				}
			}
			else{
				if (GUI.Button (new Rect (20,20, 100, 100), windButtonIcon_Off, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);
					
					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleWind", true);
					windOn = true;
					
					roverCanMove = false;
				}
			}
			//atmosphere haze alert/toggle
			if(atmosphereOn){
				if (GUI.Button (new Rect (20,160, 100, 100), atmosphereButtonIcon, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);

					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleHaze", false);
					atmosphereOn = false;
				}
			}
			else{
				if (GUI.Button (new Rect (20,160, 100, 100), atmosphereButtonIcon_Off, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);

					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleHaze", true);
					atmosphereOn = true;
				}
			}
			//day/night alert/toggle
			if(nightTime){
				if (GUI.Button (new Rect (20,300, 100, 100), nightTimeButtonIcon, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);

					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleNight", false);
					nightTime = false;
				}
			}
			else{
				if (GUI.Button (new Rect (20,300, 100, 100), nightTimeButtonIcon_Off, transparentButton)) {
					//don't let touches move the rover
					GameObject RoverMoveScript = GameObject.Find("Scripts");
					RoverMoveScript.SendMessage("toggleMoveScript", false);

					nightTime = true;
					GameObject MoveScript = GameObject.Find("Scripts");
					MoveScript.SendMessage("toggleNight", true);
				}
			}
			
		}

		rctWindow2 = GUI.Window(1, rctWindow2, SolGUIMethod, sol_l, GUI.skin.GetStyle("box"));
		rctWindow3 = GUI.Window(2, rctWindow3, SeasonGUIMethod, season_l, GUI.skin.GetStyle("box"));
		rctWindow4 = GUI.Window(3, rctWindow4, PressureGUIMethod, pressure_l, GUI.skin.GetStyle("box"));
		rctWindow1 = GUI.Window(4, rctWindow1, IntroScreen, "", GUI.skin.GetStyle("window"));

    }

    void SolGUIMethod (int windowID)
	{
	}
	void SeasonGUIMethod (int windowID)
	{
	}
	void PressureGUIMethod (int windowID)
	{
	}
    void IntroScreen(int windowID)
    {
		
        GUILayout.BeginHorizontal();
		GUILayout.Space(208);
		GUILayout.BeginVertical();
		GUILayout.Space(55);
		GUILayout.Label(maxTemp_l + "c/" + minTemp_l+"c");
		GUILayout.Label(terrestrial_date_l);
		GUILayout.Space(25);
		//tweet Screenshot
			if (GUILayout.Button("TWEET SCREENSHOT")){
				//deactivate the rover move script
				GameObject MoveScript = GameObject.Find("Scripts");
				MoveScript.SendMessage("toggleMoveScript", false);
				
				if (availability) {
			            TwitterPlugin.ComposeTweetWithScreenshot(" #SpaceApps", "http://spaceappschallenge.org/project/sol/");
			     
			    } else {
					
					showMenu = false;
					showIconButtons = false;
					SubMenuTitleText = "TWITTER ERROR";
					SubMenuBodyText = "Twitter API is not available.";
					showSubMenu = true;
			    }
			}
		//GUILayout.Label(System.DateTime.Today.Month.ToString() + ":" + System.DateTime.Today.Day.ToString() + ":" + System.DateTime.Today.Year.ToString());
		GUILayout.EndVertical();
		
		
		
		//GUILayout.Label(sol_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Date: " + terrestrial_date_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Min Temp: " + minTemp_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Max Temp: " + maxTemp_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Pressure: " + pressure_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Pressure: String " + pressure_string_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Wind Speed: " + windSpeed_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Wind Direction: " + wind_direction_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Atmosphere: " + atmo_opacity_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Season: " + season_l);
		//GUILayout.Space(3);
		//GUILayout.Label("LS: " + ls_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Sunrise: " + sunrise_l);
		//GUILayout.Space(3);
		//GUILayout.Label("Sunset: " + sunset_l);
		
		
		
		
        GUILayout.EndHorizontal();

    }
	
	
	
	public void GetWeatherData(string sol){// string sol, string terrestrial_date, string minTemp, string maxTemp, string pressure, string pressure_string, string windSpeed, string wind_direction, string atmo_opacity, string season, string ls, string sunrise, string sunset){
		//add scene name to keep track of which has been used with a dollar bill

		/*sol_l = MarsWeather1.sol.ToString();
		terrestrial_date_l = MarsWeather1.terrestrial_date.ToString();
		minTemp_l = MarsWeather1.min_temp.ToString();
		maxTemp_l = MarsWeather1.max_temp.ToString();
		pressure_l = MarsWeather1.pressure.ToString();
		pressure_string_l = MarsWeather1.pressure_string.ToString();
		windSpeed_l = MarsWeather1.wind_speed.ToString();
		wind_direction_l = MarsWeather1.wind_direction.ToString(); 
		atmo_opacity_l = MarsWeather1.atmo_opacity.ToString();
		season_l = MarsWeather1.season.ToString();
		ls_l = MarsWeather1.ls.ToString();
		sunrise_l = MarsWeather1.sunrise.ToString();
		sunset_l = MarsWeather1.sunset.ToString();
    
		sol_l = sol;
		terrestrial_date_l = terrestrial_date;
		minTemp_l = minTemp;
		maxTemp_l = maxTemp;
		pressure_l = pressure;
		pressure_string_l = pressure_string;
		windSpeed_l = windSpeed;
		wind_direction_l = wind_direction;
		atmo_opacity_l = atmo_opacity;
		season_l = season;
		ls_l = ls;
		sunrise_l = sunrise;
		sunset_l = sunset;*/
		
		sol_l = sol;
	}
	
	/*public void GetALLWeatherData(string sol){// string sol, string terrestrial_date, string minTemp, string maxTemp, string pressure, string pressure_string, string windSpeed, string wind_direction, string atmo_opacity, string season, string ls, string sunrise, string sunset){
		//add scene name to keep track of which has been used with a dollar bill
		sol_l = MarsWeather1.sol.ToString();
		terrestrial_date_l = MarsWeather1.terrestrial_date.ToString();
		minTemp_l = MarsWeather1.min_temp.ToString();
		maxTemp_l = MarsWeather1.max_temp.ToString();
		pressure_l = MarsWeather1.pressure.ToString();
		pressure_string_l = MarsWeather1.pressure_string.ToString();
		windSpeed_l = MarsWeather1.wind_speed.ToString();
		wind_direction_l = MarsWeather1.wind_direction.ToString(); 
		atmo_opacity_l = MarsWeather1.atmo_opacity.ToString();
		season_l = MarsWeather1.season.ToString();
		ls_l = MarsWeather1.ls.ToString();
		sunrise_l = MarsWeather1.sunrise.ToString();
		sunset_l = MarsWeather1.sunset.ToString();
    
		sol_l = sol;
		terrestrial_date_l = terrestrial_date;
		minTemp_l = minTemp;
		maxTemp_l = maxTemp;
		pressure_l = pressure;
		pressure_string_l = pressure_string;
		windSpeed_l = windSpeed;
		wind_direction_l = wind_direction;
		atmo_opacity_l = atmo_opacity;
		season_l = season;
		ls_l = ls;
		sunrise_l = sunrise;
		sunset_l = sunset;
	}*/
	public void GetSol(string sol){	
		sol_l = sol;
	}
	public void GetTerrestrial_date(string terrestrial_date){
		terrestrial_date_l = terrestrial_date;
	}
	public void GetMinTemp(string minTemp){	
		minTemp_l = minTemp;
	}
	public void GetMaxTemp(string maxTemp){	
		maxTemp_l = maxTemp;
	}
	public void GetPressure(string pressure){	
		pressure_l = pressure;
	}
	public void GetPressure_string(string pressure_string){	
		pressure_string_l = pressure_string;
	}
	public void GetWind_speed(string wind_speed){	
		windSpeed_l = wind_speed;
	}
	public void GetWind_direction(string wind_direction){	
		wind_direction_l = wind_direction;
	}
	public void GetAtmo_opacity(string atmo_opacity){	
		atmo_opacity_l = atmo_opacity;
	}
	public void GetSeason(string season){	
		season_l = season;
	}
	public void GetLs(string ls){	
		ls_l = ls;
	}
	public void GetSunrise(string sunrise){	
		sunrise_l = sunrise;
	}
	public void GetSunset(string sunset){	
		sunset_l = sunset;
	}
	
	public void toggleButtons(bool showButtons){
		 showIconButtons = showButtons;
	}
	
	public void isWindButtonOn(bool windOn_l){
		windOn = windOn_l;
	}
	
	public void isHazeButtonOn(bool hazeOn_l){
		atmosphereOn = hazeOn_l;
	}
	
	public void isNightButtonOn(bool nightOn_l){
		nightTime = nightOn_l;
	}
	
}
