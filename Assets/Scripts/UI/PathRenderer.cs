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
    Color lineColor;
    NavMeshAgent agent;
    LineRenderer line;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        if (isValid)
            lineColor = Color.green;
        else
            lineColor = Color.red;
    }

    void Update()
    {
        data = controls.GetRaycastHit();
        if(gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
            if (data.HitBool && controls.VerifyTag(data, "Terrain"))
            {
                CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
                NavMeshPath path = new NavMeshPath();
                agent = tempRef.Character.Agent;
                agent.CalculatePath(data.Hit.point, path);
                transform.position = tempRef.Character.transform.position;
                DisplayPath(tempRef.DisplayPath(path, isValid), lineColor, tempRef.Character);
            }
    }

    public void DisplayPath(List<Vector3> path, Color color, Character character)
    {
        line.positionCount = path.Count;

        for (int i = 1; i < path.Count; i++)
        {
            Debug.Log(path[i] - character.transform.position);
            line.SetPosition(i, path[i] - character.transform.position);
        }


        line.startColor = color;
        line.endColor = color;
    }
}
