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
}
