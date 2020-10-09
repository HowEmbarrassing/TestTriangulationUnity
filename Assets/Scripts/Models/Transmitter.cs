using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Models
{
    public class Transmitter
    {
        public const float RadioWaveSpeed = 1000000;
        public GameObject TransmitterObject;
        public Transform Route;

        public Transmitter(GameObject transmitter, Transform route)
        {
            TransmitterObject = transmitter;
            Route = route;
        }

        private Vector2 gizmosPosition;
        public void OnDrawGizmos()
        {
            foreach (Transform c in Route)
            {
                gizmosPosition = c.position;
                Gizmos.DrawSphere(gizmosPosition, 0.25f);
            }
        }

    }
}
