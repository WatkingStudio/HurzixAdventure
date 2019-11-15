using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class NarratorClickable
 * 
 * \brief This class holds the code for when a Narrator window is clicked
 * 
 * \date 2019/24/10
 * 
 */
public class NarratorClickable : Clickable
{
	[SerializeField]
	private TextMesh m_TextMesh;
	[Header("Message Sprites")]
	[SerializeField, Tooltip("The GameObject for when the message is opened.")]
	private GameObject m_OpenMessage;
	[SerializeField, Tooltip("The GameObject for when the message is closed")]
	private GameObject m_ClosedMessage;
	[Space]
	[SerializeField, Tooltip("Set to 'true' if the message is displayed by deafult, set 'false' if not")]
	private bool m_MessageOpened = true;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_OpenMessageAudioClip;
	[SerializeField]
	private AudioClip m_CloseMessageAudioClip;

	public override void Clicked()
	{
		base.Clicked();

		m_MessageOpened = !m_MessageOpened;

		if (m_MessageOpened)
		{
			m_OpenMessage.SetActive(false);
			m_ClosedMessage.SetActive(true);
			m_AudioSource.clip = m_CloseMessageAudioClip;
			m_TextMesh.gameObject.SetActive(false);
		}
		else
		{
			m_OpenMessage.SetActive(true);
			m_ClosedMessage.SetActive(false);
			m_AudioSource.clip = m_OpenMessageAudioClip;
			m_TextMesh.gameObject.SetActive(true);
		}

		m_AudioSource.Play();

	}
}
