using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Models
{
    public class TransmitterReceiverSystem
    {
        public Receiver ReceiverA;
        public Receiver ReceiverB;
        public Receiver ReceiverC;
        public Transmitter Transmitter;

        public TransmitterReceiverSystem(Receiver a, Receiver b, Receiver c)
        {
            ReceiverA = a;
            ReceiverB = b;
            ReceiverC = c;
            Transmitter = GetTransmitter();
        }

        public TransmitterReceiverSystem(Transmitter t)
        {
            Transmitter = t;
        }

        public Transmitter GetTransmitter()
        {
            GameObject routeObject = new GameObject("Route");
            routeObject.tag = "Route";
            for (int i = 0; i < this.ReceiverA.DistanceToTransmitter.Count; i++)
            {
                GameObject controlPoint = new GameObject("ControlPoint" + (i + 1));
                controlPoint.transform.parent = routeObject.transform;
                controlPoint.transform.position = GetTransmitterPoint(i,
                    this.ReceiverA,
                    this.ReceiverB,
                    this.ReceiverC);
            }
            GameObject transmitterObject = new GameObject("Transmitter");
            SpriteRenderer r = transmitterObject.AddComponent<SpriteRenderer>();
            r.sprite = Resources.Load<Sprite>("RadioReceiver");
            r.transform.localScale = new Vector3(100, 100);
            Transmitter transmitter = new Transmitter(transmitterObject, routeObject.transform);
            return transmitter;
        }

        public Vector2 GetTransmitterPoint(int index, Receiver a, Receiver b, Receiver c)
        {
            var A = (-2 * a.ReceiverCoordinates.x) + (2 * b.ReceiverCoordinates.x);
            var B = (-2 * a.ReceiverCoordinates.y) + (2 * b.ReceiverCoordinates.y);
            var C = Mathf.Pow(a.DistanceToTransmitter[index], 2)
                - Mathf.Pow(b.DistanceToTransmitter[index], 2)
                - Mathf.Pow(a.ReceiverCoordinates.x, 2)
                + Mathf.Pow(b.ReceiverCoordinates.x, 2)
                - Mathf.Pow(a.ReceiverCoordinates.y, 2)
                + Mathf.Pow(b.ReceiverCoordinates.y, 2);

            var D = (-2 * b.ReceiverCoordinates.x) + (2 * c.ReceiverCoordinates.x);
            var E = (-2 * b.ReceiverCoordinates.y) + (2 * c.ReceiverCoordinates.y);
            var F = Mathf.Pow(b.DistanceToTransmitter[index], 2)
                - Mathf.Pow(c.DistanceToTransmitter[index], 2)
                - Mathf.Pow(b.ReceiverCoordinates.x, 2)
                + Mathf.Pow(c.ReceiverCoordinates.x, 2)
                - Mathf.Pow(b.ReceiverCoordinates.y, 2)
                + Mathf.Pow(c.ReceiverCoordinates.y, 2);

            var x = ((C * E) - (B * F)) / ((A * E) - (B * D));
            var y = ((A * F) - (C * D)) / ((A * E) - (B * D));

            return new Vector2(x, y);
        }

        public void UpdateRoute(Transmitter transmitter)
        {
            GameObject routeObject = ClearRoute();
            for (int i = 0; i < transmitter.Route.childCount; i++)
            {
                GameObject controlPoint = new GameObject("ControlPoint" + (i + 1));
                controlPoint.transform.parent = routeObject.transform;
                controlPoint.transform.position = transmitter.Route.GetChild(i).position;
            }
        }

        private static GameObject ClearRoute()
        {
            GameObject routeObject = GameObject.FindGameObjectWithTag("Route");
            foreach (Transform child in routeObject.transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            return routeObject;
        }
    }
}
