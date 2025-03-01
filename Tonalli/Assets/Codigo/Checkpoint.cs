using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private bool isActive;
    public Animator anim;
    [HideInInspector]
    public CheckpointManager cpMan;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && isActive == false)
        {
            cpMan.SetActiveCheckpoints(this);
            anim.SetBool("CheckActive", true);
            isActive = true;
        }
    }

    public void DeactivateCheckpoint()
    {
        anim.SetBool("CheckActive", false);
        isActive = false;
    }
}
