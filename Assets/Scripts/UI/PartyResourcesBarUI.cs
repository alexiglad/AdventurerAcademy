using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

public class PartyResourcesBarUI : MonoBehaviour
{
    [SerializeField] private FloatValueSO health;
    [SerializeField] private FloatValueSO maxHealth;
    [SerializeField] private FloatValueSO mana;
    [SerializeField] private FloatValueSO maxMana;
    [SerializeField] private FloatValueSO stamina;
    [SerializeField] private FloatValueSO maxStamina;
    [SerializeField] protected GameStateManagerSO gameStateManager;
    [SerializeField] protected GameStateSO gameState;

    private VisualElement healthBar;
    private VisualElement manaBar;
    private VisualElement staminaBar;


    
    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        healthBar = rootVisualElement.Q<VisualElement>("HealthBar");
        manaBar = rootVisualElement.Q<VisualElement>("ManaBar");
        staminaBar = rootVisualElement.Q<VisualElement>("StaminaBar");


        

    }
    private void Update()
    {
        if (gameState.GetGameState() == GameStateEnum.Combat) {
            healthBar.transform.scale = UpdateBar(health.GetFloatValue() / maxHealth.GetFloatValue()); //Updates HP bar
            manaBar.transform.scale = UpdateBar(mana.GetFloatValue() / maxMana.GetFloatValue()); //Updates MP bar
            staminaBar.transform.scale = UpdateBar(stamina.GetFloatValue() / maxStamina.GetFloatValue()); //Updates SP bar


            //drawing movement line

            /*CombatManager tempRef = (CombatManager)gameStateManager.GetGameStateManager();
            if (tempRef.Character.GetPlayer())
            {//only do this if user is playing
                Vector2 mousePos = Input.mousePosition;

            TODO
            }*/
        }
    }
    private Vector3 UpdateBar(float sizeNormalized)
    {
        if (sizeNormalized >= 0 && sizeNormalized <= 1)
            return new Vector3(sizeNormalized, 1f);
        else
            switch (sizeNormalized < 0)
            {
                case true: return new Vector3(0, 1f);
                case false: return new Vector3(1, 1f);
            }
    }
}
