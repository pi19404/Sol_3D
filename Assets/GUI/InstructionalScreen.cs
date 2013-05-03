/*==============================================================================
            Copyright (c) 2012 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary

This  Vuforia(TM) sample application in source code form ("Sample Code") for the
Vuforia Software Development Kit and/or Vuforia Extension for Unity
(collectively, the "Vuforia SDK") may in all cases only be used in conjunction
with use of the Vuforia SDK, and is subject in all respects to all of the terms
and conditions of the Vuforia SDK License Agreement, which may be found at
https://ar.qualcomm.at/legal/license.

By retaining or using the Sample Code in any manner, you confirm your agreement
to all the terms and conditions of the Vuforia SDK License Agreement.  If you do
not agree to all the terms and conditions of the Vuforia SDK License Agreement,
then you may not retain or use any of the Sample Code in any manner.
==============================================================================*/

using UnityEngine;
using System.Collections;

public class InstructionalScreen : MonoBehaviour
{
    #region PUBLIC_MEMBER_VARIABLES

    public Texture2D iPhone;
    public Texture2D iPhone5;
    public Texture2D iPad;
	
	public Texture2D iPhone_DataBG;
    public Texture2D iPhone5_DataBG;
    public Texture2D iPad_DataBG;
	
	public Texture2D colorOverlay;
	public Texture2D colorOverlay_hazy;
	
	public Texture2D emailLinkButton;

    #endregion // PUBLIC_MEMBER_VARIABLES



    #region PRIVATE_MEMBER_VARIABLES

    private Texture2D mText;
	private Texture2D DataBG;
    private Texture2D mStartLabel;

    private Rect mTextRect;
    private Rect mStartLabelRect;
	private Rect dataBGRect;
	private Rect colorOverlayBGRect;
	private Rect mailButtonRect;

    private GUIStyle mButtonStyle;
	private GUIStyle transparentButton = new GUIStyle();
	
    private bool mShowScreen = true;
	private bool itsSunny = false;
	private bool isItMarsPlanet = false;

    #endregion // PRIVATE_MEMBER_VARIABLES


    #region UNITY_MONOBEHAVIOUR_METHODS

    void Start()
    {
        
		/*// iPhone5
            if (Screen.width <= 640 && Screen.height > 1000)
            {
                mText = iPhone5;
				DataBG = iPhone5_DataBG;
          
            }//iPhones
            else if (Screen.width <= 640)
            {
                mText = iPhone;
               DataBG = iPhone_DataBG;
            }//iPads
            else
            {*/
                mText = iPad;
                DataBG = iPad_DataBG;
			//}

        // The text box should fill the width of the screen
        float textWidth = Screen.width;
		float textHeight = Screen.height;
		
		if(Screen.height < 2000)
		{
			mailButtonRect = new Rect(282,596, 240, 30);
	        mTextRect = new Rect(0, 0, textWidth, textHeight);
			colorOverlayBGRect = new Rect(0, 0, Screen.width, Screen.height);
			dataBGRect = new Rect(0, 824, Screen.width, 200);
		}
		else{
			mailButtonRect = new Rect(565,1192, 480, 60);
	        mTextRect = new Rect(0, 0, textWidth, textHeight);
			colorOverlayBGRect = new Rect(0, 0, Screen.width, Screen.height);
			dataBGRect = new Rect(0, 1648, Screen.width, 399);
		}

    }


    void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
                // Show this screen
                mShowScreen = true;
        }
    }


    void OnGUI()
    {
		if(Screen.height < 2000)
		{
			AutoResize(768, 1024);
		}
		
		//use the tan color overlay if the atmosphere is sunny
		if(!itsSunny){
			GUI.DrawTexture(colorOverlayBGRect, colorOverlay_hazy, ScaleMode.StretchToFill, true, 0);
		}
		//use the less opaque tan color overlay if the atmosphere is NOT sunny
		if(!isItMarsPlanet){
			GUI.DrawTexture(colorOverlayBGRect, colorOverlay, ScaleMode.StretchToFill, true, 0);
		}
		//show the help screen if the marker is not found
        if (mShowScreen)
        {
            // Draw the text box
            GUI.DrawTexture(mTextRect, mText, ScaleMode.StretchToFill, true, 0);
			if (GUI.Button (mailButtonRect, emailLinkButton, transparentButton)){
				Application.OpenURL("mailto:?subject=Sol%203D%20Image%20Marker&body=Download%20the%20Marker%20at:%20https://dl.dropboxusercontent.com/u/33007601/Sol3D_Rover_Marker.pdf");
			}
        }
		GUI.DrawTexture(dataBGRect, DataBG, ScaleMode.StretchToFill, true, 0);
		
    }
	
	public void toggleGUI(bool showGUI){
		 mShowScreen = showGUI;
	}
	
	public void isItSunnyToggle(bool isItSunny){
		 itsSunny = isItSunny;
	}
	public void showingMars(bool isItMars){
		 isItMarsPlanet = isItMars;
	}
	public static void AutoResize(int screenWidth, int screenHeight)
	{
	    Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
	    GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
	}
    #endregion // UNITY_MONOBEHAVIOUR_METHODS
}


