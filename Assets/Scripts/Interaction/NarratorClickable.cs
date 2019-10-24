using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorClickable : Clickable
{
	[SerializeField]
	private SpriteRenderer m_SpriteRenderer;
	[SerializeField, Tooltip("The Sprite for when the message is opened.")]
	private Sprite m_OpenMessage;
	[SerializeField, Tooltip("The Sprite for when the message is closed")]
	private Sprite m_ClosedMessage;
	[SerializeField, Tooltip("Set to 'true' if the message is displayed by deafult, set 'false' if not")]
	private bool m_MessageOpened = true;

	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_OpenMessageAudioClip;
	[SerializeField]
	private AudioClip m_CloseMessageAudioClip;

	public override void Clicked()
	{
		base.Clicked();

		if (m_MessageOpened)
		{
			m_SpriteRenderer.sprite = m_ClosedMessage;
			m_AudioSource.clip = m_CloseMessageAudioClip;
		}
		else
		{
			m_SpriteRenderer.sprite = m_OpenMessage;
			m_AudioSource.clip = m_OpenMessageAudioClip;
		}

		m_AudioSource.Play();
		m_MessageOpened = !m_MessageOpened;

	}
}
