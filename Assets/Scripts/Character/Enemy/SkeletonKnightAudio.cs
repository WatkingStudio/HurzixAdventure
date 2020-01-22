using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class SkeletonKnightAudio
 * 
 * \brief This class is used to hold and control the audio for the Skeleton Knight Audio
 * 
 * \date 2020/22/01
 */ 
public class SkeletonKnightAudio : EnemyAudio
{
	[SerializeField]
	private AudioClip m_AttackAudioClip;

    public void PlayAttackAudioClip()
	{
		if (m_AudioSource.isPlaying)
			m_AudioSource.Stop();
		
		m_AudioSource.clip = m_AttackAudioClip;
		m_AudioSource.Play();
	}
}
