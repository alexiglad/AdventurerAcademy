using UnityEngine;
public static class ShaderInformation
{
    public static Color damage = new Color(0.4528302f, 0.01452471f, 0.0189505f);
    public static Color heal = new Color(0.04372039f, 0.1667705f, 0f);
    public static float damageFlashSpeed = 2.5f;

    public static Color selected = new Color(0.1415094f, 0.07451841f, 0.05526876f);
    public static float pulsateSpeed = 0.65f;

    public static float grayOutAmount = 0.85f;

    //status colors
    public static float statusPulsateSpeed = 1.3f;
    public static Color regen = new Color(0.3679245f, 0.08122106f, 0.2086826f);
    public static Color burn = new Color(0.3686275f, 0.1349135f, 0.08235294f);
    public static Color poison = new Color(0.02148803f, 0.1037736f, 0.01213955f);
    public static Color frozen = new Color(0.03843004f, 0.1581033f, 0.1603774f);
    public static Color sleep = new Color(0f, 0f, 0f);
    public static Color knocked = new Color(0f, 0f, 0f);
    public static Color drunk = new Color(0f, 0f, 0f);
    public static Color blind = new Color(0f, 0f, 0f);




    public static string flashColor = "_flashColor";
    public static string flashSpeed = "_flashSpeed";
    public static string doFlash = "_doFlash";
    public static string flashOrPulsate = "_flashOrPulsate";
    public static string doSelection = "_doSelection";
    public static string selectionColor = "_selectionColor";
    public static string grayOut = "_grayOut";
    public static string fadeAmount = "_thanosSnapAmount";
    public static string doShake = "_doShake";

}

