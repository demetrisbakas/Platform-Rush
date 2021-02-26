using UnityEngine;
using System.Collections;

//
[RequireComponent(typeof(Controller2D))]
//


public class Player : MonoBehaviour
{

    /*public float jumpHeight = 6, groundRadius = 0.1f, fastRun = 2;
    private static float moveSpeed = 4;
    public Transform isGround;
    public LayerMask ground;
    private bool grounded, doubleJump;

    //public Vector2 jumpVector;

	void Start()
    {
	
	}

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(isGround.position, groundRadius, ground);
    }
	
	void Update()
    {
        if (grounded)
            doubleJump = false;

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && !doubleJump)
        {
            //GetComponent<Rigidbody2D>().AddForce(jumpVector, ForceMode2D.Force);
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jumpHeight);
            doubleJump = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector2(0, 180);
        }
        else
        {
            if ((GameObject.Find("Player").transform.position.x < GameObject.Find("Main Camera").transform.position.x) || (GameObject.Find("Player").transform.eulerAngles.y > 0))
                transform.Translate(Vector2.right * moveSpeed * fastRun * Time.deltaTime);
            else
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            transform.eulerAngles = new Vector2(0, 0);
        }
        
	
	}

    //Getters
    public static float getMoveSpeed()
    {
        return moveSpeed;
    }


    //Setters
    public static void setMoveSpeed(float x)
    {
        moveSpeed = x;
    }*/

    public static float moveSpeed = 6, jumpHeight = 4, jumpDuration = .4f;
    private float gravity, jumpVelocity;

    public int jumpCount = 0, jumpLimit = 2, jumpSoundEnabled;

    bool inputTouch = false, buttonPress = false, touchedLeft = false;
    public static bool instanciated = true, pause = false;

    const int particleScore = 500;

    bool isJumping = false, isDoubleJumping = false, isGoingBack = false;

    //bool isGoingBack = false;

    long score;

    new AudioSource jumpAudio;

    Animator animator;

    GameObject newObject;

    public Transform particles;

    Vector2 input;

    Vector3 velocity;

    Controller2D controller;


    void Start()
    {
        jumpAudio = GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        controller = GetComponent<Controller2D>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(jumpDuration, 2);
        jumpVelocity = Mathf.Abs(gravity) * jumpDuration;


        score = (long)CameraMovement.GetCameraX();

        pause = CameraMovement.GetPause();

        jumpSoundEnabled = PlayerPrefs.GetInt("jumpSound");
    }

    void Update()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

        if (controller.collisions.below)
        {
            jumpCount = 0;

            isJumping = false;
            isDoubleJumping = false;
        }

        pause = CameraMovement.GetPause();

        if ((Input.touchCount > 0) && (!pause))
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.position.x < Screen.width / 2)
                {
                    input.x = -1;
                    touchedLeft = true;
                }
            }

            if(!touchedLeft)
                input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            touchedLeft = false;
        }
        else if(!pause)
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));


        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || buttonPress) && (jumpCount < jumpLimit) && (!pause))
        {
            velocity.y = jumpVelocity;

            jumpCount++;

            //Play jump sound effect
            if (jumpSoundEnabled == 1)
            {
                if (Menu.get_audio_enabled() == 0)
                {
                    if (jumpCount == 1)
                        jumpAudio.Play();
                    else if (jumpCount == 2)
                        jumpAudio.Play();
                }

                if (jumpCount == 1)
                    isJumping = true;
                else
                    isDoubleJumping = true;
            }

            buttonPress = false;
        }

        if ((input.x < 0) && (!pause))
        {
            velocity.x = input.x * moveSpeed;

            isGoingBack = true;
        }
        else if(!pause)
        {
            if(GameObject.FindGameObjectWithTag("MainCamera").transform.position.x > transform.position.x)
                velocity.x = moveSpeed + 2;
            else
                velocity.x = moveSpeed;

            isGoingBack = false;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        score = (long)CameraMovement.GetCameraX();

        if (((score % particleScore) == 0) && (!instanciated))
        {
            Particles.ParticleEmit();

            instanciated = true;
        }

        if (((score % particleScore) >= 1) && instanciated)
            instanciated = false;


        animator.SetInteger("JumpCount", jumpCount);
        animator.SetBool("isGoingBack", isGoingBack);
    }

    public void ButtonJump()
    {
        if ((jumpCount < jumpLimit) && (!pause))
            buttonPress = true;
    }

    //Getters

    public static float GetMoveSpeed()
    {
        return moveSpeed;
    }

    //Setters

    public static void SetMoveSpeed(float x)
    {
        moveSpeed = x;
    }

    public static void ChangeMoveSpeed(float x)
    {
        moveSpeed += x;
    }


    public static void Reset()
    {
        CameraMovement.SetInvincible(false);
        SetMoveSpeed(6);
        instanciated = true;
        Destroyer.ChangeFirstTime();
    }
}
