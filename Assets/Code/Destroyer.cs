using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

    public GameObject[] obj;
    public GameObject newObject;

    public float x, angle = 0, oldy = 0, rndy = 0, cameraMax = 3, minDistance = 3f, maxDistance = 7f;
    public static bool firstTime = true;
    private bool setAngles = false;

	void Start ()
    {
        obj[0] = Resources.Load("Platform Prefabs/PlatformTiny") as GameObject;
        obj[1] = Resources.Load("Platform Prefabs/PlatformSmall") as GameObject;
        obj[2] = Resources.Load("Platform Prefabs/PlatformMedium") as GameObject;
        obj[3] = Resources.Load("Platform Prefabs/PlatformLarge") as GameObject;
        obj[4] = Resources.Load("Platform Prefabs/PlatformHuge") as GameObject;

        newObject = obj[0];

        Invoke("SetAngles", 30);
	}
	
	void Update () 
    {
	    
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Destroy(other.gameObject);
        }
        else
        {
            Destroy(other.gameObject);






            Vector3 length = newObject.GetComponent<BoxCollider2D>().size;







            float realLength = ((Mathf.Cos(Mathf.Abs(angle * Mathf.Deg2Rad))) * length.x);

            oldy = ((Mathf.Sin(angle * Mathf.Deg2Rad) * length.x) + rndy);




            float distance = Random.Range(minDistance, maxDistance);

            rndy = Random.Range((oldy - distance), (oldy + distance));

            if (((oldy > cameraMax) && (rndy > oldy)) || ((oldy < -cameraMax) && (rndy < oldy)))
                rndy -= 2 * (rndy - oldy);





            float rndx = Mathf.Sqrt(Mathf.Abs(Mathf.Pow(distance, 2) - Mathf.Pow((oldy - rndy), 2)));
            //float rndx = Mathf.Sqrt(Mathf.Pow(distance, 2) - Mathf.Pow((oldy - rndy), 2));

            x += (realLength + rndx);


            //Debug.Log("Distance: " + distance);
            //Debug.Log("RealLength: " + realLength);
            //Debug.Log("Length: " + length.x);
            //Debug.Log("Angle: " + angle);
            //Debug.Log("Mathf.Cos(Mathf.Abs(angle * Mathf.Deg2Rad)): " + (Mathf.Cos(Mathf.Abs(angle * Mathf.Deg2Rad))));
            //Debug.Log("Rndy: " + rndy);
            //Debug.Log("Rndx: " + rndx);
            //Debug.Log("Oldy: " + oldy);
            //Debug.Log("Mathf.Pow(distance, 2): " + Mathf.Pow(distance, 2));
            //Debug.Log("Mathf.Pow((oldy - rndy), 2): " + Mathf.Pow((oldy - rndy), 2));

            if (setAngles)
            {
                if (rndy > 3)
                    angle = Random.Range(-30f, 0f);
                else if (rndy < 3)
                    angle = Random.Range(0f, 30f);
                else
                    angle = Random.Range(-30f, 30f);
            }

            if (firstTime)
            {
                minDistance = 4f;
                maxDistance = 6f;

                firstTime = false;
            }
            else
            {
                minDistance = 3f;
                maxDistance = 7f;
            }

            newObject = obj[Random.Range(0, obj.GetLength(0))];

            newObject = Instantiate(newObject, new Vector3(x, rndy, 70), Quaternion.Euler(0, 0, angle)) as GameObject;
        }

    }

    void SetAngles()
    {
        setAngles = true;
    }

    public static void ChangeFirstTime()
    {
        firstTime = true;
    }
}
