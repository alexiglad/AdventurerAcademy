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
        healthBar.transform.scale = UpdateBar(health.GetFloatValue() / maxHealth.GetFloatValue()); //Updates HP bar
        manaBar.transform.scale = UpdateBar(mana.GetFloatValue() / maxMana.GetFloatValue()); //Updates MP bar
        staminaBar.transform.scale = UpdateBar(stamina.GetFloatValue() / maxStamina.GetFloatValue()); //Updates SP bar
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
