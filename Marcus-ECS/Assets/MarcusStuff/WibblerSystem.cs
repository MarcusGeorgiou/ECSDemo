using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct WibblerSystem : ISystem
{
   [BurstCompile]
   public void OnUpdate(ref SystemState state)
   {
      //Old unJobbed ECS code
      /*var t = SystemAPI.Time.ElapsedTime;

      foreach (var (wibbler, xform) in SystemAPI.Query<RefRO<Wibbler>, RefRW<LocalTransform>>())
      {
         var height = noise.cnoise(new float2((float)t * wibbler.ValueRO.WibbleSpeed, 0)) * wibbler.ValueRO.WibbleHeight;
         xform.ValueRW.Position.y = height;
      }*/
      
      //New Jobbed ECS
      var job = new WibblerJob()
         { elapsedTime = (float)SystemAPI.Time.ElapsedTime };
      job.ScheduleParallel();
   }
}

[BurstCompile]
partial struct WibblerJob : IJobEntity
{
   public float elapsedTime;

   void Execute(in Wibbler wibbler, ref LocalTransform xform)
   {
      var height = noise.cnoise(new float2(elapsedTime * wibbler.WibbleSpeed, 0)) * wibbler.WibbleHeight;
      xform.Position.y = height;
   }
}
