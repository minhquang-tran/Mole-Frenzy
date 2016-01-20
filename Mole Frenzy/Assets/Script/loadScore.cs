using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Parse;
using UnityEngine.SceneManagement;

public class loadScore : MonoBehaviour {
    float timer;
    Button HSBtn;
    Text HSBtnText;
    int finalScore;
    int finalAIScore;
    void Start () {
        HSBtn = transform.Find("Result/Up Score Btn").GetComponent<Button>();
        HSBtnText = HSBtn.transform.Find("Text").GetComponent<Text>();
        GameObject.Find("Internet").GetComponent<internet>().checkConnection();
        timer = 3;
        if(scoreStorage.level.Equals("Level 3"))
        {
            finalScore = scoreStorage.score * 3;
            finalAIScore = scoreStorage.AIscore * 3;
        }
        else if (scoreStorage.level.Equals("Level 2"))
        {
            finalScore = scoreStorage.score * 2;
            finalAIScore = scoreStorage.AIscore * 2;
        }
        else
        {
            finalScore = scoreStorage.score * 1;
            finalAIScore = scoreStorage.AIscore * 1;
        }

        transform.Find("Result/Player 1/P1 score").GetComponent<Text>().text = "" + finalScore;
        transform.Find("Result/Player 2/P2 score").GetComponent<Text>().text = "" + finalAIScore;
        if(scoreStorage.score > scoreStorage.AIscore)
        {
            transform.Find("Result/Winner text").GetComponent<Text>().text = "You Win!";
            

        }
        else if(scoreStorage.score < scoreStorage.AIscore)
        {
            transform.Find("Result/Winner text").GetComponent<Text>().text = "You Lose!";
        }
        else
        {
            transform.Find("Result/Winner text").GetComponent<Text>().text = "Draw!";
        }
    }
	void Update () {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            GameObject.Find("Internet").GetComponent<internet>().checkConnection();
            timer = 3;
        }
        if (GameObject.Find("Internet").GetComponent<internet>().getConnection())
        {
            HSBtn.interactable = true;
            HSBtnText.text = "Upload Your Score";
        }
        else
        {
            HSBtnText.text = "No Connection";
            HSBtn.interactable = false;
        }
    }
}