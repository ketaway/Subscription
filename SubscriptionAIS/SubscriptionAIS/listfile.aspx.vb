Imports System.IO
Imports System.Data.SqlClient

Public Class listfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim filePaths As String() = Directory.GetFiles("D:\www\mcontent\content\", "*.*")
        'Dim filePaths As String() = Directory.GetFiles("c:\testfile\", "*.*")
        Dim dt As New DataTable
        dt.Columns.Add("filename", "".GetType)
        dt.Columns.Add("3gp", True.GetType)
        dt.Columns.Add("gif", True.GetType)
        Dim dr As DataRow
        Dim tgpcount, gifcount As Integer
        tgpcount = 0
        gifcount = 0
        Dim filename, typename As String
        For Each filefullname As String In filePaths
            filename = filefullname.Split(".")(0)
            filename = filename.Split("\")(filename.Split("\").Length - 1)
            typename = filefullname.Split(".")(1)
            If (dt.Select("filename = '" + filename + "'").Length > 0) Then
                dr = dt.Select("filename = '" + filename + "'")(0)
            Else
                dr = dt.NewRow()
                dr("filename") = filename
                dt.Rows.Add(dr)
            End If
            If (typename = "3gp") Then
                dr("3gp") = True
                tgpcount = tgpcount + 1
            ElseIf (typename = "gif") Then
                dr("gif") = True
                gifcount = gifcount + 1
            End If



        Next
        Response.Write(tgpcount.ToString + "<br>")
        Response.Write(gifcount.ToString + "<br>")
        GridView1.DataSource = dt
        GridView1.DataBind()
        'dr(0) = "1"
        'dr(1) = "2"
        'dt.Select(

      


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim dt As DataTable
        dt = GridView1.DataSource
        Dim drs As DataRow()
        drs = dt.Select("[3gp]=1 and gif=1 ")


        Dim DBConn As SqlConnection = createConnection()
        DBConn.Open()
        Dim SQLCMD_SV As New SqlCommand
        SQLCMD_SV.Connection = DBConn
        For Each dr As DataRow In drs
            SQLCMD_SV.CommandText = "insert into t_Content(filename) values('" + dr("filename") + "')"
            SQLCMD_SV.ExecuteNonQuery()
        Next

    

        DBConn.Close()


    End Sub



    Public Function createConnection() As SqlConnection
        Dim ConnString As String = ""
        Dim DBHost As String = "127.0.0.1"
        Dim DBName As String = "mcontent"
        Dim DBUsername As String = "sa"
        Dim DBPassword As String = "Password@1"
        Return New SqlConnection("Server=" & DBHost & ";Database=" & DBName & ";User ID=" & DBUsername & ";Password=" & DBPassword & ";Trusted_Connection=False")
    End Function
End Class