﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class EnemyAudio
 * 
 * \brief This class is used to hold and control the audio of an enemy.
 * 
 * \date 2019/24/10
 * 
 */
public class EnemyAudio : MonoBehaviour
{
	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private List<AudioClip> m_WalkAudio;

	private void Start()
	{
		if (!m_AudioSource)
			Debug.LogError("No Audio source connected to " + gameObject.name);

		if (m_WalkAudio.Count == 0)
			Debug.LogError("No Walk Audio has been assigned to " + gameObject.name);
	}

	public void PlayWalkAudioClip()
	{
		if(!m_AudioSource.isPlaying)
		{
			m_AudioSource.clip = m_WalkAudio[Random.Range(0, m_WalkAudio.Count)];
			m_AudioSource.Play();
		}
	}
}
