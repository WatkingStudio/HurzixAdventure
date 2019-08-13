using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDictionary")]
public class LevelDictionary : ScriptableObject
{
	[System.Serializable]
	public struct data
	{
		public Item.ItemType name;
		public RuntimeAnimatorController anim;
	}

	[SerializeField] public data[] m_LevelData;
	public int numbers;
}
