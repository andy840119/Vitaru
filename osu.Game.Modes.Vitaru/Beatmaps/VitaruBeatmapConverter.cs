using OpenTK;
using osu.Game.Beatmaps;
using osu.Game.Modes.Objects;
using osu.Game.Modes.Vitaru.Objects;
using osu.Game.Modes.Vitaru.Objects.Drawables;
using System.Collections.Generic;
using osu.Game.Modes.Objects.Types;
using System.Linq;
using osu.Game.Modes.Vitaru.Objects.Characters;

namespace osu.Game.Modes.Vitaru.Beatmaps
{
    internal class VitaruBeatmapConverter : IBeatmapConverter<VitaruHitObject>
    {
        public Beatmap<VitaruHitObject> Convert(Beatmap original)
        {
            return new Beatmap<VitaruHitObject>(original)
            {
                HitObjects = convertHitObjects(original.HitObjects, original.BeatmapInfo?.StackLeniency ?? 0.7f)
            };
        }

        private List<VitaruHitObject> convertHitObjects(List<HitObject> hitObjects, float stackLeniency)
        {
            List<VitaruHitObject> converted = hitObjects.Select(convertHitObject).ToList();

            updateStacking(converted, stackLeniency);

            return converted;
        }

        private VitaruHitObject convertHitObject(HitObject original)
        {
            IHasCurve curveData = original as IHasCurve;
            IHasEndTime endTimeData = original as IHasEndTime;
            IHasPosition positionData = original as IHasPosition;
            IHasCombo comboData = original as IHasCombo;

            return new Enemy
            {
                StartTime = original.StartTime,
                Sample = original.Sample,

                Position = positionData?.Position ?? Vector2.Zero,

                NewCombo = comboData?.NewCombo ?? false
            };
        }

        private void updateStacking(List<VitaruHitObject> hitObjects, float stackLeniency, int startIndex = 0, int endIndex = -1)
        {
            if (endIndex == -1)
                endIndex = hitObjects.Count - 1;

            int stackDistance = 3;
            float stackThreshold = DrawableVitaruHitObject.TIME_PREEMPT * stackLeniency;

            // Reset stacking inside the update range
            for (int i = startIndex; i <= endIndex; i++)
                hitObjects[i].StackHeight = 0;

            // Extend the end index to include objects they are stacked on
            int extendedEndIndex = endIndex;
            for (int i = endIndex; i >= startIndex; i--)
            {
                int stackBaseIndex = i;
                for (int n = stackBaseIndex + 1; n < hitObjects.Count; n++)
                {
                    VitaruHitObject stackBaseObject = hitObjects[stackBaseIndex];

                    double endTime = (stackBaseObject as IHasEndTime)?.EndTime ?? stackBaseObject.StartTime;
                }

                if (stackBaseIndex > extendedEndIndex)
                {
                    extendedEndIndex = stackBaseIndex;
                    if (extendedEndIndex == hitObjects.Count - 1)
                        break;
                }
            }

            //Reverse pass for stack calculation.
            int extendedStartIndex = startIndex;
            for (int i = extendedEndIndex; i > startIndex; i--)
            {
                int n = i;
                /* We should check every note which has not yet got a stack.
                    * Consider the case we have two interwound stacks and this will make sense.
                    *
                    * o <-1      o <-2
                    *  o <-3      o <-4
                    *
                    * We first process starting from 4 and handle 2,
                    * then we come backwards on the i loop iteration until we reach 3 and handle 1.
                    * 2 and 1 will be ignored in the i loop because they already have a stack value.
                    */

                VitaruHitObject objectI = hitObjects[i];


                /* If this object is a hitcircle, then we enter this "special" case.
                    * It either ends with a stack of hitcircles only, or a stack of hitcircles that are underneath a slider.
                    * Any other case is handled by the "is Slider" code below this.
                    */
                if (objectI is Enemy)
                {
                    while (--n >= 0)
                    {
                        VitaruHitObject objectN = hitObjects[n];

                        double endTime = (objectN as IHasEndTime)?.EndTime ?? objectN.StartTime;

                        if (objectI.StartTime - endTime > stackThreshold)
                            //We are no longer within stacking range of the previous object.
                            break;

                        // HitObjects before the specified update range haven't been reset yet
                        if (n < extendedStartIndex)
                        {
                            objectN.StackHeight = 0;
                            extendedStartIndex = n;
                        }

                        /* This is a special case where hticircles are moved DOWN and RIGHT (negative stacking) if they are under the *last* slider in a stacked pattern.
                            *    o==o <- slider is at original location
                            *        o <- hitCircle has stack of -1
                            *         o <- hitCircle has stack of -2
                            */

                        if (Vector2.Distance(objectN.Position, objectI.Position) < stackDistance)
                        {
                            //Keep processing as if there are no sliders.  If we come across a slider, this gets cancelled out.
                            //NOTE: Sliders with start positions stacking are a special case that is also handled here.

                            objectN.StackHeight = objectI.StackHeight + 1;
                            objectI = objectN;
                        }
                    }
                }
            }
        }
    }
}
