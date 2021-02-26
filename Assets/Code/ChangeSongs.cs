using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]

public class ChangeSongs : MonoBehaviour {

    //public AudioClip firstClip, otherClip;

    new AudioSource audio;

    bool a = false;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        /*audio.clip = otherClip;

        //
        audio.Play();
        audio.loop = true;
        //*/

        /*if (Menu.get_audio_enabled() == 0)
        {
            /*audio.Play();

            //StartCoroutine(WaitForRealSeconds(audio.clip.length));

            Invoke("NextSong", audio.clip.length);*//*
        }
        else
        {
            audio.mute = true;
        }*/


        if (Menu.get_audio_enabled() == 0)
        {
            audio.mute = false;
        }
        else
        {
            audio.mute = true;
        }

        //yield return new WaitForSeconds(audio.clip.length);
    }

    void Update()
    {
        //Makes music stop when in menu
        /*if (Time.timeScale == 0)
        {
            if (Menu.get_audio_enabled() == 0)
                audio.Pause();
            a = true;
        }

        if ((Time.timeScale == 1) && a)
        {
            if (Menu.get_audio_enabled() == 0)
                audio.Play();
            a = false;
        }*/
    }

    /*void NextSong()
    {
        audio.Stop();
        audio.clip = otherClip;
        if (Menu.get_audio_enabled() == 0)
            audio.Play();
        audio.loop = true;
    }*/

    public void Mute()
    {
        if (Menu.get_audio_enabled() == 0)
        {
            audio.mute = true;
            /*audio.Stop();
            CancelInvoke();*/
            //Menu.set_audio_enabled(false);
            Menu.set_audio_enabled(1);
            PlayerPrefs.SetInt("audio", 1);
            PlayerPrefs.Save();
        }
        else
        {
            audio.mute = false;
            /*audio.Play();
            Invoke("NextSong", audio.clip.length);*/
            //Menu.set_audio_enabled(true);
            Menu.set_audio_enabled(0);
            PlayerPrefs.SetInt("audio", 0);
            PlayerPrefs.Save();
        }
    }



    //
    /*public IEnumerator WaitForRealSeconds(float time)
    {
        float start = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < start + time)
        {
            yield return null;
        }
        NextSong();
    }*/
    /*private IEnumerator PlayNextSong()
    {
        // Do stuff
        //NextSong();

        yield return StartCoroutine(WaitForRealSeconds(audio.clip.length));

        // Do other stuff
        //NextSong();
    }*/
    //
}
