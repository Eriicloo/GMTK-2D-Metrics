using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
    public Animator playerAnimator;
    public Animator enemyAnimator;

    public void StartGettingHurtEnemyAnimation()
    {
        enemyAnimator.SetBool("IsGettingDamage", true);
        playerAnimator.SetBool("IsAttacking", false);
    }

    public void StartPlayerAttackAnimation()
    {
        enemyAnimator.SetBool("IsGettingDamage", false);
        playerAnimator.SetBool("IsAttacking", true);
    }
}
