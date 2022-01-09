using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreadDispatcher : SingletonOnLoad<ThreadDispatcher>
{
    private Queue<System.Action> jobs = new Queue<System.Action>();
    private int ownerThreadId;
    protected override void Awake()
    {
        base.Awake();
        ownerThreadId = System.Threading.Thread.CurrentThread.ManagedThreadId;
    }

    public void RunOnMainThread(System.Action newJob)
    {
        if (ManagesThisThread())
        {
            newJob();
        }
        else
        {
            lock (jobs)
            {
                jobs.Enqueue(newJob);
            }
        }
    }

    void Update()
    {
        System.Diagnostics.Debug.Assert(ManagesThisThread());

        System.Action job;
        while (true)
        {
            lock (jobs)
            {
                if (jobs.Count > 0)
                {
                    job = jobs.Dequeue();
                }
                else
                {
                    break;
                }
            }
            job?.Invoke();
        }
    }

    private bool ManagesThisThread()
    {
        return System.Threading.Thread.CurrentThread.ManagedThreadId == ownerThreadId;
    }
}