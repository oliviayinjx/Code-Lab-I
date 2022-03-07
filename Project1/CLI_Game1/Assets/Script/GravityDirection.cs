using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityDirection : MonoBehaviour
{
    private Rigidbody2D rb;
    public enum State{
        //change gravity to different directions
        graDown,
        graUp
    }

    [SerializeField]State currentState; //different gravity direction for different level

    // Start is called before the first frame update
    void Start(){


    }

    void Update(){
        //test changing state according to the z and x keys
        //debug purpose
        if(Input.GetKeyDown("z")){
            TransitionState(State.graUp);
        }
        else if(Input.GetKeyDown("x")){
            TransitionState(State.graDown);
        }
    }
    
    // Update is called once per frame
    void TransitionState(State newState)
    {
        switch (newState){
            case State.graDown:
                Physics2D.gravity = new Vector2(0,-9.81f);
            break;
            case State.graUp:
                Physics2D.gravity = new Vector2(0,9.81f);
            break;
        }
        
    }
}
