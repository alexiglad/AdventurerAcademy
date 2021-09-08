using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PopUpDisplay : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;



    RaycastData data;
    Vector3 prevData;


    void Update()
    {
        data = controls.GetRaycastHit();
        //Debug.Log(data.Hit.collider.tag);

        switch (gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                if (data.HitBool && controls.VerifyTag(data, "UI"))
                {
                    Debug.Log(data.Hit.transform.gameObject);
                }

                break;
        }
        prevData = data.Hit.point;
    }

    
}

