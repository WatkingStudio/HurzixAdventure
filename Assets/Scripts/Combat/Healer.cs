using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Healer
 * 
 * \brief This class is for any GameObject that can heal damage
 * 
 * This class only holds the generic healer functions, the there are specific ineractions with
 *  GameObjects these should be called through the event system in the Inspector.
 *  
 * \date 2019/15/11
*/
public class Healer : MonoBehaviour
{
	//The last collider hit
	public Collider2D LastHit { get { return m_LastHit; } }
	public int Healing { get { return m_Healing; } }

	[SerializeField, Tooltip("How much damge this object heals each hit")]
	private int m_Healing = 1;
	[Header("Collision Box")]
	[SerializeField, Tooltip("The offset of the collision box from the Game Object")]
	private Vector2 m_Offset = new Vector2(0f, 0f);
	[SerializeField, Tooltip("The size of the collision box")]
	private Vector2 m_Size = new Vector2(1f, 1f);
	[SerializeField, Tooltip("If set to true, the offset will take into account the facing of the sprite")]
	private bool m_OffsetBaseOnSpriteFacing = true;

	[Header("Interaction Checks")]
	[SerializeField, Tooltip("If disabled, will ignore triggers when applying damage")]
	private bool m_CanHitTriggers;
	[SerializeField, Tooltip("If enabled, when this component has been used to heal a damagable this gameobject will be disabled.")]
	private bool m_DisableAfterUse = false;
	[SerializeField, Tooltip("The layers which this Damager can interact with")]
	private LayerMask m_HittableLayers;

	[Header("Misc")]
	[SerializeField, Tooltip("SpriteRenderer used to read the flipX value used by m_OffsetBasedOnSpriteFacing")]
	private SpriteRenderer m_SpriteRenderer;
	[SerializeField, Tooltip("An Item Audio for when the healer class heals a target")]
	private ItemAudio m_ItemAudio;

	//Set to whether the sprite was flipped by default
	protected bool m_SpriteOriginallyFlipped;
	//If set then this Healer can heal
	[SerializeField]
	protected bool m_CanHeal = true;
	//Filter to restrict what game objects this Healer interacts with
	protected ContactFilter2D m_HealContactFilter;
	//An array to store the colliders found in a collision
	protected Collider2D[] m_HealOverlapResults = new Collider2D[10];
	//The transform of the Healer
	protected Transform m_HealerTransform;
	//The last collider this Healer collided with
	protected Collider2D m_LastHit;

	private void Start()
	{
		if (!m_SpriteRenderer)
			Debug.LogError("No Sprite Renderer has been assigned to " + gameObject.name);
		if (!m_ItemAudio)
			Debug.LogError("No Item Audio has been assigned to " + gameObject.name);
	}

	private void Awake()
	{
		m_HealContactFilter.layerMask = m_HittableLayers;
		m_HealContactFilter.useLayerMask = true;
		m_HealContactFilter.useTriggers = m_CanHitTriggers;

		if (m_OffsetBaseOnSpriteFacing && m_SpriteRenderer != null)
			m_SpriteOriginallyFlipped = m_SpriteRenderer.flipX;

		m_HealerTransform = transform;
	}

	public void EnableHeal()
	{
		m_CanHeal = true;
	}

	public void DisableHeal()
	{
		m_CanHeal = false;
	}

	//Every frame this function will be called and check if this object has
	// collided with any other object.
	private void FixedUpdate()
	{
		//If the object is set to not deal damage then simply return
		if (!m_CanHeal)
			return;

		//Create the collision area
		Vector2 scale = m_HealerTransform.lossyScale;

		Vector2 facingOffset = Vector2.Scale(m_Offset, scale);
		if (m_OffsetBaseOnSpriteFacing && m_SpriteRenderer != null && m_SpriteRenderer.flipX != m_SpriteOriginallyFlipped)
			facingOffset = new Vector2(-m_Offset.x * scale.x, m_Offset.y * scale.y);

		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Vector2 pointA = (Vector2)m_HealerTransform.position + facingOffset - scaledSize * 0.5f;
		Vector2 pointB = pointA + scaledSize;

		//Check number of collisions
		int hitCount = Physics2D.OverlapArea(pointA, pointB, m_HealContactFilter, m_HealOverlapResults);

		for(int i = 0; i < hitCount; ++i)
		{
			//Store the last collision hit
			m_LastHit = m_HealOverlapResults[i];
			Damageable damageable = m_LastHit.GetComponentInParent<Damageable>();

			//If the collided object has a Damageable component then deal damage
			if(damageable && !damageable.IsFullHealth())
			{
				damageable.RegainHealth(m_Healing);
				m_ItemAudio.PlayAudioClip();
				if(m_DisableAfterUse)
				{
					DisableHeal();
					gameObject.SetActive(false);
				}
			}
		}
	}

	private void OnDrawGizmos()
	{
		//Create the collision area
		Vector2 scale = transform.lossyScale;
		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Gizmos.color = new Color(1f, 0f, 0f, 1f);
		Gizmos.DrawWireCube(transform.position + (Vector3)m_Offset, (Vector3)scaledSize);
	}

	private void OnDrawGizmosSelected()
	{
		Vector2 scale = transform.lossyScale;
		Vector2 scaledSize = Vector2.Scale(m_Size, scale);

		Gizmos.color = new Color(0f, 1f, 0f, 1f);
		Gizmos.DrawWireCube(transform.position + (Vector3)m_Offset, (Vector3)scaledSize);
	}
}
