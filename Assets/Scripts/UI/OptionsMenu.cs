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
	private GameAudioSO m_GameAudio;
	[SerializeField]
	private Slider m_BackgroundAudioSlider;
	[SerializeField]
	private Slider m_SoundEffectAudioSlider;

	[SerializeField]
	private bool m_InGameMenu = false;
	[SerializeField]
	private LevelAudioManager m_LevelAudio;

	private void Start()
	{
		if (!m_GameAudio)
			Debug.LogError("No GameAudioSO has been assigned to " + gameObject.name);
		if (!m_BackgroundAudioSlider)
			Debug.LogError("No Background Audio Slider has been assigned to " + gameObject.name);
		if (!m_SoundEffectAudioSlider)
			Debug.LogError("No Sound Effect Audio Slider has been assigned to " + gameObject.name);
		if (m_InGameMenu && !m_LevelAudio)
			Debug.LogError("No LevelAudioManager has been assigned to " + gameObject.name);

		m_BackgroundAudioSlider.SetValueWithoutNotify(m_GameAudio.BackgroundVolume);
		m_SoundEffectAudioSlider.SetValueWithoutNotify(m_GameAudio.SoundEffectVolume);
	}

	public void BackGroundAudioUpdate()
	{
		m_GameAudio.SetBackgroundVolume(m_BackgroundAudioSlider.value);

		if(m_InGameMenu)
			m_LevelAudio.UpdateLevelAudio();
	}

	public void SoundEffectAudioUpdate()
	{
		m_GameAudio.SetSoundEffectVolume(m_SoundEffectAudioSlider.value);

		if (m_InGameMenu)
			m_LevelAudio.UpdateLevelAudio();
	}
}
