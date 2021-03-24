﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Projectile
 * 
 * \brief This is an abstract class for Projectiles
 * 
 * \date 2020/27/01
 */ 
public class Projectile : MonoBehaviour
{
	/// <summary>
	/// Base function for instantiate.
	/// </summary>
	/// <param name="destination"></param>
	/// <param name="speed"></param>
    public virtual void Instantiate(Vector3 destination, float speed)
	{

	}
}
