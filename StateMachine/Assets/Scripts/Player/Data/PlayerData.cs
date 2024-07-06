using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData/Player_Data")]
public class PlayerData : ScriptableObject
{
    [Header("Run State")]
    public float runSpeed;
    [Header("Jump State")]
    public float jumpSpeed;
    public int jumpAmount;
    public int amountOfJumps;

}
