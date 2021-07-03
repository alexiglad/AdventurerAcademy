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

    RaycastData data;
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
        line.positionCount = 0;
        data = controls.GetRaycastHit();
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
            if (data.HitBool && controls.VerifyTag(data, "Terrain"))
            {
                line.startColor = Color.blue;
                line.endColor = Color.blue;
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                NavMeshPath path = new NavMeshPath();
                agent = tempRef.Character.Agent;
                agent.CalculatePath(data.Hit.point, path);
                if (tempRef.Character.Animator.GetBool("walking"))
                    DisplayActivePath(tempRef.Character);
                else if (tempRef.HasMovement)
                    DisplayPath(path, tempRef.Character, tempRef);
            }
    }

    public void DisplayActivePath(Character character)
    {
        line.positionCount = agent.path.corners.Length;
        line.SetPosition(0, character.BoxCollider.bounds.center);

        if (agent.path.corners.Length < 2)
            return;

        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            line.SetPosition(i, agent.path.corners[i]);
        }
        
    }

    public void DisplayPath(NavMeshPath path, Character character, CombatManager tempRef)
    {
        line.positionCount = path.corners.Length;
        
        if(line.positionCount != 0)
        {
            line.SetPosition(0, character.BoxCollider.bounds.center);
        }            

        if (path.corners.Length < 2)
            return;

        for (int i = 1; i < path.corners.Length; i++)
        {
            line.SetPosition(i, path.corners[i]);
        }
        
        if(tempRef.IsInvalidPath(data.Hit.point))
        {
            //TODO Show visual that path is not valid
            //Debug.Log("INVALID PATH! WILL MOVE TO CLOSEST POINT! Attepted Destination: " + path.corners[path.corners.Length - 1]);
            line.startColor = Color.red;
            line.endColor = Color.red;
        }
    }
}
