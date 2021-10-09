using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Processors/ShaderProcessor")]

public class ShaderProcessor : ScriptableObject
{
    [SerializeField] GameStateManagerSO gameStateManager;
    void SetSpriteShader(Character character, SpriteShaderTypeEnum spriteShader)
    {
        switch (spriteShader)
        {
            case SpriteShaderTypeEnum.Damage:
            {


                break;
            }
            case SpriteShaderTypeEnum.Heal:
            {


                break;
            }
            case SpriteShaderTypeEnum.Selected:
            {


                break;
            }
            case SpriteShaderTypeEnum.Unable:
            {


                break;
            }
            case SpriteShaderTypeEnum.Death:
            {


                break;
            }
            case SpriteShaderTypeEnum.Regen:
            {


                break;
            }
            case SpriteShaderTypeEnum.Burn:
            {


                break;
            }
            case SpriteShaderTypeEnum.Poison:
            {


                break;
            }
            case SpriteShaderTypeEnum.Frozen:
            {


                break;
            }
            case SpriteShaderTypeEnum.Sleep:
            {


                break;
            }
            case SpriteShaderTypeEnum.Knocked:
            {
                   
                break;
            }
            case SpriteShaderTypeEnum.Drunk:
            {
                
                break;
            }
            case SpriteShaderTypeEnum.Blind:
            {
                
                break;
            }
            case SpriteShaderTypeEnum.None:
            {
                StopAllShaders(character);
                break;
            }
            default:
            {
                
                break;
            }

        }
    }



    void StopAllShaders(Character character)
    {

    }
}
