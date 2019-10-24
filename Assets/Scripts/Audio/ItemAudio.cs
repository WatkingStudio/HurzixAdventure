using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class ItemAudio
 * 
 * \brief This class is used to hold and control the audio of an item.
 * 
 * \date 2019/24/10
 * 
 */
public class ItemAudio : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_AudioClip;

	public void PlayAudioClip()
	{
		m_AudioSource.clip = m_AudioClip;
		m_AudioSource.Play();
	}
}
