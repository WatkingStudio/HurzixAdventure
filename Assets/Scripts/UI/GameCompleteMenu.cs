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
	/// <summary>
	/// Play the game again.
	/// </summary>
    public void PlayAgain()
	{
		SceneManager.LoadScene(1);
	}

	/// <summary>
	/// Quit the game.
	/// </summary>
	public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
