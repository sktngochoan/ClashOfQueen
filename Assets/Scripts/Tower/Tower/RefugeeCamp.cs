using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefugeeCamp : MonoBehaviour
{
    [SerializeField]
    private GameObject coint;
    [SerializeField]
    private Transform position;
    private Timer timer;
    GameManager gameManager;
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Duration = 5;
        timer.Run();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.Finished)
        {
            timer.Run();
            gameManager.coint.addCoint(10);
            Instantiate(coint, position.position, Quaternion.identity);
        }
    }
}
