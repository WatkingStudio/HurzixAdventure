using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class InventorySlot
 * 
 * \brief This class holds the information for an InventorySlot. 
 * 
 * This will have a specific item assigned to it, and if the player collects the item this class will
 *  execute the code the handle the collection.
 *  
 * \date 2019/21/10
 */ 
public class InventorySlot : MonoBehaviour
{
	[SerializeField]
	private Animator m_Animation;
	[SerializeField]
	private bool m_IsCollected = false;
	[SerializeField]
	private Item.ItemType m_ItemName = Item.ItemType.None;

	private void Start()
	{
		if (!m_Animation)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
	}

	// Drop the Item in This Slot.
	public void DropItem()
	{
		m_IsCollected = false;
		m_Animation.SetTrigger("Drop");
	}

	// Get This Inventory Slots Animator.
	// @return The Animator.
	public Animator GetAnimator()
	{
		return m_Animation;
	}

	// Get the ItemType of the Item in This Slot.
	// @return The Item Type.
	public Item.ItemType GetItemName()
	{
		return m_ItemName;
	}

	// Has an Item Been Collected for This Slot.
	// @return True if an Item Has Been Collected, False if Not.
	public bool IsItemCollected()
	{
		return m_IsCollected;
	}

	// Pickup An Item.
	public void PickupItem()
	{
		m_IsCollected = true;
		m_Animation.SetTrigger("Pickup");
	}

	// Set the Data For This Inventory Slot.
	public void SetData(Item.ItemType item, RuntimeAnimatorController animator)
	{
		m_ItemName = item;
		m_Animation.runtimeAnimatorController = animator;
	}
}
