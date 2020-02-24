using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class EnemyAction
 * 
 * \brief This is an abstract class that is used for the enemy actions
 * 
 * The PerformAction function is overwritten in all child classes to carry out the action
 *  for that class. Each child class must have an implementation of this function. PerformAction
 *  is the function called at runtime when the program requires the enemy to make an action.
 * 
 * \date 2019/15/10
 * 
 */ 

public class EnemyAction : MonoBehaviour
{
	protected Actions m_Action;

	public Actions Action { get { return m_Action; } private set { } }

	public enum Actions
	{
		EnemyMoveAction,
		EnemyPlayerDetection,
		EnemyMoveToPlayer,
		EnemyMeleeAttack,
		EnemyPatrol,
		EnemyRangedAttack
	}

    public virtual void PerformAction()
	{

	}

	public virtual void InitialiseAction()
	{
		Debug.Log(gameObject.name + " initialised");
	}
}
