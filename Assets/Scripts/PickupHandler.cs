using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickupHandler : MonoBehaviour
{
    private int spawnTimer, levelTimer, maxTime, minTime, exitTime;
    [SerializeField] private GameObject Pickup;
    TMP_Text calloutText;
    // Start is called before the first frame update
    void Start()
    {
        // Let's make some pickups.
        Debug.Log("Pickup Handler Start");
        Debug.LogWarning("Replace this code with something useful");
        minTime = 25;
        maxTime = 50;
        ResetSpawnTimer();
        levelTimer = 0;
    }

    // Update is called once per Time Reference (fixed time steps, independent of frame rate)
    void FixedUpdate()
    {
        spawnTimer++;
        if (spawnTimer > exitTime) {
            ResetSpawnTimer();
            float testSpawnPoint = Random.Range(-75f, 75f);
            Instantiate(Pickup,new Vector3(testSpawnPoint,0f,250f),Quaternion.identity);
        }
    }

    void ResetSpawnTimer()
    {
        spawnTimer = 0;
        exitTime = Random.Range(minTime, maxTime);
    }
}
