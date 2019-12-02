using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class GameMenu
 * 
 * \brief This class is used to control the functionality of the game menu
 * 
 * \date 2019/29/11
 */
public class GameMenu : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_SceneAudio;
	[SerializeField]
	private AudioClip m_ButtonPressClip;

	[SerializeField]
	private GameObject m_GameMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;

	public void Continue()
	{
		StartCoroutine(ContinueButton());
	}

	public IEnumerator ContinueButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_GameMenu.SetActive(false);
	}

	public void Options()
	{
		StartCoroutine(OptionsButton());
	}

	public IEnumerator OptionsButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_GameMenu.SetActive(false);
		m_OptionsMenu.SetActive(true);
	}

	public void QuitGame()
	{
		StartCoroutine(QuitGameButton());
	}

	public IEnumerator QuitGameButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		Debug.Log("QUIT!");
		Application.Quit();
	}

	private void PlayButtonClick()
	{
		m_SceneAudio.Stop();
		m_SceneAudio.clip = m_ButtonPressClip;
		m_SceneAudio.Play();
	}
}
