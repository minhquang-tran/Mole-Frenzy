using UnityEngine;
using System;
using System.Linq;

public class moleMinus : MonoBehaviour
{
    public bool isUp, isDown, isMovingUp, isMovingDown, isCalled;
    public Animation anim;
    System.Timers.Timer timer;
    public float minAvailableTime;
    public float maxAvailableTime;
    public float speed = 30f;
    public float secondsTimer;
    Vector3 downPosition;
    Vector3 upPosition;
    Vector3 currentPosition;
    float step;
    GameObject hammer;
    GameObject hole;
    handler handler;
    string moleName;
    Vector3 particlePoint;

    // Use this for initialization
    void Start()
    {
        minAvailableTime = 1f;
        maxAvailableTime = 2f;
        isUp = false;
        isDown = true;
        isCalled = false;
        particlePoint = new Vector3(transform.position.x, 28.8f, transform.position.z - 3);
        anim = GetComponent<Animation>();
        handler = GameObject.Find("Ground").GetComponent<handler>();
        moleName = this.gameObject.name;
        for (int i = 0; i < 12; i++)
        {
            if (moleName.Contains("(" + i + ")"))
            {
                hole = GameObject.Find("Hole (" + i + ")");
                break;
            }
        }
        //GameObject obj1 = GameObject.Find("Digda1");
        //bool digda1 = obj1.GetComponent<moleMovement>().isDown;


        downPosition = new Vector3(transform.position.x, 16.43f, transform.position.z);
        upPosition = new Vector3(transform.position.x, 27.25f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (isUp && secondsTimer == 0)
        {
            secondsTimer = UnityEngine.Random.Range(minAvailableTime, maxAvailableTime);
        }

        if (hole.GetComponent<hole>().reservedNum == 2 && hole.GetComponent<hole>().isReady)
        {
            isCalled = true;
        }

        step = speed * Time.deltaTime;


        if (secondsTimer > 0)
        {
            secondsTimer -= Time.deltaTime;
        }
        if (secondsTimer <= 0)
        {
            secondsTimer = 0;
        }


        //Debug.Log(waitingTime + " " + secondsTimer + " " + isUp + " " + anim.isPlaying);
        //Debug.Log(secondsTimer + " " + waitingTime + isUp + isDown);
        //Debug.Log("Up: " + isUp + " " + "Down: " + isDown + " " + isMovingDown + isCalled);

        if (((isDown && isCalled) || isMovingUp) && transform.position.y < upPosition.y) // UP
        {
            hole.GetComponent<hole>().isReserved = false;
            isMovingUp = true;
            transform.position = Vector3.MoveTowards(transform.position, upPosition, step);
        }
        else if (((isUp && secondsTimer == 0) || isMovingDown) && transform.position.y > downPosition.y) // DOWN
        {
            isMovingDown = true;
            transform.position = Vector3.MoveTowards(transform.position, downPosition, step);
        }

        if (transform.position.y == upPosition.y)
        {
            isUp = true;
            isMovingUp = false;
        }
        else
        {
            isUp = false;
        }

        if (transform.position.y == downPosition.y)
        {
            isDown = true;
            isMovingDown = false;
            isCalled = false;
        }
        else
        {
            isDown = false;
        }

    }

    /*void OnMouseDown()
    {
        isMovingDown = true;
        handler.addScore(-15);
    }*/
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player Hammer")
        {
            handler.getSoundHandler().PlaySound(3);
            isMovingDown = true;
            handler.addScore(-15);
            Instantiate(handler.getWrongEffectParticle(), particlePoint, handler.getWrongEffectParticle().transform.rotation);
            Instantiate(handler.getMinus15(), particlePoint, handler.getMinus15().transform.rotation);
        }
        else if (col.gameObject.name == "AI Hammer")
        {
            handler.getSoundHandler().PlaySound(3);         
            transform.position = Vector3.MoveTowards(transform.position, downPosition, step);
            isMovingDown = true;
            handler.addAIScore(-15);
            Instantiate(handler.getWrongEffectParticle(), particlePoint, handler.getWrongEffectParticle().transform.rotation);
            Instantiate(handler.getAIMinus15(), particlePoint, handler.getAIMinus15().transform.rotation);
        }
    }

}