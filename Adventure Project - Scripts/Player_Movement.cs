using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float player_speed = 5;
    public float player_jump = 5;
    private Animator Animator2D;
    private Rigidbody2D Rigidbody2D;
    private Vector3 player_rotation;
    private bool game_ground_check = false;
    private Coin_Manager game_coin_manager;
    public GameObject game_panel;
    void Start()
    { 
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator2D = GetComponent<Animator>();
        game_coin_manager = GameObject.FindGameObjectWithTag("game_coin_text").GetComponent<Coin_Manager>();
        player_rotation = transform.eulerAngles;
    }
    void Update()
    {
        float player_movement = Input.GetAxis("Horizontal");
        if (player_movement != 0)
        {
            Animator2D.SetBool("playerRunning", true);
        } 
        else
        {
            Animator2D.SetBool("playerRunning", false);
        }
        if (player_movement < 0)
        {
            transform.eulerAngles = player_rotation - new Vector3(0, 180, 0);
            transform.Translate(Vector2.right * player_speed * -player_movement * Time.deltaTime);
        }
        if (player_movement > 0)
        {
            transform.eulerAngles = player_rotation;
            transform.Translate(Vector2.right * player_speed * player_movement * Time.deltaTime);
        }
        if(game_ground_check == false)
        {
            Animator2D.SetBool("playerJumping", true);
        }
        else
        {
            Animator2D.SetBool("playerJumping", false);
        }
        if (Input.GetKeyDown(KeyCode.Space) && game_ground_check)
        {
            Rigidbody2D.AddForce(Vector2.up * player_jump, ForceMode2D.Impulse);
            game_ground_check = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "game_ground")
        {
            game_ground_check = true;
        }
        if (collision.gameObject.tag == "game_enemy")
        {
            Destroy(gameObject);
            game_panel.SetActive(true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "game_coin")
        {
            game_coin_manager.coinSystemAdd();
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "game_spike")
        {
            game_panel.SetActive(true);
            Destroy(gameObject);
        }
    }
}
