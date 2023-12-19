using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using fnf3qua.FNF;
using Newtonsoft.Json;

namespace fnf3qua.FNF
{
    public class Song
    {
        public string song;
        public SwagSection[] notes;
        public dynamic[] events;
        public float bpm;
        public bool needsVoices = true;
        public float speed = 1f;
        public string player1 = "bf";
        public string player2 = "dad";
        public string gfVersion = "gf";
        public string stage;

        public Song(string song, SwagSection[] notes, float bpm)
        {
            this.song = song;
            this.notes = notes;
            this.bpm = bpm;
        }

        public static SwagSong LoadFromJson(string jsonInput, Arguments args)
        {
            args.Print("check2 " + jsonInput, 3);
            using StreamReader reader = new(jsonInput);
            var json = reader.ReadToEnd();
            args.Print("check2", 3);
            
            SwagSong swagJson = JsonConvert.DeserializeObject<SongJson>(json).song;
            return swagJson;
        }
    }
}

class SongJson
{
    public SwagSong song;
}

public class SwagSong
{
    public string song;
    public Newtonsoft.Json.Linq.JArray notes;
    public Newtonsoft.Json.Linq.JArray events;
    public float bpm;
    public bool needsVoices;
    public float speed;
    public string player1;
    public string player2;
    public string gfVersion;
    public string stage;
}