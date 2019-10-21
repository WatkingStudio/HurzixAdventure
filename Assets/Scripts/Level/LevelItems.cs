using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItems : MonoBehaviour
{
	[SerializeField, Tooltip("This is a list of items that can be collected on this level")] private List<InventoryItem> m_CollectableItems;

	[SerializeField] Inventory m_Inventory;

	private void Start()
	{
		m_Inventory.SetupInventory(m_CollectableItems);
	}

	public void RevealClosestItem(Transform playerTransform)
	{
		float shortestDistance = float.MaxValue;
		int closestKey = -1;

		for(int i = 0; i < m_CollectableItems.Count; ++i)
		{
			if(m_CollectableItems[i].gameObject.activeSelf)
			{
				float tempDistance = Vector3.Distance(m_CollectableItems[i].transform.position, playerTransform.position);
				if (tempDistance < shortestDistance)
				{
					closestKey = i;
					shortestDistance = tempDistance;
				}
			}			
		} 

		//closestKey should now be revealed
		if(closestKey > -1)
			m_CollectableItems[closestKey].RevealItem(playerTransform);
		
	}
}
