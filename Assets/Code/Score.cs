using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    Text score;

	void Awake ()
    {
        score = GetComponent<Text>();
	}
	
	void Update () 
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
            score.text = "Score: " + (long)CameraMovement.GetCameraX();
	}
}
