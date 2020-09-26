﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quaver.API.Maps;

namespace qua3osu.OsuBeatmap.Sections
{
    public class HitObjectsSection : Section
    {
        public List<HitObject> HitObjects;

        public HitObjectsSection(Qua qua, Arguments args)
        {
            HitObjects = qua.HitObjects.Select(
                hitObject => new HitObject(hitObject, qua.GetKeyCount(), args.DontApplyOffset)
            ).ToList();
        }

        public override string SectionTitle { get; } = "HitObjects";

        public override string ToString()
        {
            var lines = new StringBuilder();
            lines.AppendLine(FormatTitle());
            HitObjects.ForEach(ho => lines.AppendLine(ho.ToString()));
            return lines.ToString();
        }
    }
}