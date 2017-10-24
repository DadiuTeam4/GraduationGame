# MGP2
### Documentation goes here
#### But what do I put in the documentation?

- Tag uses.
- Layer uses (for collision, culling, and raycasting – essentially, what should be in what layer).
- GUI depths for layers (what should display over what).
- Scene setup.
- Prefab structure of complicated prefabs.
- Idiom preferences.
- Build setup.

Please use markdown syntax for an easily readable format. Checkout www.hackmd.io for a markdown editor.

### Code inspection
Every time you want to commit new scripts and/or changes to scripts, you have to run the code inspection script in order to determine whether your code is up to Code Convention Standards^tm^. It is called *inspectCode.py* and is located in the root folder of the project.

#### Setup
Requirements are [Python](https://www.python.org/downloads/) (whatever version) and the [ReSharper Command Line Tools](https://www.jetbrains.com/resharper/download/index.html#section=resharper-clt). 

If you are on Windows, you have to extract the command line tools to any folder and adding that folder to your *Path*. This is done by searching for *Edit the system environment variables* -> Click *Environment variables* -> Double-click *Path* -> Click *New* and write the directory to the folder where you extracted the ReSharper Command Line Tools. The code inspector executable should now be callable from anywhere in your system.

#### Usage
Simply run the *inspectCode.py* script by double-clicking it, or by running 
`python <PROJECT-FOLDER>/inspectCode.py` in your terminal. The script will produce a file called *codeInspectOutput.md*. This file will contain all issues found in the entire solution.

#### Configuration
If the script produces irrelevant issues or otherwise unusable issues, you can add them to *ignoredIssues.csv*. Then they will be ignored when running the inspection script.

