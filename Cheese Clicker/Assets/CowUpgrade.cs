using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CowUpgrade : MonoBehaviour
{
    public LogicManager LogicScript;
    public Text priceText;
    public GameObject mouselock;
    public Slider timeSlider;

    // Start is called before the first frame update
    void Start()
    {
        priceText.text = LogicScript.cowUpgrade.Price.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        if (LogicScript.cowUpgrade.Unlocked)
        {
            mouselock.SetActive(false);
        }
        priceText.text = LogicScript.cowUpgrade.Price.ToString();

        timeSlider.value += LogicScript.cowUpgrade.Speed;
        if (timeSlider.value == 2)
        {
            LogicScript.AddPints(LogicScript.cowUpgrade);
            timeSlider.value = (float)0;
        }

        if (Input.GetMouseButtonDown(0) && LogicScript.cowUpgrade.Unlocked)
        {
            // Get the mouse position in world coordinates
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Check if the mouse click overlaps with the 2D object's collider
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);

            // If the collider is not null, a click occurred on the object
            if (hitCollider != null && hitCollider.gameObject == gameObject)
            {
                if (LogicScript.cheese.points >= LogicScript.cowUpgrade.Price)
                {
                    LogicScript.Upgrades(LogicScript.cowUpgrade, 1.6f, 0.0005f, 1.2f);
                }
            }
        }
    }
}