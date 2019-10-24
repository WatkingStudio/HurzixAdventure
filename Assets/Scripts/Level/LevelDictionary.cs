using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelDictionary
 * 
 * \brief This ScriptableObject is used to hold a Dictionary of Items and Animators.
 * 
 *  There is a different LevelDictionary for each level.
 * 
 * \date 2019/21/10
 */
[CreateAssetMenu(fileName = "LevelDictionary")]
public class LevelDictionary : ScriptableObject
{
	[System.Serializable]
	public struct CollectableItems
	{
		[SerializeField]
		private Item.ItemType ItemType;
		[SerializeField]
		private RuntimeAnimatorController AnimationController;

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
