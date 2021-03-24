using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class PlayerAudio
 * 
 * \brief This class is used to hold and control the audio of the player.
 * 
 * \date 2019/15/10
 * 
 */
public class PlayerAudio : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_PlayerEffectsSource;
	[SerializeField]
	private AudioSource m_PlayerMovementSource;

	[Header("Audio Clips")]
	[SerializeField]
	private AudioClip m_DeathAudioClip;
	[SerializeField]
	private AudioClip m_HurtAudioClip;
	[SerializeField]
	private AudioClip m_IndicatorAudioClip;
	[SerializeField]
	private AudioClip m_JumpAudioClip;

	[Header("Player Movement Audio")]
	[SerializeField]
	private PlayerMovementAudio m_ConcreteAudio;
	[SerializeField]
	private GroundFeatures.Surface m_DefaultLevelSurface;
	[SerializeField]
	private PlayerMovementAudio m_DirtAudio;
	[SerializeField]
	private PlayerMovementAudio m_SnowAudio;

	private List<AudioClip> m_ActiveLandingClips;
	private List<AudioClip> m_ActiveSprintingClips;
	private List<AudioClip> m_ActiveWalkingClips;

	private void Start()
	{
		if (!m_PlayerMovementSource)
		{
			Debug.LogError("No Audio Source for movement has been assigned to " + gameObject.name);
		}
		if (!m_PlayerEffectsSource)
		{
			Debug.LogError("No Audio Source for effects has been assigned to " + gameObject.name);
		}
		if (!m_HurtAudioClip)
		{
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for being hurt");
		}
		if (!m_DeathAudioClip)
		{
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for dying");
		}
		if (!m_JumpAudioClip)
		{
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for jumping");
		}
		if (!m_IndicatorAudioClip)
		{
			Debug.LogWarning("No Audio Clip has been assigned to " + gameObject.name + " for the indicator");
		}

		switch(m_DefaultLevelSurface)
		{
			case GroundFeatures.Surface.DIRT:
				SetDirtMovement();
				break;
			case GroundFeatures.Surface.ROCK:
				SetConcreteMovement();
				break;
			case GroundFeatures.Surface.SNOW:
				SetSnowMovement();
				break;
		}
	}
	
	// Play the Death Audio Clip.
	public void PlayDeathAudioClip()
	{
		m_PlayerEffectsSource.Stop();
		m_PlayerEffectsSource.clip = m_DeathAudioClip;
		m_PlayerEffectsSource.Play();
	}

	// Play the Hurt Audio Clip.
	public void PlayHurtAudioClip()
	{
		m_PlayerEffectsSource.Stop();
		m_PlayerEffectsSource.clip = m_HurtAudioClip;
		m_PlayerEffectsSource.Play();
	}

	// Play the Jump Audio Clip.
	public void PlayJumpingAudioClip()
	{
		m_PlayerMovementSource.Stop();
		m_PlayerMovementSource.clip = m_JumpAudioClip;
		m_PlayerMovementSource.Play();
	}

	// Play the Landing Audio Clip.
	public void PlayLandingAudioClip()
	{
		m_PlayerMovementSource.Stop();
		m_PlayerMovementSource.clip = m_ActiveLandingClips[Random.Range(0, m_ActiveLandingClips.Count)];
		m_PlayerMovementSource.Play();
	}

	// Play the Sprint Audio Clip.
	public void PlaySprintAudioClip()
	{
		if (!m_PlayerMovementSource.isPlaying)
		{
			m_PlayerMovementSource.clip = m_ActiveSprintingClips[Random.Range(0, m_ActiveSprintingClips.Count)];
			m_PlayerMovementSource.Play();
		}
	}

	// Play the Walk Audio Clip.
	public void PlayWalkAudioClip()
	{
		if(!m_PlayerMovementSource.isPlaying)
		{
			m_PlayerMovementSource.clip = m_ActiveWalkingClips[Random.Range(0, m_ActiveWalkingClips.Count)];
			m_PlayerMovementSource.Play();
		}		
	}

	// Set the Ground Audio Clips to Concrete.
	public void SetConcreteMovement()
	{
		m_ActiveLandingClips = m_ConcreteAudio.Landing;
		m_ActiveSprintingClips = m_ConcreteAudio.Sprinting;
		m_ActiveWalkingClips = m_ConcreteAudio.Walking;
	}

	// Set the Ground Audio Clips to Dirt.
	public void SetDirtMovement()
	{
		m_ActiveLandingClips = m_DirtAudio.Landing;
		m_ActiveSprintingClips = m_DirtAudio.Sprinting;
		m_ActiveWalkingClips = m_DirtAudio.Walking;
	}

	// Set the Ground Audio Clips to Snow.
	public void SetSnowMovement()
	{
		m_ActiveLandingClips = m_SnowAudio.Landing;
		m_ActiveSprintingClips = m_SnowAudio.Sprinting;
		m_ActiveWalkingClips = m_SnowAudio.Walking;
	}
}
