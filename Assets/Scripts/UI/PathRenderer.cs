using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathRenderer : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] bool isValid;

    RaycastData data;
    NavMeshAgent agent;
    //LineRenderer line;
    //LineRenderer line2;
    LineRenderer[] line = new LineRenderer [2];

    void Start()
    {
        //line = GetComponent<LineRenderer>();
        //line2 = GetComponent<LineRenderer>();
        line[0] = GetComponent<LineRenderer>();
        line[1] = GetComponentInChildren<LineRenderer>();
        line[0].useWorldSpace = false;
        line[1].useWorldSpace = false;
        line[0].startColor = Color.blue;
        line[1].startColor = Color.red;
    }

    void Update()
    {
        line[0].positionCount = 0;
        line[1].positionCount = 0;
        data = controls.GetRaycastHit();
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
            if (data.HitBool && controls.VerifyTag(data, "Terrain"))
            {
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                NavMeshPath path = new NavMeshPath();
                agent = tempRef.Character.Agent;
                agent.CalculatePath(data.Hit.point, path);
                line[0].SetPosition(0, tempRef.Character.transform.position);
                DisplayPath(tempRef.DisplayPath(path), tempRef.Character);
            }
    }

    public void DisplayPath(Path path, Character character)
    {
        line[0].positionCount = path.ValidPath.Count;

        for (int i = 1; i < path.ValidPath.Count; i++)
        {
            Debug.Log("Valid path" + (path.ValidPath[i] - character.transform.position));
            line[0].SetPosition(i, path.ValidPath[i] - character.transform.position);
        }

        line[1].positionCount = path.ValidPath.Count;
        line[1].SetPosition(0, path.ValidPath[path.ValidPath.Count - 1] - character.transform.position);

        for (int i = 1; i < path.ValidPath.Count; i++)
        {
            //Debug.Log(path.ValidPath[i] - character.transform.position);
            line[1].SetPosition(i, path.ValidPath[i] - character.transform.position);
        }

    }
}
