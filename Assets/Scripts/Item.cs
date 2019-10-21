using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] protected ItemType m_ItemType;

	public enum ItemType
	{
		None,
		Key
	};

	public ItemType GetItemType()
	{
		return m_ItemType;
	}

	//The parameter for this function is passed in so that this function can get access
	// to the components of the player.
	public void RevealItem(Transform playerTransform)
	{
		//This if statement will trigger if the sprite is visable on the Scene View
		if(GetComponent<SpriteRenderer>().isVisible)
		{
			GetComponent<SpriteRenderer>().color = Color.black;
		}
		else
		{
			playerTransform.GetComponentInChildren<Indicator>().DisplayIndicator(transform);
		}
	}
}
