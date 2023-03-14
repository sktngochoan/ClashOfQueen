using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CointAnimation : MonoBehaviour
{
    Timer timer;
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 2f;
        timer.Run();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Finished)
        {
            Destroy(gameObject);
        }
    }
}
