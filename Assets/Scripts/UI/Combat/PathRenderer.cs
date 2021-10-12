using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    Camera defaultCamera;
    bool moving;
    RaycastData data;
    Vector3 prevData;
    NavMeshAgent agent;
    readonly float PathDistance = 0.1f;
    readonly float deleteDistance = .25f;
    int prevNumDots = 0;
    int dotLoc = 0;

    void Start()
    {
        defaultCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        data = controls.GetRaycastHit();

        switch(gameStateManager.GetCurrentGameState())
        {
            case GameStateEnum.Combat:
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                if (!tempRef.AttackingOrMoving && tempRef.Character != null && tempRef.Character.IsPlayer() && !tempRef.GetTargeting() &&  tempRef.Character.Agent.enabled)
                {
                    if (tempRef.Character.Animator.GetBool("moving"))
                    {
                        moving = true;
                        DisplayActivePath(tempRef.Character);
                    }
                    else if (tempRef.Character.GetMaxMovement() >0 && tempRef.CanContinue && tempRef.Character.Agent != null && data.HitBool && controls.VerifyTag(data, "Terrain"))
                    {
                        dotLoc = 0;
                        if (!moving && data.Hit.point.Equals(prevData))
                        {//optimization todo fix where line stays
                            return;
                        }
                        moving = false;
                        NavMeshPath path = new NavMeshPath();
                        agent = tempRef.Character.Agent;
                        agent.CalculatePath(data.Hit.point, path);
                        DisplayPath(path, tempRef.Character, tempRef);
                    }
                    else
                    {
                        dotLoc = 0;
                        PathPooling.sharedInstance.DeleteDots(prevNumDots);
                        prevNumDots = 0;
                    }
                }
                else
                {
                    dotLoc = 0;
                    PathPooling.sharedInstance.DeleteDots(prevNumDots);
                    prevNumDots = 0;
                }
                break;
        }
        prevData = data.Hit.point;
    }

    public void DisplayActivePath(Character character)
    {
        if(PathPooling.sharedInstance.pooledObjects[dotLoc] != null)
        {
            Vector3 dotLocation = PathPooling.sharedInstance.pooledObjects[dotLoc].transform.position;
            dotLocation.y -= .1f;
            dotLocation.z += .1f;

            if (Mathf.Abs((character.CharacterBottom() - dotLocation).magnitude) <= deleteDistance)
            {
                PathPooling.sharedInstance.DeleteDots(dotLoc);
                dotLoc++;
            }
        }
        if (PathPooling.sharedInstance.pooledObjects[dotLoc + 1] != null)//check one ahead as well as a fail safe
        {
            Vector3 dotLocation = PathPooling.sharedInstance.pooledObjects[dotLoc + 1].transform.position;
            dotLocation.y -= .1f;
            dotLocation.z += .1f;

            if (Mathf.Abs((character.CharacterBottom() - dotLocation).magnitude) <= deleteDistance)
            {
                PathPooling.sharedInstance.DeleteDots(dotLoc + 1);
                dotLoc+=2;
            }
        }
        if (PathPooling.sharedInstance.pooledObjects[dotLoc + 2] != null)//check one ahead as well as a fail safe
        {
            Vector3 dotLocation = PathPooling.sharedInstance.pooledObjects[dotLoc + 2].transform.position;
            dotLocation.y -= .1f;
            dotLocation.z += .1f;

            if (Mathf.Abs((character.CharacterBottom() - dotLocation).magnitude) <= deleteDistance)
            {
                PathPooling.sharedInstance.DeleteDots(dotLoc + 2);
                dotLoc += 3;
            }
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
        float totalPathlength = 0;
        for (int i = 1; i < path.corners.Length; i++)
        {
            totalPathlength += (path.corners[i] - path.corners[i-1]).magnitude;
        }
        if(totalPathlength > character.GetMaxMovement())
        {
            totalPathlength = character.GetMaxMovement();
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
            PathShaderTypeEnum shader = PathShaderTypeEnum.ValidPath;
            if(i > tempRef.Character.GetMaxMovement())
            {
                shader = PathShaderTypeEnum.InvalidPath;
            }
            DisplayDot(location, numDots, shader);
            numDots++;
        }
        prevNumDots = numDots;
    }
    void DisplayDot(Vector3 position, int i, PathShaderTypeEnum shader)
    {
        position.y += .1f;
        position.z -= .1f;
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

