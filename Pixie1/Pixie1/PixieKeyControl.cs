// (c) 2010-2014 IndiegameGarden.com. Distributed under the FreeBSD license in LICENSE.txt
﻿using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using TTengine.Core;
using Pixie1.Actors;

namespace Pixie1
{
    public class PixieKeyControl: ThingControl
    {
        float pressTime = 0f;
        bool isTriggerPressed = false;

        public PixieKeyControl()
            : base()
        {
            MoveSpeed = 1.5f;
        }

        protected override void OnUpdate(ref UpdateParams p)
        {
            base.OnUpdate(ref p);

            float dx = 0f, dy = 0f;

            KeyboardState kb = Keyboard.GetState();
            if (kb.IsKeyDown(Keys.Up) || kb.IsKeyDown(Keys.W))
            {
                if (pressTime == 0f)
                    dy = -1.0f;
                pressTime += p.Dt;
            }
            else if (kb.IsKeyDown(Keys.Down) || kb.IsKeyDown(Keys.S))
            {
                if (pressTime == 0f)
                    dy = +1.0f;
                pressTime += p.Dt;
            }
            else if (kb.IsKeyDown(Keys.Left) || kb.IsKeyDown(Keys.A))
            {
                if (pressTime == 0f)
                    dx = -1.0f;
                pressTime += p.Dt;
            }
            else if (kb.IsKeyDown(Keys.Right) || kb.IsKeyDown(Keys.D))
            {
                if (pressTime == 0f)
                    dx = +1.0f;
                pressTime += p.Dt;
            }
            else
            {
                pressTime = 0f;
            }

            // trigger Toy
            KeyboardState kbstate = Keyboard.GetState();
            bool isTriggerKeyPressed =   kbstate.IsKeyDown(Keys.Space) ||
                                    kbstate.IsKeyDown(Keys.X) ||
                                    kbstate.IsKeyDown(Keys.LeftControl);
            Toy t = ParentThing.ToyActive; 
            if (!isTriggerPressed && isTriggerKeyPressed)
            {
                isTriggerPressed = true;

                // use toy                
                if (t != null)
                {
                    if (!t.IsUsed && t.UsesLeft > 0)
                        t.StartUsing();
                }
            }
            else if (!isTriggerKeyPressed)
            {
                isTriggerPressed = false;
            }

            // send trigger state to Toy
            if (t != null)
            {
                t.IsTriggered = isTriggerKeyPressed;
            }

            // key rep
            if (pressTime > 0.2f / ParentThing.Velocity ) 
                pressTime = 0f;

            // make user's requested motion vector
            TargetMove = new Vector2(dx, dy);
            IsTargetMoveDefined = true;

        }

    }
}
