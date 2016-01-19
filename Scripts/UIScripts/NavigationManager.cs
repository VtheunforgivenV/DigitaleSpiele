using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour {

	public void LoadScene (string scene_name) {
		SceneManager.LoadScene (scene_name);
//		Application.LoadLevel(scene_name);
	}

    public void startGame() {
        GameConstants.newGame = false;
        LoadScene(GameConstants.sceneGame);
    }

	public void EndGame() {
		Application.Quit ();
	}
}
