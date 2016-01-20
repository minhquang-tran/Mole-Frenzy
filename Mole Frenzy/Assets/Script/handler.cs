using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class handler : MonoBehaviour
{
    private int randomBonus;
    private float gameTime;
    private GameObject bonusHole;
    private GameObject bonusMole;
    public bool isBonusAvailable;
    public float bonusCoolDownTimer;
    private int score;
    public int AIscore;
    public GameObject hammer;
    Vector3 targetPosition;
    float bonusCoolDown;
    bool bonusFinishTurn;
    bool isLock;
    public float secureTimer;
    private int timeLimit;
    private GameObject menuBtn;
    private float readyTime;
    private bool readyResume;
    private bool pause;
    GameObject readyText;
    GameObject defaultAttack;
    GameObject starBlow;
    GameObject wrongEffect;
    string activeScene;
    private GameObject minus15;
    private GameObject add10;
    private GameObject add5;
    private GameObject perfect10;

    private GameObject AIminus15;
    private GameObject AIadd10;
    private GameObject AIadd5;
    private GameObject AIperfect10;

    soundCamera soundHandler;
    GameObject pauseMenu;
    GameObject[] normalMoles;
    GameObject[] rabbits;
    GameObject[] bonusMoles;

    // Use this for initialization
    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameObject.Find("Main Camera").GetComponent<Animation>().Play("Camera");
        gameTime = 0;
        isBonusAvailable = false;
        score = 0;
        bonusCoolDown = 10f;
        bonusCoolDownTimer = bonusCoolDown;
        bonusFinishTurn = false;
        hammer = GameObject.Find("Player Hammer");
        isLock = false;
        secureTimer = 25;
        timeLimit = 90;
        menuBtn = GameObject.Find("Menu");
        readyTime = 3f;
        readyResume = false;
        readyText = GameObject.Find("ThreeTwoOne Text");
        readyText.gameObject.SetActive(true);

        pauseMenu = GameObject.Find("UI Canvas");
        pauseMenu.SetActive(false);

        defaultAttack = GameObject.Find("Particle System default Whack");
        defaultAttack.GetComponent<ParticleSystem>().Stop();

        starBlow = GameObject.Find("Particle System star effect");
        starBlow.GetComponent<ParticleSystem>().Stop();

        wrongEffect = GameObject.Find("Particle System wrong");
        wrongEffect.GetComponent<ParticleSystem>().Stop();

        soundHandler = GameObject.Find("Main Camera").GetComponent<soundCamera>();

        activeScene = SceneManager.GetActiveScene().name;
        minus15 = GameObject.Find("-15");
        add10 = GameObject.Find("+10");
        add5 = GameObject.Find("+5");
        perfect10 = GameObject.Find("PERFECT!");

        AIminus15 = GameObject.Find("AI -15");
        AIadd10 = GameObject.Find("AI +10");
        AIadd5 = GameObject.Find("AI +5");
        AIperfect10 = GameObject.Find("AI PERFECT!");

        scoreStorage.level = activeScene;

        normalMoles = new GameObject[12];
        rabbits = new GameObject[12];
        bonusMoles = new GameObject[12];
        //Moles
        for (int i = 0; i < 12; i++)
        {
            normalMoles[i] = GameObject.Find("normalMole ("+ i +")");
            rabbits[i] = GameObject.Find("Rabbit (" + i + ")");
            bonusMoles[i] = GameObject.Find("bonusMole (" + i + ")");
        }





        if (PlayerPrefs.GetString("savedLevel").Equals(activeScene))
        {
            score = PlayerPrefs.GetInt("playerScore");
            AIscore = PlayerPrefs.GetInt("AIScore");
            gameTime = PlayerPrefs.GetFloat("currentTime");

            for (int i = 0; i < 12; i++)
            {

                Vector3 tmppositionNormalMole = new Vector3(
                    normalMoles[i].transform.position.x,
                    PlayerPrefs.GetFloat("normalMole" + i + "Y"),
                    normalMoles[i].transform.position.z);
                normalMoles[i].transform.position = tmppositionNormalMole;

                if(PlayerPrefs.GetString("normalMole" + i + "down").Equals("True"))
                {
                    normalMoles[i].GetComponent<moleMovement>().isMovingDown = true;
                    Debug.Log("aaa");
                }
                if (PlayerPrefs.GetString("normalMole" + i + "up").Equals("True"))
                {
                    normalMoles[i].GetComponent<moleMovement>().isMovingUp = true;
                }


                //
                Vector3 tmppositionRabbit = new Vector3(
                    rabbits[i].transform.position.x,
                    PlayerPrefs.GetFloat("rabbit" + i + "Y"),
                    rabbits[i].transform.position.z);
                rabbits[i].transform.position = tmppositionRabbit;

                if (PlayerPrefs.GetString("rabbit" + i + "down").Equals("True"))
                {
                    rabbits[i].GetComponent<moleMinus>().isMovingDown = true;
                }
                if (PlayerPrefs.GetString("rabbit" + i + "up").Equals("True"))
                {
                    rabbits[i].GetComponent<moleMinus>().isMovingUp = true;
                }

                //
                Vector3 tmppositionBonusMole = new Vector3(
                    bonusMoles[i].transform.position.x,
                    PlayerPrefs.GetFloat("bonusMole" + i + "Y"),
                    bonusMoles[i].transform.position.z);
                bonusMoles[i].transform.position = tmppositionBonusMole;

                if (PlayerPrefs.GetString("bonusMole" + i + "down").Equals("True"))
                {
                    bonusMoles[i].GetComponent<moleBonus>().isMovingDown = true;
                }
                if (PlayerPrefs.GetString("bonusMole" + i + "up").Equals("True"))
                {
                    bonusMoles[i].GetComponent<moleBonus>().isMovingUp = true;
                }
            }
        }

        //Ready Set Go

        Time.timeScale = 0.1f;
        readyTime = 3f;
        readyResume = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {        
        //for (int i = 0; i < Input.touchCount; i++)
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && Time.timeScale != 0 && Time.timeScale != 0.1f && gameTime < timeLimit)
            {

                if (!hammer.GetComponent<Animation>().isPlaying)
                {
                    targetPosition = hit.point;
                    targetPosition.y += 0.5f;
                    Instantiate(defaultAttack, targetPosition, defaultAttack.transform.rotation);
                    targetPosition.x += 1.25f;
                    targetPosition.y = 30f;                    
                    targetPosition.z -= 5.7f;
                    hammer.transform.position = targetPosition;
                    hammer.GetComponent<Animation>().Play("hammerWhack");
                    soundHandler.PlaySound(6);
                }
            }
       }
      
        gameTime += Time.deltaTime;

        if ((int)gameTime == timeLimit)
        {          
            soundHandler.PlaySound(1);
        }
        if ((int)gameTime == timeLimit - 10)
        {          
            soundHandler.PlaySound(8);
            if(Time.timeScale <= 0.1f)
            {
                soundHandler.stopSound(8);
            }
        }

        if ((int)gameTime == timeLimit + 3)
        {
            resetGameSave();
            scoreStorage.score = getScore();
            scoreStorage.AIscore = getAIScore();
            changeScene("Results");

        }

        if ((int)gameTime % 10 == 0&& (int)gameTime < timeLimit)
        {
            soundHandler.PlaySound(7);
        }
                
        
        if(readyResume)
        {
            if (readyTime > 0)
            {             
                readyTime -= Time.deltaTime * 10;
                readyText.GetComponent<readyText>().setText("" + ((int)readyTime + 1));
                readyText.GetComponent<Animation>().Play("321Go");               
            }
            else
            {
                Time.timeScale = 1;
                readyResume = false;
                pause = false;
                readyText.gameObject.SetActive(false);
            }
        }
        


        if (secureTimer > 0)
        {
            secureTimer -= Time.deltaTime;
        }
        else
        {
            generateBonus();
        }

        if (bonusMole != null)
        {
            bonusFinishTurn = bonusMole.GetComponent<moleBonus>().isFinishTurn;
        }

        if (bonusCoolDownTimer == 0 && bonusFinishTurn)
        {
            bonusCoolDownTimer = bonusCoolDown;
            bonusMole.GetComponent<moleBonus>().isFinishTurn = false;
            isLock = false;
        }



        if (bonusCoolDownTimer > 0)
        {
            bonusCoolDownTimer -= Time.deltaTime;
        }
        else
        {
            bonusCoolDownTimer = 0;
        }

        if (bonusCoolDownTimer == 0 && !isLock)
        {
            isBonusAvailable = true;
        }
        else
        {
            isBonusAvailable = false;
        }
        int gameTimeRoundUp = (int)gameTime;
        if (gameTimeRoundUp > 0 && isBonusAvailable)
        {
            generateBonus();
        }

        //Debug.Log("Time: " + gameTimeRoundUp + " " + "Score: " + score);
    }

    void generateBonus()
    {
        if (gameTime < timeLimit)
        {
            secureTimer = 25f;
            isLock = true;
            isBonusAvailable = false;
            randomBonus = Random.Range(0, 11);
            bonusHole = GameObject.Find("Hole (" + randomBonus + ")");
            bonusHole.GetComponent<hole>().reservedNum = 3;
            bonusHole.GetComponent<hole>().isReserved = true;

            bonusMole = GameObject.Find("bonusMole (" + randomBonus + ")");
            bonusMole.GetComponent<moleBonus>().isFinishTurn = false;
            Debug.Log(bonusMole);
        }
    }

    public void addScore(int scr)
    {
        score += scr;
    }

    public void addAIScore(int scr)
    {
        AIscore += scr;
    }

    public int getScore()
    {
        return score;
    }

    public int getAIScore()
    {
        return AIscore;
    }

    public int getCurrentTime()
    {
        return (int)gameTime;
    }

    public int getTimeLimit()
    {
        return timeLimit;
    }

    public GameObject getMenuBtn()
    {
        return menuBtn;
    }
    public GameObject getReadyText()
    {
        return readyText;
    }

    public void pauseGame()
    {
        if (readyTime <= 0)
        {
            menuBtn.GetComponent<menuBtn>().setTextColor(Color.red);
            Time.timeScale = 0;
            pause = true;
            pauseMenu.SetActive(true);
            
        }
    }

    public void resumeGame()
    {
        if( menuBtn.GetComponent<TextMesh>().color == Color.red)
        {
            getSoundHandler().PlaySound(0);
            menuBtn.GetComponent<menuBtn>().setTextColor(Color.white);
            Time.timeScale = 0.1f;
            readyTime = 3f;
            readyResume = true;      
            readyText.gameObject.SetActive(true);
        }         
    }

    public bool isPaused()
    {
        return pause;
    }

    public void OnOffSound()
    {

        if (AudioListener.pause == false)
        {
            AudioListener.pause = true;
        }
        else
        {
            AudioListener.pause = false;
        }       
    }


    public Vector3 getTargetPoint()
    {
        return targetPosition;
    }

    public GameObject getStarParticle()
    {
        return starBlow;
    }

    public GameObject getWrongEffectParticle()
    {
        return wrongEffect;
    }

    public GameObject getAttackParticle()
    {
        return defaultAttack;
    }


    public void changeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public GameObject getMinus15()
    {
        return minus15;
    }
    public GameObject getAdd5()
    {
        return add5;
    }
    public GameObject getAdd10()
    {
        return add10;
    }

    public GameObject getPerfect10()
    {
        return perfect10;
    }

    public GameObject getAIMinus15()
    {
        return AIminus15;
    }
    public GameObject getAIAdd5()
    {
        return AIadd5;
    }
    public GameObject getAIAdd10()
    {
        return AIadd10;
    }

    public GameObject getAIPerfect10()
    {
        return AIperfect10;
    }

    public soundCamera getSoundHandler()
    {
        return soundHandler;
    }

    public void saveGame()
    {
        PlayerPrefs.SetString("savedLevel", activeScene);
        PlayerPrefs.SetInt("playerScore", score);
        PlayerPrefs.SetInt("AIScore", AIscore);
        PlayerPrefs.SetFloat("currentTime", gameTime);

        for (int i = 0; i < 12; i++)
        {

            PlayerPrefs.SetFloat("normalMole" + i + "Y", normalMoles[i].transform.position.y);
            PlayerPrefs.SetFloat("rabbit" + i + "Y", rabbits[i].transform.position.y);
            PlayerPrefs.SetFloat("bonusMole" + i + "Y", bonusMoles[i].transform.position.y);
            PlayerPrefs.SetString("normalMole" + i + "down", "" + normalMoles[i].GetComponent<moleMovement>().isMovingDown);
            PlayerPrefs.SetString("normalMole" + i + "up", "" + normalMoles[i].GetComponent<moleMovement>().isMovingUp);
            PlayerPrefs.SetString("rabbit" + i + "down", "" + rabbits[i].GetComponent<moleMinus>().isMovingDown);
            PlayerPrefs.SetString("rabbit" + i + "up", "" + rabbits[i].GetComponent<moleMinus>().isMovingUp);
            PlayerPrefs.SetString("bonusMole" + i + "down", "" + bonusMoles[i].GetComponent<moleBonus>().isMovingDown);
            PlayerPrefs.SetString("bonusMole" + i + "up", "" + bonusMoles[i].GetComponent<moleBonus>().isMovingUp);

            Debug.Log(PlayerPrefs.GetString("rabbit" + i + "down"));
        }
    }

    public void resetGameSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
