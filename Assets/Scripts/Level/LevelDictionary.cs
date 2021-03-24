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

		/// <summary>
		/// Get the animator controller of the item.
		/// </summary>
		/// <returns>The runtime animator controller of the item.</returns>
		public RuntimeAnimatorController GetAnimatorController()
		{
			return AnimationController;
		}

		/// <summary>
		/// Get the item type of the item.
		/// </summary>
		/// <returns>The item type of the item.</returns>
		public Item.ItemType GetItemType()
		{
			return ItemType;
		}
	}

	[SerializeField] public CollectableItems[] m_LevelData;
	
	/// <summary>
	/// Get the data from the specified level.
	/// </summary>
	/// <param name="numerator">The index.</param>
	/// <returns>The collectable items data for the specified level.</returns>
	public CollectableItems GetData(int numerator)
	{
		return m_LevelData[numerator];
	}
}
