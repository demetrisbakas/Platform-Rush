using UnityEngine;
using System.Collections;

public class Particles : MonoBehaviour {

    static new AudioSource audio;

    public static ParticleSystem particles;

    // Use this for initialization
    void Start ()
    {
        particles = GetComponent<ParticleSystem>();

        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()  
    {
        
    }

    public static void ParticleEmit()
    {
        if (Menu.get_audio_enabled() == 0)
            audio.Play();

        particles.Play();
    }
}
