using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Coin
 * 
 * \brief This class is used to control a Coin
 * 
 * \date 2019/02/12
 */ 
public class Coin : MonoBehaviour
{
	[Header("Collision Variables")]
	[SerializeField, Tooltip("Which layers this object should be able to collect this item")]
	private LayerMask m_InteractableLayers;
	[SerializeField, Tooltip("When this object is picked up should it be disabled")]
	private bool m_DisableOnEnter = false;
	[SerializeField]
	private ItemAudio m_ItemAudio;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.GetComponentInParent<PlayerController>())
			if (collision.GetComponentInParent<PlayerController>().PriorityCollider != collision)
				return;

		if((m_InteractableLayers.value & 1 << collision.gameObject.layer) != 0)
		{
			PlayerCharacter pc = collision.GetComponentInParent<PlayerCharacter>();
			if(pc != null)
			{
				if(gameObject.activeSelf)
				{
					m_ItemAudio.PlayAudioClip();
					pc.CollectCoin();
					if (m_DisableOnEnter)
						gameObject.SetActive(false);
				}
			}
		}
	}
}
