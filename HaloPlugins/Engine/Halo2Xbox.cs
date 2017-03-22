using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HaloPlugins.Xbox;
using System.IO;

namespace HaloPlugins
{
    public class Halo2Xbox : Engine
    {
        #region Engine Definitions

        // Used when creating an instance of the tag definition object. These
        // represent the name of the .cs code file.
        public string[] TagDefinitions = new string[]
        {
            "fx",   "adlg", "ant",  "bipd", "bitm", "bloc", "bsdt", "Char", "clwd", "coll", "colo", "cont",
            "crea", "ctrl", "deca", "DECR", "effe", "egor", "eqip", "fog",  "foot", "fpch", "garb", "gldf",
            "goof", "hlmt", "hudg", "itmc", "jmad", "jpt",  "lens", "ligh", "lsnd", "ltmp", "mach", "matg",
            "mdlg", "MGS2", "mode", "mulg", "nhdt", "phmo", "phys", "pmov", "pphy", "proj", "prt3", "PRTM",
            "sbsp", "scen", "scnr", "sfx",  "shad", "sily", "skin", "sky",  "sncl", "snd",  "snde", "snmx",
            "spas", "spk",  "ssce", "stem", "styl", "tdtl", "trak", "udlg", "ugh",  "unic", "vehc", "vehi",
            "vrtx", "weap", "weat", "wgit", "wgtz", "wigl"
        };

        public string[] HierarchyClasses = new string[]
        {
            "+!#$", "nec*", "pae*", "ihe*", "hgi*", "dpi*", "piq*", "aer*", "ecs*", "/**/", ">xf<", "MooB",
            "PCED", "RCED", "2SGM", "MTRP", "glda", "**ia", "!tna", "dpib", "mtib", "colb", "tdsb", "rahc",
            "*nic", "*ulc", "dwlc", "lloc", "nloc", "oloc", "tnoc", "aerc", "lrtc", "s*cd", "*ced", "aced",
            "ived", "oved", "*rgd", "cbod", "effe", "roge", "piqe", " gof", "toof", "hcpf", "brag", "fdlg",
            "foog", "ihrg", "tmlh", " tmh", "*csh", "#duh", "gduh", "meti", "cmti", "damj", "!tpj", "snel",
            "ifil", "hgil", "dnsl", "pmtl", "hcam", "gtam", "gldm", "rtem", "edom", "tdpm", "ylpm", "glum",
            "tdhn", "ejbo", "omhp", "syhp", "vomp", "yhpp", "jorp", "3trp", "psbs", "necs", "rncs", "+xfs",
            "dahs", "ylis", "niks", " yks", "tils", "lcns", "!dns", "edns", "xmns", "saps", "!kps", "ecss",
            "tlss", "mets", "lyts", "ltdt", "kart", "*grt", "gldu", "!hgu", "ihnu", "cinu", "tinu", "chev",
            "ihev", "xtrv", "paew", "taew", "tigw", "ztgw", "pihw", "lgiw", "dniw", "ihpw"
        };

        public string[] GroupTagNames = new string[]
        {
            "<fx>", "adlg", "ant!", "bipd", "bitm", "bloc", "bsdt", "char", "clwd", "coll", "colo", "cont",
            "crea", "ctrl", "deca", "DECR", "effe", "egor", "eqip", "fog ", "foot", "fpch", "garb", "gldf",
            "goof", "hlmt", "hudg", "itmc", "jmad", "jpt!", "lens", "ligh", "lsnd", "ltmp", "mach", "matg",
            "mdlg", "MGS2", "mode", "mulg", "nhdt", "phmo", "phys", "pmov", "pphy", "proj", "prt3", "PRTM",
            "sbsp", "scen", "scnr", "sfx+", "shad", "sily", "skin", "sky ", "sncl", "snd!", "snde", "snmx",
            "spas", "spk!", "ssce", "stem", "styl", "tdtl", "trak", "udlg", "ugh!", "unic", "vehc", "vehi",
            "vrtx", "weap", "weat", "wgit", "wgtz", "wigl"
        };

        public string[] TagExtensions = new string[]
        {
            "sound_effect_template", "ai_dialogue_globals", "antenna", "biped", "bitmap", "crate", "breakable_surface",
            "character", "cloth", "collision_model", "color_table", "contrail", "creature", "control", "decal",
            "decorator", "effect", "screen_effect", "equipment", "fog", "material_effect", "fog_patch", "garbage",
            "global_lighting", "multiplayer_variant_settings", "object_properties", "hud_globals", "item_collection",
            "model_animation_graph", "damage_effect", "lens_flare", "light", "looping_sound", "lightmap", "machine",
            "match_globals", "ai_mission_dialogue", "light_volume", "render_model", "multiplayer_globals", "hud_interface",
            "physics_model", "physics", "particle_physics", "point_physics", "projectile", "particle", "particle_model",
            "scenario_structure_bsp", "scenery", "scenario", "sound_effect_collection", "shader", "ui_option",
            "user_interface_list_skin_definition", "sky", "sound_class", "sound", "sound_environment", "sound_mixture",
            "shader_pass", "speak", "sound_scenery", "shader_template", "style", "beam_trail", "camera_track", "unit_dialog",
            "sound_diagnostics", "unicode_string_list", "vehicle_collection", "vehicle", "vertex_shader", "weapon",
            "weather_system", "user_interface_screen_widget_definition", "user_interface_globals_definition",
            "user_interface_shared_globals_definition"
        };

        public string[] DefaultStringIds;
        public Dictionary<string, int> TagIds = new Dictionary<string, int>();
        public List<string> StringIds = new List<string>();

        #endregion

        #region Constructor

        public Halo2Xbox()
        {
            // Load default string ids
            DefaultStringIds = HaloPlugins.Properties.Resources.h2x_stringids.Split(new string[] { "\r\n" }, 1980, StringSplitOptions.None);
        }

        #endregion

        #region Indexers

        public override object this[string Type]
        {
            get
            {
                switch (Type)
                {
                    case "Classes":         return HierarchyClasses;
                    case "Types":           return GroupTagNames;
                    case "Extensions":      return TagExtensions;
                    case "StringIds":       return StringIds;
                    case "EngineStringIds": return DefaultStringIds;
                    case "TagIds":          return TagIds;
                    default:                return null;
                }
            }
            set
            {
                switch (Type)
                {
                    case "StringIds":       StringIds = (List<string>)value;            break;
                    case "TagIds":          TagIds = (Dictionary<string, int>)value;    break;
                }
            }
        }

        public override string this[string Type, string Value]
        {
            get
            {
                if (Type == "Class")
                {
                    int Index = Array.IndexOf(TagExtensions, Value);
                    return Index != -1 ? GroupTagNames[Index] : "";
                }
                else
                {
                    int Index = Array.IndexOf(GroupTagNames, Value);
                    return Index != -1 ? TagExtensions[Index] : "";
                }
            }
        }

        #endregion

        #region Methods

        public override TagDefinition CreateInstance(string Value)
        {
            // Try Searching Types
            int Index = -1;
            for (int i = 0; i < GroupTagNames.Length; i++)
            {
                if (GroupTagNames[i] == Value)
                { Index = i; break; }
            }

            // If Not Found Search Extensions
            if (Index == -1)
            {
                for (int i = 0; i < TagExtensions.Length; i++)
                {
                    if (TagExtensions[i] == Value)
                    { Index = i; break; }
                }
            }

            // Create a new Instance of the Tag Definition
            if (Index != -1)
            {
                // Create
                return (TagDefinition)Activator.CreateInstance("HaloPlugins", "HaloPlugins.Xbox." + TagDefinitions[Index], new object[] { }).Unwrap();
            }

            // Fail
            return null;
        }

        #endregion
    }
}
