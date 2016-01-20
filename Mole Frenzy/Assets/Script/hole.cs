using UnityEngine;
using System.Collections;

public class hole : MonoBehaviour
{
    public int reservedNum;
    public bool isReserved;
    float availableTime;
    
    public float minWaitingTime = 3f;
    public float maxWaitingTime = 6f;
    public float waitingTime;
    public bool isFree;
    public bool isReady;
    //handler handler;
    GameObject[] moles;
    string holeName;
    Vector3 targetPosition;
    public GameObject hammer;

    // Use this for initialization
    void Start()
    {
        reservedNum = 0;
        isFree = true;
        isReady = true;
        isReserved = false;
        holeName = this.gameObject.name;
        hammer = GameObject.Find("Player Hammer");
        //handler = GameObject.Find("Ground").GetComponent<handler>();
        for (int i = 0; i < 12; i++)
        {
            if (holeName.Contains("(" + i + ")"))
            {
                moles = GameObject.FindGameObjectsWithTag("moles" + i);
                break;
            }
        }

            
    }

    // Update is called once per frame
    void Update()
    {    
        isFree = checkHole();
        if (isFree && waitingTime == 0 && !isReserved)
        {
            reservedNum = 0;
            waitingTime = UnityEngine.Random.Range(minWaitingTime, maxWaitingTime);
        }


        if (waitingTime > 0)
        {
            waitingTime -= Time.deltaTime;
            isReady = false;
        }
        if(waitingTime <= 0)
        {
            waitingTime = 0;
            if(isFree)
            {
                isReady = true;
            } 
            else
            {
                isReady = false;
            }                 
        }

        if (!isReserved && isFree && waitingTime == 0)
        {

            int randomNum = Random.Range(1, 10);
            if (randomNum < 8)
            {
                reservedNum = 1;
                isReserved = true;
            }
            else
            {
                reservedNum = 2;
                isReserved = true;
            }
        }

        //Debug.Log(waitingTime + " " + secondsTimer + " " + isUp + " " + anim.isPlaying);
        //Debug.Log(secondsTimer + " " + waitingTime + isUp + isDown);


    }

    bool checkHole()
    {
        foreach (GameObject obj in moles)
        {
            if (obj.GetComponent<moleMovement>() != null && !obj.GetComponent<moleMovement>().isDown)
            {
                return false;               
            }
            /*else if (obj.GetComponent<moleMinus>() != null && !obj.GetComponent<moleMinus>().isDown)
            {
                return false;
            }*/
            else if (obj.GetComponent<moleBonus>() != null && !obj.GetComponent<moleBonus>().isDown)
            {
                return false;
            }
        }
        return true;
    }
}

