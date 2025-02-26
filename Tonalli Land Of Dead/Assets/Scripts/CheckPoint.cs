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
            cpMan.SetActiveCheckPoint(this);
            anim.SetBool("CheckActive", true);
            isActive = true;
        }
    }

    public void DeactivateCheckPoint()
    {
        anim.SetBool("CheckActive", false);
        isActive = false;
    }
}
