using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Transform[] positions;

    Transform nextPos;

    public int speed;

    private int nextPosIndex;
    private float dist;


    void Start(){

        nextPos = positions[0];

    }

    void Update(){



        MoveEnemy();
    }

    void MoveEnemy(){
        if (transform.position == nextPos.position){
            nextPosIndex++;
            if (nextPosIndex >= positions.Length){
                nextPosIndex = 0;
            }
            nextPos = positions[nextPosIndex];
        }
        else{
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed*Time.deltaTime);
        }

    }

    

    
}
