using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
	[SerializeField] private Item.ItemType m_ItemName = Item.ItemType.None;
	[SerializeField] private bool m_IsCollected = false;
	[SerializeField] private Animator m_Animation;

	public Item.ItemType GetItemName()
	{
		return m_ItemName;
	}

	public bool IsItemCollected()
	{
		return m_IsCollected;
	}

	public Animator GetAnimator()
	{
		return m_Animation;
	}

	public void SetData(Item.ItemType item, RuntimeAnimatorController animator)
	{
		m_ItemName = item;
		m_Animation.runtimeAnimatorController = animator;
	}

	public void PickupItem()
	{
		m_IsCollected = true;
		m_Animation.SetTrigger("Pickup");
	}

	public void DropItem()
	{
		m_IsCollected = false;
		m_Animation.SetTrigger("Drop");
	}
}
