using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    Camera defaultCamera;
    [SerializeField] float startWidth;
    [SerializeField] float endWidth;
    bool moving;
    RaycastData data;
    Vector3 prevData;
    NavMeshAgent agent;
    LineRenderer line;
    readonly float PathDistance = 0.25f;
    int prevNumDots = 0;

    void Start()
    {
        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
                    if (tempRef.Character.Animator.GetBool("moving"))
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
                        PathPooling.sharedInstance.DeleteDots(prevNumDots);
                    }
                }
                else
                {
                    PathPooling.sharedInstance.DeleteDots(prevNumDots);
                }
                break;
        }
        prevData = data.Hit.point;
    }

    public void DisplayActivePath(Character character)
    {
        //todo implement differently
        PathPooling.sharedInstance.DeleteDots(prevNumDots);
        if (agent.hasPath)
        {
            int numDots = 0;
            line.positionCount = agent.path.corners.Length;
            //DisplayDot(character.CharacterBottom());
            float totalPathlength = 0;
            for (int i = 1; i < agent.path.corners.Length; i++)
            {
                totalPathlength += (agent.path.corners[i] - agent.path.corners[i - 1]).magnitude;
            }
            int place = 1;
            Vector3 location = character.CharacterBottom();
            float currentLengthLeft = agent.path.corners[place].magnitude;
            
            for (float i = 0; i < totalPathlength; i+=PathDistance)
            {
                Vector3 direction = (agent.path.corners[place] - agent.path.corners[place - 1]).normalized;//todo optimize?
                if (PathDistance <= currentLengthLeft)
                {
                    //draw dot
                    location += direction * PathDistance;
                    currentLengthLeft -= PathDistance;
                }
                else
                {//go onto next path with remaining of this path
                    location += direction * currentLengthLeft;//sets location to end of first path
                    place++;
                    if(place >= agent.path.corners.Length)
                    {
                        DisplayDot(location, numDots, ShaderTypeEnum.EndPath);
                        numDots++;
                        break;
                    }
                    Vector3 tip = (PathDistance - currentLengthLeft) * ((agent.path.corners[place] - agent.path.corners[place - 1]).normalized);
                    location += tip;
                    currentLengthLeft = agent.path.corners[place].magnitude - tip.magnitude;
                }

                DisplayDot(location, numDots, ShaderTypeEnum.ValidPath);
                numDots++;
            }
            prevNumDots = numDots;
        }
    }

    public void DisplayPath(NavMeshPath path, Character character, CombatManager tempRef)
    {
        if(path.corners.Length < 2)
        {
            return;
        }
        PathPooling.sharedInstance.DeleteDots(prevNumDots);
        int numDots = 0;
        line.positionCount = path.corners.Length;
        float totalPathlength = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            totalPathlength += (path.corners[i] - path.corners[i-1]).magnitude;
        }
        int place = 1;
        Vector3 location = character.CharacterBottom();
        float currentLengthLeft = (path.corners[place] - path.corners[place - 1]).magnitude;

        for (float i = 0; i < totalPathlength; i += PathDistance)
        {
            Vector3 direction = (path.corners[place] - path.corners[place - 1]).normalized;//todo optimize?
            if (PathDistance <= currentLengthLeft)
            {
                //draw dot
                location += direction * PathDistance;
                currentLengthLeft -= PathDistance;
            }
            else
            {//go onto next path with remaining of this path
                location += direction * currentLengthLeft;//sets location to end of first path
                place++;
                if (place >= path.corners.Length)
                {
                    //DisplayDot(location, numDots, ShaderTypeEnum.EndPath);
                    //DisplayDot(path.corners[place-1], numDots, ShaderTypeEnum.EndPath);
                    numDots++;
                    break;
                }
                Vector3 tip = (PathDistance - currentLengthLeft) * ((path.corners[place] - path.corners[place - 1]).normalized);
                location += tip;
                currentLengthLeft = (path.corners[place] - path.corners[place - 1]).magnitude - tip.magnitude;
            }
            ShaderTypeEnum shader = ShaderTypeEnum.ValidPath;
            if(i > tempRef.GetRemainingMovement())
            {
                shader = ShaderTypeEnum.InvalidPath;
            }
            DisplayDot(location, numDots, shader);
            numDots++;
        }
        prevNumDots = numDots;
        /*
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
        */
    }
    void DisplayDot(Vector3 position, int i, ShaderTypeEnum shader)
    {
        position.y += .2f;
        //TODO REPLACE WITH RAYCAST CODE
        Vector3 vector = defaultCamera.ScreenToWorldPoint(position);
        //Ray ray = defaultCamera.ScreenToWorldPoint(position);
        //RaycastHit hit;
        //Physics.Raycast(ray, out hit, Mathf.Infinity);
        GameObject dot = PathPooling.sharedInstance.GetPooledObject(i);
        if(dot == null)
        {
            return;
        }
        dot.transform.position = position;
        //TODO set shader here for sprite
        dot.SetActive(true);
    }

}

