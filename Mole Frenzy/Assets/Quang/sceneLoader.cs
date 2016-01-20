using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class sceneLoader : MonoBehaviour {

	/*public void NextLevelButton(int index)
	{
		Application.LoadLevel(index);
	}
*/
	public void LoadScene(string levelName)
	{
		SceneManager.LoadScene(levelName);
	}
}
