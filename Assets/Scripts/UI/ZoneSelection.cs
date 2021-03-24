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
	private AudioClip m_ButtonPressClip;
	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private PlayerGlobals m_PlayerVariables;
	[SerializeField]
	private AudioSource m_SceneAudio;
	[SerializeField]
	private GameObject m_ZoneSelectionMenu;

    private void Start()
    {
        if(!m_ButtonPressClip)
        {
			Debug.LogError("No Audio Clip has been assigned to " + gameObject.name);
		}
		if(!m_MainMenu)
        {
			Debug.LogError("No Main Menu has been assigned to " + gameObject.name);
		}
		if(!m_PlayerVariables)
        {
			Debug.LogError("No Player Globals has been assigned to " + gameObject.name);
		}
		if(!m_SceneAudio)
        {
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name);
		}
		if(!m_ZoneSelectionMenu)
        {
			Debug.LogError("No Zone Selection Menu has been assigned to " + gameObject.name);
		}
    }

	/// <summary>
	/// Go Back to the Main Menu
	/// </summary>
    public void Back()
	{
		StartCoroutine(BackButton());
	}

	/// <summary>
	/// Process the Back Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator BackButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(true);
		m_ZoneSelectionMenu.SetActive(false);
	}

	/// <summary>
	/// Start the Game in the Jungle Zone.
	/// </summary>
	public void JungleButtonPressed()
	{
		StartCoroutine(JungleButton());
	}

	/// <summary>
	/// Process the Jungle Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator JungleButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_FOUR);
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
	/// Reset the Players Variables.
	/// </summary>
	private void ResetPlayerVariables()
	{
		m_PlayerVariables.PlayerScore = 0;
		m_PlayerVariables.PlayerHealth = m_PlayerVariables.DefaultHealth;
	}

	/// <summary>
	/// Start the Game in the Snow Zone.
	/// </summary>
	public void SnowButtonPressed()
	{
		StartCoroutine(SnowButton());
	}

	/// <summary>
	/// Process the Snow Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator SnowButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_ELEVEN);
	}

	/// <summary>
	/// Start the Game in the Spooky Zone.
	/// </summary>
	public void SpookyButtonPressed()
	{
		StartCoroutine(SpookyButton());
	}

	/// <summary>
	/// Process the Spooky Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator SpookyButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_NINE);
	}

	/// <summary>
	/// Start the Game in the Tutorial Zone.
	/// </summary>
	public void TutorialButtonPressed()
	{
		StartCoroutine(TutorialButton());
	}

	/// <summary>
	/// Process the Tutorial Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator TutorialButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_ONE);
	}

	/// <summary>
	/// Start the Game in the Volcano Zone.
	/// </summary>
	public void VolcanoButtonPressed()
	{
		StartCoroutine(VolcanoButton());
	}

	/// <summary>
	/// Process the Volcano Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	private IEnumerator VolcanoButton()
	{
		ResetPlayerVariables();
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		SceneManager.LoadScene((int)LevelTransition.Levels.LEVEL_SEVEN);
	}
}
