using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoamingBehavior : EnemyBase
{
    private Transform target;
    private NavMeshAgent selfNavMeshAgent;
    private Animator anim;

    private bool attacking, dead;
    void Start()
    {
        target = PlayerStats.PlayerStatsInstance.transform;
        selfNavMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        attacking = dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(attacking || dead) 
            return;
        selfNavMeshAgent.SetDestination(target.position);
        if (selfNavMeshAgent.remainingDistance < 3f)
        {
            Attack();
        }
        anim.SetFloat("Speed", selfNavMeshAgent.velocity.magnitude);
    }

    void Attack()
    {
        attacking = true;
        selfNavMeshAgent.isStopped = true;
        StartCoroutine(AttackAnim());
    }

    private IEnumerator AttackAnim()
    {
        yield return null;
        anim.Play(Random.Range(0, 2) == 0 ? "Attack01" : "Attack02");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length/2f);
        if (Vector3.Distance(transform.position, target.position) <= 3f)
        {
            PlayerStats.PlayerStatsInstance.TakeDamage(10);
        }
        yield return null;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length/2f);

        attacking = false;
        selfNavMeshAgent.isStopped = false;
    }


    public override void Death(Vector3 pos)
    {
        dead = true;
        GetComponent<Collider>().enabled = false;
        selfNavMeshAgent.enabled = false;
        anim.Play("Die");
        StopAllCoroutines();
        Destroy(gameObject, 5f);
    }
}
