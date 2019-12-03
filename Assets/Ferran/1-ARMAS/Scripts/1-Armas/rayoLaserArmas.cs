using UnityEngine;

public class rayoLaserArmas : MonoBehaviour
{
    public objetoCogible objetoCogible;
    public Pistola pistola;
    private LineRenderer lR;

    // Start is called before the first frame update
    void Start()
    {
        lR = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objetoCogible.parent != null && pistola.laserActivo)
        {
            lR.SetPosition(0, transform.position);

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.collider)
                {
                    lR.SetPosition(1, hit.point);
                }
            }
            else
            {
                lR.SetPosition(1, transform.forward * 50000);
            }
        }
        else
        {
            lR.SetPosition(0, transform.position);
            lR.SetPosition(1, transform.position);
        }

    }
}
