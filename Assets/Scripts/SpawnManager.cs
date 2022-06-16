using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using JetBrains.Annotations;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject powerUpPrefab;
    public GameObject asphalt;
    public GameObject house_left;
    public GameObject house_right;
    public GameObject player;
    private float spawnRangeLeftX = 6.5f;
    private float spawnRangeRightX = -0.5f;
    [SerializeField] float spawnForward = 50;
    [SerializeField] float spawnPosZ;
    public float spawnRoad = 7;
    public float spawnHouseLeft = -21.51f;
    public float spawnHouseRight = -21.44f;
    private PlayerController playerControllerScript;
    public float spawnObstacleTime = 2;
    public float spawnPowerupTime = 2;
    public float spawnTime = 3;
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            spawnObstacleTime -= Time.deltaTime;
            //Makes new obstacles 
            if (playerControllerScript.gameOver == false && spawnObstacleTime <= 0)
            {
               
                if(spawnTime >= 0)
                {
                    spawnTime -= Time.deltaTime;
                    spawnPosZ = spawnForward;
                    int objIndex = Random.Range(0, obstaclePrefab.Length);
                    UnityEngine.Vector3 spawnPos = new UnityEngine.Vector3(Random.Range(spawnRangeRightX, spawnRangeLeftX), 6.47f, spawnPosZ);
                    Instantiate(obstaclePrefab[objIndex], spawnPos, obstaclePrefab[objIndex].transform.rotation);
                    spawnForward -= 7;
                }
                spawnTime = 3;
                spawnObstacleTime = 0.1f;
            }
            //Makes PowerUps
            if(!playerControllerScript.gameOver)
            {
                spawnPowerupTime -= Time.deltaTime;
                if(spawnPowerupTime <= 0)
                {
                    UnityEngine.Vector3 spawnPowerUp = new UnityEngine.Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z - 15);
                    Instantiate(powerUpPrefab, spawnPowerUp, powerUpPrefab.transform.rotation);
                    spawnPowerupTime = 8;
                }
            }
            

            //Makes Road
            if (!playerControllerScript.gameOver)
            {
                UnityEngine.Vector3 spawnPosAsp = new UnityEngine.Vector3(3.02f, 6.4f, spawnRoad);
                Instantiate(asphalt, spawnPosAsp, asphalt.transform.rotation);
                spawnRoad -= 4;
            }
            //Makes Houses (Left)
            if (!playerControllerScript.gameOver)
            {
                UnityEngine.Vector3 spawnPosHouseLeft = new UnityEngine.Vector3(19.57f, 6.06f, spawnHouseLeft);
                Instantiate(house_left, spawnPosHouseLeft, house_left.transform.rotation);
                spawnHouseLeft -= 24;
            }
            //Makes Houses (Right)
            if (!playerControllerScript.gameOver)
            {
                UnityEngine.Vector3 spawnPosHouseRight = new UnityEngine.Vector3(-13.31f, 6.11f, spawnHouseRight);
                Instantiate(house_right, spawnPosHouseRight, house_right.transform.rotation);
                spawnHouseRight -= 23;
            }
        }  
    }
}
