using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPosRefScript : MonoBehaviour
{
    public EnemyType enemyType;

    public enum EnemyType
    {
        dummy,
        contactdamage,
        melee,
        rangedprojectile
    }
}
