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

	/// <summary>
	/// Drop the specified item.
	/// </summary>
	/// <param name="item">The item to drop.</param>
	/// <returns>True if the drop is successful, false if not.</returns>
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

	/// <summary>
	/// Does the character have the specified item.
	/// </summary>
	/// <param name="item">The item to look for.</param>
	/// <returns>True if the character has the item, false if not.</returns>
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

	/// <summary>
	/// How many of the specified item does the character have.
	/// </summary>
	/// <param name="item">The item to check.</param>
	/// <returns>The number of the specified item that the character has.</returns>
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

	/// <summary>
	/// Pickup the specified item.
	/// </summary>
	/// <param name="item">The item to pickup.</param>
	/// <returns>True if the pickup is successful, false if not.</returns>
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

	/// <summary>
	/// Reset the inventory.
	/// </summary>
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

	/// <summary>
	/// Setup the item slot.
	/// </summary>
	/// <param name="item">The item to add to the inventory.</param>
	private void SetupItemSlot(LevelDictionary.CollectableItems item)
	{
		m_InventorySlots[m_NumberUsedSlots].SetData(item.GetItemType(), item.GetAnimatorController());
		m_InventorySlots[m_NumberUsedSlots].GetAnimator().GetComponent<Image>().enabled = true;
		m_NumberUsedSlots++;
	}

	/// <summary>
	/// Setup the inventory.
	/// </summary>
	/// <param name="collectableItems"></param>
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
