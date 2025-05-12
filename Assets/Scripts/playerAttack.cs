using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public float attackRange;
    public float attackRadius;
    public float damage;
    public Transform attackOrigin;
    public LayerMask enemyLayer;

    Animator myAnimator;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            Attack();
        }
    }

//attack code, checks for enemies tagged Enemy in sphere, calls the TakeDamage function on the enemies.
    void Attack()
    {
        myAnimator.SetBool("attacked", true);
        Collider[] hitEnemies = Physics.OverlapSphere(attackOrigin.position, attackRadius, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<newEnemyAi>().TakeDamage(damage);
            }
        }

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
        yield return new WaitForSeconds(2);
        myAnimator.SetBool("attacked", false);
        Debug.Log("attack cooldown method called");
    }
}
