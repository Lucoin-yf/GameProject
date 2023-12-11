using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour
{   
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    [SerializeField]public float velocity;

    public float fast_velocity;

    private int state;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = 0.5f;
        fast_velocity = velocity * 2.0f;
        state = 0;
    }

    // Update is called once per frame
    void Update()
    {   
        //change state between patrol and catch
        if(state == 0)
        {
            animation_controller.SetInteger("state",0); 
        }
        else{
            animation_controller.SetInteger("state",1); 
        }
        
        //walk along a square, if distance too close, turns around and transition to catch
        

        //move
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        if (transform.position.y > 0.0f) // if the character starts "climbing" the terrain, drop her down
        {
            Vector3 lower_character = movement_direction * velocity * Time.deltaTime;
            lower_character.y = -100f; // hack to force her down
            character_controller.Move(lower_character);
        }
        else
        {
            character_controller.Move(movement_direction * velocity * Time.deltaTime);
        } 
    }
}
