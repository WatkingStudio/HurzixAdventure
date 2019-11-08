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
		LEVEL_ONE,
		LEVEL_TWO
	};

	[SerializeField]
	private Levels m_NextLevel;

    public void GoToNextLevel()
	{
		SceneManager.LoadScene((int)m_NextLevel);
		Debug.Log("Loading Scene: " + LevelsEnumToString());
	}

	private string LevelsEnumToString()
	{
		if (m_NextLevel == Levels.LEVEL_ONE)
			return "Level_One";
		else if (m_NextLevel == Levels.LEVEL_TWO)
			return "Level_Two";

		return "Invalid Level";
	}
}
