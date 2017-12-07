using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplicationConsole.Storage
{
    /// <summary>
    /// A base class for implmenting different persistence storage sub-classes.
    /// </summary>
    public class Storage
    {
        private string _Name;        // Name of the Storage Class item, to be used via the config file.
        private string _Location;    // Location that the data is stored to.
        public List<Item> Data;     // Storage data, basic Item list for this example.

        public Storage(string Name, string Location)
        {
            this._Name = Name;
            this._Location = Location;
        }

        public string GetName()
        {
            return this._Name;
        }

        public void SetName(string NewName)
        {
            try
            {
                if (String.IsNullOrEmpty(NewName))
                    throw new ArgumentException("A Storage Name cannot be null or empty.");
            }
            catch(ArgumentException ex)
            {
                // Normally a Logger would be used here, the Console is used for this sample.
                Console.WriteLine("The given Name is not valid: " + ex.ToString()); 
                return;
            }
            this._Name = NewName;
        }


        // Method to be used for basic argument checks when a setter is used, can be expanded upon or overloaded for classes that inherit this one. 
        private bool ValidateArgument(string property, string argument)
        {
            bool valid = true;

            try
            {
                if (String.IsNullOrEmpty(argument))
                    throw new ArgumentException("A Storage Name cannot be null or empty.");
            }
            catch (ArgumentException ex)
            {
                // Normally a Logger would be used here, the Console is used for this sample.
                Console.WriteLine("The given Name is not valid: " + ex.ToString());
                valid = false;
            }

            return valid;
        }
    }

    // Basic data construct, a key/value pair.
    public class Item
    {
        public string key   { get; set; }
        public string value { get; set; }
    }
}
