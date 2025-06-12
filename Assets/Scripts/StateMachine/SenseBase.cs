using UnityEngine;

public class SenseBase : MonoBehaviour
{
    public StateMachine owner;
    public virtual void Init(StateMachine stateOwner)
    {
        owner = stateOwner;
    }
}
