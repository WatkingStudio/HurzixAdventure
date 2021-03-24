using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class LevelAudioManager
 * 
 * \brief This class is used to assign the audio settings to all the AudioSource's in the scene
 *		   based on the values set in the GameAudioSO ScriptableObject.
 * 
 * \date 2019/29/11
 */
public class LevelAudioManager : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_BackgroundAudioSource;
	[SerializeField]
	private GameAudioSO m_GameAudio;
	[SerializeField]
	private List<AudioSource> m_SoundEffectsAudioSources;


	private void Start()
	{
		if (!m_GameAudio)
		{
			Debug.LogError("No GameAudioSO has been assigned to " + gameObject.name);
		}
		if (!m_BackgroundAudioSource)
		{
			Debug.LogError("No Background Audio Source has been assigned to " + gameObject.name);
		}
		if (m_SoundEffectsAudioSources.Count <= 0)
		{
			Debug.LogError("No Sound Effects Audio Sources have been assigned to " + gameObject.name);
		}

		UpdateLevelAudio();
	}

	// Set the Background Audio Volume.
	private void SetBackgroundAudioVolume()
	{
		m_BackgroundAudioSource.volume = m_GameAudio.BackgroundVolume;
	}

	// Set the Sound Effects Audio Volume.
	private void SetSoundEffectsAudioVolume()
	{
		foreach(AudioSource au in m_SoundEffectsAudioSources)
		{
			au.volume = m_GameAudio.SoundEffectVolume;
		}
	}

	// Update the Levels Audio.
	public void UpdateLevelAudio()
	{
		SetBackgroundAudioVolume();
		SetSoundEffectsAudioVolume();
	}
}
