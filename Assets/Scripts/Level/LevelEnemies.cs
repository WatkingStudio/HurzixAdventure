﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelEnemies
 * 
 * \brief This class holds the functionality for controlling the enemies in the level
 * 
 * \date 2019/13/01
*/
public class LevelEnemies : MonoBehaviour
{
	[SerializeField]
	List<BasicEnemy> m_EnemiesList;

	/// <summary>
	/// Reset all the enemies in the level.
	/// </summary>
	public void ResetEnemies()
	{
		foreach(BasicEnemy be in m_EnemiesList)
		{
			be.ResetEnemy();
		}
	}
}
