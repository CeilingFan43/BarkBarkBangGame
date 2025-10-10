using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipsManager : MonoBehaviour
{
    public TMP_Text tipTextUI;
    public CanvasGroup textUIOpacity;
    public float tipTime = 2f;
    public string tipText = "";
    public float fadeScale = 1f;

    private bool FadeIn;
    private GameObject tipCollider;



    // Start is called before the first frame update
    void Start()
    {
        tipTextUI = GameObject.Find("GameTip").GetComponent<TMP_Text>();
        textUIOpacity = tipTextUI.GetComponent<CanvasGroup>();
        textUIOpacity.alpha = 0;
        tipCollider = this.gameObject;
        tipTextUI.text = tipText;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FadeIn = true;
            if(textUIOpacity.alpha == 0 && FadeIn)
            {
            textUIOpacity.alpha += Time.deltaTime * fadeScale;
            }

            if(textUIOpacity.alpha == 1 && FadeIn)
            {
                StartCoroutine(TipTimer());
            }

        }
    }


    private System.Collections.IEnumerator TipTimer()
    {
        FadeIn = false;
        yield return new WaitForSeconds(tipTime);
        textUIOpacity.alpha -= Time.deltaTime * fadeScale;

        if(textUIOpacity.alpha == 0)
        {
            Destroy(tipCollider);
        }

    }
}
