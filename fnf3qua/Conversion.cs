using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using fnf3qua.FNF;
using Newtonsoft.Json.Linq;
using Quaver.API.Enums;
using Quaver.API.Maps;
using Quaver.API.Maps.Structures;
using fnf3qua.Extensions;

namespace fnf3qua
{
    public static class Conversion
    {
        public static void ConvertMapset(string mapsetPath, Arguments args)
        {
            if (!File.Exists(mapsetPath) || Path.GetExtension(mapsetPath) != ".json")
            {
                throw new ArgumentException("Invalid file. " + mapsetPath);
            }
            args.Print("check1 " + mapsetPath, 3);
            
            var outputDir = args.Output ?? Path.GetDirectoryName(mapsetPath);
            var folderName = Path.GetFileNameWithoutExtension(mapsetPath);
            var extractDir = outputDir;
            args.Print("check1 " + outputDir, 3);

            // ZipFile.ExtractToDirectory(mapsetPath, extractDir, true);
                foreach (var file in Directory.EnumerateFiles(extractDir))
                {
                    switch (Path.GetExtension(file))
                    {
                        case ".json":
                            var map = new FNFJson(file, args);
                            args.Print("Parsed json file.", 3);
                            args.Print("check1", 3);

                            SwagSong song = map.SwagSong();

                            string[] separatedDifficulty = Path.GetFileNameWithoutExtension(file).Split("-");
                            string difficulty = separatedDifficulty.Length > 1 ? separatedDifficulty[separatedDifficulty.Count() - 1].FirstCharToUpper() : "Normal";

                            args.Print("Song: " + song.song);

                            var qua = new Qua
                            {
                                Mode = GameMode.Keys4,
                                AudioFile = "audio.ogg",
                                BackgroundFile = "",
                                MapId = -1,
                                MapSetId = -1,
                                Title = song.song,
                                Artist = args.Creator ?? "FNF",
                                Source = "FNF",
                                Creator = args.Creator ?? "fnf3qua",
                                DifficultyName = difficulty,
                                Description = "This map was converted to Quaver from FNF using fnf3qua.",
                                Tags = "FNF",
                                InitialScrollVelocity = 1,
                                BPMDoesNotAffectScrollVelocity = true,
                                // I made HitObjects have a public set so I can do this.
                                HitObjects = new List<HitObjectInfo>(),
                                TimingPoints = new List<TimingPointInfo>{ new TimingPointInfo{ Bpm = song.bpm } }
                            };

                            args.Print("check1", 3);

                            args.Print("Getting FNF Sections...");

                            List<SwagSection> notes = new();
                            JArray jarry = song.notes;
                            foreach (var item in jarry)
                            {
                                SwagSection section = item.ToObject<SwagSection>();
                                notes.Add(section);
                                break;
                            }

                            args.Print("Converting FNF sections to Qua sections... (can take a while)");

                            List<JArray> sections = new();
                            List<int[]> sectionNotes = new();

                            // foreach (SwagSection section in notes)
                            // {
                            //     JArray jArray = section.sectionNotes;
                            //     foreach (var item in jarry)
                            //     {
                            //         SwagSection section2 = item.ToObject<SwagSection>();
                            //         sections.Add(section2.sectionNotes);
                            //     }
                            // }

                            JArray jArray = notes[0].sectionNotes;
                            foreach (var item in jarry)
                            {
                                SwagSection section2 = item.ToObject<SwagSection>();
                                sections.Add(section2.sectionNotes);
                            }

                            foreach (JArray array in sections)
                            {
                                foreach(var item in array)
                                {
                                    int[] note = item.Select(d => (int)Math.Round(Convert.ToDouble(d.ToString()))).ToArray();
                                    sectionNotes.Add(note);
                                }
                            }
                            foreach (int[] note in sectionNotes)
                            {
                                if ((note[1] + 1) > 4) continue;
                                qua.HitObjects.Add(new HitObjectInfo
                                {
                                    StartTime = note[0],
                                    Lane = note[1] + 1,
                                    EndTime = note[2] <= 0 ? 0 : note[2] + note[0],
                                    HitSound = HitSounds.Normal,
                                    // EditorLayer = 0
                                });
                            }

                            args.Print("Checking for BPM Change Events...");

                            foreach (var item in jarry)
                            {
                                SwagSection section2 = item.ToObject<SwagSection>();

                                if (section2.changeBPM == true) // support for changing bpm mid song.
                                {
                                    if (section2.sectionNotes.Count <= 0)
                                    {
                                        args.Print("Could not add BPM Change Event. (no found Notes in this section)");
                                        continue;
                                    }
                                    qua.TimingPoints.Add(new TimingPointInfo{
                                        Bpm = float.Parse(section2.bpm.ToString()),
                                        StartTime = float.Parse(section2.sectionNotes[0][0].ToString())
                                    });
                                }
                            }

                            args.Print("Checking for Scroll Speed Change Events...");

                            if (song.events == null || song.events.Count <= 0)
                                args.Print("Song has no events. Skipping.");
                            else {
                                JArray array = song.events;
                                foreach(JArray eventNote in array)
                                {
                                    args.Print(eventNote[1][0][0].ToString(), 3);
                                    if (eventNote[1][0][0].ToString() != "Change Scroll Speed")
                                        continue;
                                    qua.SliderVelocities.Add(new SliderVelocityInfo{
                                        StartTime = float.Parse(eventNote[0].ToString()),
                                        Multiplier = float.Parse(eventNote[1][0][1].ToString())
                                    });
                                }
                            }

                            args.Print("check1", 3);
                            args.Print("Creating file...");

                            string path = Path.Join(extractDir, Path.GetFileNameWithoutExtension(file) + ".qua");

                            File.Create(path).Close();
                            File.WriteAllText(path, qua.Serialize());

                            args.Print($"Written to file {file}", 3);

                            break;
                        case ".png":
                        case ".jpg":
                        case ".mp3":
                            args.Print($"Kept file {file} in directory", 3);
                            break;
                        default:
                            args.Print($"Did nothing to this file {file}", 3);
                            // File.Delete(file);
                            break;
                    }
                }

                // var oszPath = extractDir + ".json";
                // if (File.Exists(oszPath))
                // {
                //     args.Print($"Removed existing .json", 3);
                //     File.Delete(oszPath);
                // }

                args.Print($"Created new .qua");
        }
    }
}