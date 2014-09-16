Imports System
Imports System.Diagnostics
Imports System.Xml
Imports System.Xml.XPath
Imports System.IO
Imports System.Text


Module Module1
    Public nCount As Integer = 9988


    Sub Main()
        Try

            Dim objDB As New DBClass
            Dim dt As New DataTable
            'dt = objDB.CheckTypeLWaitingtoProcess()


            'Dim drList() As DataRow
            'drList = dt.Select("SSSActionReport='REG_SUCCESS'")
            'PutFileRegis(drList)
            'drList = dt.Select("SSSActionReport='UNREG_IMMEDIATE'")
            'PutFileUnregis(drList)
            Dim dtBRSchedule As New DataTable
            ' Dim dtBRSchedule_ByMSN As New DataTable
            ' Dim CountService As Integer

            'No MSN
            dt = objDB.getDueContentSchedule("A")
            If (dt.Rows.Count > 0) Then
                Dim TypeLFTPFile As String
                Console.WriteLine(dt.Rows.Count.ToString)
                For Each dr As DataRow In dt.Rows
                    TypeLFTPFile = ""
                    'SELECT CS.CONTENT_SCHEDULE_ID,CSS.CONTENT_SCHEDULE_SUBSERVICE_ID,S.SUBSERVICE_ID,CS.SMSCONTENT,CS.SCHEDULE FROM T_CONTENT_SCHEDULE CS
                    dtBRSchedule = objDB.getActive_Member_for_Schedule_MT(dr("CONTENT_SCHEDULE_SUBSERVICE_ID").ToString, dr("SUBSERVICE_ID").ToString, "A")
                    '            	,SV.SERVICE_NAME,SD.SENDER_NAME,S.PRICE

                    PutFileContent(dtBRSchedule, TypeLFTPFile, dr("SERVICE_NAME").ToString, dr("SENDER_NAME").ToString, dr("SMSCONTENT").ToString, dr("PRICE").ToString, dr("OPERATOR_SERVICE_ID").ToString)
                    objDB.set_Status_for_Schedule_MT(CInt(dr("CONTENT_SCHEDULE_SUBSERVICE_ID").ToString), TypeLFTPFile, "A")
                Next
            End If



            'By MSN
            'dtBRSchedule_ByMSN = objDB.getBroadcastActiveUserList_TypeL_ByMSN
            'Dim dtDistinct_ServicePMKey As DataTable = dtBRSchedule_ByMSN.DefaultView.ToTable(True, New String("servicePMkey"))

            'For Each drSubID As DataRow In dtDistinct_ServicePMKey.Rows
            '    PutFileContent_ByMSN(dtBRSchedule_ByMSN.Select("ServicePMKey=" + drSubID("ServicePMKey").ToString), TypeLFTPFile)
            '    objDB.SetSuccessTypeLProcess_ByMSN(CInt(drSubID("ServicePMKey").ToString), TypeLFTPFile)
            '    Threading.Thread.Sleep(2000)
            'Next

            '######### load test ##################
            'Dim dtMSN As New DataTable
            'dtMSN.Columns.Add("MSN", GetType(String))
            'dtMSN.Columns.Add("serviceid", GetType(String))
            'dtMSN.Columns.Add("servicename", GetType(String))
            'dtMSN.Columns.Add("sender", GetType(String))
            'dtMSN.Columns.Add("msg", GetType(String))
            'dtMSN.Columns.Add("price", GetType(String))
            'For msn As Int64 = 66871000000 To 66871009000
            '    dtMSN.Rows.Add(CStr(msn), "A450450101", "Fashion Gallery", "4504501", "http://www.www.www", "5")
            'Next
            'PutFileContent(dtMSN.Select(""), "")
            '######################################

        Catch ex As Exception
            Console.WriteLine(ex.Message)
            Console.ReadLine()
        End Try
    End Sub



    Private Sub PutFileRegis(ByVal drList() As DataRow)
        Dim drConFig As DataRow
        Dim MSNList As String = ""
        If (drList.Length > 0) Then

            drConFig = drList(0)
            For Each dr As DataRow In drList
                MSNList += dr("MSN") + vbCrLf
            Next
            MSNList = MSNList.Substring(0, MSNList.Length - 1)
            Console.WriteLine(MSNList)



            Dim path As String
            path = System.AppDomain.CurrentDomain.BaseDirectory()
            Console.WriteLine(path)


            'Console.Read()
            Dim RegConfirmTemplateText As String
            Dim RegSuccessTemplateText As String
            RegConfirmTemplateText = File.ReadAllText(path + "\TemPlateTxTFile\RegConfirmTemplate.txt")
            RegSuccessTemplateText = File.ReadAllText(path + "\TemPlateTxTFile\RegSuccessTemplate.txt")
            Dim DateTimeText As String

            DateTimeText = Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))

            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{datetime}", DateTimeText)
            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{servicename}", drConFig("servicename"))
            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{servicename}", drConFig("servicename"))
            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{sender}", drConFig("sender"))
            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{msg}", drConFig("RegisterSuccessText"))
            RegConfirmTemplateText = RegConfirmTemplateText.Replace("{msnlist}", MSNList)


            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{datetime}", DateTimeText)
            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{servicename}", drConFig("servicename"))
            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{servicename}", drConFig("servicename"))
            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{sender}", drConFig("sender"))
            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{msg}", drConFig("RegisterSuccessText"))
            RegSuccessTemplateText = RegSuccessTemplateText.Replace("{msnlist}", MSNList)


            Dim sb As New StringBuilder()



            sb.Append(RegSuccessTemplateText)
            'Dim TextIndexBegin As Integer = 0
            'Dim TextIndexEnd As Integer = 0
            'Dim keyWord As String = ""
            'While TemplateText.IndexOf("{", TextIndexBegin) > -1
            '    keyWord = TemplateText.Substring(TemplateText.IndexOf("{", TextIndexBegin) + 1, TemplateText.IndexOf("}", TextIndexEnd) - TemplateText.IndexOf("{", TextIndexBegin) - 1)

            '    TextIndexBegin = TemplateText.IndexOf("{", TextIndexBegin) + 1
            '    TextIndexEnd = TemplateText.IndexOf("}", TextIndexEnd) + 1

            '    If (drConFig.) Then

            '    End If

            'End While

            'For Each keyWord As String In TemplateText.Split("{")
            '    keyWord = keyWord.Split("}")(0)
            '    TemplateText.Replace(keyWord, drConFig(keyWord))
            'Next



            'Dim outfile As New StreamWriter(path + "\AllTxtFiles.txt")

            Dim textFileName As String
            textFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
            Dim outfile As New StreamWriter(path + "\PrepareFile\" + textFileName, False, System.Text.Encoding.Default)

            outfile.Write(sb.ToString())
            outfile.Close()



            Const logname As String = "log.xml"

            Dim winscp As Process = New Process()
            winscp.StartInfo.FileName = "winscp.com"
            winscp.StartInfo.Arguments = "/log=" + logname
            winscp.StartInfo.UseShellExecute = False
            winscp.StartInfo.RedirectStandardInput = True
            winscp.StartInfo.RedirectStandardOutput = True
            winscp.StartInfo.CreateNoWindow = True
            winscp.Start()
            winscp.StandardInput.WriteLine("option batch abort")
            winscp.StandardInput.WriteLine("option confirm off")
            winscp.StandardInput.WriteLine("open aisprod")
            winscp.StandardInput.WriteLine("ls")
            winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
            winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + textFileName)
            winscp.StandardInput.Close()
            Dim output As String = winscp.StandardOutput.ReadToEnd()
            Console.WriteLine(output)
            winscp.WaitForExit()



        End If


        'Const logname As String = "log.xml"

        '' Run hidden WinSCP process
        'Dim winscp As Process = New Process()
        'winscp.StartInfo.FileName = "winscp.com"
        'winscp.StartInfo.Arguments = "/log=" + logname
        'winscp.StartInfo.UseShellExecute = False
        'winscp.StartInfo.RedirectStandardInput = True
        'winscp.StartInfo.RedirectStandardOutput = True
        'winscp.StartInfo.CreateNoWindow = True
        'winscp.Start()

        '' Feed in the scripting commands
        'winscp.StandardInput.WriteLine("option batch abort")
        'winscp.StandardInput.WriteLine("option confirm off")
        'winscp.StandardInput.WriteLine("open ais")
        'winscp.StandardInput.WriteLine("ls")
        'winscp.StandardInput.WriteLine("cd data")
        'winscp.StandardInput.WriteLine("put d:\examplefile.txt")
        'winscp.StandardInput.Close()

        '' Collect all output (not used in this example)
        'Dim output As String = winscp.StandardOutput.ReadToEnd()

        '' Wait until WinSCP finishes
        'winscp.WaitForExit()

        '' Parse and interpret the XML log
        '' (Note that in case of fatal failure the log file may not exist at all)

        ''Dim log As XPathDocument = New XPathDocument(logname)
        ''Dim ns As XmlNamespaceManager = New XmlNamespaceManager(New NameTable())
        ''ns.AddNamespace("w", "http://winscp.net/schema/session/1.0")
        ''Dim nav As XPathNavigator = log.CreateNavigator()

        '' Success (0) or error?
        'If winscp.ExitCode <> 0 Then

        '    Console.WriteLine("Error occured")

        '    ' See if there are any messages associated with the error
        '    'For Each message As XPathNavigator In nav.Select("//w:message", ns)
        '    '    Console.WriteLine(message.Value)
        '    'Next

        'Else

        '    ' It can be worth looking for directory listing even in case of
        '    ' error as possibly only upload may fail

        '    'Dim files As XPathNodeIterator = nav.Select("//w:file", ns)
        '    'Console.WriteLine(String.Format("There are {0} files and subdirectories:", files.Count))
        '    'For Each file As XPathNavigator In files
        '    '    Console.WriteLine(file.SelectSingleNode("w:filename/@value", ns).Value)
        '    'Next

        'End If
    End Sub
    Private Sub PutFileUnregis(ByVal drList() As DataRow)
        Dim drConFig As DataRow
        Dim MSNList As String = ""
        If (drList.Length > 0) Then

            drConFig = drList(0)
            For Each dr As DataRow In drList
                MSNList += dr("MSN") + vbCrLf
            Next
            MSNList = MSNList.Substring(0, MSNList.Length - 1)
            Console.WriteLine(MSNList)



            Dim path As String
            path = System.AppDomain.CurrentDomain.BaseDirectory()
            Console.WriteLine(path)


            'Console.Read()
            Dim TemplateText As String
            TemplateText = File.ReadAllText(path + "\TemPlateTxTFile\UnregImmediateTemplate.txt")
            Dim DateTimeText As String

            DateTimeText = Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))

            TemplateText = TemplateText.Replace("{datetime}", DateTimeText)
            TemplateText = TemplateText.Replace("{servicename}", drConFig("servicename"))
            TemplateText = TemplateText.Replace("{servicename}", drConFig("servicename"))
            TemplateText = TemplateText.Replace("{sender}", drConFig("sender"))
            TemplateText = TemplateText.Replace("{msg}", drConFig("UnRegisterSuccessText"))
            TemplateText = TemplateText.Replace("{msnlist}", MSNList)


            Dim sb As New StringBuilder()



            sb.Append(TemplateText)


            Dim textFileName As String
            textFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
            Dim outfile As New StreamWriter(path + "\PrepareFile\" + textFileName, False, System.Text.Encoding.Default)

            outfile.Write(sb.ToString())
            outfile.Close()



            Const logname As String = "log.xml"

            Dim winscp As Process = New Process()
            winscp.StartInfo.FileName = "winscp.com"
            winscp.StartInfo.Arguments = "/log=" + logname
            winscp.StartInfo.UseShellExecute = False
            winscp.StartInfo.RedirectStandardInput = True
            winscp.StartInfo.RedirectStandardOutput = True
            winscp.StartInfo.CreateNoWindow = True
            winscp.Start()
            winscp.StandardInput.WriteLine("option batch abort")
            winscp.StandardInput.WriteLine("option confirm off")
            winscp.StandardInput.WriteLine("open aisprod")
            winscp.StandardInput.WriteLine("ls")
            winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
            winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + textFileName)
            winscp.StandardInput.Close()
            Dim output As String = winscp.StandardOutput.ReadToEnd()
            Console.WriteLine(output)
            winscp.WaitForExit()



        End If



    End Sub
    Private Sub PutFileContent(ByVal dtBRSchedule As DataTable, ByRef TypeLFTPFile As String, ServiceName As String, SenderName As String, SMSContent As String, Price As String, Oper_Service_ID As String)
        Dim MSNList As String = ""
        Dim path As String
        If (dtBRSchedule.Rows.Count > 0) Then



            For Each dr As DataRow In dtBRSchedule.Rows
                MSNList += dr("MSN") + vbLf
            Next
            '######### old code ##################
            'MSNList = MSNList.Substring(0, MSNList.Length - 1)
            'Console.WriteLine(MSNList)
            '#####################################



            path = System.AppDomain.CurrentDomain.BaseDirectory()
            Console.WriteLine(path)


            'Console.Read()
            Dim TemplateText As String
            TemplateText = File.ReadAllText(path + "\TemPlateTxTFile\BroadcastTemplate.txt")
            Dim DateTimeText As String

            '######### old code ##################
            'DateTimeText = Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
            'TemplateText = TemplateText.Replace("{datetime}", DateTimeText.ToString.Trim)
            '#####################################
            TemplateText = TemplateText.Replace("{servicename}", ServiceName)
            TemplateText = TemplateText.Replace("{sender}", SenderName)
            TemplateText = TemplateText.Replace("{msg}", SMSContent)
            TemplateText = TemplateText.Replace("{price}", Price)
            TemplateText = TemplateText.Replace("{type}", IIf(SMSContent.StartsWith("http://"), "B", "T"))
            TemplateText = TemplateText.Replace(vbCrLf & "{subject}", IIf(SMSContent.StartsWith("http://"), vbCrLf & "SUBJECT:" + ServiceName, ""))
            TemplateText = TemplateText.Replace(vbCrLf, vbLf)


            '############## new code #############
            Dim numOfFile As Integer = genFileName(dtBRSchedule.Rows.Count)
            Dim nFiles(numOfFile - 1) As String
            Dim nLineLength As Integer = 12
            For nFile As Integer = 1 To numOfFile

                DateTimeText = Now.AddMinutes(nFile).ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                Dim ntextFileName As String
                ntextFileName = Oper_Service_ID + "_" + DateTimeText + ".txt"
                Dim n As Integer = 1
                While IO.File.Exists(path + "\PrepareFile\" + ntextFileName) 'check name of file is not exists.
                    DateTimeText = Now.AddMinutes(nFile + n).ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                    ntextFileName = Oper_Service_ID + "_" + DateTimeText + ".txt"
                    n += 1
                    Threading.Thread.Sleep(1000)
                End While
                Dim m As String = ""
                Try
                    m = MSNList.Substring(nCount * ((nFile - 1) * nLineLength), nCount * nLineLength)
                Catch ex As Exception
                    m = MSNList.Substring(nCount * ((nFile - 1) * nLineLength))
                End Try

                Dim nTemplateText As String = TemplateText.Replace("{msnlist}", m.Substring(0, m.Length - 1))
                nTemplateText = nTemplateText.Replace("{datetime}", DateTimeText.ToString.Trim)
                Dim nsb As New StringBuilder()
                nsb.Append(nTemplateText)

                nFiles(nFile - 1) = ntextFileName
                TypeLFTPFile = ntextFileName.Replace(".txt", "")
                Console.WriteLine(path + "\PrepareFile\" + ntextFileName)
                Dim nOutfile As New StreamWriter(path + "\PrepareFile\" + ntextFileName, False, System.Text.Encoding.Default)

                nOutfile.Write(nsb.ToString())
                nOutfile.Close()
            Next
            TypeLFTPFile = String.Join(",", nFiles).Replace(".txt", "")
            'Console.WriteLine("Finished")
            'Threading.Thread.Sleep(5000)

            Dim logname As String = path + "\" + Now.ToString("ddMMyyyy_") + "log.xml"
            Dim winscp As Process = New Process()
            winscp.StartInfo.FileName = "C:\Program Files (x86)\WinSCP\winscp.com"
            winscp.StartInfo.Arguments = "/log=" + logname
            winscp.StartInfo.UseShellExecute = False
            winscp.StartInfo.RedirectStandardInput = True
            winscp.StartInfo.RedirectStandardOutput = True
            winscp.StartInfo.CreateNoWindow = True
            winscp.Start()
            winscp.StandardInput.WriteLine("option batch abort")
            winscp.StandardInput.WriteLine("option confirm off")
            winscp.StandardInput.WriteLine("open " + IIf(Oper_Service_ID.StartsWith("350"), "staging", "prod"))
            winscp.StandardInput.WriteLine("ls")
            winscp.StandardInput.WriteLine("cd /export/home/cpuser503/data")
            For Each uploadFname As String In nFiles
                winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + uploadFname)
                Threading.Thread.Sleep(1000)
            Next
            winscp.StandardInput.Close()
            Dim output As String = winscp.StandardOutput.ReadToEnd()
            Console.WriteLine(output)
            winscp.WaitForExit()
            Console.WriteLine("Finished")
            'Console.ReadLine()
            '#####################################


            '######### old code ##################
            'Dim sb As New StringBuilder()

            'sb.Append(TemplateText)

            'Dim textFileName As String
            'textFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
            'TypeLFTPFile = textFileName.Replace(".txt", "")
            'Dim outfile As New StreamWriter(path + "\PrepareFile\" + textFileName, False, System.Text.Encoding.Default)

            'outfile.Write(sb.ToString())
            'outfile.Close()

            'Dim logname As String = path + "\log.xml"

            'Dim winscp As Process = New Process()
            'winscp.StartInfo.FileName = "winscp.com"
            'winscp.StartInfo.Arguments = "/log=" + logname
            'winscp.StartInfo.UseShellExecute = False
            'winscp.StartInfo.RedirectStandardInput = True
            'winscp.StartInfo.RedirectStandardOutput = True
            'winscp.StartInfo.CreateNoWindow = True
            'winscp.Start()
            'winscp.StandardInput.WriteLine("option batch abort")
            '' Console.ReadLine()
            'winscp.StandardInput.WriteLine("option confirm off")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("open " + IIf(drConFig("serviceid").ToString.StartsWith("350"), "ais", "aisprod"))
            '' Console.ReadLine()
            'winscp.StandardInput.WriteLine("ls")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + textFileName)
            '' Console.ReadLine()
            'winscp.StandardInput.Close()
            'Dim output As String = winscp.StandardOutput.ReadToEnd()
            'Console.WriteLine(output)
            'winscp.WaitForExit()
            'Console.WriteLine("Finished")
            'Console.ReadLine()
            '###############################################################################3



        End If



    End Sub
    Private Sub PutFileContent_ByMSN(ByVal dtBRSchedule() As DataRow, ByRef TypeLFTPFile As String)
        Dim drConFig As DataRow
        Dim MSNList As String = ""
        Dim path As String


        If (dtBRSchedule.Length > 0) Then

            drConFig = dtBRSchedule(0)

            For Each dr As DataRow In dtBRSchedule
                MSNList += dr("MSN") + vbLf
            Next
            '######### old code ##################
            'MSNList = MSNList.Substring(0, MSNList.Length - 1)
            'Console.WriteLine(MSNList)
            '#####################################



            path = System.AppDomain.CurrentDomain.BaseDirectory()
            Console.WriteLine(path)


            'Console.Read()
            Dim TemplateText As String
            TemplateText = File.ReadAllText(path + "\TemPlateTxTFile\BroadcastByMSNTemplate.txt")
            Dim DateTimeText As String

            '######### old code ##################
            'DateTimeText = Now.ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
            'TemplateText = TemplateText.Replace("{datetime}", DateTimeText.ToString.Trim)
            '#####################################
            TemplateText = TemplateText.Replace("{servicename}", drConFig("servicename").ToString.Trim)
            TemplateText = TemplateText.Replace("{sender}", IIf(CBool(drConFig("IsQuiz").ToString), drConFig("ReplyQuizTo").ToString.Trim, drConFig("sender").ToString.Trim))
            'TemplateText = TemplateText.Replace("{msg}", drConFig("msg").ToString.Trim)
            TemplateText = TemplateText.Replace("{price}", drConFig("price").ToString.Trim)
            'TemplateText = TemplateText.Replace("{type}", IIf(drConFig("msg").ToString.StartsWith("http://"), "B", "T"))
            'TemplateText = TemplateText.Replace(vbCrLf & "{subject}", IIf(drConFig("msg").ToString.StartsWith("http://"), vbCrLf & "SUBJECT:" + drConFig("servicename"), ""))
            Dim SMS_MSNList = ""
            Dim HeaderType As String = IIf(drConFig("msg").ToString.StartsWith("http://"), "B", "T")
            Dim prevKey As String = ""
            Dim curKey As String = ""
            For Each dr As DataRow In dtBRSchedule
                curKey = dr("FromQuizID")
                If (curKey <> prevKey) Then
                    If (HeaderType = "B") Then
                        SMS_MSNList += "B:" + dr("msg").ToString + vbLf
                        SMS_MSNList += "SUBJECT:" + drConFig("servicename") + vbLf
                    Else
                        SMS_MSNList += "T:" + dr("msg").ToString + vbLf
                    End If
                End If
                SMS_MSNList += dr("MSN") + vbLf
                prevKey = curKey
            Next
            'TemplateText = TemplateText.Replace("{MSNLIST}",

            TemplateText = TemplateText.Replace(vbCrLf, vbLf)


            '############## new code #############
            Dim numOfFile As Integer = genFileName(dtBRSchedule.Length)
            Dim nFiles(numOfFile - 1) As String
            Dim nLineLength As Integer = 12
            For nFile As Integer = 1 To numOfFile

                DateTimeText = Now.AddMinutes(nFile).ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                Dim ntextFileName As String
                ntextFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
                Dim n As Integer = 1
                While IO.File.Exists(path + "\PrepareFile\" + ntextFileName) 'check name of file is not exists.
                    DateTimeText = Now.AddMinutes(nFile + n).ToString("yyyyMMddhhmmss", System.Globalization.CultureInfo.GetCultureInfo("en-US"))
                    ntextFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
                    n += 1
                    Threading.Thread.Sleep(1000)
                End While
                Dim m As String = ""
                'Try
                '    m = MSNList.Substring(nCount * ((nFile - 1) * nLineLength), nCount * nLineLength)
                'Catch ex As Exception
                '    m = MSNList.Substring(nCount * ((nFile - 1) * nLineLength))
                'End Try

                Dim nTemplateText As String = TemplateText.Replace("{Template}", SMS_MSNList.Substring(0, SMS_MSNList.Length - 1))
                nTemplateText = nTemplateText.Replace("{datetime}", DateTimeText.ToString.Trim)
                Dim nsb As New StringBuilder()
                nsb.Append(nTemplateText)

                nFiles(nFile - 1) = ntextFileName
                TypeLFTPFile = ntextFileName.Replace(".txt", "")
                Console.WriteLine(path + "\PrepareFile\" + ntextFileName)
                Dim nOutfile As New StreamWriter(path + "\PrepareFile\" + ntextFileName, False, System.Text.Encoding.Default)

                nOutfile.Write(nsb.ToString())
                nOutfile.Close()
            Next
            TypeLFTPFile = String.Join(",", nFiles).Replace(".txt", "")
            'Console.WriteLine("Finished")
            'Threading.Thread.Sleep(5000)

            Dim logname As String = path + "\log.xml"
            Dim winscp As Process = New Process()
            winscp.StartInfo.FileName = "winscp.comww"
            winscp.StartInfo.Arguments = "/log=" + logname
            winscp.StartInfo.UseShellExecute = False
            winscp.StartInfo.RedirectStandardInput = True
            winscp.StartInfo.RedirectStandardOutput = True
            winscp.StartInfo.CreateNoWindow = True
            winscp.Start()
            winscp.StandardInput.WriteLine("option batch abort")
            winscp.StandardInput.WriteLine("option confirm off")
            winscp.StandardInput.WriteLine("open " + IIf(drConFig("serviceid").ToString.StartsWith("350"), "ais", "aisprod"))
            winscp.StandardInput.WriteLine("ls")
            winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
            For Each uploadFname As String In nFiles
                winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + uploadFname)
                Threading.Thread.Sleep(1000)
            Next
            winscp.StandardInput.Close()
            Dim output As String = winscp.StandardOutput.ReadToEnd()
            Console.WriteLine(output)
            winscp.WaitForExit()
            Console.WriteLine("Finished")
            'Console.ReadLine()
            '#####################################


            '######### old code ##################
            'Dim sb As New StringBuilder()

            'sb.Append(TemplateText)

            'Dim textFileName As String
            'textFileName = drConFig("serviceid").ToString + "_" + DateTimeText + ".txt"
            'TypeLFTPFile = textFileName.Replace(".txt", "")
            'Dim outfile As New StreamWriter(path + "\PrepareFile\" + textFileName, False, System.Text.Encoding.Default)

            'outfile.Write(sb.ToString())
            'outfile.Close()

            'Dim logname As String = path + "\log.xml"

            'Dim winscp As Process = New Process()
            'winscp.StartInfo.FileName = "winscp.com"
            'winscp.StartInfo.Arguments = "/log=" + logname
            'winscp.StartInfo.UseShellExecute = False
            'winscp.StartInfo.RedirectStandardInput = True
            'winscp.StartInfo.RedirectStandardOutput = True
            'winscp.StartInfo.CreateNoWindow = True
            'winscp.Start()
            'winscp.StandardInput.WriteLine("option batch abort")
            '' Console.ReadLine()
            'winscp.StandardInput.WriteLine("option confirm off")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("open " + IIf(drConFig("serviceid").ToString.StartsWith("350"), "ais", "aisprod"))
            '' Console.ReadLine()
            'winscp.StandardInput.WriteLine("ls")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("cd /export/home/cpuser504/data")
            ''Console.ReadLine()
            'winscp.StandardInput.WriteLine("put " + path + "PrepareFile\" + textFileName)
            '' Console.ReadLine()
            'winscp.StandardInput.Close()
            'Dim output As String = winscp.StandardOutput.ReadToEnd()
            'Console.WriteLine(output)
            'winscp.WaitForExit()
            'Console.WriteLine("Finished")
            'Console.ReadLine()
            '###############################################################################3



        End If



    End Sub

    Function genFileName(ByVal n As Integer) As Integer
        Dim nFile As Integer = n
        nFile += (nCount - 1)
        nFile -= (nFile Mod nCount)
        nFile /= nCount
        Return nFile
    End Function

End Module
