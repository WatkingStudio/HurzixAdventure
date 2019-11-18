using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * \class Inventory
 * 
 * \brief This class is used to track the items found in each level.
 * 
 * It currently is not designed to carry items across levels.
 * 
 * \date 2019/21/10
 */ 
public class Inventory : MonoBehaviour
{
	[SerializeField, Tooltip("An Array of Inventory Slots for the Players Inventory")]
	private InventorySlot[] m_InventorySlots;
	[SerializeField, Tooltip("A Scriptable Object that holds a Dictionary of Items and Animators for the Current Level")]
	private LevelDictionary m_LevelDictionary;

	[SerializeField, Tooltip("A Default Animator for Black Inventory Slots")]
	private RuntimeAnimatorController m_BlankInventorySlot;

	private int m_NumberUsedSlots = 0;

	private void Start()
	{
		if (m_InventorySlots.Length == 0)
			Debug.LogError("No Inventory Slots have been assigned to " + gameObject.name);
		if (!m_LevelDictionary)
			Debug.LogError("No Level Dictionary has been assigned to " + gameObject.name);
		if (!m_BlankInventorySlot)
			Debug.LogError("No Blank Inventory Animator Controller has been assigned to " + gameObject.name);
	}

	private void ResetInventory()
	{
		m_NumberUsedSlots = 0;
		//For each animator set to a default animator, and set the objects image script to disabled.
		foreach(InventorySlot slot in m_InventorySlots)
		{
			slot.GetAnimator().runtimeAnimatorController = m_BlankInventorySlot;
			slot.GetAnimator().GetComponent<Image>().enabled = false;
		}
	}

	//For each level setup the inventory slots.
	public void SetupInventory(List<InventoryItem> collectableItems)
	{
		ResetInventory();

		foreach(Item item in collectableItems)
		{
			for(int i = 0; i < m_LevelDictionary.m_LevelData.Length; ++i)
			{
				if(item.GetItemType() == m_LevelDictionary.GetData(i).GetItemType())
				{
					SetItemSlot(m_LevelDictionary.GetData(i));
				}
			}
		}
	}

	//This is called when the Level to add a slot for the item on this level to be collected
	private void SetItemSlot(LevelDictionary.CollectableItems item)
	{
		m_InventorySlots[m_NumberUsedSlots].SetData(item.GetItemType(), item.GetAnimatorController());
		m_InventorySlots[m_NumberUsedSlots].GetAnimator().GetComponent<Image>().enabled = true;
		m_NumberUsedSlots++;
	}

	//Check if the player has picked up the specific item.
	public bool HasItem(Item.ItemType item)
	{
		//Check through item slots.
		//If item collected return true.
		foreach(InventorySlot slot in m_InventorySlots)
		{
			if (slot.GetItemName() == item && slot.IsItemCollected())
				return true;
		}

		return false;
	}

	//Pickup the specified item, return false if pickup failed.
	public bool PickupItem(Item.ItemType item)
	{
		foreach (InventorySlot slot in m_InventorySlots)
		{
			if(slot.GetItemName() == item && !slot.IsItemCollected())
			{
				slot.PickupItem();
				return true;
			}
		}

		return false;
	}

	//Drop the specified item, return false if drop failed.
	public bool DropItem(Item.ItemType item)
	{
		foreach(InventorySlot slot in m_InventorySlots)
		{
			if(slot.GetItemName() == item && slot.IsItemCollected())
			{
				slot.DropItem();
				return true;
			}
		}

		return false;
	}
}
