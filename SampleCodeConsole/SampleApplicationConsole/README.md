<h2>Sample Code Console</h2>

<p>
This is a sample code "Hello World" program showing general programming princibles and Unit Test, along with using a seperate API In this case the used API is <a href="https://azure.microsoft.com/en-us/">Microsoft Azure</a>, a network of cloud services including: Web Services, Mobile Cloud Applications, Cloud Storage, Databases, and more. As such I thought this would be a good choice for a general API.
</p>

<p>
Running this Code:
Go the the following URL https://github.com/SpencerWie/CodeSamples, then clone the repository and open it in Visual Studios (<em>2015 Reccommended</em>).
</p>
<p>
<strong>Running Unit Test</strong><br />
To run the Unit Test in the window click "Run All":<br />
<img src="https://i.imgur.com/KpVGKYf.png" alt="Unit Test Run All"></img>
</p>
<p>
<strong>Running the Code</strong><br />
If not already, ensure the main project <em>SampleApplicationConsole</em> is the StartUp Project, if VS defaulted to the UnitTest Project:<br />

<img src="https://i.imgur.com/Ha4t9BP.png" alt="Right-click 'SampleApplicationConsole', click 'Set as startup project'">
</p>

<p>
This sample will include three classes with a general Storage using CRUD operations, using a key/value pair for the inital data storage.
</p>

<div>
  <h4>Storage</h4> 
  <p>
  A base class containing in-memory non-persistent storage, this base class is to expanded by any other entity to store information. It's   built to be flexiable to support any type of basic storage including file-based storage and database storage.
  </p>
  <h4>JSONStorage <em>: Storage</em></h4>
  <p>
  A class inheriting from the <em>Storage</em> class, which is used for JSON File Storage. 
  </p>
  <h4>BlobStorage <em>: JSONStorage</em></h4>
  <p>
  A class inheriting from the <em>JSONStorage</em> class, it also uses a JSON File Storage. But further expands it to use the API to store   the JSON file on the Azure Cloud. 
  </p>
</div>

<div>
  <h4>Dependencies</h4>
  <p> 
  This Code Sample uses both an <a href="https://azure.microsoft.com/en-us/">Azure</a> along with the <a href="https://www.newtonsoft.com/json)">Json.NET</a>. These files and DLLs are included in this sample.
  </p>
  <p>
  This needs to use a Windows machine and the following directory needs to exist:
  </p>
  <div><code>C:\Temp\</code></div><br />
  <br />
  <p>
  This is the location that the JSONStorage and BlobStorage will write local JSON files to.
  </p>
</div>

<h2>Execution</h2>

<p>
Running the project <em>SampleApplicationConsole.csproj</em> will produce a generic "Hello World" message along with do some basic steps for each of the three classes to ensure they are working:

<img src="https://i.imgur.com/uRV1ZHY.png" alt="Output of Console Execution"></img>
</p>

<p>
In Visual Studio The execution of the test cases will produce all passes:

<img src="https://i.imgur.com/BJrs6DX.png" alt="Test Cases Passed"></img>
</p>
