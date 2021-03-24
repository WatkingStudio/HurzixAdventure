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

	/// <summary>
	/// Execute this code when the lava causes damage.
	/// </summary>
    public void LavaCausesDamage()
	{
		if (m_AudioSource.clip != null && !m_AudioSource.isPlaying)
		{
			m_AudioSource.Play();
		}
	}
}
