using AssemblyCSharp.Assets.Scripts.Data;
using AssemblyCSharp.Assets.Scripts.Models;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Emulation : MonoBehaviour
{
    public InputField InputAx;
    public InputField InputAy;
    public InputField InputBx;
    public InputField InputBy;
    public InputField InputCx;
    public InputField InputCy;

    private GameObject receiverA;
    private GameObject receiverB;
    private GameObject receiverC;

    public TransmitterReceiverSystem TRSystem;
    public Receiver ReceiverA;
    public Receiver ReceiverB;
    public Receiver ReceiverC;
    public Transmitter Transmitter;

    private float tParam;
    private bool coroutineAllowed;

    // Start is called before the first frame update
    void Start()
    {
        DataParser dataParser = new DataParser();
        TRSystem = dataParser.ParseInputData(@"input.txt");
        ReceiverA = TRSystem.ReceiverA;
        ReceiverB = TRSystem.ReceiverB;
        ReceiverC = TRSystem.ReceiverC;
        Transmitter = TRSystem.Transmitter;

        CreateReceivers();
        UpdateInputs(TRSystem);

        tParam = 0f;
        coroutineAllowed = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(Transmitter.Route));
        }
        UpdateReceiverPosition(InputAx, InputAy, InputBx, InputBy, InputCx, InputCy);    
    }

    private IEnumerator GoByTheRoute(Transform route)
    {
        coroutineAllowed = false;
        while (tParam < route.childCount)
        {
            tParam += 0.1f;
            int index = (int)tParam;
            Vector2 curPoint = route.GetChild(index).position;

            if (index + 1 >= route.childCount)
            {
                break;
            }

            Vector2 nextPoint = route.GetChild(index + 1).position;

            if (tParam % 1 != 0)
            {
                Transmitter.TransmitterObject.transform.position = Vector2.Lerp(curPoint, nextPoint, tParam % 1);
            }
            else
            {
                Transmitter.TransmitterObject.transform.position = nextPoint;
            }

            transform.position = Transmitter.TransmitterObject.transform.position;
            yield return new WaitForSeconds(0.1f);
        }

        tParam = 0f;

        coroutineAllowed = true;
    }

    void CreateReceivers()
    {
        receiverA = new GameObject("ReceiverA");
        receiverA.transform.position = ReceiverA.ReceiverCoordinates;
        receiverB = new GameObject("ReceiverB");
        receiverB.transform.position = ReceiverB.ReceiverCoordinates;
        receiverC = new GameObject("ReceiverC");
        receiverC.transform.position = ReceiverC.ReceiverCoordinates;
        
        SpriteRenderer r = receiverA.AddComponent<SpriteRenderer>();
        SpriteRenderer g = receiverB.AddComponent<SpriteRenderer>();
        SpriteRenderer b = receiverC.AddComponent<SpriteRenderer>();
        
        r.sprite = Resources.Load<Sprite>("RadioReceiver");
        r.transform.localScale = new Vector3(50, 50, 50);
        g.sprite = Resources.Load<Sprite>("RadioReceiver");
        g.transform.localScale = new Vector3(50, 50, 50);
        b.sprite = Resources.Load<Sprite>("RadioReceiver");
        b.transform.localScale = new Vector3(50, 50, 50);
    }

    private void UpdateInputs(TransmitterReceiverSystem TransmitterReceiverSystem)
    {
        InputAx.text = TransmitterReceiverSystem.ReceiverA.ReceiverCoordinates.x.ToString();
        InputAy.text = TransmitterReceiverSystem.ReceiverA.ReceiverCoordinates.y.ToString();
        InputBx.text = TransmitterReceiverSystem.ReceiverB.ReceiverCoordinates.x.ToString();
        InputBy.text = TransmitterReceiverSystem.ReceiverB.ReceiverCoordinates.y.ToString();
        InputCx.text = TransmitterReceiverSystem.ReceiverC.ReceiverCoordinates.x.ToString();
        InputCy.text = TransmitterReceiverSystem.ReceiverC.ReceiverCoordinates.y.ToString();
    }

    private void UpdateReceiverPosition(InputField ax, InputField ay, InputField bx,
    InputField by, InputField cx, InputField cy)
    {
        receiverA.transform.position = new Vector3(float.Parse(ax.text), float.Parse(ay.text));
        receiverB.transform.position = new Vector3(float.Parse(bx.text), float.Parse(by.text));
        receiverC.transform.position = new Vector3(float.Parse(cx.text), float.Parse(cy.text));
    }

    public void SaveFile()
    {
        DataWriter dataWriter = new DataWriter();
        dataWriter.WriteOutputData(Transmitter, "outputFile.txt");
    }
}
