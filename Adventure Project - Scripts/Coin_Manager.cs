using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Coin_Manager : MonoBehaviour
{
    public int game_coin;
    public TextMeshProUGUI game_coin_text;
    void Start()
    {
    }
    void Update()
    {
        game_coin_text.text = game_coin.ToString();
    }
    public void coinSystemAdd()
    {
        game_coin++;
    }
}
