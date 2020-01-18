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

	[Header("Misc")]
	[SerializeField]
	public Vector3 m_BasePoint;
	[SerializeField]
	private Animator m_Animator;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_AudioSource;

	private bool m_FireballPreparingFlight = false;

	public void FireballCausesDamage()
	{
		if (!m_AudioSource.isPlaying)
		{
			m_AudioSource.Play();
		}		
	}

    // Update is called once per frame
    void Update()
    {
		if(m_Projectile.transform.position.y < m_BasePoint.y)
		{
			m_ProjectileRigidBody.velocity = Vector2.zero;
			m_Projectile.transform.position = new Vector2(m_Projectile.transform.position.x, m_BasePoint.y);
			StartCoroutine(PrepareFireball());
		}

		if(m_ProjectileRigidBody.velocity.y > 0) 
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

	IEnumerator PrepareFireball()
	{
		yield return new WaitForSeconds(m_FireballDelay);
		m_ProjectileRigidBody.AddForce(new Vector2(0, m_Force));
	}
}
