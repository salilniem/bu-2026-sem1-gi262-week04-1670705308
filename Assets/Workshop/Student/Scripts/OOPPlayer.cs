using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Solution
{
    public class OOPPlayer : Character
    {
        public Inventory inventory;

        private InputAction moveAction;

        // LinkedList
        public LinkedList<string> eventList = new LinkedList<string>();

        // Score และ Level
        public int score = 0;
        public int level = 1;
        public int Kill = 10;
        public int deaths = 2;
        public int assist = 0;
        private int killall = 0;
        private int assistall = 0;

        void Awake()
        {
            moveAction = InputSystem.actions.FindAction("Move");
        }

        public void Start()
        {
            PrintInfo();
            GetRemainEnergy();
            inventory = GetComponent<Inventory>();
        }

        public void Update()
        {
            var dir = moveAction.ReadValue<Vector2>();

            if (dir != Vector2.zero && moveAction.triggered)
            {
                Move(dir);
            }

            // กด I เพื่อดู Inventory
            if (Keyboard.current.iKey.wasPressedThisFrame)
            {
                inventory.PrintInventory();
            }
        }

        // เพิ่ม Enemy เข้า LinkedList
        public void AddEnemy()
        {
            eventList.AddLast("Enemy");
            killall = Kill + 3;
            Debug.Log(Kill + "+" + "3" + "=" + killall);
            Kill = killall;
            Debug.Log("EVENT : Enemy");

            AddPowerUp();
        }

        // เพิ่ม PowerUp เข้า LinkedList
        public void AddPowerUp()
        {
            eventList.AddLast("PowerUp");

            Debug.Log("EVENT : PowerUp");

            AddScore();
        }

        // เพิ่ม Score
        public void AddScore()
        {
            score++;
            assistall = assist + 5;
            Debug.Log("Score : " + score);
            Debug.Log(assist + "5" + "=" + assistall);
            assist = assistall;
            // FIFO
            if (eventList.Count > 0)
            {
                Debug.Log("First.Value : " + eventList.First.Value);

                eventList.RemoveFirst();
            }

            // ครบ 5 คะแนน
            if (score >= 5)
            {
                LevelUp();
            }
        }

        // Level Up
        public void LevelUp()
        {
            level++;

            Debug.Log("EVENT : Level Up!");
            Debug.Log("Level : " + level);

            score = 0;
        }

        public void Attack(OOPEnemy _enemy)
        {
            _enemy.energy -= AttackPoint;
            Debug.Log(_enemy.name + " is energy " + _enemy.energy);
        }

        protected override void CheckDead()
        {
            base.CheckDead();

            if (energy <= 0)
            {
                Debug.Log("Player is Dead");
            }
        }
    }
}