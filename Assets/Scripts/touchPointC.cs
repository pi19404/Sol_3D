using UnityEngine;
using System.Collections;

public class touchPointC : MonoBehaviour  {
	
	public GameObject targetItem;
	public GameObject targetMarker;
	public GameObject FrontStatPlane;
	public GameObject BackStatPlane;
	public Texture2D RoverTexture1;
	public Texture2D RoverTexture2;
	public Texture2D RoverTexture3;
	public Texture2D RoverTexture4;
	public Texture2D RoverTexture5;
	public Texture2D RoverTexture6;
	public Texture2D RoverTexture7;
	public Texture2D RoverTexture8;
	private Vector3 startPoint;
	private Vector3 planePoint;
	private float startTime;
	private float duration = 5.0f;
	private bool TargetActive = false;
	private int statCounter = 1;
	//private bool dollarModeLocal = false;
	
	// Use this for initialization
	void Start () {
		
			startPoint = targetItem.transform.position;
			planePoint = targetItem.transform.position;
		
	}
	
	// Update is called once per frame
	void Update () {
		// Attach this script to a trackable object

	
	if(TargetActive){
			// Create a plane that matches the target plane
    Plane targetPlane = new Plane(targetMarker.transform.up, targetMarker.transform.position);
		foreach (Touch touch in Input.touches)
	    {
	        if (touch.phase == TouchPhase.Ended)
	        {
				//reset dollar mode so the rover will start to move
				//dollarModeLocal = false;
	            Ray ray = Camera.main.ScreenPointToRay(touch.position);
				
	            float dist = 0.0f;
	            targetPlane.Raycast(ray, out dist);
	            planePoint = ray.GetPoint(dist);
				//make it lookat the opposite direction
				//planePoint = 2.0f * transform.position - planePoint;
				startPoint = targetItem.transform.position;
				startTime = Time.time;
				targetItem.transform.LookAt(planePoint);
				//targetItem.transform.Rotate(0,180,0,Space.World);
				//targetItem.animation.PlayQueued("BucketAnim");
	            /*GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
	            cube.transform.parent = transform;
	            cube.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
	            cube.transform.position = planePoint;*/
				
				FrontStatPlane.animation.Stop("FadeUpFacts");
				BackStatPlane.animation.Stop("FadeUpFacts");
				FrontStatPlane.animation.Play("FadeUpFacts");
				BackStatPlane.animation.Play("FadeUpFacts");
					
				statCounter++;
				if(statCounter == 2){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture2;
					BackStatPlane.renderer.material.mainTexture = RoverTexture2;
					
					}
					else if(statCounter == 3){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture3;
					BackStatPlane.renderer.material.mainTexture = RoverTexture3;

					}
					else if(statCounter == 4){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture4;
					BackStatPlane.renderer.material.mainTexture = RoverTexture4;

					}
					else if(statCounter == 5){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture5;
					BackStatPlane.renderer.material.mainTexture = RoverTexture5;

					}
					else if(statCounter == 6){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture6;
					BackStatPlane.renderer.material.mainTexture = RoverTexture6;

					}
					else if(statCounter == 7){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture7;
					BackStatPlane.renderer.material.mainTexture = RoverTexture7;

					}
					else if(statCounter == 8){
					FrontStatPlane.renderer.material.mainTexture = RoverTexture8;
					BackStatPlane.renderer.material.mainTexture = RoverTexture8;
			
					}
					else{
					statCounter = 1;
					FrontStatPlane.renderer.material.mainTexture = RoverTexture1;
					BackStatPlane.renderer.material.mainTexture = RoverTexture1;
					}
	        }
	    }
		//if it is on dollar mode the initial set up will not be captured because I don't want it to reposition the rover on load
		/*if(!dollarModeLocal){
			
		}*/
		targetItem.transform.position = Vector3.Lerp(startPoint, planePoint, (Time.time - startTime) / duration);
		}
	}
	void toggleMoveScript(bool isActive){
		TargetActive = isActive;
		
		//Debug.Log("TARGET ACTIVE = " + TargetActive);
	}/*
	void dollarStartMode(bool dollarMode){
		dollarModeLocal = dollarMode;
		
		//Debug.Log("TARGET ACTIVE = " + TargetActive);
	}*/
	
}
