using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private GameStateManagerSO currentGameStateManager;
    [SerializeField] UIHandler uiHandler;
    [SerializeField] GameControllerSO gameController;
    [SerializeField] InputHandler controls;
    [SerializeField] CharacterListSO characterList;

    [SerializeField] PlayerData playerData;
    [SerializeField] GameObject[] playerPrefabs;//this stores all the prefabs for all characters to create dynamically
    SceneSO sceneSO;
    [SerializeField] AllSceneReferencesSO sceneReferences;
    CanvasGroup fadeImage;

    #region gamecontroller basic methods
    void OnEnable()
    {
        if(playerData.CurrentMission == null)
        {            //temp code to load up immediate scene (aka when testing)
            foreach(SceneSO scene in sceneReferences.scenes)
            {
                if(scene.name.Equals(SceneManager.GetActiveScene().name))
                {
                    sceneSO = scene;
                    break;
                }
            }
            if (sceneSO == null)
            {
                Debug.Log("missing sceneSO for given scene please create and add to allSceneReferencesSO");
                //if this is still true
            }
        }
        else//when using load system
        {
            sceneSO = playerData.CurrentMission.Subscenes[playerData.CurrentMission.Pos];
        }
        Application.targetFrameRate = 60;//temp code

        gameController.SetGameController(this);

        controls.ManualAwake();
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        fadeImage = GameObject.Find("LoadScreen").GetComponent<CanvasGroup>();
        bool dontLoad = false;
        foreach(Character character in characterList.GetCharacters())
        {
            if(character.IsPlayer())
            {
                dontLoad = true;
            }
        }
        if (sceneSO.CharacterPositions.Length == 0 || dontLoad)
        {
            //Debug.Log("ERROR PLEASE ADD POSITIONS");
            currentGameStateManager.CreateStateInstance(sceneSO.TargetGameState, characterList.GetCharacters());//actual code for release
            return;
        }
        int pos = 0;
        if (sceneSO.CharacterPositions.Length == 0)
        {
            Debug.Log("please add positions on gamecontroller for characters to spawn");
        }
        foreach (CharacterIDEnum name in playerData.MissionCharacters)
        {
            if (pos >= sceneSO.CharacterPositions.Length)
            {
                Debug.Log("possible error tried to add to many characters to given scene");
            }
            foreach (GameObject prefab in playerPrefabs)
            {
                if (prefab.GetComponent<Player>().CharacterID == name)
                {
                    GameObject go = Instantiate(prefab, sceneSO.CharacterPositions[pos], Quaternion.identity);
                    Character character = go.GetComponent<Character>();
                    characterList.AddCharacter(character);
                    character.ManualAwake();
                    break;//breaks out of currrent foreach loop bc found equivalent prefab
                }
            }
            pos++;
        }

        currentGameStateManager.CreateStateInstance(sceneSO.TargetGameState, characterList.GetCharacters());


    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
        Destroy(this);

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        StartCoroutine(FadeIn());
    }
    void OnSceneUnloaded(Scene scene)
    {
        StartCoroutine(FadeOut());
    }
    #endregion

    #region coroutines
    IEnumerator FadeIn()
    {
        fadeImage.alpha = 1f;
        yield return new WaitForSeconds(1f);
        fadeImage.LeanAlpha(0, .5f);
        yield return new WaitForSeconds(1f);
        fadeImage.blocksRaycasts = false;
        controls.GetControls().Enable();
    }

    IEnumerator FadeOut()
    {
        fadeImage.blocksRaycasts = true;
        controls.GetControls().Disable();
        fadeImage.LeanAlpha(1, 1f);
        yield return new WaitForSeconds(1f);
    }
    
    
    public void StartCoroutineTime(float time, Action action)
    {
        StartCoroutine(Routine(time, action));
    }
    IEnumerator Routine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action.Invoke();
    }
    public void StartDoubleCoroutineTime(float time, Action action, Action action2)
    {
        StartCoroutine(Routine0(time, action, action2));
    }
    IEnumerator Routine0(float time, Action action, Action action2)
    {
        action.Invoke();
        yield return new WaitForSeconds(time);
        action2.Invoke();
    }
    public void StartCoroutineCC(Action action)
    {
        StartCoroutine(Routine1(action));
    }
    IEnumerator Routine1(Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            action.Invoke();
        }
        else if(currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(RoamingManager))
        {
            RoamingManager tempRef = (RoamingManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            action.Invoke();
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineNMA(Action action, List<Character> turnOrder)
    {
        StartCoroutine(Routine2(action, turnOrder));
    }
    IEnumerator Routine2(Action action, List<Character> turnOrder)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            yield return new WaitUntil(tempRef.CanContinueMethod);
            tempRef.DisableCombatInput();
            uiHandler.UpdateCombatTurnUI(tempRef.Character);
            uiHandler.UpdateCameraPan(tempRef.Character.transform);
            //yield return new WaitForSeconds(.01f);
            uiHandler.StopDisplayingAbilities();
            uiHandler.StopDisplayingEndTurn();
            uiHandler.EnableAPBar(false);
            uiHandler.UpdateTurnOrder(turnOrder);
            yield return new WaitUntil(tempRef.CanContinueMethod);
            //uiHandler.UpdateCombatTurnUI(tempRef.Character);
            tempRef.EnableCombatInput();
            action.Invoke();
            //eventually add animation here for switching turns
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineTOS(float time, Action action)
    {
        StartCoroutine(Routine3(time, action));
    }
    IEnumerator Routine3(float time, Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            yield return new WaitForSeconds(time);
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            action.Invoke();
            tempRef.DisableCombatInput();
            uiHandler.UpdateCameraPan(tempRef.Character.transform);
            yield return new WaitUntil(tempRef.CanContinueMethod);
            tempRef.EnableCombatInput();
        }
        else
        {
            Debug.Log("error");
        }
    }
    public void StartCoroutineNMAGravity(Action action, SortedSet<Character> characters)
    {
        StartCoroutine(Routine4(action, characters));
    }
    IEnumerator Routine4(Action action, SortedSet<Character> characters)
    {
        foreach (Character character in characters)
        {
            character.Initializing = true;
            character.Obstacle.enabled = false;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (Character character in characters)
        {
            character.Agent.enabled = true;
        }
        yield return new WaitForSeconds(0.25f);
        foreach (Character character in characters)
        {
            character.Agent.enabled = false;
            character.Obstacle.enabled = true;
            character.Initializing = false;
        }
        action?.Invoke();
    }
    public void StartCoroutineDisplayEnemyAbility(Character attacker, Character attackee, Action action)
    {
        StartCoroutine(Routine5(attacker, attackee, action));
    }
    IEnumerator Routine5(Character attacker, Character attackee, Action action)
    {
        if (currentGameStateManager.GetCurrentGameStateManager().GetType() == typeof(CombatManager))
        {
            
            CombatManager tempRef = (CombatManager)currentGameStateManager.GetCurrentGameStateManager();
            tempRef.DisableCombatInput();
            yield return new WaitForSeconds(.5f);
            uiHandler.UpdateCameraPan(attackee.transform);
            yield return new WaitForSeconds(.5f);
            //TODO project selection thing on character
            uiHandler.SetTargetCharacterHover(attackee);
            yield return new WaitForSeconds(1f);
            uiHandler.StopDisplayingTargetCharacterHover();
            action.Invoke();
        }
    }
    #endregion methods
}
