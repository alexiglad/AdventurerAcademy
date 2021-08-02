using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
[CreateAssetMenu(fileName = "MissionData", menuName = "ScriptableObjects/Missions/MissionData")]
public class MissionDataSO : ScriptableObject
{
    [SerializeField] new string name;//name for searching

    [SerializeField] MapStatusEnum mapStatus;//map status either visible, invisible, or completed

    //a mission can have anywhere from 0-infinite unlocks
    //a mission directly launched from roaming would never be unlocked but would rather just get
    //initiated directly from roaming
    [SerializeField] int numRequisites;//when this hits 0 the mission is unlocked


    [SerializeField] string[] unlocks;//this is an array containing the strings of all other scenes
    //that completing this mission unlocks

    //PREREQ/UNLOCK SYSTEM
    //every object affecting a mission contains an array of strings
    //containing references to the keys of each mission it unlocks when completed/triggered

    //when that object (i.e. another mission/npc/academy event) 
    //is succesfully completed/triggered it goes through its array of strings "unlocks"
    //and subtracts one from the "numrequisites" of each of the missions it affects
    //its string unlocks is the key to the atlas



    [SerializeField] Vector3 endingWeight;//x,y,z correspond to humans, monsters, neutral
    //so 1, 0, 0 would be +1 for humans and 0 for monsters/neutral
    //if the mission is failed then do -1?? determine this
    //TODO discuss this idea with cedric

    [SerializeField] string[] subscenes;//this is all the subscenes associated with the mission
    //to complete a mission a user must go through all the subscenes if this array has size 0 once
    //they complete the original mission scene they are finished

    [SerializeField] Vector3 mapLocation;//is vector3 just in case we wanna go with a 3D map

    //todo add weight for affecting characters
    

    //UI stuff
    [SerializeField] Sprite image;
    [SerializeField] float dimWidth;
    [SerializeField] float dimHeight;

    public MapStatusEnum MapStatus { get => mapStatus; set => mapStatus = value; }
    public int NumRequisites { get => numRequisites; set => numRequisites = value; }
    public string[] Unlocks { get => unlocks; set => unlocks = value; }
    public Vector3 Weight { get => endingWeight; set => endingWeight = value; }
    public string[] Subscenes { get => subscenes; set => subscenes = value; }
    public Vector3 MapLocation { get => mapLocation; set => mapLocation = value; }
    public Sprite Image { get => image; set => image = value; }
    public float DimWidth { get => dimWidth; set => dimWidth = value; }
    public float DimHeight { get => dimHeight; set => dimHeight = value; }

    /*Stores Map data(How to unlock location, missions tied to it 
    (Combat, roaming, etc^), ending weight data upon completion/failure^, mapstatus: 
    visible/invisible/completed,  Faction/Character relation data upon completion/failure^, 
    region/local map image, subscenes(list of scenes tied to a mission), location on map)*/
}
