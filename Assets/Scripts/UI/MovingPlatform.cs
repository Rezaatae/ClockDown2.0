using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform[] positions;

    private Transform nextPos;

    public int speed;

    private int nextPosIndex;

    private float dist;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = positions[0];
    }

    // Update is called once per frame
    void Update()
    {
         MovePlatform();
    }

    // moving platform to be moved between waypoints (similar to enemy)
    private void MovePlatform()
    {
        if (transform.position == nextPos.position)
        {
            nextPosIndex++;
            if (nextPosIndex >= positions.Length)
            {
                nextPosIndex = 0;
            }
            nextPos = positions[nextPosIndex];
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed*Time.deltaTime);
        }

    }
}
