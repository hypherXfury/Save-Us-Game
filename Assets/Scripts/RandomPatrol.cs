using UnityEngine;

public class RandomPatrol : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    private float speed;
    public float timeToMaxDifficulty;
    Vector2 targetPos;

    
    void Start()
    {
        targetPos = GetRandomPosition();
    }

    void Update()
    {
        if ((Vector2)transform.position != targetPos)
        {
            speed = Mathf.Lerp(minSpeed, maxSpeed, GetDifficultyPercentage());
            transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            targetPos = GetRandomPosition();
        }
    }

    Vector2 GetRandomPosition()
    {
        float randX = Random.Range(minX, maxX);
        float randY = Random.Range(minY, maxY);

        return new Vector2(randX, randY);
    }
      float GetDifficultyPercentage()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / timeToMaxDifficulty);
    }
}
