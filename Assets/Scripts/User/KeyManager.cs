using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
	[SerializeField]
	private LevelItems m_CurrentLevelItems;
	[SerializeField]
	private GameObject m_ControlsUI;

	// Update is called once per frame
	void Update()
    {
        if(Input.GetButtonDown("Reveal"))
		{
			//Reveal the closest item
			m_CurrentLevelItems.RevealClosestItem(transform);
		}

		if (Input.GetButtonDown("ToggleControls"))
		{
			m_ControlsUI.SetActive(!m_ControlsUI.activeSelf);
		}
	}
}
