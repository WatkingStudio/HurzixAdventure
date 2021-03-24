using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRangedAttack : EnemyAction
{
	[SerializeField]
	private float m_AttackIntival = 2;
	[SerializeField]
	private Transform m_PlayerTransform;
	[SerializeField]
	private Object m_Projectile;
	[SerializeField]
	private Transform m_ProjectileOrigin;
	[SerializeField]
	private float m_ProjectileSpeed;

	public UnityEvent m_RangedAttackMade;

	private float m_AttackTimer = 0;
	private Vector2 m_ProjectileDestination;
	private List<GameObject> m_Snowballs;

	public void Start()
	{
		if(!m_PlayerTransform)
        {
			Debug.LogError("No Player Transform has been assigned to " + gameObject.name);
		}
		if(!m_Projectile)
        {
			Debug.LogError("No Projectile has been assigned to " + gameObject.name);
		}

		m_Action = Actions.EnemyRangedAttack;
	}

	/// <summary>
	/// Perform the ranged attack action.
	/// </summary>
	public override void PerformAction()
	{
		m_AttackTimer -= Time.deltaTime;
		if (m_AttackTimer < 0)
		{
			m_AttackTimer = m_AttackIntival;
			var projectile = Instantiate((GameObject)m_Projectile, m_ProjectileOrigin.transform.position, m_ProjectileOrigin.transform.rotation);
			projectile.GetComponent<Projectile>().Instantiate(m_PlayerTransform.position, m_ProjectileSpeed);
			m_RangedAttackMade.Invoke();
		}
	}

	/// <summary>
	/// Set the destination of the projectile.
	/// </summary>
	/// <param name="vec">The destination of the projectile.</param>
	public void SetProjectileDestination(Vector2 vec)
	{
		m_ProjectileDestination = vec;
	}
}
