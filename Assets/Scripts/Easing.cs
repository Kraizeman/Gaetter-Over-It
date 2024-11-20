using UnityEngine;

// Source: https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf

/*
 * Most functions taken from Tween.js - Licensed under the MIT license
 * at https://github.com/sole/tween.js
 * Quadratic.Bezier by @fonserbc - Licensed under WTFPL license
 */
public delegate float EasingFunction(float k);

public enum ETween
{
    Linear,
    Smooth2Start,
    Smooth2Stop,
    Smooth2Step,
    Smooth3Start,
    Smooth3Stop,
    Smooth3Step,
    Smooth4Start,
    Smooth4Stop,
    Smooth4Step,
    Smooth5Start,
    Smooth5Stop,
    Smooth5Step,
    SinusStart,
    SinusStop,
    SinusStep,
    CircStart,
    CircStop,
    CircStep,
}

/// <summary>
/// A collection of very handy easing functions.
/// </summary>
public class Easing
{
    public static float FromTween(ETween tween, float k)
    {
        return tween switch
        {
            ETween.Linear => Linear(k),
            ETween.Smooth2Start => Smooth2.Start(k),
            ETween.Smooth2Stop => Smooth2.Stop(k),
            ETween.Smooth2Step => Smooth2.Step(k),
            ETween.Smooth3Start => Smooth3.Start(k),
            ETween.Smooth3Stop => Smooth3.Stop(k),
            ETween.Smooth3Step => Smooth3.Step(k),
            ETween.Smooth4Start => Smooth4.Start(k),
            ETween.Smooth4Stop => Smooth4.Stop(k),
            ETween.Smooth4Step => Smooth4.Step(k),
            ETween.Smooth5Start => Smooth5.Start(k),
            ETween.Smooth5Stop => Smooth5.Stop(k),
            ETween.Smooth5Step => Smooth5.Step(k),
            ETween.SinusStart => Sinus.Start(k),
            ETween.SinusStop => Sinus.Stop(k),
            ETween.SinusStep => Sinus.Step(k),
            ETween.CircStart => Circ.Start(k),
            ETween.CircStop => Circ.Stop(k),
            ETween.CircStep => Circ.Step(k),
            _ => Linear(k),
        };
    }

    public static float Linear(float k)
    {
        return k;
    }

    public class Smooth2
    {
        public static float Start(float k)
        {
            return k * k;
        }

        public static float Stop(float k)
        {
            return k * (2f - k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k;
            return -0.5f * ((k -= 1f) * (k - 2f) - 1f);
        }

        /*
         * Quadratic.Bezier(k,0) behaves like Quadratic.Start(k)
         * Quadratic.Bezier(k,1) behaves like Quadratic.Stop(k)
         *
         * If you want to learn more check Alan Wolfe's post about it
         * http://www.demofox.org/bezquad1d.html
         */
        public static float Bezier(float k, float c)
        {
            return c * 2 * k * (1 - k) + k * k;
        }
    };

    public class Smooth3
    {
        public static float Start(float k)
        {
            return k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f + ((k -= 1f) * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k;
            return 0.5f * ((k -= 2f) * k * k + 2f);
        }
    };

    public class Smooth4
    {
        public static float Start(float k)
        {
            return k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f - ((k -= 1f) * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k;
            return -0.5f * ((k -= 2f) * k * k * k - 2f);
        }
    };

    public class Smooth5
    {
        public static float Start(float k)
        {
            return k * k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f + ((k -= 1f) * k * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k * k;
            return 0.5f * ((k -= 2f) * k * k * k * k + 2f);
        }
    };

    public class Smooth6
    {
        public static float Start(float k)
        {
            return k * k * k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f - ((k -= 1f) * k * k * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k * k * k;
            return 0.5f * ((k -= 2f) * k * k * k * k * k + 2f);
        }
    };

    public class Smooth7
    {
        public static float Start(float k)
        {
            return k * k * k * k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f + ((k -= 1f) * k * k * k * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k * k * k * k;
            return 0.5f * ((k -= 2f) * k * k * k * k * k * k + 2f);
        }
    };

    public class Smooth8
    {
        public static float Start(float k)
        {
            return k * k * k * k * k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f - ((k -= 1f) * k * k * k * k * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k * k * k * k * k;
            return 0.5f * ((k -= 2f) * k * k * k * k * k * k * k + 2f);
        }
    };

    public class Smooth9
    {
        public static float Start(float k)
        {
            return k * k * k * k * k * k * k * k * k;
        }

        public static float Stop(float k)
        {
            return 1f + ((k -= 1f) * k * k * k * k * k * k * k * k);
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * k * k * k * k * k * k * k * k * k;
            return 0.5f * ((k -= 2f) * k * k * k * k * k * k * k * k + 2f);
        }
    };

    public class Sinus
    {
        public static float Start(float k)
        {
            return 1f - Mathf.Cos(k * Mathf.PI / 2f);
        }

        public static float Stop(float k)
        {
            return Mathf.Sin(k * Mathf.PI / 2f);
        }

        public static float Step(float k)
        {
            return 0.5f * (1f - Mathf.Cos(Mathf.PI * k));
        }
    };

    public class Expo
    {
        public static float Start(float k)
        {
            return k == 0f ? 0f : Mathf.Pow(1024f, k - 1f);
        }

        public static float Stop(float k)
        {
            return k == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * k);
        }

        public static float Step(float k)
        {
            if (k == 0f)
                return 0f;
            if (k == 1f)
                return 1f;
            if ((k *= 2f) < 1f)
                return 0.5f * Mathf.Pow(1024f, k - 1f);
            return 0.5f * (-Mathf.Pow(2f, -10f * (k - 1f)) + 2f);
        }
    };

    public class Circ
    {
        public static float Start(float k)
        {
            return 1f - Mathf.Sqrt(1f - k * k);
        }

        public static float Stop(float k)
        {
            return Mathf.Sqrt(1f - ((k -= 1f) * k));
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return -0.5f * (Mathf.Sqrt(1f - k * k) - 1);
            return 0.5f * (Mathf.Sqrt(1f - (k -= 2f) * k) + 1f);
        }
    };

    public class Elastic
    {
        public static float Start(float k)
        {
            if (k == 0)
                return 0;
            if (k == 1)
                return 1;
            return -Mathf.Pow(2f, 10f * (k -= 1f)) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
        }

        public static float Stop(float k)
        {
            if (k == 0)
                return 0;
            if (k == 1)
                return 1;
            return Mathf.Pow(2f, -10f * k) * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f) + 1f;
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return -0.5f
                    * Mathf.Pow(2f, 10f * (k -= 1f))
                    * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f);
            return Mathf.Pow(2f, -10f * (k -= 1f))
                    * Mathf.Sin((k - 0.1f) * (2f * Mathf.PI) / 0.4f)
                    * 0.5f
                + 1f;
        }
    };

    public class Back
    {
        static float s = 1.70158f;
        static float s2 = 2.5949095f;

        public static float Start(float k)
        {
            return k * k * ((s + 1f) * k - s);
        }

        public static float Stop(float k)
        {
            return (k -= 1f) * k * ((s + 1f) * k + s) + 1f;
        }

        public static float Step(float k)
        {
            if ((k *= 2f) < 1f)
                return 0.5f * (k * k * ((s2 + 1f) * k - s2));
            return 0.5f * ((k -= 2f) * k * ((s2 + 1f) * k + s2) + 2f);
        }
    };

    public class Bounce
    {
        public static float Start(float k)
        {
            return 1f - Stop(1f - k);
        }

        public static float Stop(float k)
        {
            if (k < (1f / 2.75f))
            {
                return 7.5625f * k * k;
            }
            else if (k < (2f / 2.75f))
            {
                return 7.5625f * (k -= (1.5f / 2.75f)) * k + 0.75f;
            }
            else if (k < (2.5f / 2.75f))
            {
                return 7.5625f * (k -= (2.25f / 2.75f)) * k + 0.9375f;
            }
            else
            {
                return 7.5625f * (k -= (2.625f / 2.75f)) * k + 0.984375f;
            }
        }

        public static float Step(float k)
        {
            if (k < 0.5f)
                return Start(k * 2f) * 0.5f;
            return Stop(k * 2f - 1f) * 0.5f + 0.5f;
        }
    };
}
