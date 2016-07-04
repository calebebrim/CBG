using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToMainMenu : MonoBehaviour {

	public void mainMenu(){
		SceneManager.LoadScene ("main_menu");
	}
}
