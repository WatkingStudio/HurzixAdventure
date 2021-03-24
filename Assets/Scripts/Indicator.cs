using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class Indicator
 * 
 * \brief This class holds the functionality for the Indicator used to show where the closest collectable
 *         item is.
 * 
 * \date 2019/21/10
 */ 
public class Indicator : MonoBehaviour
{
	[SerializeField]
	private GameObject m_IndicatorArrow;
	[SerializeField]
	private float m_IndicatorOffset = 0;

	private void Start()
	{
		if (!m_IndicatorArrow)
		{
			Debug.Log("No Indicator Arrow game object has been assigned to " + gameObject.name);
		}
	}

	/// <summary>
	/// Display the Indicator.
	/// </summary>
	/// <param name="itemTransform">The Transform of the Target Object</param>
	public void DisplayIndicator(Transform itemTransform)
	{
		m_IndicatorArrow.gameObject.SetActive(true);
		m_IndicatorArrow.transform.position = new Vector3(transform.position.x, transform.position.y + m_IndicatorOffset, transform.position.z);

		Vector3 vectorToTarget = itemTransform.position - m_IndicatorArrow.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		m_IndicatorArrow.transform.rotation = Quaternion.RotateTowards(m_IndicatorArrow.transform.rotation, q, 180);

		StartCoroutine(FlashIndicator(3));
	}

	/// <summary>
	/// Flash the Indicator for a Specified Length of Time.
	/// </summary>
	/// <param name="duration">How Long to Flash for (seconds)</param>
	/// <returns></returns>
	IEnumerator FlashIndicator(int duration)
	{
		for(int i = 0; i < duration; ++i)
		{
			m_IndicatorArrow.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			m_IndicatorArrow.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.5f);
		}
	}
}
