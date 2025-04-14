using UnityEngine;

public class ParticleBlinker : MonoBehaviour
{
    public ParticleSystem ps;
    private bool isOn = false;

    public void TurnOn()
    {
        if (!isOn)
        {
            ps.Play();
            isOn = true;
        }
    }

    public void TurnOff()
    {
        if (isOn)
        {
            ps.Stop();
            isOn = false;
        }
    }

    public bool IsOn()
    {
        return isOn;
    }
}
