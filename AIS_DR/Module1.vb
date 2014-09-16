Imports System.IO

Module Module1

    Sub Main()
        'Dim a As New ZipUtil
        'a.DecompressFile("C:\TEMP\4503200_2014081618_SDGApp5.log.gz", "C:\TEMP\4503200_2014081618_SDGApp5.log")
        MoveDRFiletoLocalAndExtract()
        ' RunReport()
    End Sub
    Private Sub RunReport()
        Dim objDB As New DBClass
        objDB.RunReport()
    End Sub
    Private Sub MoveDRFiletoLocalAndExtract()
        Dim MSNList As String = ""
        Dim path As String
        path = System.AppDomain.CurrentDomain.BaseDirectory()

        Dim logname As String = path + "\log\" + Now.ToString("ddMMyyyyhhmm") + "_log.xml"
        Dim script As String = path + "\GetFileScript.txt"
        Dim winscp As Process = New Process()
        winscp.StartInfo.FileName = path + "\winscp.com"
        winscp.StartInfo.Arguments = "/console /command ""lcd " + path + """ /script=" + script + " /log=" + logname '/console /script=a.txt /log=a2.xml
        winscp.StartInfo.UseShellExecute = False
        winscp.StartInfo.RedirectStandardInput = True
        winscp.StartInfo.RedirectStandardOutput = True
        winscp.StartInfo.CreateNoWindow = True
        winscp.Start()
        'winscp.StandardInput.WriteLine("option batch abort")
        'winscp.StandardInput.WriteLine("option confirm off")
        'winscp.StandardInput.WriteLine("open " + IIf(drConFig("serviceid").ToString.StartsWith("350"), "ais", "aisprod"))
        'winscp.StandardInput.WriteLine("ls")
        'winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
        '    winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + uploadFname)
        winscp.StandardInput.Close()
        Dim output As String = winscp.StandardOutput.ReadToEnd()
        Console.WriteLine(output)
        winscp.WaitForExit()
        Console.WriteLine("Finished")


        Dim di As New IO.DirectoryInfo(path + "\DRFileTmp")
        Dim diar1 As IO.FileInfo() = di.GetFiles("*.gz")
        Dim dra As IO.FileInfo

        'list the names of all files in the specified directory
        Dim Zip As New ZipUtil
        Dim ExtractedFile As String
        Dim counter As Integer = 0
        For Each dra In diar1
            ExtractedFile = dra.FullName.Replace(".gz", "")
            Zip.DecompressFile(dra.FullName, ExtractedFile)
            dra.CopyTo(path + "\DRFile\" + dra.Name, True)
            dra.Delete()
            Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(ExtractedFile)
            Dim line As String
            Dim linesplit As String()
            line = reader.ReadLine
            Dim objDB As New DBClass
            Dim DRDAY, DRMONTH, DRYEAR, CHARGE As String
            Do While Not line Is Nothing
                'FR|adapter@SDGApp6:I0sZovVG|667190936826106|450320001|Forwarded|1000|external:success|3PE|WAPPUSH|66871941803|127238184300654627|BROADCAST||450320001|0|22||177||00:54:36|14|
                'DN|ad14437@CPE5:GNCPrUnB|669232575298040|450320001|Retrieved|1000|external:DELIVRD:000|3PE|SMS|66923194337|125051117700402712|BROADCAST|450320001_20140816231001||0|0||1083||23:16:06|11|
                '4503200_2014081719_SDGApp5.log.gz
                linesplit = line.Split("|")
                If (linesplit(4) = "Retrieved") Then
                    CHARGE = "Y"
                Else
                    CHARGE = "N"
                End If

                DRDAY = CInt(dra.Name.Split("_")(1).Substring(6, 2)).ToString
                DRMONTH = CInt(dra.Name.Split("_")(1).Substring(4, 2)).ToString
                DRYEAR = dra.Name.Split("_")(1).Substring(0, 4)

                objDB.AddDR(linesplit(1), linesplit(3), linesplit(5), linesplit(6), linesplit(7), linesplit(8), linesplit(9), linesplit(20), CHARGE, DRDAY, DRMONTH, DRYEAR, linesplit(19), dra.Name)

                line = reader.ReadLine
            Loop

            reader.Close()

            File.Delete(ExtractedFile)
        Next

    End Sub

End Module
