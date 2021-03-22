using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Listeners : MonoBehaviour
{
    public static Listeners main;
    public List<GameObject> listeners;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Debug.LogWarning ("Attempted to recreate listener, destroying new listener");
            Destroy (gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (listeners == null)
        {
            listeners = new List<GameObject> ();
        }

        GameObject[] gos = GameObject.FindGameObjectsWithTag ("Listener");

        listeners.AddRange (gos);
    }

    public void AdddListener (GameObject go)
    {
        if (!listeners.Contains (go))
        {
            listeners.Add (go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
