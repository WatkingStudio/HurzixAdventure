using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
	[SerializeField]
	private GameObject m_IndicatorArrow;

    public void DisplayIndicator(Transform itemTransform)
	{
		//itemTransform is the target

		Vector3 vectorToTarget = itemTransform.position - m_IndicatorArrow.transform.position;
		float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		m_IndicatorArrow.transform.rotation = Quaternion.RotateTowards(m_IndicatorArrow.transform.rotation, q, 180);
	}
}
