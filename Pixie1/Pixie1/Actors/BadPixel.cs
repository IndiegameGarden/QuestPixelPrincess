﻿using System;
using TTengine.Core;
using TTengine.Util;
using Microsoft.Xna.Framework;
using Pixie1;
using Pixie1.Behaviors;

namespace Pixie1.Actors
{
    public class BadPixel: Thing
    {
        // behaviors - the things that bad pixels do 
        public BlinkBehavior Blinking;
        public ChaseBehavior  Chasing;
        public AlwaysTurnRightBehavior Turning;
        public RandomWanderBehavior Wandering;

        public static BadPixel Create()
        {
            return new BadPixel(Level.Current.pixie);
        }

        public static BadPixel CreateCloaky()
        {
            BadPixel p = new BadPixel(Level.Current.pixie);
            p.IsCloaky = true;
            return p;
        }

        bool isCloaky = false;

        public BadPixel(Thing chaseTarget)
            : base()
        {
            DrawInfo.DrawColor = new Color(255, 10, 4);
            float blinkPeriod = RandomMath.RandomBetween(0.64f, 2.12f);
            float blinkDutycycle = RandomMath.RandomBetween(0.848f, 0.99f);
            Blinking = new BlinkBehavior(blinkPeriod, blinkDutycycle);
            Add(Blinking);

            SubsumptionBehavior sub = new SubsumptionBehavior();
            Add(sub);

            Chasing = new ChaseBehavior(chaseTarget);
            Chasing.MoveSpeed = RandomMath.RandomBetween(0.47f, 0.75f);
            Chasing.ChaseRange = 10f; // RandomMath.RandomBetween(12f, 40f);
            sub.Add(Chasing);

            Turning = new AlwaysTurnRightBehavior();
            Turning.MoveSpeed = Chasing.MoveSpeed; //RandomMath.RandomBetween(0.57f, 1.05f);
            Turning.MoveSpeed = 0.7f;
            sub.Add(Turning);

            Wandering = new RandomWanderBehavior(2.7f, 11.3f);
            Wandering.MoveSpeed = 0.7f;
            sub.Add(Wandering);
            
        }

        /// <summary>
        /// set 'cloaky' status, a cloaky is a hardly visible bad pixel
        /// </summary>
        public bool IsCloaky
        {
            get
            {
                return isCloaky;
            }
            set
            {
                if (IsCloaky == value)
                    return;
                // if change - swap dutycycle
                Blinking.DutyCycle = 1f - Blinking.DutyCycle;
                isCloaky = value;                
            }
        }
        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);
        }
    }
}
