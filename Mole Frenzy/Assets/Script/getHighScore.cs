using UnityEngine;
using System.Collections;
using Parse;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Threading.Tasks;

public class getHighScore : MonoBehaviour {
    Text HSdisplay;
    public string scoreString;
    float timer, timer2;
    // Use this for initialization
    void Start () {
        HSdisplay = GameObject.Find("Canvas").transform.Find("HighScore/HSTable").GetComponent<Text>();
        scoreString = "";
        GameObject.Find("Internet").GetComponent<internet>().checkConnection();
        timer = 3;
        timer2 = 10;
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        timer2 -= Time.deltaTime;
        if (timer < 0)
        {
            GameObject.Find("Internet").GetComponent<internet>().checkConnection();           
            timer = 3;
        }
        if (timer2 < 0)
        {
            getAndDisplayHighScore();
            timer2 = 10;
        }

        if (GameObject.Find("Internet").GetComponent<internet>().getConnection() && !scoreString.Equals(""))
        {
            HSdisplay.text = scoreString;
        }
        else if(!GameObject.Find("Internet").GetComponent<internet>().getConnection())
        {
            HSdisplay.text = "No Internet Connection";
        }
        
    }

    public void getAndDisplayHighScore()
    {
        scoreString = "";
        HSdisplay.text = "Loading...";
        var query = ParseObject.GetQuery("HighScore").Limit(10).OrderByDescending("score");

        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;
            foreach (var result in results)
            {
                string name = result.Get<string>("playerName");
                int score = result.Get<int>("score");
                if (!scoreString.Contains(name + "  -----------  " + score))
                {
                    scoreString += name + "  -----------  " + score + "\n";
                }
            }
        });
    }
}
