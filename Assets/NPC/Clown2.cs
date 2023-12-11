using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown2 : MonoBehaviour
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
    }
}
