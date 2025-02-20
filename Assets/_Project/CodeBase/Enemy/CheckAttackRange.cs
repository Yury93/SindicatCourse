using CodeBase.Enemy;
using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Attack))]
public class CheckAttackRange : MonoBehaviour
{
    public Attack Attack;
    public TriggerObserver TriggerObserver;

    private void Start()
    {
        TriggerObserver.TriggerEnter += TriggerEnter;
        TriggerObserver.TriggerExit += TriggerExit;

        Attack.DisableAttack();
    }

    private void TriggerExit(Collider collider)
    {
        Attack.DisableAttack();
    }

    private void TriggerEnter(Collider collider)
    {
        Attack.EnableAttack();
    }
}
