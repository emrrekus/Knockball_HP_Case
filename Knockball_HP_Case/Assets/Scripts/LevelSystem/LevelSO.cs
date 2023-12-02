using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/LevelSO")]
public class LevelSO : ScriptableObject
{
   [SerializeField] private int _level;
   [SerializeField] private int _childCount;
   [SerializeField] private int _neededBall;

   public int Level => _level;
   public int ChildCount => _childCount;

   public int NeededBall => _neededBall;
}
