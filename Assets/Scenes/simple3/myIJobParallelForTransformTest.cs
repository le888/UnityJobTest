using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
using UnityEngine.Jobs;

struct  myIJobParallelForTransform :  IJobParallelForTransform
{
    [ReadOnly]
    public float a;
    [ReadOnly]
    public float b;
    public NativeArray<float> result;

    public void Execute(int index, TransformAccess transform)
    {
        var pos = transform.position;
        pos.x = a + b;
        transform.position = pos;
        result[index] = pos.x;
    }
}
public class myIJobParallelForTransformTest : MonoBehaviour
{
   public Transform[] tlist;
    // Update is called once per frame
    void Update()
    {
        NativeArray<float> array = new NativeArray<float>(tlist.Length,Allocator.TempJob);
        myIJobParallelForTransform job = new myIJobParallelForTransform();
        job.a = 1;
        job.b = 3;
        job.result = array;

        TransformAccessArray ta = new TransformAccessArray(tlist);
        JobHandle handle = job.Schedule(ta);
        handle.Complete();
        var data = job.result[1];
        Debug.LogError(data);

        array.Dispose();
        ta.Dispose();
    }
}
