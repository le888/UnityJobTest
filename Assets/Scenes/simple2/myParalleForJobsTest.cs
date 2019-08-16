using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using Unity.Collections;
struct myParalleForJobs : IJobParallelFor
{
    [ReadOnly]
    public float a;
    [ReadOnly]
    public float b;
    public NativeArray<float> result;

    public void Execute(int index)
    {
        result[index] = index + a + b;
    }
   
}
public class myParalleForJobsTest : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        NativeArray<float> array = new NativeArray<float>(2,Allocator.TempJob);
        myParalleForJobs job = new myParalleForJobs();
        job.a = 1;
        job.b = 3;
        job.result = array;

        JobHandle handle = job.Schedule(2,2);
        handle.Complete();
        var data = job.result[1];
        Debug.LogError(data);

        array.Dispose();
    }
}
