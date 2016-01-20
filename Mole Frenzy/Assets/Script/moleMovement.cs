using UnityEngine;
using System;
using System.Linq;

public class moleMovement : MonoBehaviour
{
    public bool isUp, isDown, isMovingUp, isMovingDown, isCalled;
    public Animation anim;
    System.Timers.Timer timer;
    public float minAvailableTime = 0.5f;
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

    // Use this for initialization
    void Start()
    {
        isUp = false;
        isDown = true;
        isCalled = false;
        particlePoint = new Vector3(transform.position.x, 28.8f, transform.position.z);
        particlePoint2 = new Vector3(transform.position.x, 28.8f, transform.position.z);
        anim = GetComponent<Animation>();
        handler = GameObject.Find("Ground").GetComponent<handler>();
        moleName = this.gameObject.name;
        for(int i = 0; i < 12; i++)
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

        if (hole.GetComponent<hole>().reservedNum == 1 && hole.GetComponent<hole>().isReady)
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
            //Debug.Log("down");
            isDown = true;
            isMovingDown = false;
            isCalled = false;
        }
        else
        {
            //Debug.Log("not down");
            isDown = false;
        }

    }

    /*void OnMouseDown()
    {
        isMovingDown = true;
        handler.addScore(10);       
    }*/

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Player Hammer")
        {
            handler.getSoundHandler().PlaySound(4);
            if ((isUp && secondsTimer >= 1.5f) || isMovingUp)
            {
                handler.getSoundHandler().PlaySound(5);
                handler.addScore(10);
                Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
                Instantiate(handler.getPerfect10(), particlePoint2, handler.getPerfect10().transform.rotation);
            }

            isMovingDown = true;
            handler.addScore(10);
            Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
            Instantiate(handler.getAdd10(), particlePoint, handler.getAdd10().transform.rotation);
        }
        else if (col.gameObject.name == "AI Hammer")
        {
            Vector3 hitPoint = handler.getTargetPoint();
            hitPoint.x += -1;
            hitPoint.y = 30.8f;
            hitPoint.z += 7;
            handler.getSoundHandler().PlaySound(4);
            if ((isUp && secondsTimer >= 1.5f) || isMovingUp)
            {
                handler.getSoundHandler().PlaySound(5);
                handler.addAIScore(10);
                Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
                Instantiate(handler.getAIPerfect10(), particlePoint2, handler.getAIPerfect10().transform.rotation);
            }
            transform.position = Vector3.MoveTowards(transform.position, downPosition, step);
            isMovingDown = true;
            handler.addAIScore(10);
            Instantiate(handler.getStarParticle(), particlePoint, handler.getStarParticle().transform.rotation);
            Instantiate(handler.getAIAdd10(), particlePoint, handler.getAIAdd10().transform.rotation);
        }
    }
}

