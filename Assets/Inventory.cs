using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
	//Which items have been collected
	private bool m_HasKeyOne;

	[SerializeField] private Animator m_Animator;
	[SerializeField] private LevelDictionary m_LevelDictionary;

	struct InventorySlot
	{
		string name;
		bool isCollected;
		RuntimeAnimatorController animation;
	};

	private void Start()
	{
		
	}

	public void SetupInventory(List<Item> collectableItems)
	{
		foreach(Item item in collectableItems)
		{
			for(int i = 0; i < m_LevelDictionary.m_LevelData.Length; ++i)
			{
				if(item.GetItemType() == m_LevelDictionary.m_LevelData[i].name)
				{
					m_Animator.runtimeAnimatorController = m_LevelDictionary.m_LevelData[i].anim;
				}
			}
		}
	}

	//This is called when the Level to add a slot for the item on this level to be collected
	public void AddItemSlot()
	{

	}

	//Returns whether the player has KeyOne in their inventory
	public bool HasKeyOne()
	{
		return m_HasKeyOne;
	}

	//Triggers when KeyOne has been collected
	public void PickUpKeyOne()
	{
		m_Animator.SetTrigger("KeyCollected");
		m_HasKeyOne = true;
	}

	public void DropKeyOne()
	{
		m_Animator.SetTrigger("KeyDropped");
		m_HasKeyOne = false;
	}
}
