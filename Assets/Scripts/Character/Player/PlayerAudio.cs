using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class PlayerAudio
 * 
 * \brief This class is used to hold and control the audio of the player.
 * 
 * \date 2019/15/10
 * 
 */
public class PlayerAudio : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	public AudioClip m_HurtAudioClip;
	[SerializeField]
	public AudioClip m_DeathAudioClip;

	public void PlayHurtAudioClip()
	{
		m_AudioSource.clip = m_HurtAudioClip;
		m_AudioSource.Play();
	}

	public void PlayDeathAudioClip()
	{
		m_AudioSource.clip = m_DeathAudioClip;
		m_AudioSource.Play();
	}
}
