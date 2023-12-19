using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Quaver.API.Maps;
using System.IO;
using fnf3qua.FNF;
using Newtonsoft.Json;

namespace fnf3qua.FNF
{
    public class FNFJson
    {
        private SwagSong swagSong;
        private string json;
        public FNFJson(string fileName, Arguments args)
        {
            args.Print(fileName);
            this.swagSong = Song.LoadFromJson(fileName, args);
            using StreamReader reader = new(fileName);
            this.json = reader.ReadToEnd();
        }

        public SwagSong SwagSong()
        {
            return swagSong;
        }
        public string JSON()
        {
            return json;
        }
    }
}