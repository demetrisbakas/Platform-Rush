using UnityEngine;
using System.Collections;

public class DestroyerFollow : MonoBehaviour {

    public static float x;
    Vector3 velocity;

    new AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
        x = 0;
    }
	
	// Update is called once per frame
	void Update () 
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            velocity.x = Player.GetMoveSpeed();
            x += velocity.x * Time.deltaTime;
            transform.position = new Vector3(x, transform.position.y, -11);
        }
        else
            x = 0;
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            if (Menu.get_audio_enabled() == 0)
                audio.Play();
        }
    }

    public static void SetX(float temp)
    {
        x = temp;
    }
}
