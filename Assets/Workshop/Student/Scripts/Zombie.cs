using UnityEngine;

namespace Solution
{
    public class Zombie : Identity
    {
        public override bool Hit()
        {
            Debug.Log("Zombie");
            Debug.Log("PowerUp");

            mapGenerator.player.AddEnemy();

            mapGenerator.mapdata[positionX, positionY] = null;

            Destroy(gameObject);

            return true;
        }
    }
}