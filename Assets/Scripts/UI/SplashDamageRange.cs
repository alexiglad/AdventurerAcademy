using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class SplashDamageRange : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float radius;
    LineRenderer line;
    public void Awake()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.positionCount = 0;
    }
    public void Run(Vector3 position, float radius)
    {
        this.radius = radius;
        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        CreatePoints(position);
        //wait 2 seconds
        Invoke("Kill", 1);
    }

    void CreatePoints(Vector3 position)
    {
        float x;
        //float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius + position.x;
            z = Mathf.Cos(Mathf.Deg2Rad * angle) * radius + position.z;

            line.SetPosition(i, new Vector3(x, 0, z));

            angle += (360f / segments);
        }
    }
    public void Kill()
    {
        //Destroy(line.gameObject);
        line.positionCount = 0;
        
        //line = new LineRenderer();
        //line = gameObject.GetComponent<LineRenderer>();
    }
}