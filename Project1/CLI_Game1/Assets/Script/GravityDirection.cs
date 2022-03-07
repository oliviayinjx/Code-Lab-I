using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDirection : MonoBehaviour
{
    public enum State{
        //change gravity to different directions
        graRight,
        graDown,
        graUp
    }
    //singleton for the gravity direction status script
    private static GravityDirection instance;

    State currentState; 

    // Start is called before the first frame update
    void Awake(){
        //singleton
        if(instance!= this && instance != null){
            Destroy(this);
        }else{
            instance = this;
            DontDestroyOnLoad(gameObject); //keep through scene
        }

    }
    // Update is called once per frame
    void Update()
    {
        switch (currentState){
            case State.graRight:
                Physics2D.gravity = new Vector2(9.81f,0);
            break;
            case State.graDown:
                Physics2D.gravity = new Vector2(0,-9.81f);
            break;
            case State.graUp:
                Physics2D.gravity = new Vector2(0,9.81f);
            break;
        }
        
    }
}
