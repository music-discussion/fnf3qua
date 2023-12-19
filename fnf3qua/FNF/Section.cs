using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fnf3qua.FNF
{
    public class Section
    {
       public dynamic[] sectionNotes;

       public float sectionBeats = 4f;
       public bool gfSection = false;
       public int typeOfSection = 0; 
       public bool mustHitSection = true;

       public static int COPYCAT = 0;

       public Section(float sectionBeats = 4f)
       {
            this.sectionBeats = sectionBeats;
       }
    }

    public class SwagSection
    {
        public  Newtonsoft.Json.Linq.JArray sectionNotes;
        public float sectionBeats;
        public int typeOfSection;
        public bool mustHitSection;
        public bool gfSection;
        public float bpm;
        public bool changeBPM;
        public bool altAnim;
        public SwagSection()
        {}
    }
}