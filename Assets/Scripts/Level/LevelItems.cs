using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelItems : MonoBehaviour
{
	[SerializeField, Tooltip("This is a list of items that can be collected on this level")] private List<Item> m_CollectableItems;

	[SerializeField] Inventory m_Inventory;

	private void Start()
	{
		m_Inventory.SetupInventory(m_CollectableItems);
	}
}
