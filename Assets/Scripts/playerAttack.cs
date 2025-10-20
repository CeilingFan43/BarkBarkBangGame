using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerAttack : MonoBehaviour
{
    private PlayerManagement pm;
    public float attackRange;
    public float attackRadius;
    public float damage;
    public float attackCooldown;
    public Transform attackOrigin;
    public LayerMask enemyLayer;

    //Charge Attack stuff
    public float maxChargeTime = 2f;
    public float maxDamageMultiplier = 2f;
    public GameObject chargeUI;
    public Image chargeFillBar;

    private bool isCharging = false;
    private float chargeTime = 0f;
    private bool canAttack = true;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        pm = FindObjectOfType<PlayerManagement>();
        myAnimator = GetComponent<Animator>();
        if (chargeUI != null)
        chargeUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCharging();
        }

        if (Input.GetMouseButton(0) && isCharging)
        {
            Charge();
        }

        if (Input.GetMouseButtonUp(0) && isCharging)
        {
            ReleaseAttack();
        }
    }

    private void StartCharging()
    {
        isCharging = true;
        chargeTime = 0f;

        if (chargeUI != null)
            chargeUI.SetActive(true);

        if (chargeFillBar != null)
            chargeFillBar.fillAmount = 0f;

    }

    private void Charge()
    {
        chargeTime += Time.deltaTime;
        float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime);

        if (chargeFillBar != null)
        {
            chargeFillBar.fillAmount = chargePercent; 
        }
    }


//attack code, checks for enemies tagged Enemy in sphere, calls the TakeDamage function on the enemies.
    void ReleaseAttack()
    {
        isCharging = false;
        if (chargeUI != null)
        {
            chargeUI.SetActive(false);
        }

        //damage calc
        float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime);
        float finalDamage = damage * Mathf.Lerp(1f, maxDamageMultiplier, chargePercent);

        myAnimator.SetBool("PlayerAttacked", true);
        // ADD IN A SCALE FACTOR TO ANIMATION TIME FOR CHARGE ATTACK !. MAYBE ADD A SEPARATE CHARGE ANIM TO CHARGING.

        Collider[] hitEnemies = Physics.OverlapSphere(attackOrigin.position, attackRadius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<TallWolfAI>().TakeDamage(finalDamage);
            }

            else if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<SmallWolfAI>().TakeDamage(finalDamage);
            }

            if (enemy.CompareTag("EnemySpawner"))
            {
                enemy.GetComponentInParent<EnemySpawner>().TakeDamage(finalDamage);
            }
        }

        Debug.Log("Charge attack completed. Damage;"+ finalDamage + " || Charged for " + chargeTime + "seconds");

        StartCoroutine(AttackCooldown());
    }

//DEBUGGING showing the attack sphere of effect
    void OnDrawGizmosSelected()
    {
        if (attackOrigin == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackOrigin.position, attackRadius);
    }

    private IEnumerator AttackCooldown()
    {
        myAnimator.SetBool("PlayerAttacked", false);
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
        myAnimator.SetBool("attacked", false);
        Debug.Log("attack cooldown method called");
    }
}
