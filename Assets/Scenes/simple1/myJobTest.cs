using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
struct MyJob : IJob
{
    [ReadOnly]
    public float a;
    [ReadOnly]
    public float b;
    public NativeArray<float> result;
    public void Execute()
    {
        result[0] = a + b;
    }
}
public class myJobTest : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        NativeArray<float> array = new NativeArray<float>(1,Allocator.TempJob);
        MyJob job = new MyJob();
        job.a = 1;
        job.b = 3;
        job.result = array;

        JobHandle handle = job.Schedule();
        handle.Complete();
        var data = job.result[0];
        Debug.LogError(data);

        array.Dispose();
    }
}
