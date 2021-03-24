using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Clickable
 * 
 * \brief This is a base class for any clickable GameObjects in the scene
 * 
 * \date 2019/24/10
 */ 
public class Clickable : MonoBehaviour
{
	// Actives When This Object is Clicked.
    public virtual void Clicked()
	{
		Debug.Log(gameObject.name + " was clicked");
	}
}
