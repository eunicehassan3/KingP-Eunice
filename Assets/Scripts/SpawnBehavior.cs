using UnityEngine;


public class SpawnBehavior : MonoBehaviour
{
    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    float timer;
    public float interval = 5;

    public Pins pinsDB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnBall();
        spawnPin();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        interval = getInterval(20,100);
        if(timer > interval){
            spawnBall();
            timer = 0;
        }
    }

    void spawnBall(){
        int numVariants = ballVariants.Length;
        if(numVariants > 0){
            int selection = Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
            newObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            BallBehavior2 ballBehavior = newObject.GetComponent<BallBehavior2>();
            ballBehavior.setBounds(minX,maxX,minY,maxY);
            ballBehavior.setTarget(targetObject);
            ballBehavior.initialPosition();
            // ballBehavior.gameObject.layer = 3;

        }
    }


    private float getInterval(float minNum, float maxNum){
        interval = Random.Range(minNum, maxNum);
        return interval;
        
    }

    void spawnPin(){
        targetObject = Instantiate(pinsDB.getPin(CharacterManager.selection).prefab, new Vector3(0.0f,0.0f,0.0f), Quaternion.identity);
        targetObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
    }

    
}
