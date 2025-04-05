using UnityEngine;

public class ParticleBlinker : MonoBehaviour
{
    public ParticleSystem ps;
    public float onDuration = 1f;
    public float minOffDuration = 1f;
    public float maxOffDuration = 2f;

    private bool isOn = false;
    private float timer;

    void Start()
    {
        timer = 0.1f;
        ps.Stop();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            if (isOn)
            {
                // Kapatma zamaný
                ps.Stop();
                timer = Random.Range(minOffDuration, maxOffDuration);
            }
            else
            {
                // Açma zamaný
                ps.Play();
                timer = onDuration;
            }

            isOn = !isOn;
        }
    }
    
}
