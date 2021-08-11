using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetDisplay : MonoBehaviour
{
    [SerializeField] InputHandler controls;
    [SerializeField] GameStateManagerSO gameStateManager;
    [SerializeField] MovementProcessor movementProcessor;
    [SerializeField] float startWidth;
    [SerializeField] float endWidth;
    public int segments; 


    RaycastData data;
    Vector3 prevData;

    LineRenderer line;


    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.startWidth = startWidth;
        line.endWidth = endWidth;
        segments = 50;
    }

    void Update()
    {
        data = controls.GetRaycastHit();

        if (gameStateManager.GetCurrentGameState() == GameStateEnum.Combat)
        {
            CombatManager tempRef = (CombatManager)gameStateManager.GetCurrentGameStateManager();
            if (tempRef.Character != null && tempRef.Character.IsPlayer() && data.HitBool && tempRef.GetTargeting() && controls.VerifyTag(data, "Character") && data.Hit.transform.GetComponent<Character>() != null)//user is targeting and selecting a character, make sure character us proper otherwise do not highlight character
            {
                if (data.Hit.point.Equals(prevData))
                {//optimization todo fix where line stays
                    return;
                }
                line.positionCount = 0;
                HoverTarget(tempRef, data.Hit.transform.GetComponent<Character>());
            }
            else if (data.HitBool && tempRef.GetTargeting())//user is targeting and is on terrain 
            {
                if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Melee ||
                    tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Ranged ||
                    tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Heal)
                {//display characters within range
                    if (data.Hit.point.Equals(prevData))
                    {//optimization todo fix where line stays
                        return;
                    }
                    line.positionCount = 0;
                    DisplayWithinRange(tempRef);
                }
                else if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Movement)
                {
                    if (data.Hit.point.Equals(prevData))
                    {//optimization todo fix where line stays
                        return;
                    }
                    line.positionCount = 0;
                    DisplayWithinPathRange(tempRef);
                }
                else
                {//Display splash
                    if (data.Hit.point.Equals(prevData))
                    {//optimization todo fix where line stays
                        return;
                    }
                    line.positionCount = 0;
                    DisplayPointsWithinRange(tempRef);
                }
            }
            else if(data.HitBool && tempRef.Attacked && tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Movement && tempRef.Character.Animator.GetBool("walking"))
            {
                line.positionCount = 0;
                DisplayActivePath(tempRef.Character);
                MainDisplay(tempRef, tempRef.Character);
            }
            else
            {
                //display nothing
                if (data.Hit.point.Equals(prevData))
                {//optimization todo fix where line stays
                    return;
                }
                line.positionCount = 0;
                ResetTargeting(tempRef);
            }
        }
        prevData = data.Hit.point;
    }
    public void MainDisplay(CombatManager tempRef, Character charactere)
    {
        if (charactere == tempRef.Character)
        {
            charactere.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else if (movementProcessor.WithinRange(tempRef, charactere))
        {//character is valid to display
            if (tempRef.Character.IsPlayer() ^ charactere.IsPlayer())
            {
                charactere.GetComponent<SpriteRenderer>().color = Color.green;
            }
            else
            {
                charactere.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
        }
        else
        {//character is invalid to display
            charactere.GetComponent<SpriteRenderer>().color = Color.black;
        }
    }
    public void DisplayWithinRange(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            MainDisplay(tempRef, charactere);
        }
    }
    public void DisplayWithinPathRange(CombatManager tempRef)
    {

        //TODO huge bug where this doesnt consider the actual path of the character but rather just a straight line..
        //determine if this should be clickable anywhere or just on characters
        //actually im gonna say just characters


        List<Character> charactersWithinRange = movementProcessor.GetCharactersInLine(tempRef.Character.transform.position, data.Hit.point, tempRef.Turn.GetAbility().Radius);
        foreach (Character charactere in tempRef.Characters)
        {
            if (charactersWithinRange.Contains(charactere))
            {
                charactere.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                MainDisplay(tempRef, charactere);
            }
        }
        DisplayPath(tempRef);
    }
    public void DisplayPointsWithinRange(CombatManager tempRef)
    {
        line.positionCount = segments + 1;

        CreatePoints(data.Hit.point, tempRef.Turn.GetAbility().Radius);
        CheckForLineColor(tempRef, data.Hit.point);
        foreach (Character charactere in tempRef.Characters)
        {
            if (movementProcessor.WithinRange(tempRef, charactere, data.Hit.point))
            {
                charactere.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            else
            {
                MainDisplay(tempRef, charactere);
            }
        }
    }
    public void HoverTarget(CombatManager tempRef, Character character)
    {
        if(movementProcessor.WithinRange(tempRef, character))
        {
            if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Splash)
            {
                line.positionCount = segments + 1;
                Vector3 bottom = character.BoxCollider.bounds.center;
                bottom.y -= character.BoxCollider.bounds.size.y / 2;
                CreatePoints(bottom, tempRef.Turn.GetAbility().Radius);
                foreach (Character charactere in tempRef.Characters)
                {
                    if (movementProcessor.WithinRange(tempRef, charactere, bottom))
                    {
                        charactere.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else
                    {
                        MainDisplay(tempRef, charactere);
                    }
                }
            }
            else if(tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Movement)
            {
                DisplayPath(tempRef);//change this to special method which just goes close to obstacle
                List<Character> charactersWithinRange = movementProcessor.GetCharactersInLine(tempRef.Character.transform.position, data.Hit.point, tempRef.Turn.GetAbility().Radius);
                foreach (Character charactere in tempRef.Characters)
                {
                    if (charactersWithinRange.Contains(charactere))
                    {
                        charactere.GetComponent<SpriteRenderer>().color = Color.blue;
                    }
                    else
                    {
                        MainDisplay(tempRef, charactere);
                    }
                }

                //todo implement this part
            }
            else
            {
                character.GetComponent<SpriteRenderer>().color = Color.blue;
                foreach (Character charactere in tempRef.Characters)
                {
                    if (charactere != character)
                    {
                        MainDisplay(tempRef, charactere);
                    }
                }
            }
        }
        else
        {
            DisplayWithinRange(tempRef);
            if (tempRef.Turn.GetAbility().AbilityType == AbilityTypeEnum.Splash)
            {
                line.positionCount = segments + 1;
                Vector3 bottom = character.BoxCollider.bounds.center;
                bottom.y -= character.BoxCollider.bounds.size.y / 2;
                CreatePoints(bottom, tempRef.Turn.GetAbility().Radius);
            }
        }
    }
    public void CheckForLineColor(CombatManager tempRef, Vector3 position)
    {
        if(movementProcessor.WithinSplashRange(tempRef, position))
        {
            line.startColor = Color.blue;
            line.endColor = Color.blue;
        }
        else
        {
            line.startColor = Color.red;
            line.endColor = Color.red;
        }
    }

    public void ResetTargeting(CombatManager tempRef)
    {
        foreach (Character charactere in tempRef.Characters)
        {
            charactere.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    void CreatePoints(Vector3 position, float radius)
    {

        float x;
        float z;
        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius + position.x;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius + position.z;

            line.SetPosition(i, new Vector3(x, position.y + .2f, z));

            angle += (360f / segments);
        }
    }
    void DisplayPath(CombatManager tempRef)
    {
        if(tempRef.Character.Agent.enabled)
        {
            NavMeshPath path = new NavMeshPath();
            NavMeshAgent agent = tempRef.Character.Agent;
            agent.CalculatePath(data.Hit.point, path);
            line.positionCount = path.corners.Length;

            Vector3 bottom = tempRef.Character.BoxCollider.bounds.center;
            bottom.y -= tempRef.Character.BoxCollider.bounds.size.y / 2;

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
            if (!IsValidPath(tempRef))
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
    public void DisplayActivePath(Character character)
    {
        if (character.Agent.hasPath)
        {
            line.positionCount = character.Agent.path.corners.Length;

            Vector3 bottom = character.BoxCollider.bounds.center;
            bottom.y -= character.BoxCollider.bounds.size.y / 2;
            line.SetPosition(0, bottom);

            if (character.Agent.path.corners.Length < 2)
                return;

            for (int i = 1; i < character.Agent.path.corners.Length; i++)
            {
                line.SetPosition(i, character.Agent.path.corners[i]);
            }
        }
    }
    bool IsValidPath(CombatManager tempRef)
    {
        return Vector3.Distance(data.Hit.point, tempRef.Character.transform.position) <= tempRef.Turn.GetAbility().Range;
    }



}

