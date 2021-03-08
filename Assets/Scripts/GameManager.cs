using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;
    public void GameOver(){

        if(gameHasEnded == false){
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
        }
        

    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
