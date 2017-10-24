import xml.etree.ElementTree as ET

FILENAME = "inspect_code_output.xml"
OUTFILENAME = "inspect_code_output.md"

tree = ET.parse(FILENAME)
root = tree.getroot()
issues = root.find('Issues')
issueArray = issues.find('Project').findall('Issue')

outputFile = open(OUTFILENAME, 'w')

outputFile.write("Issues: " + str(len(issueArray)) + "\n\n")

for child in issueArray:
    typeId = child.get('TypeId')
    fileName = child.get('File')
    offset = child.get('Offset')
    line = child.get('Line')
    message = child.get('Message')

    outString = "**" + fileName + "**\n"
    outString += "*Issue type: " + typeId + "*\n"
    if (line is not None):
        outString += "*Line: " + line + "*\n"
    outString += "Message: " + message + "\n"
    outString += "\n"

    outputFile.write(outString)

outputFile.close()