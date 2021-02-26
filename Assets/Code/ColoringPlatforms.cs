using UnityEngine;
using System.Collections;

public class ColoringPlatforms : MonoBehaviour {

    public Color color1;
    public Color color2;
    float duration;
    bool c;

	void Start ()
    {
	    float rnd = Random.Range(0, 10);

        if (rnd == 0)
            c = true;

        duration = Random.Range(0.5f, 8f);

        color1 = CameraMovement.GetColor2();
	}
	
	void Update ()
    {
        if(c || CameraMovement.GetInvincible())
        {
            float t = Mathf.PingPong(Time.time, duration) / duration;
            GetComponent<Renderer>().material.color = Color.Lerp(color1, color2, t);
        }
	}
}
