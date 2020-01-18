using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
	[SerializeField]
	Rigidbody2D m_ProjectileRigidBody;
	[SerializeField]
	GameObject m_Projectile;
	[SerializeField]
	float m_Force;
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
		Debug.Log(m_ProjectileRigidBody.velocity.y);
    }

	IEnumerator PrepareFireball()
	{
		yield return new WaitForSeconds(2);
		m_ProjectileRigidBody.AddForce(new Vector2(0, m_Force));
	}
}
