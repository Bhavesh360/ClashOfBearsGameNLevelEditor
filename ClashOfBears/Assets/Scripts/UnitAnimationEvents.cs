using UnityEngine;

public class UnitAnimationEvents : MonoBehaviour {

    private Unit _Owner;

    private void Awake()
    {
        _Owner = GetComponentInParent<Unit>();
    }

    public void UnitAttackAnimationEvent()
    {
        if (_Owner.currentTarget != null && _Owner.prevTarget == _Owner.currentTarget && _Owner.currentTarget.health > 0)
        {
            _Owner.AttackAnimationEvent();
        }

    }
}
