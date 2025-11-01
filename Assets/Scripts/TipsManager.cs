using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TipsManager : MonoBehaviour
{
    public TMP_Text tipText;
    public Animator tipAnimator;

    void OnEnable()
    {
        tipAnimator.Play("TipAnimation");
    }
}
