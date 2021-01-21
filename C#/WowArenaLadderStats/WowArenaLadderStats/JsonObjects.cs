using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowArenaLadderStats
{
    [Serializable]
    public class PvpLeaderboard3v3Response
    {
        public _links _links;
        public season season;
        public string name;
        public bracket bracket;
        public entry[] entries;
    }

    [Serializable]
    public class _links
    {
        public self self;
    }

    [Serializable]
    public class self
    {
        public string href;
    }

    [Serializable]
    public class season
    {
        public key key;
        public int id;
    }

    [Serializable]
    public class key
    {
        public string href;
    }

    [Serializable]
    public class bracket
    {
        public int id;
        public string type;
    }

    [Serializable]
    public class entry
    {
        public character character;
        public faction faction;
        public int rank;
        public int rating;
        public season_match_statistics season_match_statistics;
        public tier tier;
    }

    [Serializable]
    public class character
    {
        public string name;
        public int id;
        public realm realm;

        public CharacterProfile CharacterProfile;

        public string @class;
        public string @spec;
        public string @race;

        public void SetClassAndSpec()
        {
            try
            {
                var x = ApiClient.Get($"profile/wow/character/{realm.slug}/{name.ToLower()}?namespace=profile-us");
                CharacterProfile = JsonConvert.DeserializeObject<CharacterProfile>(x);
                @class = CharacterProfile.character_class.Name;
                spec = CharacterProfile.active_spec.Name;
                race = CharacterProfile.race.Name;
            }
            catch(Exception ex)
            {
                CharacterProfile = new CharacterProfile()
                {
                    _links = new _links() { self = new self() { href = "" } },
                    id = 0,
                    name = "UNABLE TO FIND",
                    gender = new gender() { name = new English() { en_US = "UNABLE TO FIND" }, type = "UNABLE TO FIND" },
                    faction = new faction() { type = "UNABLE TO FIND" },
                    race = new race() { name = new English() { en_US = "UNABLE TO FIND" }, id = 0, key = new key() { href = "UNABLE TO FIND" } },
                    character_class = new character_class() { name = new English() { en_US = "UNABLE TO FIND" }, id = 0, key = new key() { href = "UNABLE TO FIND" } },
                    active_spec = new active_spec() { name = new English() { en_US = "UNABLE TO FIND" }, id = 0, key = new key() { href = "UNABLE TO FIND" } },
                    realm = new realm() { slug = "UNABLE TO FIND", id = 0, key = new key() { href = "UNABLE TO FIND" } },
                    guild = new guild() { id = 0, key = new key() { href = "UNABLE TO FIND" }, name = "UNABLE TO FIND", realm = new realm() { slug = "UNABLE TO FIND", id = 0, key = new key() { href = "UNABLE TO FIND" } }, faction = new faction() { type = "UNABLE TO FIND" } },
                    achievement_points = 0,
                    last_login_timestamp = 0,
                    average_item_level = 0,
                    equipped_item_level = 0,
                    covenant_progress = new covenant_progress() { renown_level = 0, chosen_covenant = new chosen_covenant() { id = 0, key = new key() { href = "UNABLE TO FIND" }, name = new English() { en_US = "UNABLE TO FIND" } }, soulbinds = new soulbinds() { href = "UNABLE TO FIND" } }
                };
                @class = CharacterProfile.character_class.Name;
                spec = CharacterProfile.active_spec.Name;
                race = CharacterProfile.race.Name;
            }
        }
    }

    [Serializable]
    public class CharacterProfile
    {
        public _links _links;
        public int id;
        public string name;
        public gender gender;
        public faction faction;
        public race race;
        public character_class character_class;
        public active_spec active_spec;
        public realm realm;
        public guild guild;
        public int achievement_points;
        public long last_login_timestamp;
        public int average_item_level;
        public int equipped_item_level;
        //Can do specializations if we want to know what tallents are specced
        public covenant_progress covenant_progress;
    }

    [Serializable]
    public class English
    {
        public string en_US;

        public override string ToString()
        {
            return en_US;
        }
    }


    [Serializable]
    public class gender
    {
        public string type;
        public English name;
        public string Name { get { return name.en_US; } }
    }

    [Serializable]
    public class race
    {
        public key key;
        public English name;
        public string Name { get { return name.en_US; } }
        public int id;
    }

    [Serializable]
    public class character_class
    {
        public key key;
        public English name;
        public string Name { get { return name.en_US; } }
        public int id;
    }

    [Serializable]
    public class active_spec
    {
        public key key;
        public English name;
        public string Name { get { return name.en_US; } }
        public int id;
    }

    [Serializable]
    public class guild
    {
        public key key;
        public string name;
        public int id;
        public realm realm;
        public faction faction;
    }

    [Serializable]
    public class covenant_progress
    {
        public chosen_covenant chosen_covenant;
        public int renown_level;
        public soulbinds soulbinds;
    }

    [Serializable]
    public class chosen_covenant
    {
        public key key;
        public English name;
        public string Name { get { return name.en_US; } }
        public int id;
    }

    [Serializable]
    public class soulbinds
    {
        public string href; //Can be used later to parse out what soulbinds people are using
    }

    [Serializable]
    public class realm
    {
        public key key;
        public int id;
        public string slug;
    }

    [Serializable]
    public class faction
    {
        public string type;
    }

    [Serializable]
    public class season_match_statistics
    {
        public int played;
        public int won;
        public int lost;
    }

    [Serializable]
    public class tier
    {
        public key key;
        public int id;
    }
}
