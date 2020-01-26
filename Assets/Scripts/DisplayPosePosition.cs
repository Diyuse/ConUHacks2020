using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayPosePosition : MonoBehaviour
{
    private ARTapToPlaceObject arTapToPlaceObject;
    private GameObject indicator;

    public TextMeshProUGUI x;

    public TextMeshProUGUI y;

    public TextMeshProUGUI z;
    // Start is called before the first frame update
    void Start()
    {
        arTapToPlaceObject = FindObjectOfType<ARTapToPlaceObject>();
        indicator = arTapToPlaceObject.placementIndicator;
    }

    // Update is called once per frame
    void Update()
    {
        x.text = String.Format("X: {0:0.00}", indicator.transform.position.x);
        y.text = String.Format("Y: {0:0.00}", indicator.transform.position.y);
        z.text = String.Format("Z: {0:0.00}", indicator.transform.position.z);
    }
}
