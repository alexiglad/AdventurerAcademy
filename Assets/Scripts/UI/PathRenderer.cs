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
        
        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (!tempRef.GetTargeting() && data.HitBool && controls.VerifyTag(data, "Terrain"))
            {
                line.startColor = Color.blue;
                line.endColor = Color.blue;

                NavMeshPath path = new NavMeshPath();
                agent = tempRef.Character.Agent;
                agent.CalculatePath(data.Hit.point, path);

                if (tempRef.Character.Animator.GetBool("walking"))
                    DisplayActivePath(tempRef.Character);
                else if (tempRef.HasMovement)
                {
                    DisplayPath(path, tempRef.Character, tempRef);
                    //DisplayPathTwo(tempRef.Character, data.Hit.point, tempRef);
                }
            }
        }
            
    }

    public void DisplayActivePath(Character character)
    {
        /*if(counter%10 == 0) 
        {
            Debug.Log("active path");
            foreach (Vector3 corner in agent.path.corners)
            {
                Debug.Log(corner);
            }
        }*/
        
        line.positionCount = agent.path.corners.Length;
        
        Vector3 bottom = character.BoxCollider.bounds.center;
        bottom.y -= character.BoxCollider.bounds.size.y / 2;
        //bottom.y -= character.BoxCollider.bounds.size.y+100;
        line.SetPosition(0, bottom);

        if (agent.path.corners.Length < 2)
            return;

        for (int i = 1; i < agent.path.corners.Length; i++)
        {
            line.SetPosition(i, agent.path.corners[i]);
        }
        
    }

    public void DisplayPath(NavMeshPath path, Character character, CombatManager tempRef)
    {
        /*if (counter % 10 == 0)
        {
            Debug.Log("just display; corners length is " + path.corners.Length );
            foreach (Vector3 corner in path.corners)
            {
                Debug.Log(corner);
            }
        }*/
        line.positionCount = path.corners.Length;

        Vector3 bottom = character.BoxCollider.bounds.center;
        bottom.y -= character.BoxCollider.bounds.size.y / 2;
        //bottom.y -= character.BoxCollider.bounds.size.y+100;

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
            //TODO Show visual that path is not valid
            //Debug.Log("INVALID PATH! WILL MOVE TO CLOSEST POINT! Attepted Destination: " + path.corners[path.corners.Length - 1]);
            line.startColor = Color.red;
            line.endColor = Color.red;
        }
    }
    public void DisplayPathTwo(Character character, Vector3 destination, CombatManager tempRef)
    {
        Vector3 characterBottom = character.BoxCollider.bounds.center;
        //characterBottom.y -= character.BoxCollider.bounds.size.y / 2;
        characterBottom.y -= character.BoxCollider.bounds.size.y;
        NavMeshPath path = new NavMeshPath();
        line.positionCount = path.corners.Length;

        
        if (character.Agent.CalculatePath(destination, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            line.positionCount = path.corners.Length;
            if (line.positionCount != 0)
            {
                line.SetPosition(0, character.BoxCollider.bounds.center);
            }
            if (path.corners.Length < 2)
            {
                return;
            }
            if (Vector3.Distance(destination, characterBottom) <= tempRef.GetRemainingMovement())
            {
                for (int i = 1; i < path.corners.Length; i++)
                {
                    line.SetPosition(i, path.corners[i]);
                }
            }
            else
            {
                line.startColor = Color.red;
                line.endColor = Color.red;
                //If the destination is not valid, find the closest point to the destination within range
                Vector3 newDestination = (destination - characterBottom);
                newDestination.Normalize();
                newDestination = (characterBottom + (tempRef.GetRemainingMovement() * newDestination));
                NavMeshPath newPath = new NavMeshPath();
                if (character.Agent.CalculatePath(newDestination, newPath) && newPath.status == NavMeshPathStatus.PathComplete)
                {
                    for (int i = 1; i < newPath.corners.Length; i++)
                    {
                        line.SetPosition(i, newPath.corners[i]);
                    }
                }
            }
        }
    }
}

