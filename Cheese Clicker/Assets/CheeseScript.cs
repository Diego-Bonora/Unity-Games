using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseScript : MonoBehaviour
{
    public LogicManager LogicScript;

    void Start()
    {
    }

    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && LogicScript.status)
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click overlaps with the 2D object's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // If the collider is not null, a click occurred on the object
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                LogicScript.cheese.points+= LogicScript.cheese.damage;
                LogicScript.cheese.points = Mathf.Round(LogicScript.cheese.points * 100.0f) * 0.01f;
            }
        }

    }

}

