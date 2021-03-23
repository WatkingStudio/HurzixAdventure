using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * \class EnemyMeleeAttack
 * 
 * \brief This class holds the functionality of an enemy performing an attack
 * 
 * \date 2019/23/12
 */ 
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

	public UnityEvent OnAttackEvent;

	private bool m_IsAttacking = false;

	private void Start()
	{
		m_Action = Actions.EnemyMeleeAttack;

		if (!m_Damager)
		{
			Debug.LogError("No Damager has been assigned to " + gameObject.name);
		}
		if (!m_Animator)
		{
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		}
		if (!m_AttackClip)
		{
			Debug.LogError("No Attack Clip has been assigned to " + gameObject.name);
		}
		if (!m_BasicEnemy)
		{
			Debug.LogError("No Basic Enemy script has been assigned to " + gameObject.name);
		}
	}

	private void Awake()
	{
		if (OnAttackEvent == null)
		{
			OnAttackEvent = new UnityEvent();
		}
	}

	// Sets up the Melee Attack Action
	public override void PerformAction()
	{
		if(!m_IsAttacking)
		{
			m_Animator.SetTrigger("Attack");
			StartCoroutine(AttackTimer());
		}
	}

	// Performs the Attack Action
	IEnumerator AttackTimer()
	{
		m_IsAttacking = true;
		m_Damager.EnableDamage();
		yield return new WaitForSeconds(m_AttackClip.length);
		m_Damager.DisableDamage();
		m_IsAttacking = false;
		m_BasicEnemy.SetDefaultAction();
		OnAttackEvent.Invoke();
	}
}
