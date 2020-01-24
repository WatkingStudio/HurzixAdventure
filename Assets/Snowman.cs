using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Snowman
 * 
 * \brief This class holds the functionality for a Snowman AI
 * 
 * \date 2020/24/01
 */ 
public class Snowman : BasicEnemy
{
    // Start is called before the first frame update
    void Start()
    {
		SetActiveAction(m_DefaultAction);
		m_StartPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		if (m_ActiveAction == null)
			return;
		m_ActiveAction.PerformAction();


    }

	public override void ResetEnemy()
	{
		base.ResetEnemy();
	}

	public override void StopEnemy()
	{
		
	}

	public override void IsWalking(bool walking)
	{
		base.IsWalking(walking);
	}
}
