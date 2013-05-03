/*==============================================================================
            Copyright (c) 2010-2013 QUALCOMM Austria Research Center GmbH.
            All Rights Reserved.
            Qualcomm Confidential and Proprietary
==============================================================================*/

using UnityEngine;

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour,
                                            ITrackableEventHandler
{
    #region PRIVATE_MEMBER_VARIABLES
 
    private TrackableBehaviour mTrackableBehaviour;
    
    #endregion // PRIVATE_MEMBER_VARIABLES



    #region UNTIY_MONOBEHAVIOUR_METHODS
    
    void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        OnTrackingLost();
    }

    #endregion // UNTIY_MONOBEHAVIOUR_METHODS



    #region PUBLIC_METHODS

    /// <summary>
    /// Implementation of the ITrackableEventHandler function called when the
    /// tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
                                    TrackableBehaviour.Status previousStatus,
                                    TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED)
        {
            OnTrackingFound();
        }
        else
        {
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS



    #region PRIVATE_METHODS


    private void OnTrackingFound()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();

        // Enable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = true;
        }

        // Enable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = true;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
		
		if(mTrackableBehaviour.name == "FrameMarker1"){
				GameObject MarsPlanet = GameObject.Find("MarsWrapper");
			    MarsPlanet.animation.Play("Planet");
			
				GameObject screenFilm = GameObject.Find("Scripts");
				screenFilm.SendMessage("showingMars", true);
		}
		else{
			GameObject screenFilm = GameObject.Find("Scripts");
			screenFilm.SendMessage("showingMars", false);
		}
		
		//Make sure the instruction screen is not showing
		GameObject GUIScript = GameObject.Find("Scripts");
		GUIScript.SendMessage("toggleGUI", false);
		//don't let touches move the rover if tracking is found
		GameObject MoveScript = GameObject.Find("Scripts");
		MoveScript.SendMessage("toggleMoveScript", true);
		//show the weather alert buttons
		GameObject ButtonScript = GameObject.Find("Scripts");
		ButtonScript.SendMessage("toggleButtons", true);
    }


    private void OnTrackingLost()
    {
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>();
        Collider[] colliderComponents = GetComponentsInChildren<Collider>();

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            component.enabled = false;
        }

        // Disable colliders:
        foreach (Collider component in colliderComponents)
        {
            component.enabled = false;
        }

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
		//Make sure the instruction screen is showing
		GameObject GUIScript = GameObject.Find("Scripts");
		GUIScript.SendMessage("toggleGUI", true);
		//don't let touches move the rover if tracking is found
		GameObject MoveScript = GameObject.Find("Scripts");
		MoveScript.SendMessage("toggleMoveScript", false);
		//show the weather alert buttons
		GameObject ButtonScript = GameObject.Find("Scripts");
		ButtonScript.SendMessage("toggleButtons", false);
    }

    #endregion // PRIVATE_METHODS
}
