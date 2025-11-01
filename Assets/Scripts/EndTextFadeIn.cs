using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTextFadeIn : MonoBehaviour
{
    public Animator UIAnimator;
    public AnimationClip TextFade;
    // Update is called once per frame
    void OnEnable()
    {
        UIAnimator.Play(TextFade.name, 0, 0f);

    }
}
