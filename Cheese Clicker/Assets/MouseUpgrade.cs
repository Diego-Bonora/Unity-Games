using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseUpgrade : MonoBehaviour
{
    public LogicManager LogicScript;
    public Text priceText;
    public GameObject mouselock;
    public Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (LogicScript.mouseUpgrade.Unlocked)
        {
            mouselock.SetActive(false);
        }
        priceText.text = LogicScript.mouseUpgrade.Price.ToString();
        timeSlider.value+= LogicScript.mouseUpgrade.Speed;
        if (timeSlider.value == 2 )
        {
            LogicScript.AddPints(LogicScript.mouseUpgrade);
            timeSlider.value = (float)0;
        }
      
        if (Input.GetMouseButtonDown(0) && LogicScript.mouseUpgrade.Unlocked)
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click overlaps with the 2D object's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // If the collider is not null, a click occurred on the object
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                if (LogicScript.cheese.points >= LogicScript.mouseUpgrade.Price)
                {
                    LogicScript.Upgrades(LogicScript.mouseUpgrade, 1.6f, 0.0005f, 1.2f);
                    LogicScript.cowUpgrade.Unlocked = true;
                }
            }
        }
    }
}