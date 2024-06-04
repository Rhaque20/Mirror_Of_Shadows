using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot VFX Parameter", menuName = "Loot VFX Parameter")]
public class LootVFXParameter: ScriptableObject
{
    public float circleMeshUpSpeed,circleMeshSize,glowGroundSize;
    public int particleRate,flareRate,circleMeshRate;
    public Vector2 flareSize;
    [ColorUsage(true, true)]
    public Color rarityColor;
}