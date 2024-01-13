using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickUpgrade : MonoBehaviour
{
    public LogicManager LogicScript;
    public Text priceText;
    // Start is called before the first frame update
    void Start()
    {
        priceText.text = LogicScript.cheese.clickUpgradePrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click overlaps with the 2D object's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // If the collider is not null, a click occurred on the object
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                if (LogicScript.cheese.points >= LogicScript.cheese.clickUpgradePrice) 
                {
                    LogicScript.ClickUpdate();
                    priceText.text = LogicScript.cheese.clickUpgradePrice.ToString();
                }
                
            }
        }
    }
}
