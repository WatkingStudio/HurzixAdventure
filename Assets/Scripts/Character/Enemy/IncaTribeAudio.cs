using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncaTribeAudio : EnemyAudio
{
	[SerializeField]
	private AudioClip m_AttackAudioClip;

	public void PlayerAttackAudioClip()
	{
		if (m_AudioSource.isPlaying)
			m_AudioSource.Stop();

		m_AudioSource.clip = m_AttackAudioClip;
		m_AudioSource.Play();
	}
}
