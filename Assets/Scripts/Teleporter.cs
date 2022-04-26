using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Teleporter linkedTeleporter;
    [SerializeField] GameObject Pad1;
    [SerializeField] GameObject Pad2;
    [SerializeField] Material chargedMat;
    [SerializeField] Material chargingMat;
    [SerializeField] Vector3 teleportOffset;
    [SerializeField] float rechargeTime;
    [SerializeField] CameraFollow cameraView;

    private float timeSinceLastTeleport = 0;
    private bool charging;

    // Start is called before the first frame update
    void Start()
    {
        setPads(chargedMat);
        charging = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (charging)
        {
            timeSinceLastTeleport += Time.deltaTime;

            if (timeSinceLastTeleport > rechargeTime)
            {
                setPads(chargedMat);
                timeSinceLastTeleport = 0;
                charging = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!charging)
        {
            charging = true;
            setPads(chargingMat);
            other.transform.position =  linkedTeleporter.TeleportTo();
        }
    }

    public Vector3 TeleportTo()
    {
        cameraView.teleport();
        setPads(chargingMat);
        charging = true;
        return this.transform.position + teleportOffset;
    }
    
    private void setPads(Material mat)
    {
        Pad1.GetComponent<MeshRenderer>().material = mat;
        Pad2.GetComponent<MeshRenderer>().material = mat;
    }
}
