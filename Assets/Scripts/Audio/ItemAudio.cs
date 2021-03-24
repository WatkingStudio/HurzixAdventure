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
	[Header("Audio")]
	[SerializeField]
	private AudioClip m_AudioClip;
	[SerializeField]
	private AudioSource m_AudioSource;

	private void Start()
	{
		if (!m_AudioSource)
		{
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		}
		if (!m_AudioClip)
		{
			Debug.LogError("No Audio Clip has been assigned to " + gameObject.name);
		}
	}

	/// <summary>
	/// Play the attached audio clip.
	/// </summary>
	public void PlayAudioClip()
	{
		m_AudioSource.clip = m_AudioClip;
		m_AudioSource.Play();
	}
}
