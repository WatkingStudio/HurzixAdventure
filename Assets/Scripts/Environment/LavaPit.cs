using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class LavaPit : MonoBehaviour
{
	[SerializeField]
	private AudioSource m_AudioSource;

    private void Start()
    {
        if(!m_AudioSource)
        {
			Debug.LogError("No AudioSource has been assigned to " + gameObject.name);
		}
    }

	// Execute This Code When the Lava Causes Damage.
    public void LavaCausesDamage()
	{
		if (m_AudioSource.clip != null && !m_AudioSource.isPlaying)
		{
			m_AudioSource.Play();
		}
	}
}
