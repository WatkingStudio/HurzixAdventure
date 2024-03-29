﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Item
 * 
 * \brief This class is an abstract class that will be inherited by every Item in the program.
 * 
 * \date 2019/21/10
 */ 
public class Item : MonoBehaviour
{
	[SerializeField] protected ItemType m_ItemType = ItemType.None;

	public enum ItemType
	{
		None,
		Key
	};

	/// <summary>
	/// Get the type of the item.
	/// </summary>
	/// <returns>The item type of the item.</returns>
	public ItemType GetItemType()
	{
		return m_ItemType;
	}

	
}
