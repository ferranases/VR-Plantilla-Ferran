using OVRTouchSample;
using UnityEngine;

public class agarreCohete : MonoBehaviour
{
    public Transform posicioManoIzq;
    public Cohete cohete;
    public Transform posicionPlayerCohete;
    public Transform player;

    private OVRPlayerController playerController;
    private GameObject manoCerca = null;
    private Transform padreManos;

    private bool manoColocada = false;
    private bool activado = false;

    private void Start()
    {
        padreManos = GameObject.Find("TrackingSpace").transform;
        playerController = player.gameObject.GetComponent<OVRPlayerController>();
    }

    private void Update()
    {
        if (manoCerca != null)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, manoCerca.transform.parent.GetComponent<Hand>().m_controller) && !manoColocada)
            {
                player.SetParent(cohete.transform);
                cohete.activar();
                activado = true;
                //player.GetComponent<Astronauta>().activar(posicionPlayerCohete);

                Vector3 posOffset = new Vector3(0.0f, 0.0f, 0.0f); // offset to tweak cam position
                Vector3 pos = OVRManager.tracker.GetPose(0).position - UnityEngine.XR.InputTracking.GetLocalPosition(UnityEngine.XR.XRNode.CenterEye) + posOffset;
                pos = OVRManager.tracker.GetPose(0).position + posOffset;
                player.localPosition = pos;


                manoColocada = true;
                manoCerca.transform.parent.GetComponent<OVRGrabber>().m_parentTransform.localPosition = posicioManoIzq.localPosition;
            }

            if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, manoCerca.transform.parent.GetComponent<Hand>().m_controller) && manoColocada)
            {
                //manoCerca.transform.parent.GetComponent<OVRGrabber>().m_parentTransform = padreManos;
                manoColocada = false;
            }

        }


    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Mano")
        {
            manoCerca = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Mano")
        {
            //manoCerca.transform.parent.GetComponent<OVRGrabber>().m_parentTransform = padreManos;
            manoCerca = null;
        }
    }
}
