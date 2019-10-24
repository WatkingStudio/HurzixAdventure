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
	private AudioClip m_HurtAudioClip;
	[SerializeField]
	private AudioClip m_DeathAudioClip;
	[SerializeField]
	private List<AudioClip> m_WalkingGrass;
	[SerializeField]
	private List<AudioClip> m_SprintingGrass;
	[SerializeField]
	private List<AudioClip> m_LandingGrass;
	[SerializeField]
	private AudioClip m_JumpAudioClip;
	[SerializeField]
	private AudioClip m_IndicatorAudioClip;

	public void PlayHurtAudioClip()
	{
		m_AudioSource.Stop();
		m_AudioSource.clip = m_HurtAudioClip;
		m_AudioSource.Play();
	}

	public void PlayDeathAudioClip()
	{
		m_AudioSource.Stop();
		m_AudioSource.clip = m_DeathAudioClip;
		m_AudioSource.Play();
	}

	public void PlayWalkAudioClip()
	{
		if(!m_AudioSource.isPlaying)
		{
			m_AudioSource.clip = m_WalkingGrass[Random.Range(0, m_WalkingGrass.Count)];
			m_AudioSource.Play();
		}		
	}

	public void PlaySprintAudioClip()
	{
		if(!m_AudioSource.isPlaying)
		{
			m_AudioSource.clip = m_SprintingGrass[Random.Range(0, m_SprintingGrass.Count)];
			m_AudioSource.Play();
		}
	}

	public void PlayLandingAudioClip()
	{
		m_AudioSource.Stop();
		m_AudioSource.clip = m_LandingGrass[Random.Range(0, m_LandingGrass.Count)];
		m_AudioSource.Play();
	}

	public void PlayJumpingAudioClip()
	{
		m_AudioSource.Stop();
		m_AudioSource.clip = m_JumpAudioClip;
		m_AudioSource.Play();
	}

	public void PlayerIndicatorAudioClip()
	{
		m_AudioSource.clip = m_IndicatorAudioClip;
		m_AudioSource.Play();
	}
}
