using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseDamage : SenseBase
{
    public override void Init(StateMachine stateOwner)
    {
        base.Init(stateOwner);
        GetComponent<EnemyHealth>().OnTakeDamage += Notify;
    }

    private void Notify(float damage)
    {
        throw new System.NotImplementedException();
    }
}
