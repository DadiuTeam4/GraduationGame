# Author: Mathias Dam Hedelund
# Contributors: 
import os
import csv
import xml.etree.ElementTree as ET

# Constant filenames
XMLFILENAME = "inspectCodeOutput.xml"
MDFILENAME = "inspectCodeOutput.md"
IGNOREDFILENAME = "ignoredIssues.csv"

# Issues set to be ignored
ignoredIssues = []
with open(IGNOREDFILENAME) as csvfile:
    ignoredIssueReader = csv.reader(csvfile, delimiter = ',', quotechar = '"')
    for issues in ignoredIssueReader:
        ignoredIssues = issues

# Run ReSharper
os.system("inspectcode.exe .\GraduationGame.sln -o=\"" + XMLFILENAME + "\"")

# Get issues from XML output
tree = ET.parse(XMLFILENAME)
root = tree.getroot()
issues = root.find('Issues')
if (len(issues) > 0):
    issueArray = issues.find('Project').findall('Issue')
else:
    print("No issues found")
    exit()

# Open .md file and initialize variables
outputFile = open(MDFILENAME, 'w')
issueCounter = 0
outStrings = {}

# Loop through issues and write them
for child in issueArray:
    # Ignore issue if included in ignoredIssues
    typeId = child.get('TypeId')
    if (typeId.strip() in ignoredIssues):
        print("Ignoring issue: " + typeId.strip())
        continue

    fileName = child.get('File')
    line = child.get('Line')
    if (line is None):
        line = "1"
    message = child.get('Message')

    # Add filename to dictionary
    if (fileName not in outStrings):
        outStrings[fileName] = []

    # Format issue nicely
    outString = "*Issue type: " + typeId + "*\n"
    outString += "*Line: " + line + "*\n"
    outString += "Message: " + message + "\n"
    outString += "\n"

    outStrings[fileName].append(outString)

    issueCounter += 1

# Write to file and close
outputFile.write("**Issues: " + str(issueCounter) + "**\n\n")
for name, array in outStrings.items():
    outputFile.write("**" + name + "**\n")
    for issue in array:
        outputFile.write(issue)
outputFile.close()

# Clean up XML file
os.remove(XMLFILENAME)

# Report success
print("Inspection report was converted to markdown and written to " + os.getcwd() + "\\" + MDFILENAME)
