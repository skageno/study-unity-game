using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Manager : MonoBehaviour
{
    public float enemy_speed = 3f;
    public float enemy_right_movement;
    public float enemy_left_movement;

    private Vector3 enemy_rotation;
    void Start()
    {
        enemy_right_movement = transform.position.x + enemy_right_movement;
        enemy_left_movement = transform.position.x - enemy_left_movement;
        enemy_rotation = transform.eulerAngles;
    }
    void Update()
    {
        transform.Translate(Vector3.left * enemy_speed * Time.deltaTime);
        if (transform.position.x < enemy_left_movement)
        {
            transform.eulerAngles = enemy_rotation - new Vector3(0, 180, 0);
        }
        if (transform.position.x > enemy_right_movement)
        {
            transform.eulerAngles = enemy_rotation;
        }
    }
}
