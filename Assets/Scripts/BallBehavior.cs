using System;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class BallBehavior : MonoBehaviour
{
    public float minX = -6.31f;
    public float maxX= 6.22f;
    public float minY = -4.3f;
    public float maxY = 3.7f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPostions;
    public int secondsToMaxSpeed;
    // for launching to target pin
    public GameObject target; 
    public float minLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secondsToMaxSpeed = 30;
        // minSpeed = .1;
        // maxSpeed = 5;
        targetPostions = getRandomPosition();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 currPos = gameObject.GetComponent<Transform>().position;
        float distance = Vector2.Distance(currPos, targetPostions);
        if(distance > 0.1){
        
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage()) * Time.deltaTime; 
            Vector2 newPosition = Vector2.MoveTowards(currPos, targetPostions, currentSpeed);
            transform.position = newPosition;
        }
        
        else{
            targetPostions = getRandomPosition();
        }
       
    }

    Vector2 getRandomPosition(){
        float RandomX = Random.Range(minX, maxX);
        float RandomY = Random.Range(minY, maxY);
        
        return new Vector2(RandomX,RandomY);
    }

    private float getDifficultyPercentage(){
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    public void launch(){
        launching = true;
    }
}
