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

    public void DisplayIndicator(Transform itemTransform)
	{
		m_IndicatorArrow.gameObject.SetActive(true);
		m_IndicatorArrow.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);

		Vector3 vectorToTarget = itemTransform.position - m_IndicatorArrow.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		m_IndicatorArrow.transform.rotation = Quaternion.RotateTowards(m_IndicatorArrow.transform.rotation, q, 180);

		StartCoroutine(FlashIndicator());
	}

	IEnumerator FlashIndicator()
	{
		yield return new WaitForSeconds(0.5f);
		m_IndicatorArrow.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		m_IndicatorArrow.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		m_IndicatorArrow.gameObject.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		m_IndicatorArrow.gameObject.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		m_IndicatorArrow.gameObject.SetActive(false);
	}
}
