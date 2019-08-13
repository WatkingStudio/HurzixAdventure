using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDictionary")]
public class LevelDictionary : ScriptableObject
{
	[System.Serializable]
	public struct CollectableItems
	{
		[SerializeField] private Item.ItemType ItemType;
		[SerializeField] private RuntimeAnimatorController AnimationController;

		public Item.ItemType GetItemType()
		{
			return ItemType;
		}

		public RuntimeAnimatorController GetAnimatorController()
		{
			return AnimationController;
		}
	}

	[SerializeField] public CollectableItems[] m_LevelData;
	
	public CollectableItems GetData(int numerator)
	{
		return m_LevelData[numerator];
	}
}
