using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

/**
 * \class LevelTransition
 * 
 * \brief This class is used handle the transition between two levels in the game.
 * 
 * \date 2019/08/11
 * 
 */
public class LevelTransition : MonoBehaviour
{
	[Serializable]
	private enum Levels
	{
		MAIN_MENU,
		LEVEL_ONE,
		LEVEL_TWO,
		LEVEL_THREE,
		LEVEL_FOUR,
		LEVEL_FIVE,
		LEVEL_SIX,
		LEVEL_TEN,
		LEVEL_ELEVEN,
		GAME_COMPLETE
	};

	[SerializeField]
	private Levels m_NextLevel;
	[SerializeField]
	private PlayerCharacter m_PlayerCharacter;

	public void GoToNextLevel()
	{
		m_PlayerCharacter.UpdatePlayerGlobals();
		SceneManager.LoadScene((int)m_NextLevel);
		Debug.Log("Loading Scene: " + LevelsEnumToString());
	}

	private string LevelsEnumToString()
	{
		switch(m_NextLevel)
		{
			case Levels.MAIN_MENU:
				return "Main_Menu";
			case Levels.LEVEL_ONE:
				return "Level_One";
			case Levels.LEVEL_TWO:
				return "Level_Two";
			case Levels.LEVEL_THREE:
				return "Level_Three";
			case Levels.LEVEL_FOUR:
				return "Level_Four";
			case Levels.LEVEL_FIVE:
				return "Level_Five";
			case Levels.LEVEL_SIX:
				return "Level_Six";
			case Levels.LEVEL_TEN:
				return "Level_Ten";
			case Levels.LEVEL_ELEVEN:
				return "Level_Eleven";
			case Levels.GAME_COMPLETE:
				return "Game_Complete";
			default:
				return "Invalid Level";
		}
	}
}
