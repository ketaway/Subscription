Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Public Class DBConnSQL


    Protected _DBServer As String = ""
    Public Property DBServer() As String
        Get
            Return _DBServer
        End Get
        Set(ByVal value As String)
            _DBServer = value
        End Set
    End Property

    Protected _DBName As String = ""
    Public Property DBName() As String
        Get
            Return _DBName
        End Get
        Set(ByVal value As String)
            _DBName = value
        End Set
    End Property

    Protected _DBUsername As String = ""
    Public Property DBUsername() As String
        Get
            Return _DBUsername
        End Get
        Set(ByVal value As String)
            _DBUsername = value
        End Set
    End Property

    Protected _DBPassword As String = ""
    Public Property DBPassword() As String
        Get
            Return _DBPassword
        End Get
        Set(ByVal value As String)
            _DBPassword = value
        End Set
    End Property
    Protected _strconn As String = ""
    Public Property StrConn() As String
        Get
            Return _strconn
        End Get
        Set(ByVal value As String)
            _strconn = value
        End Set
    End Property
    Public Conn As SqlConnection = Nothing

    Sub New()
      

        _strconn = My.Settings.DBCon
        Conn = New SqlConnection(StrConn)
    End Sub

    Sub DBConnect(Optional ByVal DatabasePassword As String = "")
        Try
            Dim ConnectionString As String = StrConn
            With Conn
                If Conn.State = ConnectionState.Open Then Conn.Close()
                .ConnectionString = ConnectionString
                .Open()
            End With
        Catch Er As Exception
            Debug.Print(Er.Message)
        End Try
    End Sub

    Sub DBClose()
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
        Catch
        End Try
    End Sub

End Class


'Public Class DBConnAccess


'    Protected _AccessSource As String = ""
'    Public Property AccessSource() As String
'        Get
'            Return _AccessSource
'        End Get
'        Set(ByVal value As String)
'            _AccessSource = value
'        End Set
'    End Property


'    Protected _strconn As String = ""
'    Public Property StrConn() As String
'        Get
'            Return _strconn
'        End Get
'        Set(ByVal value As String)
'            _strconn = value
'        End Set
'    End Property
'    Public Conn As OleDbConnection = Nothing
'    Sub New()
'        _AccessSource = My.Settings.ACCSource

'        _strconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + My.Settings.ACCSource + ";Persist Security Info=False;"
'        Conn = New OleDbConnection(StrConn)
'    End Sub

'    Sub DBConnect()
'        Try
'            Dim ConnectionString As String = StrConn
'            With Conn
'                If Conn.State = ConnectionState.Open Then Conn.Close()
'                .ConnectionString = ConnectionString
'                .Open()
'            End With
'        Catch Er As Exception
'            Debug.Print(Er.Message)
'        End Try
'    End Sub

'    Sub DBClose()
'        Try
'            If Conn.State = ConnectionState.Open Then Conn.Close()
'        Catch
'        End Try
'    End Sub

'End Class
