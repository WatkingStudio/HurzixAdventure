using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
