using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class GameMenu
 * 
 * \brief This class is used to control the functionality of the game menu
 * 
 * \date 2019/29/11
 */
public class GameMenu : MonoBehaviour
{
    public void QuitGame()
	{
		Debug.Log("QUIT!");
		Application.Quit();
	}
}
