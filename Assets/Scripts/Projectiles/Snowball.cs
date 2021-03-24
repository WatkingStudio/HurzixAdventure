using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Snowball
 * 
 * \brief This class holds the functionality for a Snowball projectile
 * 
 * \date 2020/27/01
 */ 
public class Snowball : Projectile
{
	[SerializeField]
	private Collider2D m_Collision;
	[SerializeField]
	private LayerMask m_CollisionLayer;
	[SerializeField]
	private float m_LiveSpan = 3;

	private Vector3 m_DestinationPoint;
	private bool m_Despawned = false;
	private float m_ProjectileSpeed;

    private void Start()
    {
        if(!m_Collision)
        {
			Debug.LogError("No Collider2D has been assigned to " + gameObject.name);
		}
	}

	public void Update()
	{
		if (transform.position == m_DestinationPoint)
		{
			StartCoroutine(Despawn());
		}
		transform.position = Vector3.MoveTowards(transform.position, m_DestinationPoint, m_ProjectileSpeed * Time.deltaTime);

		if (m_Collision.IsTouchingLayers(m_CollisionLayer))
		{
			StartCoroutine(Despawn());
		}
	}

	// Begin a Countdown to Despawn this Object.
	// @return The Current IEnumerator Step.
	public IEnumerator DespawnCountDown()
	{
		yield return new WaitForSeconds(m_LiveSpan);
		if (!m_Despawned)
		{
			StartCoroutine(Despawn());
		}
	}

	// Despawn the Object.
	// @return The Current IEnumerator Step.
	public IEnumerator Despawn()
	{
		yield return new WaitForEndOfFrame();
		m_Despawned = true;
		Destroy(gameObject);
	}

	// Instantiate a Snowball and the Specified Location.
	public override void Instantiate(Vector3 destination, float speed)
	{
		m_DestinationPoint = destination;
		m_ProjectileSpeed = speed;
		StartCoroutine(DespawnCountDown());
	}

	// Set the Destination Point of the Snowball.
	public void SetDestinationPoint(Vector3 pos)
	{
		m_DestinationPoint = pos;
	}

	// Set the Projectile Speed of the Snowball.
	public void SetProjectileSpeed(float speed)
	{
		m_ProjectileSpeed = speed;
	}
}
