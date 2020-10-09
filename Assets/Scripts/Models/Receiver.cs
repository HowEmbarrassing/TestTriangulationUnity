using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Models
{
    public class Receiver
    {
        public Vector2 ReceiverCoordinates { get; set; }
        public List<float> DistanceToTransmitter { get; set; }

        public Receiver(float x, float y, List<float> timePeriods)
        {
            ReceiverCoordinates = new Vector2(x, y);
            DistanceToTransmitter = MultiplyList(timePeriods, 1000000f);
        }

        public List<float> MultiplyList(List<float> list, float multiplier)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i] = list[i] * multiplier;
            }
            return list;
        }
    }
}
