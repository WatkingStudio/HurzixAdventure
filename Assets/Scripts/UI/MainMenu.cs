using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * \class MainMenu
 * 
 * \brief This class is used to control the functionality of the main menu
 * 
 * \date 2019/29/11
 */
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
