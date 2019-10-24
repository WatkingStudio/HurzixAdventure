using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
		//Left Click
        if(Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
			if(hit.collider != null)
			{
				if(hit.collider.GetComponent<Clickable>())
				{
					hit.collider.GetComponent<Clickable>().Clicked();
				}
			}
		}
		
		//Right Click
		if(Input.GetMouseButtonDown(1))
		{

		}
    }
}
