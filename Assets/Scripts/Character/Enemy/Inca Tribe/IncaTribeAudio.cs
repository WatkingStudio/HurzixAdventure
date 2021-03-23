using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class IncaTribeAudio
 * 
 * \brief This class is used to hold and control the audio for the Inca Tribe Enemy
 * 
 * \date 2020/22/01
 */ 
public class IncaTribeAudio : EnemyAudio
{
	[SerializeField]
	private AudioClip m_AttackAudioClip;

    private new void Start()
    {
		base.Start();

		if(!m_AttackAudioClip)
        {
			Debug.LogError("No Attack Audio Clip attached to " + gameObject.name);
		}
    }

    // Plays the Attack Audio Clip.
    public void PlayAttackAudioClip()
	{
		if (m_AudioSource.isPlaying)
		{
			m_AudioSource.Stop();
		}

		m_AudioSource.clip = m_AttackAudioClip;
		m_AudioSource.Play();
	}
}
