using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerGlobals")]
public class PlayerGlobals : ScriptableObject
{
	[SerializeField]
	private int m_PlayerHealth;
	[SerializeField]
	private int m_PlayerScore;

	public int PlayerHealth { get { return m_PlayerHealth; } set { m_PlayerHealth = value; } }
	public int PlayerScore { get { return m_PlayerScore; } set { m_PlayerScore = value; } }
}
