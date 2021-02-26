using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class Menu : MonoBehaviour 
{
    public static bool /*audio_enabled = true,*/ touch_enabled = false;
    public static int audioEnabled;
    AudioSource audio;

    public GameObject resetPanel;
    public GameObject settingsPanel;
    public GameObject toogleButton;

    void Start()
    {
        //audioEnabled is reversed logic because the default is 0
        audioEnabled = PlayerPrefs.GetInt("audio");

        audio = GetComponent<AudioSource>();
        if (audioEnabled == 0/*audio_enabled*/)
            //audio.Play();
            audio.mute = false;
        else
            audio.mute = true;

        if(PlayerPrefs.GetInt("jumpSound") == 1)
            toogleButton.gameObject.GetComponent<Toggle>().isOn = true;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch_enabled = true;
        }
    }

    public void Play()
    {
        Player.Reset();
        Application.LoadLevel(1);

        CameraMovement.CameraReset();
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Mute()
    {
        if (audioEnabled == 0)
        {
            //audio.Stop();
            audio.mute = true;
            //audio_enabled = false;
            audioEnabled = 1;
            PlayerPrefs.SetInt("audio", 1);
            PlayerPrefs.Save();
        }
        else
        {
            //audio.Play();
            audio.mute = false;
            //audio_enabled = true;
            audioEnabled = 0;
            PlayerPrefs.SetInt("audio", 0);
            PlayerPrefs.Save();
        }
    }

    public void ShowResetPanel()
    {
        resetPanel.SetActive(!resetPanel.activeSelf);
    }

    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    public void Yes()
    {
        PlayerPrefs.SetInt("highscore", 0);
        PlayerPrefs.Save();
        resetPanel.SetActive(!resetPanel.activeSelf);
    }

    public void No()
    {
        resetPanel.SetActive(!resetPanel.activeSelf);
    }

    public void JumpSound(bool checkBox)
    {
        if(checkBox)
        {
            PlayerPrefs.SetInt("jumpSound", 1);
            PlayerPrefs.Save();
        }
        else
        {
            PlayerPrefs.SetInt("jumpSound", 0);
            PlayerPrefs.Save();
        }
    }


    //GETTERS
    public static int get_audio_enabled()
    {
        //return audio_enabled;
        return audioEnabled;
    }

    public static bool get_touch_enabled()
    {
        return touch_enabled;
    }

    //SETTERS
    public static void set_audio_enabled(int a)
    {
        //audio_enabled = a;
        audioEnabled = a;
    }

    public static void set_touch_enabled(bool a)
    {
        touch_enabled = a;
    }
}
