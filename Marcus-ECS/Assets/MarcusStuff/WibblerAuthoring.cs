using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public struct Wibbler : IComponentData
{
    public float WibbleSpeed;
    public float WibbleHeight;

    public static Wibbler Random(uint seed)
    {
        var random = new Random(seed);
        return new Wibbler()
        {
            WibbleHeight = random.NextFloat(1f, 5f),
            WibbleSpeed = random.NextInt(1, 3)
        };
    }
}

public class WibblerAuthoring : MonoBehaviour
{
    public float wibbleSpeed = 1;
    public float wibbleHeight = 1;

    class Baker : Baker<WibblerAuthoring>
    {
        public override void Bake(WibblerAuthoring authoring)
        {
            var data = new Wibbler()
            {
                WibbleSpeed = authoring.wibbleSpeed,
                WibbleHeight = authoring.wibbleHeight
            };
            AddComponent(GetEntity(TransformUsageFlags.Dynamic), data);
        }
    }
}
