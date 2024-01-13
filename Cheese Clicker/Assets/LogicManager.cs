using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    public Cheese cheese = new Cheese();
    public Upgrade mouseUpgrade = new Upgrade(100f, 0.0f, 10f, true);
    public Upgrade cowUpgrade = new Upgrade(500f, 0.0f, 50f);
    public bool status = false;
    public Text PointText;
    public ParticleSystem particles;

    List<Upgrade> upgradeList = new List<Upgrade>();
            

    private float timerDuration = 300f; // 5 minutes in seconds
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        upgradeList.Add(mouseUpgrade);
        upgradeList.Add(cowUpgrade);

        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        PointText.text = cheese.points.ToString();
        currentTime -= Time.deltaTime;
        if (currentTime <= 0f)
        {
            SaveCheese();
            ResetTimer();
        }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            particles.transform.position = mousePosition;
            particles.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            particles.Stop();
        }
    }

    public void AddPints(Upgrade upgrade)
    {
        cheese.points += upgrade.Points;
        cheese.points = Mathf.Round(cheese.points * 100.0f) * 0.01f;
    }
    public void SaveCheese()
    {
        string points = JsonUtility.ToJson(cheese);
        string filePath = Application.persistentDataPath + "/GameData.json";
        File.WriteAllText(filePath, points);
        Debug.Log($"Saved GameData to {filePath}");

        string jsonString = JsonUtility.ToJson(new UpgradeWrapper { upgrades = upgradeList });
        filePath = Application.persistentDataPath + "/GameUpgrades.json";
        File.WriteAllText(filePath, jsonString);
        Debug.Log($"Saved upgrades to {filePath}");
        status = true;
    }

    public void LoadCheese()
    {
        string filePath = Application.persistentDataPath + "/GameData.json";
        string points = File.ReadAllText(filePath);
        cheese = JsonUtility.FromJson<Cheese>(points);

        filePath = Application.persistentDataPath + "/GameUpgrades.json";
        string upgrades = File.ReadAllText(filePath);
        UpgradeWrapper wrapper = JsonUtility.FromJson<UpgradeWrapper>(upgrades);
        upgradeList = wrapper.upgrades;

        mouseUpgrade = upgradeList[0];
        cowUpgrade = upgradeList[1];

        // Accessing objects in the loaded list
        foreach (Upgrade upgrade in upgradeList)
        {
            Debug.Log($"Price={upgrade.Price}, Speed={upgrade.Speed}, Points={upgrade.Points}");
        }
        status = true;
    }

    public void Upgrades(Upgrade upgrade, float price, float speed, float points)
    {
        if (upgrade.Active)
        {
            upgrade.Points *= points;
        }
        else
        {
            upgrade.Active = true;
        }
        cheese.points -= upgrade.Price;
        upgrade.Price *= price;
        upgrade.Speed += speed;
        upgrade.Price = Mathf.Round(upgrade.Price * 100.0f) * 0.01f;
    }

    public void ClickUpdate()
    {
        cheese.damage *= 1.5f;
        cheese.damage = Mathf.Round(cheese.damage * 100.0f) * 0.01f;

        cheese.points -= cheese.clickUpgradePrice;

        cheese.clickUpgradePrice *= 2;
    }

    void ResetTimer()
    {
        currentTime = timerDuration;
    }

    [System.Serializable]
    private class UpgradeWrapper
    {
        public List<Upgrade> upgrades;
    }
}

[System.Serializable]
public class Cheese
{
    public float points = 0;
    public float damage = 1;
    public float clickUpgradePrice = 100;
    
}

[System.Serializable]
public class Upgrade
{
    public bool Active = false;
    public float Price;
    public float Speed;
    public float Points;
    public bool Unlocked;

    public Upgrade(float price = 0.0f, float speed = 0.0f, float points = 0.0f, bool unlocked = false)
    {
        Price = price;
        Speed = speed;
        Points = points;
        Unlocked = unlocked; 
    }
}

