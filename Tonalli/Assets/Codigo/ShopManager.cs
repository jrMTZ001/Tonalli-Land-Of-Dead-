using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShopManager : MonoBehaviour
{
    public Jugador player; // Asume que tienes el script del jugador
    public Text coinsText;
    public Text feedbackText;

    public int priceJumpStrike = 100;
    public int priceClimb = 150;
    public int priceProjectile = 200;

    void Start()
    {
        UpdateCoinsUI();
    }

    void UpdateCoinsUI()
    {
        coinsText.text = "Monedas: " + player.playerCoins.ToString();
        UpdateCoinsUI();  // Actualiza cada frame, o puedes usar eventos para optimizar
    }
}