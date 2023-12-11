using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown1 : MonoBehaviour
{   
    public GameObject player;
    private Animator animation_controller;
    private CharacterController character_controller;
    public Vector3 movement_direction;
    [SerializeField]public float walking_velocity;
    [SerializeField]public float crouch_velocity;
    public float velocity; //norm speed
    
    // Start is called before the first frame update
    void Start()
    {
        animation_controller = GetComponent<Animator>();
        character_controller = GetComponent<CharacterController>();
        movement_direction = new Vector3(0.0f, 0.0f, 0.0f);
        walking_velocity = 1.0f;
        crouch_velocity = walking_velocity * 2.0f;
        velocity = 0.0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        //state table
        //walk = 0
        //crouch_catch = 1

        //just two states
        //1. walk along the square
        //2. if distance too close, rotate towards the player, and switch to crouch, and speed up

        //when distance too far, rotate to 90/180/270/360 degree, and switch back to walk state.
        
        //notice clown only change rotation on y, x,z won't change!

        //四个顶点(y == 0 高永远), x_1 = 3.3 z_1 = 17.7 x_2= 3.3 z_2 = 7.54
        //                        x_3 = -3.6 z_3= 7.54  x_4= -3.6 z_4 = 17.7

            // Accessing position
            Vector3 position = player.transform.position;
            float x = position.x;
            float y = position.y;
            float z = position.z;

            // Accessing rotation
            Quaternion rotation = player.transform.rotation;

            // You can also access Euler angles from the rotation
            float eulerX = rotation.eulerAngles.x;
            float eulerY = rotation.eulerAngles.y; //we only use y
            float eulerZ = rotation.eulerAngles.z;
            
            Vector3 playerPosition = position;
            playerPosition.y = 0;
            Vector3 clownPosition = transform.position;
            clownPosition.y = 0;

            float euclidean_distance = Vector3.Distance(playerPosition,clownPosition);

            if(euclidean_distance < 3.5f)
            {
                Debug.Log("Shit!");
            }

            if (Mathf.Abs(eulerY % 90) > 0.01f) // Using a small epsilon to handle floating-point imprecisions
            {
                // Correct to the closest multiple of 90 degrees
                float correctedEulerY = Mathf.Round(eulerY / 90) * 90;

                // Apply the corrected rotation
                transform.rotation = Quaternion.Euler(0, correctedEulerY, 0);

                // Log the correction
                Debug.Log("Corrected eulerY to " + correctedEulerY);
            }

        

        //if walking
        if(euclidean_distance > 3.5f)
        {
            animation_controller.SetInteger("state",0);
            // Check if the clown is facing along the Z-axis
            if (Mathf.Abs(eulerY % 180) < 0.01f)
            {
                Debug.Log("Clown is facing along the Z-axis");

            }
            // Check if the clown is facing along the X-axis
            else if (Mathf.Abs((eulerY + 90) % 180) < 0.01f || Mathf.Abs((eulerY - 90) % 180) < 0.01f)
            {
                Debug.Log("Clown is facing along the X-axis");
            }
        }

        //if catching
        else{
            
        }

    }
}
