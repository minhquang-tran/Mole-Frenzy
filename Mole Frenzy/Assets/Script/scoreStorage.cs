using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Parse;
using UnityEngine.UI;
using System.Threading.Tasks;

public class scoreStorage : MonoBehaviour {
    public static int score;
    public static int AIscore;
    public static string level;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void retry()
    {
        Debug.Log(level);
        SceneManager.LoadScene(level);
    }

    public void upHighScore()
    {
            string name = GameObject.Find("Canvas").transform.Find("Fill Name Panel/InputField/Text").GetComponent<Text>().text;
        ParseObject highScore = new ParseObject("HighScore");
        highScore["playerName"] = name;
        if (level.Equals("Level 3"))
        {
            highScore["score"] = score * 3;
        }
        else if(level.Equals("Level 2"))
        {
            highScore["score"] = score * 2;
        }
        else
        {
            highScore["score"] = score * 1;
        }

        highScore.SaveAsync();
    }
}
