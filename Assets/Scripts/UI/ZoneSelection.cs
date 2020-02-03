using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * \class ZoneSelection
 * 
 * \brief This class is used to control the functionality of the Zone Selection menu
 * 
 * \date 2020/03/02
 */ 
public class ZoneSelection : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_SceneAudio;
	[SerializeField]
	private AudioClip m_ButtonPressClip;
	[SerializeField]
	private PlayerGlobals m_PlayerVariables;

	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private GameObject m_ZoneSelectionMenu;

	public void TutorialButtonPressed()
	{
		StartCoroutine(TutorialButton());
	}

	private IEnumerator TutorialButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_ONE);
	}

	public void JungleButtonPressed()
	{
		StartCoroutine(JungleButton());
	}

	private IEnumerator JungleButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_FOUR);
	}

	public void VolcanoButtonPressed()
	{
		StartCoroutine(VolcanoButton());
	}

	private IEnumerator VolcanoButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_SEVEN);
	}

	public void SpookyButtonPressed()
	{
		StartCoroutine(SpookyButton());
	}

	private IEnumerator SpookyButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_NINE);
	}

	public void SnowButtonPrtessed()
	{
		StartCoroutine(SnowButton());
	}

	private IEnumerator SnowButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_ELEVEN);
	}

	private void PlayButtonClick()
	{
		m_SceneAudio.Stop();
		m_SceneAudio.clip = m_ButtonPressClip;
		m_SceneAudio.Play();
	}

	private void ResetPlayerVariables()
	{
		m_PlayerVariables.PlayerScore = 0;
		m_PlayerVariables.PlayerHealth = m_PlayerVariables.DefaultHealth;
	}

	public void Back()
	{
		StartCoroutine(BackButton());
	}

	private IEnumerator BackButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(true);
		m_ZoneSelectionMenu.SetActive(false);
	}
}
