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
	[Header("Audio Clips")]
	[SerializeField]
	private AudioClip m_HurtAudioClip;
	[SerializeField]
	private AudioClip m_DeathAudioClip;
	[SerializeField]
	private AudioClip m_JumpAudioClip;
	[SerializeField]
	private AudioClip m_IndicatorAudioClip;
	[SerializeField]
	private List<AudioClip> m_WalkingGrass;
	[SerializeField]
	private List<AudioClip> m_SprintingGrass;
	[SerializeField]
	private List<AudioClip> m_LandingGrass;

	private void Start()
	{
		if (!m_AudioSource)
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		if (!m_HurtAudioClip)
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for being hurt");
		if (!m_DeathAudioClip)
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for dying");
		if (!m_JumpAudioClip)
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for jumping");
		if (!m_IndicatorAudioClip)
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for the indicator");
		if (m_WalkingGrass.Count == 0)
			Debug.LogWarning("No Audio Clips have been assigned to " + gameObject.name + " for walking on grass");
		if (m_SprintingGrass.Count == 0)
			Debug.LogWarning("No Audio Clips have been assigned to " + gameObject.name + " for sprinting on grass");
		if (m_LandingGrass.Count == 0)
			Debug.LogWarning("No Audio Clips have been assigned to " + gameObject.name + " for landing on the grass");
	}

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
