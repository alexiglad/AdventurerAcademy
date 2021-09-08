using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GameState/Substate")]
public class SubstateSO : ScriptableObject
{
    [SerializeField] private SubstateEnum substate;
    private void Awake()
    {
        substate = SubstateEnum.Default;
    }
    public SubstateEnum GetSubstate()
    {
        return substate;
    }

    public void SetSubstate(SubstateEnum state)
    {
        substate = state;
    }
}
