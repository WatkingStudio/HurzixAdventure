using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
	private AudioClip m_ButtonPressClip;
	[SerializeField]
	private PlayerCharacter m_PlayerCharacter;
	[SerializeField]
	private AudioSource m_SceneAudio;

	[Header("UI Items")]
	[SerializeField]
	private GameObject m_GameMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;

	private void Start()
	{
		if (!m_SceneAudio)
		{
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		}
		if (!m_ButtonPressClip)
		{
			Debug.LogError("No Button Press Audio Clip has been assigned to " + gameObject.name);
		}
		if (!m_PlayerCharacter)
		{
			Debug.LogError("No Player Character has been assigned to " + gameObject.name);
		}
		if (!m_GameMenu)
		{
			Debug.LogError("No Game Menu has been assigned to " + gameObject.name);
		}
		if (!m_OptionsMenu)
		{
			Debug.LogError("No Options Menu has been assigned to " + gameObject.name);
		}
	}

	// Continue With the Game.
	public void Continue()
	{
		StartCoroutine(ContinueButton());
	}

	// Process the Continue Button Being Clicked.
	// @return The Current IEnumerator Step.
	public IEnumerator ContinueButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_GameMenu.SetActive(false);
	}

	// Open the Options Menu.
	public void Options()
	{
		StartCoroutine(OptionsButton());
	}

	// Process the Options Button Being Clicked.
	// @return The Current IEnumerator Step.
	public IEnumerator OptionsButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_GameMenu.SetActive(false);
		m_OptionsMenu.SetActive(true);
	}

	// Play the Buton Click Audio.
	private void PlayButtonClick()
	{
		m_SceneAudio.Stop();
		m_SceneAudio.clip = m_ButtonPressClip;
		m_SceneAudio.Play();
	}

	// Quit the Game.
	public void QuitGame()
	{
		StartCoroutine(QuitGameButton());
	}

	// @return The Current IEnumerator Step.
	public IEnumerator QuitGameButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		Debug.Log("QUIT!");
		SceneManager.LoadScene((int)LevelTransition.Levels.MAIN_MENU);
	}

	// Respawn the Player.
	public void RespawnPlayer()
	{
		m_PlayerCharacter.OnRespawn(true);
	}
}
