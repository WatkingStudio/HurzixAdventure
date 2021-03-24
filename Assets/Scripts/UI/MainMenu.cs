using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * \class MainMenu
 * 
 * \brief This class is used to control the functionality of the main menu
 * 
 * \date 2019/29/11
 */
public class MainMenu : MonoBehaviour
{
	[SerializeField]
	private AudioClip m_ButtonPressClip;
	[SerializeField]
	private PlayerGlobals m_PlayerVariables;
	[SerializeField]
	private AudioSource m_SceneAudio;

	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;
	[SerializeField]
	private GameObject m_ZoneSelectionMenu;

	private void Start()
	{
		if (!m_SceneAudio)
			Debug.LogError("No Audio Source has been added to " + gameObject.name);
		if (!m_ButtonPressClip)
			Debug.LogError("No Button Press Audio Clip has been assigned to " + gameObject.name);
		if (!m_MainMenu)
			Debug.LogError("No Main Menu has been assigned to " + gameObject.name);
		if (!m_OptionsMenu)
			Debug.LogError("No Options Menu has been assigned to " + gameObject.name);
	}

	/// <summary>
	/// Open the Options Menu.
	/// </summary>
	public void Options()
	{
		StartCoroutine(OptionsButton());
	}

	/// <summary>
	/// Process the Options Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator OptionsButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(false);
		m_OptionsMenu.SetActive(true);
	}

	/// <summary>
	/// Play the Button Click Audio.
	/// </summary>
	private void PlayButtonClick()
	{
		m_SceneAudio.Stop();
		m_SceneAudio.clip = m_ButtonPressClip;
		m_SceneAudio.Play();
	}

	/// <summary>
	/// Start the Game.
	/// </summary>
	public void PlayGame()
	{
		StartCoroutine(PlayGameButton());
	}

	/// <summary>
	/// Process the Play Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator PlayGameButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	/// <summary>
	/// Quit the Game.
	/// </summary>
	public void QuitGame()
	{
		StartCoroutine(QuitGameButton());
	}

	/// <summary>
	/// Process the Quit Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator QuitGameButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		Debug.Log("QUIT!");
		Application.Quit();
	}

	/// <summary>
	/// Reset the Player Variables.
	/// </summary>
	private void ResetPlayerVariables()
	{
		m_PlayerVariables.PlayerScore = 0;
		m_PlayerVariables.PlayerHealth = m_PlayerVariables.DefaultHealth;
	}

	/// <summary>
	/// Open the Zone Selection Screen.
	/// </summary>
	public void ZoneSelection()
	{
		m_MainMenu.SetActive(false);
		m_ZoneSelectionMenu.SetActive(true);
	}
}
