using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {

    float x, parallaxingSpeed = 2;
    Vector3 velocity;

	void Start ()
    {
        x = transform.position.x;
        velocity.x = 6;
        //velocity.x = Player.GetMoveSpeed();
	}
	
	void Update ()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            x -= velocity.x * Time.deltaTime / parallaxingSpeed;
            transform.position = new Vector3(x, transform.position.y, 100);
        }
	}
}
