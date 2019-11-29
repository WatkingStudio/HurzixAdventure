using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class GameAudioSO
 * 
 * \brief This ScriptableObject is used to control the audio volume of the game
 * 
 * \date 2019/29/11 
 */
[CreateAssetMenu(fileName = "GameAudioSO")]
public class GameAudioSO : ScriptableObject
{
	[SerializeField, Range(0, 1)]
	private float m_BackgroundVolume = 0.5f;
	[SerializeField, Range(0, 1)]
	private float m_SoundEffectVolume = 0.5f;

	public float BackgroundVolume { get { return m_BackgroundVolume; } private set { } }
	public float SoundEffectVolume { get { return m_SoundEffectVolume; } private set { } }

	public void SetBackgroundVolume(float volume)
	{
		m_BackgroundVolume = volume;
	}

	public void SetSoundEffectVolume(float volume)
	{
		m_SoundEffectVolume = volume;
	}
}
