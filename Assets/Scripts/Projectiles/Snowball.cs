using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snowball : Projectile
{
	[SerializeField]
	private float m_LiveSpan = 3;
	[SerializeField]
	private LayerMask m_CollisionLayer;
	[SerializeField]
	private Collider2D m_Collision;

	private Vector3 m_DestinationPoint;
	private float m_ProjectileSpeed;

	private bool m_Despawned = false;

	public override void Instantiate(Vector3 destination, float speed)
	{
		m_DestinationPoint = destination;
		m_ProjectileSpeed = speed;
		StartCoroutine(CountDown());
	}

	public void Update()
	{
		if (transform.position == m_DestinationPoint)
			StartCoroutine(Despawn());
		transform.position = Vector3.MoveTowards(transform.position, m_DestinationPoint, m_ProjectileSpeed * Time.deltaTime);

		if(m_Collision.IsTouchingLayers(m_CollisionLayer))
		{
			StartCoroutine(Despawn());
		}
	}

	public void SetDestinationPoint(Vector3 pos)
	{
		m_DestinationPoint = pos;
	}

	public void SetProjectileSpeed(float speed)
	{
		m_ProjectileSpeed = speed;
	}

	public IEnumerator Despawn()
	{
		yield return new WaitForEndOfFrame();
		m_Despawned = true;
		Destroy(gameObject);
	}

	public IEnumerator CountDown()
	{
		yield return new WaitForSeconds(m_LiveSpan);
		if(!m_Despawned)
			StartCoroutine(Despawn());
	}
}
