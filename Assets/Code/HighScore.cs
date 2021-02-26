using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScore : MonoBehaviour {

    Text highscore;

    int hscore;

    void Awake()
    {
        highscore = GetComponent<Text>();

        hscore = PlayerPrefs.GetInt("highscore", 0);
    }

	void Update () 
    {
        hscore = PlayerPrefs.GetInt("highscore", 0);
        highscore.text = "High Score: " + hscore;
	}
}
