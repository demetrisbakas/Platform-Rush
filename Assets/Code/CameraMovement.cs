using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    //public float slowDownSpeed = 2;
    public static float x, y;

    public static Color color1;// = Color.red;
    public static Color color2;// = Color.green;

    public Color[] colorTable1 = new Color[maxColorNumber];
    public Color[] colorTable2 = new Color[maxColorNumber];

    //public Color[,] colorTable = new Color[maxColorNumber, 2];

    public float duration = 10f;

    public static bool pause = false, invincible = false;

    public GameObject pausePanel, jumpButton, gameOverPanel;

    Vector3 velocity;

    public Texture2D textureToDisplay;

    public const int maxColorNumber = 5;

    void Awake()
    {
        if (!Menu.get_touch_enabled())
            jumpButton.SetActive(!jumpButton.activeSelf);

    }

	void Start ()
    {
        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        Vector3 thePosition = transform.TransformPoint(-((width / 2) - 1), 0, 0);

        Vector3 thePosition2 = transform.TransformPoint(-((width / 2) + 1), 0, 0);

        Vector3 thePosition3 = transform.TransformPoint(-((width / 2) + 50), 0, 0);

        GameObject.Find("LeftCollider").transform.position = thePosition;

        GameObject.Find("LeftPlayerDestroyer").transform.position = thePosition2;

        GameObject.Find("LeftDestroyer").transform.position = thePosition3;

        //pausePanel.SetActive(!pausePanel.activeSelf);

        InvokeRepeating("MoveFaster", 30, 30);

        int i = Random.Range((int)0, (maxColorNumber));
        color1 = colorTable1[i];
        color2 = colorTable2[i];


        //
        //This is the invoke that make all the platforms invincible late in the game, it better be turned off
        //Invoke("PlatformsInvincible", 240);
        //

        //style.normal.textColor = Color.black;
        //style.normal.background = textureToDisplay;


        //GameObject.Find("LeftCollider").transform.position = new Vector3((GameObject.FindGameObjectWithTag("MainCamera").transform.position.x - ((Camera.main.orthographicSize / Camera.main.aspect) / 2)), GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, 0);

        //
        /*// set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 16.0f / 9.0f;

        // determine the game window's current aspect ratio
        float windowaspect = (float)Screen.width / (float)Screen.height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = camera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            camera.rect = rect;


            GameObject leftCollider = Instantiate(Resources.Load("LeftCollider"), new Vector3(-(rect.x / 2), GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, 0), Quaternion.identity) as GameObject;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = camera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            camera.rect = rect;


            GameObject leftCollider = Instantiate(Resources.Load("LeftCollider"), new Vector3(-(rect.y / 2), GameObject.FindGameObjectWithTag("MainCamera").transform.position.y, 0), Quaternion.identity) as GameObject;
        }
        */
        //
    }
	
	void Update ()
    {
        /*if(GameObject.Find("Player") != null)
            transform.Translate(Vector2.right * Player.getMoveSpeed() * Time.deltaTime);
            if ((GameObject.Find("Player").transform.position.x >= GameObject.Find("Main Camera").transform.position.x) || (GameObject.Find("Player").transform.eulerAngles.y > 0))
                transform.Translate(Vector2.right * Player.getMoveSpeed() * Time.deltaTime);
            else if(GameObject.Find("Player").transform.eulerAngles.y == 0)
                transform.Translate(Vector2.right * (Player.getMoveSpeed() - slowDownSpeed) * Time.deltaTime);*/

        if (!Menu.get_touch_enabled())
            if (Input.touchCount > 0)
            {
                jumpButton.SetActive(!jumpButton.activeSelf);
                Menu.set_touch_enabled(true);
            }


        float t = Mathf.PingPong(Time.time, duration) / duration;
        Camera.main.backgroundColor = Color.Lerp(color1, color2, t);


        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }


        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            gameOverPanel.SetActive(false);

            velocity.x = Player.GetMoveSpeed();
            x += velocity.x * Time.deltaTime;
            y = (GameObject.FindGameObjectWithTag("Player").transform.position.y / 2);

            if (((Camera.main.transform.position.y - Camera.main.orthographicSize) <= GameObject.Find("DownPlayerDestroyer").transform.position.y) && (GameObject.Find("Player").transform.position.y < Camera.main.transform.position.y))
                transform.position = new Vector3(x, ((GameObject.Find("DownPlayerDestroyer").transform.position.y + Camera.main.orthographicSize)), -1);
            else
                transform.position = new Vector3(x, y, -1);
        }
        else
        {
            gameOverPanel.SetActive(true);

            StoreHighscore((int)x);

            x = 0;
        }

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && (GameObject.FindGameObjectWithTag("Player") == null) && (!pause))
        {
            Player.Reset();
            Application.LoadLevel(1);
        }
	}

    void MoveFaster()
    {
        if (Player.GetMoveSpeed() < 17)
            Player.ChangeMoveSpeed(1f);
        else
            CancelInvoke("MoveFaster");
    }

    void PlatformsInvincible()
    {
        invincible = true;
    }

    public static bool GetInvincible()
    {
        return invincible;
    }

    public void Respawn()
    {
        if ((GameObject.FindGameObjectWithTag("Player") == null) && (!pause))
        {
            Player.Reset();
            Application.LoadLevel(1);
        }
    }

    public void Restart()
    {
        Player.Reset();
        Application.LoadLevel(1);

        CameraReset();
        DestroyerFollow.SetX(0);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        if (!pause)
        {
            Time.timeScale = 0;
            pause = true;

            pausePanel.SetActive(!pausePanel.activeSelf);
        }
        else
        {
            Time.timeScale = 1;
            pause = false;

            pausePanel.SetActive(!pausePanel.activeSelf);
        }
    }

    public void MenuButton()
    {
        Application.LoadLevel(0);
    }

    public static void CameraReset()
    {
        x = 0;
        pause = false;
    }

    void StoreHighscore(int newHighscore)
    {
        int oldHighscore = PlayerPrefs.GetInt("highscore", 0);
        if (newHighscore > oldHighscore)
        {
            PlayerPrefs.SetInt("highscore", newHighscore);
            PlayerPrefs.Save();
        }
    }

    //Getters
    public static float GetCameraX()
    {
        return x;
    }

    public static bool GetPause()
    {
        return pause;
    }

    public static Color GetColor2()
    {
        return color2;
    }

    //Setters
    public static void SetInvincible(bool inv)
    {
        invincible = inv;
    }
}
