using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public virtual void Clicked()
	{
		Debug.Log(gameObject.name + " was clicked");
	}
}
