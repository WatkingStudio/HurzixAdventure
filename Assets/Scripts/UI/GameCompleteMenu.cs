using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * \class GameCompleteMenu
 * 
 * \brief This class is used to control the functionality of the game complete menu
 * 
 * \date 2019/30/11
 */ 
public class GameCompleteMenu : MonoBehaviour
{
    public void PlayAgain()
	{
		SceneManager.LoadScene(1);
	}

	public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
