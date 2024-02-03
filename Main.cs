using Exiled.API.Enums;
using Exiled.API.Features;
using MEC;
using Scp330 = Exiled.Events.Handlers.Scp330;
using PlayerHandler = Exiled.Events.Handlers.Player;
using BalaRosa;
using Exiled.Events.EventArgs.Scp330;
using System;
using System.Linq;
using System.Collections.Generic;
using InventorySystem.Items.Usables.Scp330;
using Exiled.Events.EventArgs.Player;

namespace BalaRosa
{
    public class ConfigurableScp330 : Plugin<Config>
    {
        public static ConfigurableScp330 Instance { get; private set; }
        public override string Name => "ConfigurableScp330";
        public override string Author => "Admiro";
        public override string Prefix => "Scp330";
        public override Version Version => new Version(1, 1, 0);
        public override Version RequiredExiledVersion => new Version(8, 4, 3);
        public override PluginPriority Priority => PluginPriority.Medium;

        public override void OnEnabled()
        {
            Instance = this;
            RegisterEvents();
            base.OnEnabled();
        }
        public override void OnDisabled()
        {
            UnRegisterEvents();
            base.OnDisabled();
        }

        public void RegisterEvents()
        {
            Scp330.InteractingScp330 += OnInteractingWithScp330;
            PlayerHandler.Spawned += PlayerSpawned;
        }
        public void UnRegisterEvents()
        {
            Scp330.InteractingScp330 -= OnInteractingWithScp330;
            PlayerHandler.Spawned -= PlayerSpawned;
        }

        public Dictionary<Player, int> Scp330Interactions = new();

        public void PlayerSpawned(SpawnedEventArgs ev)
        {
            if (Scp330Interactions.ContainsKey(ev.Player))
            {
                Scp330Interactions.Remove(ev.Player);
            }
        }

        public void OnInteractingWithScp330(InteractingScp330EventArgs ev) 
        {
            if (ev.Player == null) return;


            ev.ShouldSever = false;

            if (!Scp330Interactions.ContainsKey(ev.Player))
            {
                Scp330Interactions.Add(ev.Player, 1);
            }
            else
            {
                Scp330Interactions[ev.Player]++;
            }

            if (Scp330Interactions[ev.Player] > Instance.Config.MaxInteractionAmount)
            {
                if ((UnityEngine.Random.value * 100) <= Instance.Config.SeveringHandsChance)
                {
                    ev.ShouldSever = true;
                    Scp330Interactions.Remove(ev.Player);
                    return;
                }
            }

            List<CandyKindID> List = Instance.Config.Chances.Keys.ToList();

            float RandomNumber = UnityEngine.Random.value * Instance.Config.Chances.Values.Sum();
            
            if (RandomNumber <= Sum(1))
            {
                ev.Candy = List[0];
            }
            else if (RandomNumber <= Sum(2))
            {
                ev.Candy = List[1];
            }
            else if (RandomNumber <= Sum(3))
            {
                ev.Candy = List[2];
            }
            else if (RandomNumber <= Sum(4))
            {
                ev.Candy = List[3];
            }
            else if (RandomNumber <= Sum(5))
            {
                ev.Candy = List[4];
            }
            else if (RandomNumber <= Sum(6))
            {
                ev.Candy = List[5];
            }
            else if (RandomNumber <= Sum(7))
            {
                ev.Candy = List[6];
            }
        }

        public float Sum(int value)
        {
            List<float> values = Instance.Config.Chances.Values.ToList();
            float result = 0;
            for (int i = 0; i < value; i++)
            {
               result += values[i];
            }
            return result;
        }
    }
}
