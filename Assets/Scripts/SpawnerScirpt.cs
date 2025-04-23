using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerScirpt : MonoBehaviour
{
    [SerializeField]
    private GameObject pipePrefab;
    private float pipeOffsetMax = 1.75f;

    [SerializeField]
    private GameObject[] foodPrefabs;
    private float foodOffsetMax = 3.50f;

    [SerializeField]
    private float timePeriod = 5.0f;
    private float pipeTimeout;
    private float foodTimeout;


    void Start()
    {
        pipeTimeout = 0f;
        foodTimeout = 1.5f * timePeriod;
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
        GameObject pipe = GameObject.Instantiate(pipePrefab);
        pipe.transform.position = this.transform.position +
            Random.Range(-pipeOffsetMax, pipeOffsetMax) * Vector3.up;
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
            Debug.Log($"Random value: {randomValue}; Sum of chance: {sumChance}; Spawn skipped");
            return;
        }

        GameObject selected = null;
        int lastFoodChance = 0;
        foreach (GameObject obj in foodPrefabs)
        {
            int newFoodChance = lastFoodChance + obj.GetComponent<FoodScript>().chanceSpawn;
            if (lastFoodChance <= randomValue && randomValue <= newFoodChance)
            {
                selected = obj;
                Debug.Log($"Random value: {randomValue}; Food prefab name: {obj.name}; Ñhance beetwen {lastFoodChance} and {newFoodChance}; Bonus health from food: {obj.GetComponent<FoodScript>().giveHealth}");
                break;
            };
            lastFoodChance = newFoodChance;
        }

        GameObject food = GameObject.Instantiate(selected);
        food.transform.position = this.transform.position +
            Random.Range(-foodOffsetMax, foodOffsetMax) * Vector3.up;
        food.transform.Rotate(0, 0, Random.Range(0, 360));
    }
}
