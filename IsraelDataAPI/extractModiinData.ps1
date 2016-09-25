#
$sourceFolder = "."
$targetFolder = "."

$sourceDataFile = "atm.xml"
$targetDataFile = "modiin-atm.xml"

$xmlFile = Join-Path -Path $sourceFolder -ChildPath $sourceDataFile
$targetFile = Join-Path -Path $targetFolder -ChildPath $targetDataFile

$atmLocationTag = "ישוב"
$atmLocationPath = "//ATMs/ATM/" + $atmLocationTag
$atmLocationFilter = "מודיעין-מכבים-רעות*"
$atmXPATH = $atmLocationTag + '[text()="' + $atmLocationFilter + '"]'

[xml]$xml = Get-Content -path $xmlFile  -Encoding UTF8
$atmNodes = $xml.ChildNodes[1].SelectNodes("ATM")

foreach ($node in $atmNodes) {
    if ($node.$atmLocationTag -ne $atmLocationFilter) {
        $removedNode = $node.ParentNode.RemoveChild($node)
    }    
}

$xml.Save($targetFile)

#$modiinATMs = $xml | Select-Xml -Xpath $atmXPATH
#"Found " + $modiinATMs.Count + " nodes for " + $atmLocationFilter

#[ xml]$xml = Get-Content -path $xmlFile.FullName
# $ns = New-Object System.Xml.XmlNamespaceManager($xml.NameTable)
# $ns.AddNamespace("ns", $xml.DocumentElement.NamespaceURI)
# $node = $xml.SelectSingleNode("//ns:" + $removedTag, $ns);
