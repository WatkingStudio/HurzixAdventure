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

		// Get The Animator Controller of the Item.
		// @return The Runtime Animator Controller of the Item.
		public RuntimeAnimatorController GetAnimatorController()
		{
			return AnimationController;
		}

		// Get the Item Type of the Item.
		// @return The Item Type of the Item.
		public Item.ItemType GetItemType()
		{
			return ItemType;
		}
	}

	[SerializeField] public CollectableItems[] m_LevelData;
	
	// Get the Data From the Specified Level.
	// @return The CollectableItems Data for the Specified Level.
	public CollectableItems GetData(int numerator)
	{
		return m_LevelData[numerator];
	}
}
