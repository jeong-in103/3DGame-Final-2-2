using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    public enum CurrentState { idle, trace, attack, dead };
    public CurrentState curState = CurrentState.idle;

    public float traceDist = 10.0f;
    public float attackDist = 2.2f;
    public int hp;

    private Transform monsterTr;
    private Transform playerTr;
    private NavMeshAgent nvAgent;
    private Animator animator;

    private bool isDead = false;

    void Start()
    {
        monsterTr = GetComponent<Transform>();
        playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        nvAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        StartCoroutine(this.CheckState());
        StartCoroutine(this.MonsterAction());
    }

    IEnumerator CheckState()
    {
        while (isDead == false)
        {
            yield return new WaitForSeconds(0.2f);

            float dist = Vector3.Distance(playerTr.position, monsterTr.position);

            if (dist <= attackDist)
            {
                curState = CurrentState.attack;
            }
            else if (dist <= traceDist)
            {
                curState = CurrentState.trace;
            }
            else
            {
                curState = CurrentState.idle;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while (!isDead)
        {
            switch (curState)
            {
                case CurrentState.idle:
                    nvAgent.isStopped = true;
                    animator.SetBool("isTrace", false);
                    break;

                case CurrentState.trace:
                    nvAgent.destination = playerTr.position;
                    nvAgent.isStopped = false;
                    animator.SetBool("isAttack", false);
                    animator.SetBool("isTrace", true);
                    break;

                case CurrentState.attack:
                    nvAgent.isStopped = true;
                    animator.SetBool("isAttack", true);
                    break;
            }

            yield return null;
        }
    }

    public void GetDamaged(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            MonsterDead();
    }

    void MonsterDead()
    {
        StopAllCoroutines();

        isDead = true;
        curState = CurrentState.dead;
        nvAgent.isStopped = true;
        animator.SetTrigger("isDead");
        
        gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;

        foreach (Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
        {
            coll.enabled = false;
        }

        Invoke("MonsterDisappear", 2f);
    }

    private void MonsterDisappear()
    {
        Destroy(this.gameObject);
    }
}
