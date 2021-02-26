using UnityEngine;
using System.Collections;

public class PlayerDestroyer : MonoBehaviour {

    new AudioSource audio;

    // Use this for initialization
    void Start ()
    {
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        //if the object that triggered the event is tagged player
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
            if (Menu.get_audio_enabled() == 0)
                audio.Play();
        }
    }
}
