using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public struct counterFood
{
    public string name;
    public int counter;

    public counterFood(string name)
    {
        this.name = name;
        counter = 0;
    }
    public override string ToString()
    {
        return $"{name}: {counter:00}";
    }

};

public class SpawnerScirpt : MonoBehaviour
{
    private static float _distanceDifficulty = 0.5f;
    public static float distanceDifficulty
    {
        get => _distanceDifficulty;
        set
        {
            _distanceDifficulty = value;
            foodTimeout = pipeTimeout + timePeriod * 1.5f;
        }
    }
    private static float timePeriod => 3.5f - 2.0f * distanceDifficulty;
    [SerializeField]
    private static float pipeTimeout;
    [SerializeField]
    private static float foodTimeout;

    [SerializeField]
    private GameObject pipePrefab;
    private float pipeOffsetMax = 1.5f;
    public static float gapDifficulty;

    [SerializeField]
    private GameObject[] foodPrefabs;
    private float foodOffsetMax = 3.50f;
    public static counterFood[] countFood;

    void Start()
    {
        gapDifficulty = 0.5f;

        pipeTimeout = 0f;
        foodTimeout = timePeriod / 1.5f;

        countFood = new counterFood[foodPrefabs.Length];
        for (int i = 0; i < countFood.Length; i++)
        {
            countFood[i] = new counterFood(foodPrefabs[i].name);
        }

        SendStats();
    }

    void Update()
    {
        pipeTimeout -= Time.deltaTime;
        if (pipeTimeout < 0)
        {
            pipeTimeout = timePeriod;
            SpawnPipe();
        }

        foodTimeout -= Time.deltaTime;
        if (foodTimeout < 0)
        {
            foodTimeout = timePeriod;
            SpawnFood();
        }
    }

    private void SpawnPipe()
    {
        float gap = (gapDifficulty - 0.5f) * 2f;

        GameObject pipe = Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position +
            Random.Range(-pipeOffsetMax, pipeOffsetMax) * Vector3.up;

        Transform top = pipe.transform.Find("Top");
        Transform bottom = pipe.transform.Find("Bottom");
        if (top != null && bottom != null)
        {
            Vector3 topPos = top.transform.position;
            top.position = new Vector3(topPos.x, topPos.y - gap, topPos.z);
            Vector3 bottomPos = bottom.transform.position;
            bottom.position = new Vector3(bottomPos.x, bottomPos.y + gap, bottomPos.z);
        }
    }

    private void SpawnFood()
    {
        int randomValue = Random.Range(0, 100);
        int sumChance = 0;
        foreach (GameObject obj in foodPrefabs)
        {
            sumChance += obj.GetComponent<FoodScript>().chanceSpawn;
        }
        if (randomValue > sumChance)
        {
            //Debug.Log($"Random value: {randomValue}; Sum of chance: {sumChance}; Spawn skipped");
            return;
        }

        GameObject selected = null;
        int lastFoodChance = 0;
        for (int i = 0; i < foodPrefabs.Length; i++)
        {
            GameObject obj = foodPrefabs[i];

            int newFoodChance = lastFoodChance + obj.GetComponent<FoodScript>().chanceSpawn;
            if (lastFoodChance <= randomValue && randomValue <= newFoodChance)
            {
                selected = obj;
                countFood[i].counter++;

                //Debug.Log($"Random value: {randomValue}; Food prefab name: {obj.name}; Ñhance beetwen {lastFoodChance} and {newFoodChance}; Bonus health from food: {obj.GetComponent<FoodScript>().giveHealth}");
                break;
            };
            lastFoodChance = newFoodChance;
        }

        GameObject food = GameObject.Instantiate(selected);
        food.transform.position = this.transform.position +
            Random.Range(-foodOffsetMax, foodOffsetMax) * Vector3.up;
        food.transform.Rotate(0, 0, Random.Range(0, 360));

        SendStats();
    }

    private void SendStats()
    {
        string stats = "";
        for (int i = 0; i < countFood.Length; i++)
        {
            if (i == countFood.Length - 1)
                stats += $"{countFood[i].ToString()}";
            else
                stats += $"{countFood[i].ToString()} \t | \t";
        }
        StatsScript.Show(stats);
    }
}
