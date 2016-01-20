using UnityEngine;
using System;
using System.Linq;

public class moleBonus : MonoBehaviour
{
    public bool isUp, isDown, isMovingUp, isMovingDown, isCalled;
    public Animation anim;
    System.Timers.Timer timer;
    public float minAvailableTime = 1.5f;
    public float maxAvailableTime = 3f;
    public float speed = 30f;
    public float secondsTimer;
    Vector3 downPosition;
    Vector3 upPosition;
    Vector3 currentPosition;
    Vector3 particlePoint;
    Vector3 particlePoint2;
    float step;
    GameObject hammer;
    GameObject hole;
    handler handler;
    string moleName;
    public bool isFinishTurn;

    // Use this for initialization
    void Start()
    {
        isUp = false;
        isDown = true;
        isMovingUp = false;
        isMovingDown = false;
        isCalled = false;
        isFinishTurn = false;
        particlePoint = new Vector3(transform.position.x, 28.8f, transform.position.z);
        particlePoint2 = new Vector3(transform.position.x, 30f, transform.position.z);
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


        downPosition = new Vector3(transform.position.x, 15.5f, transform.position.z);
        upPosition = new Vector3(transform.position.x, 23.8f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(transform.position);

        if (isUp && secondsTimer == 0)
        {
            secondsTimer = UnityEngine.Random.Range(minAvailableTime, maxAvailableTime);
        }

        if (hole.GetComponent<hole>().reservedNum == 3 && hole.GetComponent<hole>().isReady)
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
            transform.position = Vector3.MoveTowards(transform.position, upPosition,  step);
        }
        else if ((secondsTimer == 0 || transform.position.y < 20) && transform.position.y > downPosition.y) // DOWN
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
            if (isMovingDown)
            {                
                isFinishTurn = true;
            }
            
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
        transform.position = Vector3.MoveTowards(transform.position, downPosition, step * 0.75f);
        handler.addScore(5);
    }*/

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player Hammer")
        {
            handler.getSoundHandler().PlaySound(4);
            if((isUp && secondsTimer >= 1.5f) || isMovingUp)
            {
                handler.getSoundHandler().PlaySound(5);
                handler.addScore(10);
                Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
                Instantiate(handler.getPerfect10(), particlePoint2, handler.getPerfect10().transform.rotation);
            }
            transform.position = Vector3.MoveTowards(transform.position, downPosition, step);
            handler.addScore(5);
            Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
            Instantiate(handler.getAdd5(), particlePoint, handler.getAdd5().transform.rotation);
        }
        else if (col.gameObject.name == "AI Hammer")
        {
            handler.getSoundHandler().PlaySound(4);
            if ((isUp && secondsTimer >= 1.5f) || isMovingUp)
            {
                handler.getSoundHandler().PlaySound(5);
                handler.addAIScore(10);
                Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
                Instantiate(handler.getAIPerfect10(), particlePoint2, handler.getAIPerfect10().transform.rotation);
            }
            transform.position = Vector3.MoveTowards(transform.position, downPosition, step);
            handler.addAIScore(5);
            Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
            Instantiate(handler.getAIAdd5(), particlePoint, handler.getAIAdd5().transform.rotation);
        }
    }
}

