using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelItems
 * 
 * \brief This class holds the data for each item in the level.
 * 
 * \date 2019/15/10
 * 
 */
public class LevelItems : MonoBehaviour
{
	[SerializeField, Tooltip("This is a list of items that can be collected on this level")]
	private List<InventoryItem> m_CollectableInventoryItems;

	[Space]
	[SerializeField]
	private Inventory m_Inventory;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_RevealAudioClip;

	private void Start()
	{
		if (m_CollectableInventoryItems.Count == 0)
			Debug.LogError("Collectable Items Set To 0 for " + gameObject.name);
		if (!m_Inventory)
			Debug.LogError("No Inventory has been assigned to " + gameObject.name);
		if (!m_AudioSource)
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		if (!m_RevealAudioClip)
			Debug.LogError("No Audio Clip has been assigned to " + gameObject.name);

		m_Inventory.SetupInventory(m_CollectableInventoryItems);
	}

	public void RevealClosestItem(Transform playerTransform)
	{
		float shortestDistance = float.MaxValue;
		int closestKey = -1;

		for(int i = 0; i < m_CollectableInventoryItems.Count; ++i)
		{
			if(m_CollectableInventoryItems[i].gameObject.activeSelf)
			{
				float tempDistance = Vector3.Distance(m_CollectableInventoryItems[i].transform.position, playerTransform.position);
				if (tempDistance < shortestDistance)
				{
					closestKey = i;
					shortestDistance = tempDistance;
				}
			}			
		}

		//closestKey should now be revealed
		if (closestKey > -1)
		{
			m_CollectableInventoryItems[closestKey].RevealItem(playerTransform);
			m_AudioSource.clip = m_RevealAudioClip;
			m_AudioSource.Play();
		}
		
	}
}
