using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class InventoryItem : Item
{
	[SerializeField, Tooltip("Which layers this object should be able to collect this item")] private LayerMask m_InteractableLayers;
	[SerializeField, Tooltip("When this object is picked up should it be disabled")] private bool m_DisableOnEnter = false;

	[SerializeField] private CircleCollider2D m_Collider;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Check if the colliding object is on an interactable layer
		if((m_InteractableLayers.value & 1 << collision.gameObject.layer) != 0)
		{
			Inventory inv = collision.GetComponentInChildren<Inventory>();
			
			if(inv.PickupItem(m_ItemType))
			{
				if (m_DisableOnEnter)
				{
					gameObject.SetActive(false);
				}
			}
			else
			{
				Debug.Log("Item Could Not be Picked Up!");
			}
			
		}
	}
}
