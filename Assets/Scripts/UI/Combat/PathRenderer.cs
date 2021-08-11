using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] float startWidth;
    [SerializeField] float endWidth;
    bool moving;
    RaycastData data;
    Vector3 prevData;
    NavMeshAgent agent;
    LineRenderer line;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = startWidth;
        line.endWidth = endWidth;
    }

    void Update()
    {
        data = controls.GetRaycastHit();

        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                if (tempRef.Character != null && tempRef.Character.IsPlayer() && !tempRef.GetTargeting() && data.HitBool && controls.VerifyTag(data, "Terrain") && tempRef.Character.GetComponent<NavMeshAgent>().enabled)
                {
                    if (tempRef.Character.Animator.GetBool("walking"))
                    {
                        moving = true;
                        line.positionCount = 0;
                        line.startColor = Color.blue;
                        line.endColor = Color.blue;
                        DisplayActivePath(tempRef.Character);
                    }
                    else if (tempRef.HasMovement && tempRef.CanContinue && tempRef.Character.Agent != null)
                    {
                        if (!moving && data.Hit.point.Equals(prevData))
                        {//optimization todo fix where line stays
                            return;
                        }
                        moving = false;
                        line.positionCount = 0;
                        NavMeshPath path = new NavMeshPath();
                        agent = tempRef.Character.Agent;
                        agent.CalculatePath(data.Hit.point, path);
                        DisplayPath(path, tempRef.Character, tempRef);
                    }
                    else
                    {
                        line.positionCount = 0;
                    }
                }
                else
                {
                    line.positionCount = 0;
                }
                break;
        }
        prevData = data.Hit.point;
    }

    public void DisplayActivePath(Character character)
    {
        if (agent.hasPath)
        {
            line.positionCount = agent.path.corners.Length;

            Vector3 bottom = character.BoxCollider.bounds.center;
            bottom.y -= character.BoxCollider.bounds.size.y / 2;
            line.SetPosition(0, bottom);

            if (agent.path.corners.Length < 2)
                return;

            for (int i = 1; i < agent.path.corners.Length; i++)
            {
                line.SetPosition(i, agent.path.corners[i]);
            }
        }
    }

    public void DisplayPath(NavMeshPath path, Character character, CombatManager tempRef)
    {
        line.positionCount = path.corners.Length;

        Vector3 bottom = character.BoxCollider.bounds.center;
        bottom.y -= character.BoxCollider.bounds.size.y / 2;

        if (line.positionCount != 0)
        {
            line.SetPosition(0, bottom);
        }            

        if (path.corners.Length < 2)
        {
            return;
        }

        for (int i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
        
        if(tempRef.IsInvalidPath(data.Hit.point))
        {
            line.startColor = Color.red;
            line.endColor = Color.red;
        }
        else
        {
            line.startColor = Color.blue;
            line.endColor = Color.blue;
        }
    }
}

