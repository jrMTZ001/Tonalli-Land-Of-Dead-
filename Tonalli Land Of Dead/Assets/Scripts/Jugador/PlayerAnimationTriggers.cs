using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private CaballeroJaguar  jaguar => GetComponent<CaballeroJaguar>();

    private void AnimationTrigger()
    {
        jaguar.AnimationTrigger();
    }
}
