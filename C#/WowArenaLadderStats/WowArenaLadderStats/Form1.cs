using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WowArenaLadderStats
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSerialize_Click(object sender, EventArgs e)
        {
            //HttpResponseMessage response = await ApiClient.
            var x = JsonConvert.DeserializeObject<PvpLeaderboard3v3Response>(ApiClient.Get("data/wow/pvp-season/30/pvp-leaderboard/3v3?namespace=dynamic-us"));
            foreach (entry item in x.entries)
            {
                item.character.SetClassAndSpec();
            }

            Serialize(x, $@"D:\Repositories\Small Projects\C#\WowArenaLadderStats\Recorded Data\{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}");
        }

        private void Serialize(PvpLeaderboard3v3Response response, string filePath)
        {
            using (FileStream fs = File.Create(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, response);
            }
        }

        private PvpLeaderboard3v3Response Deserialize(string filePath)
        {
            using (FileStream fs = File.OpenRead(filePath))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (PvpLeaderboard3v3Response)formatter.Deserialize(fs);
            }
        }

        private void btnDeserialize_Click(object sender, EventArgs e)
        {
            PvpLeaderboard3v3Response response = new PvpLeaderboard3v3Response();
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                   response = Deserialize(ofd.FileName);
                }
            }

            var idiotFeral = response.entries.First(t => t.character.CharacterProfile.name == "Idiotferal");
            var feralDruids = response.entries.Where(t => t.character.spec.ToLower() == "feral");
            var feralDruidNames = feralDruids.Select(t => t.character.name);
            Dictionary<string, int> faction = response.entries.GroupBy(t => t.faction.type).ToDictionary(t => t.Key, t => t.Count());
            Dictionary<string, int> gender = response.entries.GroupBy(t => t.character.CharacterProfile.gender.type).ToDictionary(t => t.Key, t => t.Count());
            Dictionary<string, int> races = response.entries.GroupBy(t => t.character.race).ToDictionary(t => t.Key, t => t.Count());
            Dictionary<string, int> specs = response.entries.GroupBy(t => t.character.spec + " " + t.character.@class).ToDictionary(t => t.Key, t => t.Count());
            Dictionary<string, double> specIlvl = response.entries.Where(t => t.character.CharacterProfile.average_item_level != 0).GroupBy(t => t.character.spec + " " + t.character.@class).ToDictionary(t => t.Key, t => t.Average(x => x.character.CharacterProfile.average_item_level));
            double averageIlvl = response.entries.Where(t => t.character.CharacterProfile.average_item_level != 0).Average(t => t.character.CharacterProfile.average_item_level);
        }

        private void btnCustom_Click(object sender, EventArgs e)
        {
            var x = ApiClient.Get("/profile/wow/character/proudmoore/idiotferal/soulbinds?namespace=profile-us");
        }
    }
}
