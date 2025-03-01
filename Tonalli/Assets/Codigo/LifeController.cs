using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{   
    public static LifeController instance;
    private Jugador thePlayer;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = FindFirstObjectByType<Jugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Respawn()
    {
        thePlayer.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
        PlayerHealthController.Instance.AddHealth(PlayerHealthController.Instance.maxHealth);
    }
}
