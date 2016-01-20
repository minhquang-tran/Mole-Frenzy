using UnityEngine;
using System.Collections;

public class AI : MonoBehaviour
{
    System.Timers.Timer timer;
    public float easyTimer;
    public float mediumTimer;
    public float hardTimer;
    public float secondsTimer;
    Vector3 targetPosition;
    public bool attack;
    handler handler;
    ArrayList arrayMoles;
    bool[] check;
    GameObject[][] moles;
    GameObject[] holes;
    Vector3 lastPosition;

    public int AIlevel;

    // Use this for initialization
    void Start()
    {
        attack = false;
        easyTimer = 1f;
        mediumTimer = 0.7f;
        hardTimer = 0.5f;

        handler = GameObject.Find("Ground").GetComponent<handler>();
        check = new bool[12];
        holes = new GameObject[12];

        moles = new GameObject[12][];
        moles[0] = GameObject.FindGameObjectsWithTag("moles0");
        moles[1] = GameObject.FindGameObjectsWithTag("moles1");
        moles[2] = GameObject.FindGameObjectsWithTag("moles2");
        moles[3] = GameObject.FindGameObjectsWithTag("moles3");
        moles[4] = GameObject.FindGameObjectsWithTag("moles4");
        moles[5] = GameObject.FindGameObjectsWithTag("moles5");
        moles[6] = GameObject.FindGameObjectsWithTag("moles6");
        moles[7] = GameObject.FindGameObjectsWithTag("moles7");
        moles[8] = GameObject.FindGameObjectsWithTag("moles8");
        moles[9] = GameObject.FindGameObjectsWithTag("moles9");
        moles[10] = GameObject.FindGameObjectsWithTag("moles10");
        moles[11] = GameObject.FindGameObjectsWithTag("moles11");

        holes[0] = GameObject.Find("Hole (0)");
        holes[1] = GameObject.Find("Hole (1)");
        holes[2] = GameObject.Find("Hole (2)");
        holes[3] = GameObject.Find("Hole (3)");
        holes[4] = GameObject.Find("Hole (4)");
        holes[5] = GameObject.Find("Hole (5)");
        holes[6] = GameObject.Find("Hole (6)");
        holes[7] = GameObject.Find("Hole (7)");
        holes[8] = GameObject.Find("Hole (8)");
        holes[9] = GameObject.Find("Hole (9)");
        holes[10] = GameObject.Find("Hole (10)");
        holes[11] = GameObject.Find("Hole (11)");
        lastPosition = gameObject.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // Animation
        if (!GetComponent<Animation>().isPlaying)
        {
            transform.position = new Vector3(59, 30, -40);
        }

        if (secondsTimer > 0)
        {
            secondsTimer -= Time.deltaTime;
        }
        else
        {
            secondsTimer = 0;
        }

        // AI: Calculate where to move next
        if (secondsTimer == 0 && handler.getCurrentTime() < handler.getTimeLimit())
        {
            if (AIlevel == 1 && !GetComponent<Animation>().isPlaying)
            {
                transform.position = mediumAI();
                GetComponent<Animation>().Play("AI Hammer Whack");
            }
            else if (AIlevel == 2 && !GetComponent<Animation>().isPlaying)
            {
                transform.position = hardAI();
                GetComponent<Animation>().Play("AI Hammer Whack");
            }
            else if (AIlevel == 0 && !GetComponent<Animation>().isPlaying)
            {
                transform.position = easyAI();
                GetComponent<Animation>().Play("AI Hammer Whack");
            }
        }
    }

    Vector3 easyAI()
    {
        int randomHole = UnityEngine.Random.Range(0, 12);
        // Debug.Log (randomHole);
        Vector3 nextPosition = new Vector3(holes[randomHole].transform.position.x, transform.position.y, holes[randomHole].transform.position.z);
        nextPosition.x -= 14f;
        nextPosition.z -= 7f;
        // Debug.Log (nextPosition);
        secondsTimer = easyTimer;
        lastPosition = nextPosition;
        return nextPosition;
    }

    Vector3 mediumAI()
    {
        // Check which holes has a mole up
        Vector3 nextPosition = lastPosition;
        for (int i = 0; i < moles.Length; i++)
        {
            bool moleUp = false;
            for (int n = 0; n < moles[i].Length; n++)
            {
                if (moles[i][n].gameObject.name.Contains("normalMole"))
                {
                    if (moles[i][n].GetComponent<moleMovement>().isUp)
                    {
                        moleUp = true;
                    }
                }
                else if (moles[i][n].gameObject.name.Contains("Rabbit"))
                {
                    if (moles[i][n].GetComponent<moleMinus>().isUp)
                    {
                        int hitChance = UnityEngine.Random.Range(0, 3);
                        if (hitChance == 1)
                        {
                            moleUp = true;
                        }
                    }
                }
                else {
                    if (moles[i][n].GetComponent<moleBonus>().isUp)
                    {
                        moleUp = true;
                    }
                }
            }
            if (moleUp)
            {
                check[i] = true;
                // Debug.Log (i);
            }
            else {
                check[i] = false;
            }
        }

        /* Calculate the distance between the current hole and the next hole
		then choose the nearest hole that currently has a mole up to move to */
        float distance = 1000f;
        for (int z = 0; z < check.Length; z++)
        {
            if (check[z])
            {
                Vector3 nextHole = new Vector3(holes[z].transform.position.x, transform.position.y, holes[z].transform.position.z);

                float nextDistance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(nextHole.x) - Mathf.Abs(lastPosition.x), 2) + Mathf.Pow(Mathf.Abs(nextHole.z) - Mathf.Abs(lastPosition.z), 2));
                if (nextDistance < distance)
                {
                    nextPosition = nextHole;
                    nextPosition.x -= 14f;
                    nextPosition.z -= 7f;
                    // Debug.Log (z + " " + check [z] + " " + nextDistance);
                }
            }
        }

        secondsTimer = mediumTimer;
        if (lastPosition == nextPosition)
        {
            nextPosition = new Vector3(59, 30, -40);
        }
        else {
            lastPosition = nextPosition;
        }
        return nextPosition;
    }

    Vector3 hardAI()
    {
        // Check which holes has a mole up
        Vector3 nextPosition = lastPosition;
        for (int i = 0; i < moles.Length; i++)
        {
            bool moleUp = false;
            for (int n = 0; n < moles[i].Length; n++)
            {
                if (moles[i][n].gameObject.name.Contains("normalMole"))
                {
                    if (moles[i][n].GetComponent<moleMovement>().isUp)
                    {
                        moleUp = true;
                    }
                }
                else if (moles[i][n].gameObject.name.Contains("Rabbit"))
                {
                    if (moles[i][n].GetComponent<moleMinus>().isUp)
                    {
                        int hitChance = UnityEngine.Random.Range(0, 8);
                        if (hitChance == 0)
                        {
                            moleUp = true;
                        }
                    }
                }
                else {
                    if (moles[i][n].GetComponent<moleBonus>().isUp)
                    {
                        moleUp = true;
                    }
                }
            }
            if (moleUp)
            {
                check[i] = true;
                // Debug.Log (i);
            }
            else {
                check[i] = false;
            }
        }

        /* Calculate the distance between the current hole and the next hole
		then choose the nearest hole that currently has a mole up to move to */
        float distance = 1000f;
        for (int z = 0; z < check.Length; z++)
        {
            if (check[z])
            {
                Vector3 nextHole = new Vector3(holes[z].transform.position.x, transform.position.y, holes[z].transform.position.z);

                float nextDistance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(nextHole.x) - Mathf.Abs(lastPosition.x), 2) + Mathf.Pow(Mathf.Abs(nextHole.z) - Mathf.Abs(lastPosition.z), 2));
                if (nextDistance < distance)
                {
                    nextPosition = nextHole;
                    nextPosition.x -= 14f;
                    nextPosition.z -= 7f;
                    // Debug.Log (z + " " + check [z] + " " + nextDistance);
                }
            }
        }

        secondsTimer = mediumTimer;
        if (lastPosition == nextPosition)
        {
            nextPosition = new Vector3(59, 30, -40);
        }
        else {
            lastPosition = nextPosition;
        }
        return nextPosition;
    }
}


