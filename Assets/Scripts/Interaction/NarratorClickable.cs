using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorClickable : Clickable
{
	[SerializeField]
	private SpriteRenderer m_SpriteRenderer;
	[SerializeField]
	private Sprite m_OpenMessage;
	[SerializeField]
	private Sprite m_ClosedMessage;
	[SerializeField]
	private bool m_MessageOpened = true;

	public override void Clicked()
	{
		base.Clicked();

		if (m_MessageOpened)
			m_SpriteRenderer.sprite = m_ClosedMessage;
		else
			m_SpriteRenderer.sprite = m_OpenMessage;

		m_MessageOpened = !m_MessageOpened;

	}
}
