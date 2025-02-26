using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    public static LifeController instance;
    private CaballeroJaguar jaguar;
    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        jaguar = FindFirstObjectByType<CaballeroJaguar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Respawn()
    {
        jaguar.transform.position = FindFirstObjectByType<CheckpointManager>().respawnPosition;
     
    }
}
