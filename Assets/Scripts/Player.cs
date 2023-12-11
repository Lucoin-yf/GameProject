using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    [SerializeField]public float walking_velocity;
    [SerializeField]public float walk_back_velocity;
    [SerializeField]public float run_velocity;
    public float velocity; //norm speed
    public bool has_won;
    
    public GameObject deathpanel;
    
    public GameObject winpanel;
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.5f;
        walk_back_velocity = walking_velocity / -1.5f;
        run_velocity = walking_velocity * 2.0f;
        velocity = 0.0f;
        has_won = false;
    }

    // Update is called once per frame
    void Update()
    {
        //WASD system, which is more common.

        //turn left 0.5 is really fast
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0.0f, -0.3f, 0.0f));
        }
        //turn right 0.5 is really fast
        else if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0.0f, 0.3f, 0.0f));
        }

        //state table
        //idle = 0
        //walk_forward = 1
        //walk_backward = 2
        //run_forward = 3

        //walk forward use key W
            if (Input.GetKey(KeyCode.W))
            {   
                //press W + leftshift to run  
                if(Input.GetKey(KeyCode.LeftShift))  
                {
                    animation_controller.SetInteger("state",2);
                    velocity += 1f;
                    if(velocity > run_velocity)
                    {
                        velocity = run_velocity;
                    } 
                   
                   
                }
                //press W + Left CTRL to crouch forward
                else
                {   
                    //back to walk forward
                    animation_controller.SetInteger("state",1); 
                    velocity += 0.5f;
                    if(velocity > walking_velocity)
                    {
                        velocity = walking_velocity;
                    }
                }

            }
            //press S to walk back
            else if(Input.GetKey(KeyCode.S))
            {
                animation_controller.SetInteger("state",3);
                velocity -= 0.2f;
                if(velocity < walk_back_velocity)
                {
                    velocity = walk_back_velocity;
                }
                
            }
            //back to idle state
            else{
                animation_controller.SetInteger("state",0); 
                velocity = 0; 
            }   
        float xdirection = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        float zdirection = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.y);
        movement_direction = new Vector3(xdirection, 0.0f, zdirection);

        // character controller's move function is useful to prevent the character passing through the terrain
        // (changing transform's position does not make these checks)
        if (transform.position.y > 0.1f) // if the character starts "climbing" the terrain, drop her down
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
    //keyboard typing
    
}
