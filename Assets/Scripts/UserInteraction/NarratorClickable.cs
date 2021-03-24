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
	[SerializeField, Tooltip("The GameObject for when the message is closed")]
	private GameObject m_ClosedMessage;
	[Header("Message Sprites")]
	[SerializeField, Tooltip("The GameObject for when the message is opened.")]
	private GameObject m_OpenMessage;
	[SerializeField]
	private TextMesh m_TextMesh;

	[Space]
	[SerializeField, Tooltip("Set to 'true' if the message is displayed by deafult, set 'false' if not")]
	private bool m_MessageOpened = true;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_CloseMessageAudioClip;
	[SerializeField]
	private AudioClip m_OpenMessageAudioClip;

	private void Start()
	{
		if (!m_TextMesh)
		{
			Debug.LogError("No Text Mesh has been assigned to " + gameObject.name);
		}
		if (!m_OpenMessage)
		{
			Debug.LogError("No Open Message game object has been assigned to " + gameObject.name);
		}
		if (!m_ClosedMessage)
		{
			Debug.LogError("No Closed Message game object has been assigned to " + gameObject.name);
		}
		if (!m_AudioSource)
		{
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		}
		if (!m_OpenMessageAudioClip)
		{
			Debug.LogError("No Open Message Audio Clip has been assigned to " + gameObject.name);
		}
		if (!m_CloseMessageAudioClip)
		{
			Debug.LogError("No Close Message Audio Clip has been assigned to " + gameObject.name);
		}

		m_TextMesh.GetComponent<Renderer>().sortingLayerName = m_OpenMessage.GetComponent<Renderer>().sortingLayerName;
	}

	/// <summary>
	/// Execute this Code when the Narrator is Clicked.
	/// </summary>
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
