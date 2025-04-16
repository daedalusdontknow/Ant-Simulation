using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Antsimulation.entities
{
    public class DataEntry
    {
        public int Count { get; set; }
        public float DNutrition { get; set; }
        public float DSpeed { get; set; }
        public float DStrength { get; set; }
        public float DSight { get; set; }
        public float HGen { get; set; }
        public float DHealth { get; set; }
        public int Timestamp { get; set; }

        public DataEntry(int count, float dNutrition, float dSpeed, float dStrength, float dSight, float hGen, float dHealth, int timestamp)
        {
            Count = count;
            DNutrition = dNutrition;
            DSpeed = dSpeed;
            DStrength = dStrength;
            DSight = dSight;
            HGen = hGen;
            DHealth = dHealth;
            Timestamp = timestamp;
        }
    }
}
