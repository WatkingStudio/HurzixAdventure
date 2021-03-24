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

	/// <summary>
	/// Drop the item in this slot.
	/// </summary>
	public void DropItem()
	{
		m_IsCollected = false;
		m_Animation.SetTrigger("Drop");
	}

	/// <summary>
	/// Get this inventory slots animator.
	/// </summary>
	/// <returns>The animator.</returns>
	public Animator GetAnimator()
	{
		return m_Animation;
	}

	/// <summary>
	/// Get the item type of the item in this slot.
	/// </summary>
	/// <returns>The item type.</returns>
	public Item.ItemType GetItemName()
	{
		return m_ItemName;
	}

	/// <summary>
	/// Has an item been collected for this slot.
	/// </summary>
	/// <returns>True if an item has been collected, false if not.</returns>
	public bool IsItemCollected()
	{
		return m_IsCollected;
	}

	/// <summary>
	/// Pickup and item.
	/// </summary>
	public void PickupItem()
	{
		m_IsCollected = true;
		m_Animation.SetTrigger("Pickup");
	}

	/// <summary>
	/// Set the data for this inventory slot.
	/// </summary>
	/// <param name="item">The item to add to the inventory slot.</param>
	/// <param name="animator">The runtime animator controller for the item.</param>
	public void SetData(Item.ItemType item, RuntimeAnimatorController animator)
	{
		m_ItemName = item;
		m_Animation.runtimeAnimatorController = animator;
	}
}
