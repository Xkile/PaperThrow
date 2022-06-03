using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    Vector2 startPos, endPos, direction; // touch start position, touch end position, swipe direction
	float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction

	[SerializeField]
	float throwForceInXandY = 1f; // to control throw force in X and Y directions

	[SerializeField]
	float throwForceInZ = 50f; // to control throw force in Z direction

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if(SpawnBall.instance.SwipeOn){
        #if !UNITY_EDITOR
		// if you touch the screen
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0)) {
			SpawnBall.instance.startGame = true;

			// getting touch position and marking time when you touch the screen
			touchTimeStart = Time.time;
			startPos = Input.GetTouch (0).position;
		}

		// if you release your finger
		if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended || Input.GetMouseButtonUp(0)) {

			SpawnBall.instance.SwipeOn = false;
			// marking time when you release it
			touchTimeFinish = Time.time;

			// calculate swipe time interval 
			timeInterval = touchTimeFinish - touchTimeStart;

			// getting release finger position
			endPos = Input.GetTouch (0).position;

			// calculating swipe direction in 2D space
			direction = startPos - endPos;

			// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
			rb.isKinematic = false;
			rb.AddForce (- direction.x * throwForceInXandY, - direction.y * throwForceInXandY, throwForceInZ / timeInterval);

			//Destroy ball in 4 seconds
			Invoke("DestroyMe",3f);

		}

        #else 
         if (Input.GetMouseButtonDown(0))                                     //if mouse button down is pressed then mouse position and time will be stored.
            {
				SpawnBall.instance.startGame = true;
                touchTimeStart = Time.time;
                startPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))    //when mouse button up is triggered and menu icons are not open then again mouse position and time will be stored.
            {
				SpawnBall.instance.SwipeOn = false;
                touchTimeFinish = Time.time;
                timeInterval = touchTimeFinish - touchTimeStart;
                endPos = Input.mousePosition;
                direction = startPos - endPos;
                rb.isKinematic = false;
                rb.AddForce (  direction.x * throwForceInXandY, - direction.y * throwForceInXandY, - throwForceInZ / timeInterval);    //To add force on ball
                Invoke("DestroyMe",3f);
            }
			 #endif
		}
	}

    	private void OnTriggerEnter(Collider other) {
			SpawnBall.instance.InceaseScore();
			print("ScoreIncreased");
         CancelInvoke("DestroyMe");
		 Invoke ("stopMovement",3f);
         
      }

	  void stopMovement(){
		  this.GetComponent<Rigidbody>().isKinematic = true;
		  SpawnBall.instance.SpawnButton.interactable = true;
		  Destroy(this.GetComponent<Swipe>());
	  }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.name == "Floor")
        {
			Invoke("TrailOff", 1f);
			
        }
    }

	void TrailOff()
    {
		this.GetComponent<TrailRenderer>().enabled = false;
	}

    void DestroyMe(){
		print("interactable true");
		SpawnBall.instance.SpawnButton.interactable = true;
        Destroy(gameObject,0.1f);

    }
}
