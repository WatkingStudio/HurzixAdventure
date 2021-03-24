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
		{
			Debug.LogError("No Inventory Slots have been assigned to " + gameObject.name);
		}
		if (!m_LevelDictionary)
		{
			Debug.LogError("No Level Dictionary has been assigned to " + gameObject.name);
		}
		if (!m_BlankInventorySlot)
		{
			Debug.LogError("No Blank Inventory Animator Controller has been assigned to " + gameObject.name);
		}
	}

	// Drop the Specified Item.
	// @return True if the Drop is Successful, False if Not.
	public bool DropItem(Item.ItemType item)
	{
		foreach (InventorySlot slot in m_InventorySlots)
		{
			if (slot.GetItemName() == item && slot.IsItemCollected())
			{
				slot.DropItem();
				return true;
			}
		}

		return false;
	}

	// Does the Character Have the Specified item.
	// @return True if the Character has the Item, False if Not.
	public bool HasItem(Item.ItemType item)
	{
		//Check through item slots.
		//If item collected return true.
		foreach (InventorySlot slot in m_InventorySlots)
		{
			if (slot.GetItemName() == item && slot.IsItemCollected())
				return true;
		}

		return false;
	}

	// How Many of the Specified Item Does the Character Have.
	// @return The Number of the Specified Item That the Character Has.
	public int NumberOfItem(Item.ItemType item)
	{
		int val = 0;
		foreach (InventorySlot slot in m_InventorySlots)
		{
			if (slot.GetItemName() == item && slot.IsItemCollected())
				val++;
		}
		return val;
	}

	// Pickup the Specified Item.
	// @return True if the Pickup is Successful, False if Not.
	public bool PickupItem(Item.ItemType item)
	{
		foreach (InventorySlot slot in m_InventorySlots)
		{
			if (slot.GetItemName() == item && !slot.IsItemCollected())
			{
				slot.PickupItem();
				return true;
			}
		}

		return false;
	}

	// Reset the Inventory.
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

	// Setup the Item Slot
	private void SetupItemSlot(LevelDictionary.CollectableItems item)
	{
		m_InventorySlots[m_NumberUsedSlots].SetData(item.GetItemType(), item.GetAnimatorController());
		m_InventorySlots[m_NumberUsedSlots].GetAnimator().GetComponent<Image>().enabled = true;
		m_NumberUsedSlots++;
	}

	// Setup the Inventory.
	public void SetupInventory(List<InventoryItem> collectableItems)
	{
		ResetInventory();

		foreach(Item item in collectableItems)
		{
			for(int i = 0; i < m_LevelDictionary.m_LevelData.Length; ++i)
			{
				if(item.GetItemType() == m_LevelDictionary.GetData(i).GetItemType())
				{
					SetupItemSlot(m_LevelDictionary.GetData(i));
				}
			}
		}
	}
}
