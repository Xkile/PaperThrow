using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
	GameObject ball;
    //[SerializeField]
   // GameObject ballParent;

    private GameObject newBall;

	public void Spawn()
	{
		newBall  = Instantiate (ball, new Vector3(-0.83f, 0.885f, 3.01f), Quaternion.identity);
        newBall.transform.rotation = Quaternion.Euler(-90,0,0);
       // newBall.transform.SetParent(ballParent.transform);
        //newBall.transform.localScale = new Vector3 (0.05f,0.05f,0.05f);

	}
}
