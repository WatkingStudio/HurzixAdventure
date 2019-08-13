using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] private ItemType m_ItemType;

	public enum ItemType
	{
		Key
	};

	public ItemType GetItemType()
	{
		return m_ItemType;
	}
}
