using UnityEngine;
using System.Collections.Generic;

public class ParticleBlinkerManager : MonoBehaviour
{
    public List<ParticleBlinker> blinkers;
    public float switchInterval = 3f;

    private float timer;
    private int currentOffIndex = 0;

    void Start()
    {
        timer = switchInterval;
        currentOffIndex= Random.Range(0, blinkers.Count);
        ActivateBlinkers();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            // Bir sonrakini kapat, diðerlerini aç
            currentOffIndex = (currentOffIndex + 1) % blinkers.Count;
            ActivateBlinkers();

            timer = switchInterval;
        }
    }

    void ActivateBlinkers()
    {
        for (int i = 0; i < blinkers.Count; i++)
        {
            if (i == currentOffIndex)
            {
                blinkers[i].TurnOff();
            
            }
            else
            {
                blinkers[i].TurnOn();
            
            }
        }
    }
}
