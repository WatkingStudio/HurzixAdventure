using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyRangedAttack : EnemyAction
{
	[SerializeField]
	private float m_AttackIntival = 2;
	[SerializeField]
	private float m_ProjectileSpeed;
	[SerializeField]
	private Object m_Projectile;
	[SerializeField]
	private Transform m_PlayerTransform;
	[SerializeField]
	private Transform m_ProjectileOrigin;

	private float m_AttackTimer = 0;
	private List<GameObject> m_Snowballs;

	private Vector2 m_ProjectileDestination;

	public UnityEvent m_RangedAttackMade;

	public void Start()
	{
		m_Action = Actions.EnemyRangedAttack;
	}

	public void SetProjectileDestination(Vector2 vec)
	{
		m_ProjectileDestination = vec;
	}

	public override void PerformAction()
	{
		m_AttackTimer -= Time.deltaTime;
		if(m_AttackTimer < 0)
		{
			m_AttackTimer = m_AttackIntival;
			var projectile = Instantiate((GameObject)m_Projectile, m_ProjectileOrigin.transform.position, m_ProjectileOrigin.transform.rotation);
			projectile.GetComponent<Projectile>().Instantiate(m_PlayerTransform.position, m_ProjectileSpeed);
			m_RangedAttackMade.Invoke();
		}
	}
}
