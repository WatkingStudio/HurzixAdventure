using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : EnemyAction
{
	[SerializeField]
	private Damager m_Damager;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private AnimationClip m_AttackClip;
	[SerializeField]
	private BasicEnemy m_BasicEnemy;

	private bool m_IsAttacking = false;

	private void Start()
	{
		m_Action = Actions.EnemyMeleeAttack;
	}

	public override void PerformAction()
	{
		if(!m_IsAttacking)
		{
			m_Animator.SetTrigger("Attack");
			StartCoroutine(AttackTimer());
		}
	}

	IEnumerator AttackTimer()
	{
		m_IsAttacking = true;
		m_Damager.EnableDamage();
		yield return new WaitForSeconds(m_AttackClip.length);
		m_Damager.DisableDamage();
		m_IsAttacking = false;
		m_BasicEnemy.SetDefaultAction();
	}
}
