using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadGameScript : MonoBehaviour {
   

    string savedLevel;
    int playerScore;
    int AIScore;

    void Start () {
	    
	}
	

	void Update () {
        if(PlayerPrefs.GetString("savedLevel").Equals(""))
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
	
	}

    public void loadGame()
    {
        savedLevel = PlayerPrefs.GetString("savedLevel");

        SceneManager.LoadScene(savedLevel);
    }

    public void resetGameSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
