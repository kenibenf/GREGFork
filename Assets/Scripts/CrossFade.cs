using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

public class CrossFade : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishedCrossfade(){
        GameObject.Find ("Player").GetComponent<PlayerInput> ().ActivateInput();

    }
}

