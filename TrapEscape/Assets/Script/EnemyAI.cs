using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform[] controlPoint;
    public float enemySpeed;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = RandPoint();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyWalk();
    }

    void EnemyWalk()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * enemySpeed);
        
        if (transform.position == targetPos)
        {
            targetPos = RandPoint();
        }
    }

    Vector3 RandPoint()
    {
        int possibility = Random.Range(0, controlPoint.Length);

        return controlPoint[possibility].position;
    }
}
