using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TipTrigger : MonoBehaviour
{
    private GameObject tipObject;
    public GameObject tipUIObject;
    public string newTipText = "";
    private TipsManager tipsManager; 
    private bool beenTriggered = false;



    // Start is called before the first frame update
    void Start()
    {
        tipObject = this.gameObject;
        tipsManager = tipUIObject.GetComponent<TipsManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            {
                beenTriggered = true;
                tipsManager.tipText.text = newTipText;
                tipUIObject.SetActive(true);
                StartCoroutine(TipTimer());
            }

        }
    }


    private System.Collections.IEnumerator TipTimer()
    {
        yield return new WaitForSeconds(10f);
        tipUIObject.SetActive(false);
        Destroy(tipObject);
    }
}
