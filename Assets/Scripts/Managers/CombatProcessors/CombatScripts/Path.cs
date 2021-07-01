using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    List<Vector3> validPath;
    List<Vector3> invalidPath;
    public Path(List<Vector3> validPath, List<Vector3> invalidPath)
    {
        this.validPath = validPath;
        this.invalidPath = invalidPath;
    }

    public List<Vector3> ValidPath { get => validPath; set => validPath = value; }
    public List<Vector3> InvalidPath { get => invalidPath; set => invalidPath = value; }
}