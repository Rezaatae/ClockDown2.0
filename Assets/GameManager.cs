using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    bool gameHasEnded = false;
    public void GameOver(){

        if(gameHasEnded == false && SceneManager.GetActiveScene().name == "SampleScene"){
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
        }
        else{
            SceneManager.LoadScene("SampleScene");
        }
        

    }

    void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
