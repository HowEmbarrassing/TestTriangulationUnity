using AssemblyCSharp.Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Data
{
    public class DataParser
    {
        public string FilePath = @"input.txt";

        public TransmitterReceiverSystem ParseInputData(string filePath)
        {
            if (!CheckIfFileIsValid(filePath))
            {
                return null;
            }

            var reader = new StreamReader(filePath, Encoding.UTF8);
            var line = reader.ReadLine();
            string[] receiverCoordinates = line.Split(',');

            Receiver a = new Receiver(float.Parse(receiverCoordinates[0], CultureInfo.InvariantCulture),
                float.Parse(receiverCoordinates[1], CultureInfo.InvariantCulture),
                new List<float>());
            Receiver b = new Receiver(float.Parse(receiverCoordinates[2], CultureInfo.InvariantCulture),
                float.Parse(receiverCoordinates[3], CultureInfo.InvariantCulture),
                new List<float>());
            Receiver c = new Receiver(float.Parse(receiverCoordinates[4], CultureInfo.InvariantCulture),
                float.Parse(receiverCoordinates[5], CultureInfo.InvariantCulture),
                new List<float>());

            line = reader.ReadLine();

            while (line != null)
            {
                string[] receivingTimeValues = line.Split(',');
                try
                {
                    a.DistanceToTransmitter.Add(float.Parse(receivingTimeValues[0], CultureInfo.InvariantCulture) * Transmitter.RadioWaveSpeed);
                    b.DistanceToTransmitter.Add(float.Parse(receivingTimeValues[1], CultureInfo.InvariantCulture) * Transmitter.RadioWaveSpeed);
                    c.DistanceToTransmitter.Add(float.Parse(receivingTimeValues[2], CultureInfo.InvariantCulture) * Transmitter.RadioWaveSpeed);
                }
                catch
                {
                    return null;
                }
                line = reader.ReadLine();
            }
            TransmitterReceiverSystem parsedData = new TransmitterReceiverSystem(a, b, c);
            reader.Close();
            return parsedData;
        }

        public static bool CheckIfFileIsValid(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return false;
            }
            if (new FileInfo(filePath).Length == 0)
            {
                return false;
            }
            return true;
        }
    }
}
