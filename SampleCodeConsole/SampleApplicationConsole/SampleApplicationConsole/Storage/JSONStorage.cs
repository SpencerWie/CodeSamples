using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace SampleApplicationConsole.Storage
{
    /// <summary>
    /// Inherited from the Storage, this class uses local JSON files as persitance storage.
    /// </summary>
    // Storage Class using Json.NET Libary 
    public class JSONStorage : Storage
    {
        public JSONStorage(string name, string location) : base(name, location)
        {
            this._name = name;
            this._location = location;
            this._connection = null;
            _Data = new Dictionary<string, string>();
        }

        // Writes a JSON File of the Data [@"c:\storage.json"]
        public virtual void WriteFile()
        {
            try
            {
                string json = JsonConvert.SerializeObject(_Data);
                File.WriteAllText(GetLocation(), json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with writing to file: " + ex.ToString());
            }
        }

        // Reads a JSON File into the Data
        public virtual void ReadFile()
        {
            try
            {
                string json = File.ReadAllText(GetLocation());
                AssignJSONStringToData(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with reading file: " + ex.ToString());
            }
        }

        // Parse JSON String into a Dictionary Object
        public void AssignJSONStringToData(string json)
        {
            _Data = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        // Removes a file
        public virtual void RemoveFile()
        {
            File.Delete(GetLocation());
        }
    }
}
