using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApplicationConsole.Storage
{
    /// <summary>
    /// A base class for implmenting different persistence storage sub-classes.
    /// This class has a basic "Data" property that for now is just in-memeory non-persistent storage.
    /// Classes that inheirt this class will use a form of persistence storage, which will load that information into "Data".
    /// </summary>
    public class Storage
    {
        #region Properties and Cosntructors
        protected string _name;                                           // Name of the Storage Class item, to be used via the config file.
        protected string _location;                                       // Location that the data is stored to.
        protected string _connection;                                     // Connection String if needed.    
        protected virtual Dictionary<string, string> _Data { get; set; }  // Storage data, basic Item list for this example.

        public Storage(string name, string location, string connection = null)
        {
            this._name = name;
            this._location = location;
            this._connection = connection;
            _Data = new Dictionary<string, string>();
        }
        #endregion

        #region Getters/Setters
        public string GetName()
        {
            return this._name;
        }

        public void SetName(string newName)
        {
            if(ValidateArgument(newName))
                this._name = newName;
        }

        public string GetLocation()
        {
            return this._location;
        }

        public void SetLocation(string newLocation)
        {
            if (ValidateArgument(newLocation))
                this._location = newLocation;
        }

        public string GetConnectionString()
        {
            return this._connection;
        }

        public void SetConnectionString(string newConnectionString)
        {
            if (ValidateArgument(newConnectionString))
                this._connection = newConnectionString;
        }

        // Returns a List of all the keys in the Data
        public List<string> GetKeyList()
        {
            return this._Data.Keys.ToList();
        }
        #endregion

        #region CRUD Operations (Basic)

        // Create for base class will append a new item to the dictionary
        public virtual void Create(string key, string value)
        {
            if (!this._Data.ContainsKey(key))
                this._Data.Add(key, value);
            else
                Console.WriteLine($"This storage already contains an item with the key: {key}.");
        }

        // Read for base class attempts to get a dictionary item by key, if it does not exist null is returned.
        public virtual string Read(string key)
        {
            if (this._Data.ContainsKey(key))
                return this._Data[key];

            Console.WriteLine($"There is no item with the key: {key}.");
            return null;
        }

        // Update for base class attempts to change a directory value if the key exist.
        public virtual void Update(string key, string value)
        {
            if (this._Data.ContainsKey(key))
                this._Data[key] = value;
            else
                Console.WriteLine("Cannot update a item that does not exist.");
        }

        // Delte for base class removes a item by it's key if it exist.
        public virtual void Delete(string key)
        {
            if (this._Data.ContainsKey(key))
                this._Data.Remove(key);
            else
                Console.WriteLine("Cannot remove an item which does not exist.");
        }

        #endregion

        #region Validators
        // Method to be used for basic argument checks when a setter is used, can be expanded upon or overloaded for classes that inherit this one. 
        protected virtual bool ValidateArgument(string argument)
        {
            bool valid = true;

            try
            {
                if (String.IsNullOrEmpty(argument))
                    throw new ArgumentException("This property cannot be null or empty.");
            }
            catch (ArgumentException ex)
            {
                // Normally a Logger would be used here, the Console is used for this sample.
                Console.WriteLine("The property is not valid: " + ex.ToString());
                valid = false;
            }

            return valid;
        }
        #endregion
    }
}
