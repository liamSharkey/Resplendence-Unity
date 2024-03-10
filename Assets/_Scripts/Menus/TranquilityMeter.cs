using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TranquilityMeter : MonoBehaviour
{
    public TextMeshProUGUI tranquilityText;

    public GameObject playerObject;
    private PlayerMovement player;

    private float tranquility;
    private float maxTranquility;

    // Start is called before the first frame update
    void Start()
    {
        player = playerObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        tranquility = player.tranquility;
        maxTranquility = player.tranquilityLimit;

        tranquilityText.text = tranquility.ToString("F0") + "/" + maxTranquility.ToString("F0");
    }
}
