using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class InventoryItem
 * 
 * \brief This class inherits from the Item class and adds functionality to be used with the Inventory
 *         class.
 *         
 * \date 2019/21/10
 */ 
[RequireComponent(typeof(CircleCollider2D))]
public class InventoryItem : Item
{
	[SerializeField, Tooltip("Which layers this object should be able to collect this item")]
	private LayerMask m_InteractableLayers;
	[SerializeField, Tooltip("When this object is picked up should it be disabled")]
	private bool m_DisableOnEnter = false;

	[SerializeField]
	private CircleCollider2D m_Collider;
	[SerializeField]
	private Behaviour m_ItemHalo;
	[SerializeField]
	private ItemAudio m_ItemAudio;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Check if the colliding object is on an interactable layer
		if((m_InteractableLayers.value & 1 << collision.gameObject.layer) != 0)
		{
			Inventory inv = collision.GetComponentInChildren<Inventory>();
			
			if(inv.PickupItem(m_ItemType))
			{
				if (m_ItemAudio != null)
					m_ItemAudio.PlayAudioClip();
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

	//The parameter for this function is passed in so that this function can get access
	// to the components of the player.
	public void RevealItem(Transform playerTransform)
	{
		//This if statement will trigger if the sprite is visable on the Scene View
		if (GetComponent<SpriteRenderer>().isVisible)
		{
			StartCoroutine(EnableHalo());
		}
		else
		{
			playerTransform.GetComponentInChildren<Indicator>().DisplayIndicator(transform);
		}
	}

	IEnumerator EnableHalo()
	{
		m_ItemHalo.enabled = true;
		yield return new WaitForSeconds(2f);
		m_ItemHalo.enabled = false;
	}
}
