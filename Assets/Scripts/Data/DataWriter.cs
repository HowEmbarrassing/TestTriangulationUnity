using AssemblyCSharp.Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace AssemblyCSharp.Assets.Scripts.Data
{
    public class DataWriter
    {
        public void WriteOutputData(Transmitter transmitter, string fileName)
        {
            StreamWriter writer = new StreamWriter(fileName);
            using (writer)
            {
                foreach(Transform child in transmitter.Route)
                {
                    writer.WriteLine(child.position.x + ", " + child.position.y);
                }
            }
        }
    }
}
