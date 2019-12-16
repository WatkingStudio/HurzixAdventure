using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Gravestone
 * 
 * \brief This class is to control the functionality of a Gravestone
 * 
 * This class hold the functionality for when the user interacts with the Gravestone, and the
 *  effect when it has been destroyed.
 * 
 * \date 2019/16/12
 * 
 */
[RequireComponent(typeof(Damageable))]
[RequireComponent(typeof(Damager))]
[RequireComponent(typeof(AudioSource))]
public class Gravestone : MonoBehaviour
{
	[SerializeField]
	private Damageable m_Damageable;
	[SerializeField]
	private Damager m_Damager;
	[SerializeField]
	private Animator m_Animator;
	[SerializeField]
	private AudioSource m_AudioSource;
	[SerializeField]
	private AudioClip m_CrumbleAudioClip;

	[SerializeField]
	private AnimationClip m_CoinAnimation;
	[SerializeField]
	private AnimationClip m_HealthAnimation;
	[SerializeField]
	private AnimationClip m_DamageAnimation;

	private float m_DespawnTimer;

	private void Start()
	{
		if (!m_Animator)
			Debug.LogError("No Animator has been assigned to " + gameObject.name);
		if (!m_CrumbleAudioClip)
			Debug.LogError("No Crumbling audio clip has been assigned to " + gameObject.name);
		if (!m_CoinAnimation)
			Debug.LogError("No Coin Animation has been assigned to " + gameObject.name);
		if (!m_HealthAnimation)
			Debug.LogError("No Health Animation has been assigned to " + gameObject.name);
		if (!m_DamageAnimation)
			Debug.LogError("No Damage Animation has been assigned to " + gameObject.name);
	}

	public void GravestoneDamaged()
	{
		m_Animator.SetInteger("Health", m_Damageable.CurrentHealth());
		if (m_Damageable.CurrentHealth() == 0)
			m_AudioSource.clip = m_CrumbleAudioClip;
		m_AudioSource.Play();
	}

	public void GravestoneDestroyed(Damager damager, Damageable damageable)
	{
		int random = Random.Range(0, 3);

		if (random == 0) //Give Coins
		{
			m_Animator.SetBool("CoinDrop", true);
			m_DespawnTimer = m_CoinAnimation.length + 0.5f;
			damager.GetComponentInParent<PlayerCharacter>().CollectCoin();
		}
		else if (random == 1) //Give Health
		{
			m_Animator.SetBool("HealthDrop", true);
			m_DespawnTimer = m_HealthAnimation.length + 0.5f;
			damager.GetComponentInParent<Damageable>().RegainHealth(1);
		}
		else if (random == 2) //Deal Damage
		{
			m_Animator.SetBool("DamageDrop", true);
			m_DespawnTimer = m_DamageAnimation.length + 0.5f;
			damager.GetComponentInParent<Damageable>().TakeDamage(m_Damager, true);
		}

		StartCoroutine(Despawn());
	}

	public IEnumerator Despawn()
	{
		yield return new WaitForSeconds(m_DespawnTimer);
		gameObject.SetActive(false);
	}
}
