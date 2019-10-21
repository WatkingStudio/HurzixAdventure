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
		if(GetComponent<SpriteRenderer>().isVisible)
		{
			GetComponent<SpriteRenderer>().color = Color.black;
			playerTransform.GetComponentInChildren<Indicator>().DisplayIndicator(transform);
		}
		else
		{
			playerTransform.GetComponentInChildren<Indicator>().DisplayIndicator(transform);
		}
	}
}
