using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Fireball
 * 
 * \brief This class is used to control the mechanics of a Fireball
 * 
 * \date 2020/18/01
 */
public class Fireball : MonoBehaviour
{
	[Header("Projectile")]
	[SerializeField]
	Rigidbody2D m_ProjectileRigidBody;
	[SerializeField]
	GameObject m_Projectile;

	[Header("Fireball Variables")]
	[SerializeField]
	private float m_Force = 12.8f;
	[SerializeField]
	private float m_FireballDelay = 2.0f;

	[Header("Initial Delay")]
	[SerializeField]
	private float m_InitialTimer;
	private bool m_InitialTimerDone = false;

	[Header("Misc")]
	[SerializeField]
	public Vector3 m_BasePoint;
	[SerializeField]
	private Animator m_Animator;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	private void Start()
	{
		if(!m_ProjectileRigidBody)
        {
			Debug.LogError("No Rigidbody2D has been assigned to " + gameObject.name);
		}
		if(!m_Projectile)
        {
			Debug.LogError("No Projectile GameObject has been assigned to " + gameObject.name);
		}
		if(!m_Animator)
        {
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if(!m_AudioSource)
        {
			Debug.LogError("No AudioSource has been assigned to " + gameObject.name);
		}

		StartCoroutine(InitialTimer());
	}

	void Update()
	{
		if (m_InitialTimerDone)
		{
			if (m_Projectile.transform.position.y < m_BasePoint.y)
			{
				m_ProjectileRigidBody.velocity = Vector2.zero;
				m_Projectile.transform.position = new Vector2(m_Projectile.transform.position.x, m_BasePoint.y);
				StartCoroutine(PrepareFireball());
			}

			if (m_ProjectileRigidBody.velocity.y > 0)
			{
				m_Animator.SetBool("Up", true);
				m_Animator.SetBool("Down", false);
			}
			else
			{
				m_Animator.SetBool("Up", false);
				m_Animator.SetBool("Down", true);
			}
		}
	}

	/// <summary>
	/// Execute when this fireball causes damage.
	/// </summary>
	public void FireballCausesDamage()
	{
		if (!m_AudioSource.isPlaying)
		{
			m_AudioSource.Play();
		}
	}

	/// <summary>
	/// Start a initial timer before starting the loop.
	/// </summary>
	/// <returns>The current ienumberator step.</returns>
	IEnumerator InitialTimer()
	{
		yield return new WaitForSeconds(m_InitialTimer);
		m_InitialTimerDone = true;
	}

	/// <summary>
	/// Set the velocity of this fireball after a delay.
	/// </summary>
	/// <returns>The current ienumerator step.</returns>
	IEnumerator PrepareFireball()
	{
		yield return new WaitForSeconds(m_FireballDelay);
		m_ProjectileRigidBody.velocity = new Vector2(m_ProjectileRigidBody.velocity.x, m_Force);
	}
}
