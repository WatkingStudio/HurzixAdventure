using System.Collections;
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
