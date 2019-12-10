﻿using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RimConnection { 
    public abstract class Action : IAction
    {
        public string name { get; set; }
        public string description { get; set; }
        public string category { get; set; } = "Other";
        public string prefix { get; set; } = "Spawn";
        public bool shouldShowAmount { get; set; } = false;
        public int localCooldownSeconds { get; set; } = 120;
        public int globalCooldownSeconds { get; set; } = 60;
        public int costSilverStore { get; set; } = 1000;
        public int costBitStore { get; set; } = 100;

        public string ActionHash()
        {
            var input = $"{name}{description}{category}{prefix}";
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash) sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public ValidCommand ToApiCall()
        {
            var command = new ValidCommand
            {
                name = name,
                description = description,
                category = category,
                prefix = prefix,
                shouldShowAmount = shouldShowAmount,
                actionHash = ActionHash(),
                localCooldownSeconds = localCooldownSeconds,
                globalCooldownSeconds = globalCooldownSeconds,
                costSilverStore = costSilverStore,
                costBitStore = costBitStore
            };
            return command;
        }



        public abstract void Execute(int amount);


    }
}
