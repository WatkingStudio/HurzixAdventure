using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * \class KeyManager
 * 
 * \brief This class holds the code for when the user presses a key on the keyboard.
 * 
 * This does not included interactions which directly affect the player. This is 
 *  handled in the CharacterMovement2D class.
 * 
 * \date 2019/24/10
 * 
 */
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
