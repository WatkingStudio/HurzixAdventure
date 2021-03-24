using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * \class Options Menu
 * 
 * \brief This class is used to control the functionality of the options menu
 * 
 * \date 2019/29/11
 */
public class OptionsMenu : MonoBehaviour
{
	[SerializeField]
	private Slider m_BackgroundAudioSlider;
	[SerializeField]
	private AudioClip m_ButtonPressClip;
	[SerializeField]
	private GameAudioSO m_GameAudio;
	[SerializeField]
	private LevelAudioManager m_LevelAudio;
	[SerializeField]
	private bool m_LiveUpdate = false;
	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;
	[SerializeField]
	private AudioSource m_SceneAudio;
	[SerializeField]
	private Slider m_SoundEffectAudioSlider;

	private void Start()
	{
		if (!m_SceneAudio)
		{
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name); ;
		}
		if (!m_ButtonPressClip)
		{
			Debug.Log("No Button Press Audio Clip has been assigned to " + gameObject.name);
		}
		if (!m_MainMenu)
		{
			Debug.LogError("No Main Menu has been assigned to " + gameObject.name);
		}
		if (!m_OptionsMenu)
		{
			Debug.LogError("No Options Menu has been assigned to " + gameObject.name);
		}
		if (!m_GameAudio)
		{
			Debug.LogError("No GameAudioSO has been assigned to " + gameObject.name);
		}
		if (!m_BackgroundAudioSlider)
		{
			Debug.LogError("No Background Audio Slider has been assigned to " + gameObject.name);
		}
		if (!m_SoundEffectAudioSlider)
		{
			Debug.LogError("No Sound Effect Audio Slider has been assigned to " + gameObject.name);
		}
		if (m_LiveUpdate && !m_LevelAudio)
		{
			Debug.LogError("No LevelAudioManager has been assigned to " + gameObject.name);
		}

		m_BackgroundAudioSlider.SetValueWithoutNotify(m_GameAudio.BackgroundVolume);
		m_SoundEffectAudioSlider.SetValueWithoutNotify(m_GameAudio.SoundEffectVolume);
	}

	/// <summary>
	/// Go Back to the Main Menu.
	/// </summary>
	public void Back()
	{
		StartCoroutine(BackButton());
	}

	/// <summary>
	/// Process the Back Button Being Clicked.
	/// </summary>
	/// <returns>The Current IEnumerator Step.</returns>
	public IEnumerator BackButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(true);
		m_OptionsMenu.SetActive(false);
	}

	/// <summary>
	/// Update the Background Audio.
	/// </summary>
	public void BackGroundAudioUpdate()
	{
		m_GameAudio.SetBackgroundVolume(m_BackgroundAudioSlider.value);

		if (m_LiveUpdate)
		{
			m_LevelAudio.UpdateLevelAudio();
		}
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
	/// Update the Sound Effect Audio.
	/// </summary>
	public void SoundEffectAudioUpdate()
	{
		m_GameAudio.SetSoundEffectVolume(m_SoundEffectAudioSlider.value);

		if (m_LiveUpdate)
		{
			m_LevelAudio.UpdateLevelAudio();
		}
	}
}
