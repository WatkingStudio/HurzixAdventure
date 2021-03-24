using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class SnowmanAudio
 * 
 * \brief This class is used to hold and control the audio for the Snowman
 * 
 * \date 2020/24/01
 */
public class SnowmanAudio : EnemyAudio
{
	[SerializeField]
	private AudioClip m_AttackAudioClip;

    private new void Start()
    {
		base.Start();
		if(!m_AttackAudioClip)
        {
			Debug.LogError("No Audio Clip has been assigned to " + gameObject.name);
		}
    }

	// Play the Attack Audio Clip.
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
