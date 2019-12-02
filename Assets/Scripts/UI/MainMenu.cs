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
	private AudioSource m_SceneAudio;
	[SerializeField]
	private AudioClip m_ButtonPressClip;

	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;

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

	public void PlayGame()
	{
		StartCoroutine(PlayGameButton());
	}

	public IEnumerator PlayGameButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Options()
	{
		StartCoroutine(OptionsButton());
	}

	public IEnumerator OptionsButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(false);
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
