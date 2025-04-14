using UnityEngine;

public class ParticleBlinker : MonoBehaviour
{
    public ParticleSystem ps;
    public  bool isOn = false;

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
    private void OnTriggerEnter(Collider other)
    {
        ICollectible icol= other.GetComponent<ICollectible>();
        if (isOn && icol!= null)
        {
            SoundManager.instance.PlayGameSound(SoundType.Fire);
            icol.DeCollect(1);
        }
    }
}
