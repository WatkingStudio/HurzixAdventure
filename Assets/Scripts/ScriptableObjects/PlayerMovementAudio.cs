using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementAudio")]
public class PlayerMovementAudio : ScriptableObject
{
	[SerializeField]
	private List<AudioClip> m_WalkingClips;
	[SerializeField]
	private List<AudioClip> m_SprintingClips;
	[SerializeField]
	private List<AudioClip> m_LandingClips;

	public List<AudioClip> Walking { get { return m_WalkingClips; } private set { } }
	public List<AudioClip> Sprinting { get { return m_SprintingClips; } private set { } }
	public List<AudioClip> Landing { get { return m_LandingClips; } private set { } }
}
