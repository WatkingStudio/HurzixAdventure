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
	private AudioSource m_SceneAudio;
	[SerializeField]
	private AudioClip m_ButtonPressClip;

	[SerializeField]
	private GameObject m_MainMenu;
	[SerializeField]
	private GameObject m_OptionsMenu;

	[SerializeField]
	private GameAudioSO m_GameAudio;
	[SerializeField]
	private Slider m_BackgroundAudioSlider;
	[SerializeField]
	private Slider m_SoundEffectAudioSlider;

	[SerializeField]
	private bool m_LiveUpdate = false;
	[SerializeField]
	private LevelAudioManager m_LevelAudio;

	private void Start()
	{
		if (!m_SceneAudio)
			Debug.LogError("No Audio Source has been assigned to " + gameObject.name); ;
		if (!m_ButtonPressClip)
			Debug.Log("No Button Press Audio Clip has been assigned to " + gameObject.name);
		if (!m_MainMenu)
			Debug.LogError("No Main Menu has been assigned to " + gameObject.name);
		if (!m_OptionsMenu)
			Debug.LogError("No Options Menu has been assigned to " + gameObject.name);
		if (!m_GameAudio)
			Debug.LogError("No GameAudioSO has been assigned to " + gameObject.name);
		if (!m_BackgroundAudioSlider)
			Debug.LogError("No Background Audio Slider has been assigned to " + gameObject.name);
		if (!m_SoundEffectAudioSlider)
			Debug.LogError("No Sound Effect Audio Slider has been assigned to " + gameObject.name);
		if (m_LiveUpdate && !m_LevelAudio)
			Debug.LogError("No LevelAudioManager has been assigned to " + gameObject.name);

		m_BackgroundAudioSlider.SetValueWithoutNotify(m_GameAudio.BackgroundVolume);
		m_SoundEffectAudioSlider.SetValueWithoutNotify(m_GameAudio.SoundEffectVolume);
	}

	public void Back()
	{
		StartCoroutine(BackButton());
	}

	public IEnumerator BackButton()
	{
		PlayButtonClick();
		yield return new WaitForSeconds(m_ButtonPressClip.length);
		m_MainMenu.SetActive(true);
		m_OptionsMenu.SetActive(false);
	}

	public void BackGroundAudioUpdate()
	{
		m_GameAudio.SetBackgroundVolume(m_BackgroundAudioSlider.value);

		if(m_LiveUpdate)
			m_LevelAudio.UpdateLevelAudio();
	}

	public void SoundEffectAudioUpdate()
	{
		m_GameAudio.SetSoundEffectVolume(m_SoundEffectAudioSlider.value);

		if (m_LiveUpdate)
			m_LevelAudio.UpdateLevelAudio();
	}

	private void PlayButtonClick()
	{
		m_SceneAudio.Stop();
		m_SceneAudio.clip = m_ButtonPressClip;
		m_SceneAudio.Play();
	}
}
