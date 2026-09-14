using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Solution
{
    public class OOPItemPotion : Identity
    {
        public int healPoint = 10;
        public bool isBonues;

        private void Start()
        {
            isBonues = Random.Range(0, 100) < 20;

            if (isBonues)
            {
                GetComponent<SpriteRenderer>().color = Color.blue;
            }
        }

        public override bool Hit()
        {
            if (isBonues)
            {
                mapGenerator.player.Heal(healPoint, isBonues);
                Debug.Log("You got " + Name + " Bonus : " + healPoint * 2);
            }
            else
            {
                mapGenerator.player.Heal(healPoint);
                Debug.Log("You got " + Name + " : " + healPoint);
            }

            // เก็บไอเทม = ได้ 1 คะแนน
            mapGenerator.player.AddScore();

            mapGenerator.player.inventory.AddItem(Name, 1);

            mapGenerator.mapdata[positionX, positionY] = null;

            mapGenerator.player.UpdatePosition(positionX, positionY);

            Destroy(gameObject);

            return true;
        }
    }
}